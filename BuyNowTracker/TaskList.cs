using BuyNowTrackerBIZ;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace BuyNowTracker
{
    public partial class TaskList : Form
    {
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(typeof(TaskList));
        List<UserTask> LstUser = new List<UserTask>();

        public static TaskList Current;

        User usr = new User();
        string Token = string.Empty;
        bool isTimerStart = false;
        int logActivityId = 0;
        UserTask usrTsk;

        public TaskList(User u,string token)
        {
            Token = token;

            InitializeComponent();

            grdTaskList.CellClick +=
              new DataGridViewCellEventHandler(dataGridView1_CellClick);

            usr = u;
        }

        void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                // Ignore clicks that are not on button cells. 
                if (e.RowIndex < 0 || e.ColumnIndex !=
                    grdTaskList.Columns["Starttask"].Index)
                    return;


                 usrTsk = LstUser.Find(r => r.id == (int)grdTaskList.Rows[e.RowIndex].Cells["id"].Value);

                StartTimer(usrTsk.id);

            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }
        }


        private async void StartTimer(int taskId)
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

            HttpResponseMessage message = await client.PostAsync("https://buynowdepot.com/api.php", content);

            var responseString = await message.Content.ReadAsStringAsync();

            this.Cursor = Cursors.Default;

            JObject j = (JObject)JsonConvert.DeserializeObject(responseString);

            if(j["result"].ToString().ToLower() == "success")
            {
                isTimerStart = true;
                logActivityId =  Convert.ToInt32(j["data"]);

                frmTracker td = new frmTracker(usrTsk, usr, Token, logActivityId);
                td.Show();
                this.Hide();


            }
        }

        public async void ReadTasks(int userId)
        {
            try
            {

                this.Cursor = Cursors.Default;

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

                HttpResponseMessage message = await client.PostAsync("https://buynowdepot.com/api.php", content);

                var responseString = await message.Content.ReadAsStringAsync();

                JObject j = (JObject)JsonConvert.DeserializeObject(responseString);

                this.Cursor = Cursors.Default;

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

                grdTaskList.DataSource = LstUser; DataGridViewButtonColumn buttonColumn =
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
                buttonColumn.UseColumnTextForButtonValue = true;

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

            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }
        }

        private void TaskList_Load(object sender, EventArgs e)
        {
            
            Current = this;

            lblName.Text = usr.name;

            ReadTasks((int)usr.uid);            
        }

        private void picCross_Click(object sender, EventArgs e)
        {
            DialogResult dialog = new DialogResult();

            dialog = MessageBox.Show("Do you want to exit?", "Confirm", MessageBoxButtons.YesNo);

            if (dialog == DialogResult.Yes)
            {
                System.Environment.Exit(1);
            }
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
