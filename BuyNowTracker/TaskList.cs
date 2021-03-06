﻿using BuyNowTrackerBIZ;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MaterialSkin;
using MaterialSkin.Controls;

namespace BuyNowTracker
{
    public partial class TaskList : MaterialForm
    {

        List<UserTask> LstUser = new List<UserTask>();

        public static TaskList Current;

        bool isTimerStart = false;

        bool isFirstTaskClicked = false;

        User usr = new User();
        public string Token { get; set; }
        public int LogActivityId { get; set; }
        public int TaskId { get; set; }
        public string TaskTitle { get; set; }

        UserTask usrTsk;

        public long timeCount = 1;

        private FindInputCtrl lastInput;

        private KeyboardInput keyboard;

        private MouseInput mouse;

        private DateTime startTime;

        private static int elapseTime = 0;

        private static int elapseindex = 0;

        private string buttonText;

        private static int keyInputCount = 0;

        private static int mouseInputCount = 0;

        private static DateTime IdlTimeStart;

        private bool isSameTaskClicked = false;

        public int PrevTaskId { get; set; }
        public int CurrentTaskId { get; set; }

        List<Idltime> randomTime = new List<Idltime>();

        public TaskList(User u, string token)
        {
            PrevTaskId = 0;
            Token = token;
            isSameTaskClicked = false;

            InitializeComponent();

            this.FormClosing += TaskList_FormClosing;

            MaterialSkinManager materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;

            // Configure color schema
            materialSkinManager.ColorScheme = new ColorScheme(
                Primary.Blue400, Primary.Blue500,
                Primary.Blue500, Accent.LightBlue200,
                TextShade.WHITE
            );


            grdTaskList.CellClick +=
              new DataGridViewCellEventHandler(dataGridView1_CellClick);

            // grdTaskList.CellMouseMove += GrdTaskList_CellMouseMove;

            //  grdTaskList.CellMouseLeave += GrdTaskList_CellMouseLeave;


            usr = u;
        }

        private void TaskList_Load(object sender, EventArgs e)
        {
            mouseInputCount = keyInputCount = 0;
            isTimerStart = false;

            IdlTimeStart = DateTime.Now;

            randomTime.Add(new Idltime { Id = 1, Value = 3 });
            randomTime.Add(new Idltime { Id = 2, Value = 4 });
            randomTime.Add(new Idltime { Id = 3, Value = 3 });
            randomTime.Add(new Idltime { Id = 4, Value = 5 });
            randomTime.Add(new Idltime { Id = 5, Value = 4 });
            randomTime.Add(new Idltime { Id = 6, Value = 3 });
            randomTime.Add(new Idltime { Id = 7, Value = 7 });

            elapseTime = randomTime[0].Value;
            elapseindex = randomTime[0].Value;

            startTime = DateTime.Now.AddMinutes(elapseTime);

            Current = this;

            TaskList.Current.Text = usr.name;

            keyboard = new KeyboardInput();

            keyboard.KeyBoardKeyPressed += keyboard_KeyBoardKeyPressed;

            mouse = new MouseInput();

            mouse.MouseMoved += mouse_MouseMoved;

            lastInput = new FindInputCtrl();


            timer1 = new Timer();

            timer1.Interval = 1000;

            timer1.Tick += timer1_Tick;


            ReadTasks((int)usr.uid);
        }

        void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                grdTaskList.ClearSelection();
                // Ignore clicks that are not on button cells. 
                if (e.RowIndex < 0 || e.ColumnIndex !=
                    grdTaskList.Columns["Starttask"].Index)
                    return;

                usrTsk = LstUser.Find(r => r.id == (int)grdTaskList.Rows[e.RowIndex].Cells["id"].Value);


                if (grdTaskList.Rows[e.RowIndex].Cells["Starttask"].Value.Equals("Stop Task"))
                {
                    DialogResult dialog = new DialogResult();


                    dialog = MessageBox.Show("Are you sure you want to stop task", "Info", MessageBoxButtons.YesNo);

                    if (dialog == DialogResult.Yes)
                    {
                        isSameTaskClicked = true;
                        ShowMemoDilog(grdTaskList.Rows[e.RowIndex].Cells[1].Value.ToString());
                        PrevTaskId = 0;
                        CurrentTaskId = 0;
                        //EndTimer(false);
                        return;
                    }
                    else
                    {
                        return;
                    }
                }


                if (PrevTaskId == 0)
                    PrevTaskId = usrTsk.id;

                if (PrevTaskId != usrTsk.id)
                {
                    DialogResult dialog = new DialogResult();

                    dialog = MessageBox.Show("Starting new task will stop your current ruunig task. Are your sure?", "Confirm", MessageBoxButtons.YesNo);

                    if (dialog == DialogResult.Yes)
                    {
                        if(CurrentTaskId > 0)
                            PrevTaskId = CurrentTaskId;
                        CurrentTaskId = usrTsk.id;

                        string tskName = "";
                        for (int i = 0; i < grdTaskList.Rows.Count; i++)
                        {
                            if (PrevTaskId.Equals((int)grdTaskList.Rows[i].Cells["id"].Value))
                            {
                                tskName = grdTaskList.Rows[i].Cells[1].Value.ToString();
                                break;
                            }
                        }

                        ShowMemoDilog(tskName);
                    }
                }
                else
                {
                    this.Cursor = Cursors.WaitCursor;
                    StartTimer(usrTsk.id, e.RowIndex);

                    WindowState = FormWindowState.Minimized;
                }


            }
            catch (Exception ex)
            {
                //logger.Error(ex.Message);
            }
        }

        public void ShowMemoDilog(string taskName)
        {

            frmSummary frmSum = new frmSummary();
            frmSum.TaskSummary = taskName;
            frmSum.lstTask = this;

            DialogResult result = frmSum.ShowDialog();

            frmSum.Dispose();
        }
        public void EndTaskTimer()
        {
            EndTimer(false);
           
        }

        void keyboard_KeyBoardKeyPressed(object sender, EventArgs e)
        {
            try
            {
                buttonText = sender.ToString();
                keyInputCount = keyInputCount + 1;
            }
            catch (Exception ex)
            {
                //logger.Error(ex);
            }

        }

        void mouse_MouseMoved(object sender, EventArgs e)
        {
            if (isTimerStart == true)
            {
                buttonText = sender.ToString();
                mouseInputCount = mouseInputCount + 1;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                DateTime dtSecond = DateTime.Now;

                timeCount++;

                if (dtSecond > startTime)
                {

                    byte[] bytes = ScreenCapture.SaveScreen();

                    SaveScreenShot(bytes);

                    LogActivity();

                    int _arrIndex = randomTime.FindIndex(a => a.Id == elapseindex);

                    if (_arrIndex < randomTime.Count)
                    {
                        if (_arrIndex + 1 == randomTime.Count)
                        {
                            elapseTime = randomTime[0].Value;
                            elapseindex = randomTime[0].Id;
                        }
                        else
                        {
                            elapseTime = randomTime[_arrIndex + 1].Value;
                            elapseindex = randomTime[_arrIndex + 1].Id;
                        }
                    }
                    else
                    {
                        elapseTime = randomTime[0].Value;
                        elapseindex = randomTime[0].Id;
                    }
                    startTime = DateTime.Now.AddMinutes(elapseTime);
                }
                else
                {
                    //  log.Info("Time difference is less then ellapse time");
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void TaskList_FormClosing(Object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.WindowState = FormWindowState.Minimized;
        }

        private void GrdTaskList_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            grdTaskList.Cursor = Cursors.Default;
        }

        private void GrdTaskList_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex == 3)
            {
                grdTaskList.Cursor = Cursors.Hand;
            }
            else
                grdTaskList.Cursor = Cursors.Default;
        }

        private void TaskList_MouseHover(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Hand;
        }

        public async void ReadTasks(int userId)
        {
            try
            {

                this.Cursor = Cursors.WaitCursor;

                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                string jsonString = string.Empty;

                var client = new HttpClient();

                client.DefaultRequestHeaders.Add("Authorizations", "Bearer " + Token);


                var values = new Dictionary<string, string>
                {
                   { "action", "tasks" },

                   { "userid",  userId.ToString() }
                };

                var content = new FormUrlEncodedContent(values);

                HttpResponseMessage message = await client.PostAsync("https://www.buynowdepot.com/api.php", content);

                var responseString = await message.Content.ReadAsStringAsync();

                JObject j = (JObject)JsonConvert.DeserializeObject(responseString);


                if (j["data"].Count() > 0)
                {
                    for (int i = 0; i < ((Newtonsoft.Json.Linq.JContainer)(j["data"])).Count; i++)
                    {
                        List<JToken> jt = j["data"][i].Select(jp => ((JProperty)jp)).ToList<JToken>();

                        UserTask usrTask = new UserTask();

                        foreach (JToken item in jt)
                        {
                            switch (((Newtonsoft.Json.Linq.JProperty)item).Name)
                            {
                                case "id":
                                    usrTask.id = (int)((Newtonsoft.Json.Linq.JProperty)item).Value;
                                    break;
                                case "title":
                                    usrTask.title = ((Newtonsoft.Json.Linq.JProperty)item).Value.ToString();
                                    break;
                                case "description":
                                    usrTask.description = ((Newtonsoft.Json.Linq.JProperty)item).Value.ToString();
                                    break;
                            }
                        }

                        LstUser.Add(usrTask);
                    }

                        

                        grdTaskList.DataSource = LstUser;
                        DataGridViewButtonColumn buttonColumn =
                         new DataGridViewButtonColumn();

                        buttonColumn.Name = "Starttask";
                        buttonColumn.Text = "Start Task";

                        buttonColumn.FlatStyle = FlatStyle.Flat;
                        buttonColumn.CellTemplate.Style.BackColor = Color.Orange;
                        buttonColumn.CellTemplate.Style.ForeColor = Color.White;
                        buttonColumn.CellTemplate.Style.SelectionBackColor = Color.Orange;
                        buttonColumn.CellTemplate.Style.SelectionForeColor = Color.White;
                        buttonColumn.HeaderCell.Style.BackColor = Color.Orange;
                        buttonColumn.CellTemplate.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                        buttonColumn.UseColumnTextForButtonValue = false;


                        grdTaskList.Columns[0].Visible = false;
                        grdTaskList.Columns[1].HeaderText = "Tasks";
                        grdTaskList.Columns[1].HeaderCell.Style.BackColor = Color.Silver;
                        grdTaskList.Columns[1].HeaderCell.Style.ForeColor = Color.White;
                        grdTaskList.GridColor = Color.White;
                        grdTaskList.Columns[2].Visible = false;
                        grdTaskList.Columns.Add(buttonColumn);
                        grdTaskList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                        grdTaskList.Columns[1].Width = 340;
                        grdTaskList.ColumnHeadersVisible = false;


                        grdTaskList.BorderStyle = BorderStyle.None;
                        //grdTaskList.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
                        grdTaskList.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
                        // grdTaskList.DefaultCellStyle.SelectionBackColor = Color.;
                        //grdTaskList.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
                        grdTaskList.BackgroundColor = Color.White;

                        grdTaskList.EnableHeadersVisualStyles = false;
                        grdTaskList.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
                        grdTaskList.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
                        grdTaskList.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

                        foreach (DataGridViewRow row in grdTaskList.Rows)
                        {
                            DataGridViewCellStyle CellStyle = new DataGridViewCellStyle();
                            CellStyle.BackColor = Color.Orange;
                            CellStyle.ForeColor = Color.White;
                            row.Cells["Starttask"].Value = "Start Task";
                            row.Cells["Starttask"].Style = CellStyle;

                        }
                    
                }
                else
                {
                    MessageBox.Show("Their is no task assigned to you.");
                }

                this.Cursor = Cursors.Default;

            }
            catch (Exception ex)
            {
                //logger.Error(ex.Message);
            }
        }

        public async void GridColorChange(int RowIndex)
        {
            await Task.Run(() => ColorChange(RowIndex));
        }

        void ColorChange(int RowIndex)
        {
            if (RowIndex >= 0)
            {
                DataGridViewButtonCell cell = (DataGridViewButtonCell)grdTaskList.Rows[RowIndex].Cells["Starttask"];

                DataGridViewCellStyle CellStyle = new DataGridViewCellStyle();
                CellStyle.BackColor = Color.LightGray;
                CellStyle.ForeColor = Color.White;
                cell.Value = "Stop Task";
                cell.Style = CellStyle;
            }

            DataGridViewCellStyle CellStyle1 = new DataGridViewCellStyle();
            CellStyle1.BackColor = Color.Orange;
            CellStyle1.ForeColor = Color.White;


            for (int i = 0; i < grdTaskList.Rows.Count; i++)
            {
                if (i == RowIndex) continue;

                grdTaskList.Rows[i].Cells["Starttask"].Value = "Start Task";
                grdTaskList.Rows[i].Cells["Starttask"].Style = CellStyle1;

            }

        }

        private async void StartTimer(int taskId, int rowIndex)
        {
            try
            {

           
            this.Cursor = Cursors.WaitCursor;

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            string jsonString = string.Empty;

            var client = new HttpClient();

            client.DefaultRequestHeaders.Add("Authorizations", "Bearer " + Token);

            var values = new Dictionary<string, string>
                {
                   { "action", "starttimer" },

                   { "taskid",  taskId.ToString() }
                };

            var content = new FormUrlEncodedContent(values);

            HttpResponseMessage message = await client.PostAsync("https://www.buynowdepot.com/api.php", content);

            var responseString = await message.Content.ReadAsStringAsync();

            this.Cursor = Cursors.Default;

            JObject j = (JObject)JsonConvert.DeserializeObject(responseString);

            if (j["result"].ToString().ToLower() == "success")
            {
                isTimerStart = isFirstTaskClicked = true;
                GridColorChange(rowIndex);
                //  MessageBox.Show("Your timer has been started for " + usrTsk.title + " task.", "Info", MessageBoxButtons.OK);

                LogActivityId = Convert.ToInt32(j["data"]);
                timer1.Enabled = true;

                timer1.Start();


            }
            else
            {
                PrevTaskId = 0;
                CurrentTaskId = 0;
                MessageBox.Show(j["messages"][0].ToString(), "Info", MessageBoxButtons.OK);
            }

            this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {

            }
        }

        private async void SaveScreenShot(byte[] bytes)
        {
            try
            {

           
            this.Cursor = Cursors.WaitCursor;

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            string jsonString = string.Empty;

            var client = new HttpClient();

            var values = new Dictionary<string, string>
            {
                {"screenshot", Convert.ToBase64String(bytes)}
            };

            var str = JsonConvert.SerializeObject(values);

            var content = new StringContent(str, Encoding.UTF8, "application/json");

            client.DefaultRequestHeaders.Add("Authorizations", "Bearer " + Token);

            HttpResponseMessage message = await client.PostAsync("https://www.buynowdepot.com/api.php?action=savescreenshot&timerid=" + LogActivityId, content);

            var responseString = await message.Content.ReadAsStringAsync();

            this.Cursor = Cursors.Default;

            JObject j = (JObject)JsonConvert.DeserializeObject(responseString);

            if (j["result"].ToString().ToLower() == "success")
            {
                // MessageBox.Show("Screen shot saved", "Info", MessageBoxButtons.OK);

            }
            else
            {
                MessageBox.Show(j["messages"][0].ToString(), "Error", MessageBoxButtons.OK);
            }
            }
            catch (Exception ex)
            {

            }

        }

        private async void LogActivity()
        {
            this.Cursor = Cursors.WaitCursor;

            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("MouseCount", mouseInputCount);
            dic.Add("KeyStrokeCount", keyInputCount);


            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            string jsonString = string.Empty;

            var client = new HttpClient();

            client.DefaultRequestHeaders.Add("Authorizations", "Bearer " + Token);

            var values = new Dictionary<string, string>
                {
                   { "action", "logactivity" },

                   { "timerid",  LogActivityId.ToString() },

                   { "stats", JsonConvert.SerializeObject(dic) }
                };

            var content = new FormUrlEncodedContent(values);

            HttpResponseMessage message = await client.PostAsync("https://www.buynowdepot.com/api.php", content);

            var responseString = await message.Content.ReadAsStringAsync();

            this.Cursor = Cursors.Default;

            JObject j = (JObject)JsonConvert.DeserializeObject(responseString);

            if (j["result"].ToString().ToLower() == "success")
            {

                object value = ((Newtonsoft.Json.Linq.JValue)(j["data"][0]["stoptimer"])).Value;

                if (Convert.ToBoolean(value) == true)
                {
                    EndTimer(true);

                    timer1.Enabled = false;
                    timer1.Stop();

                }
                mouseInputCount = keyInputCount = 0;

            }
            else
            {
                MessageBox.Show(j["messages"][0].ToString(), "Error", MessageBoxButtons.OK);
            }

        }

        private async void EndTimer(bool islog)
        {
            this.Cursor = Cursors.WaitCursor;

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            string jsonString = string.Empty;

            var client = new HttpClient();

            client.DefaultRequestHeaders.Add("Authorizations", "Bearer " + Token);

            var values = new Dictionary<string, string>
                {
                   { "action", "stoptimer" },

                   { "timerid",  LogActivityId.ToString() }
                };

            var content = new FormUrlEncodedContent(values);

            HttpResponseMessage message = await client.PostAsync("https://www.buynowdepot.com/api.php", content);

            var responseString = await message.Content.ReadAsStringAsync();

            this.Cursor = Cursors.Default;

            JObject j = (JObject)JsonConvert.DeserializeObject(responseString);


            if (j["result"].ToString().ToLower() == "success")
            {
                DialogResult dialog = new DialogResult();

                if (islog)
                {
                    dialog = MessageBox.Show("Timer stopped!", "Info", MessageBoxButtons.OK);

                    if (dialog == DialogResult.OK)
                    {
                        frmLogin td = new frmLogin();
                        td.Show();
                        this.Hide();
                    }
                }
                else
                {
                    timer1.Enabled = false;
                    timer1.Stop();

                    GridColorChange(-1);

                    int rowIndex = 0;

                    if (CurrentTaskId > 0)
                    {
                        for (int i = 0; i < grdTaskList.Rows.Count; i++)
                        {
                            if (CurrentTaskId == ((int)grdTaskList.Rows[i].Cells["id"].Value))
                            {
                                rowIndex = i;
                                break;
                            }
                        }


                        if (!isSameTaskClicked)
                            StartTimer(CurrentTaskId, rowIndex);

                        isSameTaskClicked = false;
                    }
                    else
                        isSameTaskClicked = false;


                }
                //else
                //{
                //    MessageBox.Show("Timer stopped for " + usrTsk.title + " task.", "Info", MessageBoxButtons.OK);
                //}

            }
            else
            {
                MessageBox.Show(j["messages"][0].ToString(), "Error", MessageBoxButtons.OK);
            }

        }

        private void grdTaskList_MouseHover(object sender, EventArgs e)
        {

        }
    }

    public class UserTask
    {
        public int id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
    }

    public class User
    {
        public Int64 uid { get; set; }
        public string name { get; set; }

    }
}
