using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using BuyNowTrackerBIZ;
using System.Globalization;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace BuyNowTracker
{
    public partial class frmTracker : Form
    {
        private static readonly log4net.ILog log =
                 log4net.LogManager.GetLogger(typeof(frmTracker));

        public long timeCount = 1;

        private FindInputCtrl lastInput;

        private KeyboardInput keyboard;

        private MouseInput mouse;

        private DateTime startTime;

        private DateTime endTime;

        private static int elapseTime = 0;

        private static int elapseindex = 0;

        private string buttonText;
        private static int keyInputCount = 0;
        private static int mouseInputCount = 0;

        List<Idltime> randomTime = new List<Idltime>();

        UserTask taskObj = null;

        User usrTracker = null;

        string _token = string.Empty;
        int _logActivityId;

        public frmTracker(UserTask tsk, User usr, string token,int logActivityId)
        {
            taskObj = tsk;
            usrTracker = usr;
            _token = token;
            _logActivityId = logActivityId;

            mouseInputCount=keyInputCount = 0;

            InitializeComponent();
        }

        private void frmTracker_Load(object sender, EventArgs e)
        {
            //log.Info("Initializing Component...");

            //lblStartTimer.Text = "Start Time " + startTime.ToString("dd-MMM-yyyy hh:mm tt");

            //log.Info("Initializing Classess...");

            lblTaskTltle.Text = taskObj.title;
            if (taskObj.description.Length > 57)
            {
                lblDescription.Text = taskObj.description.Substring(0, 57);
                lblDescription.Text += Environment.NewLine + taskObj.description.Substring(lblDescription.Text.Length - 1);
            }
            else
                lblDescription.Text = taskObj.description;


            lblUserName.Text = usrTracker.name;

            keyboard = new KeyboardInput();

            keyboard.KeyBoardKeyPressed += keyboard_KeyBoardKeyPressed;

            mouse = new MouseInput();

            mouse.MouseMoved += mouse_MouseMoved;

            lastInput = new FindInputCtrl();

           // log.Info("Initializing Timer...");

            randomTime.Add(new Idltime { Id = 1, Value = 3 });
            randomTime.Add(new Idltime { Id = 2, Value = 4 });
            randomTime.Add(new Idltime { Id = 3, Value = 7 });
            randomTime.Add(new Idltime { Id = 4, Value = 6 });
            randomTime.Add(new Idltime { Id = 5, Value = 8 });

            elapseTime = randomTime[0].Value;
            elapseindex = randomTime[0].Value;

            startTime = DateTime.Now.AddMinutes(elapseTime);


            timer1 = new Timer();

            timer1.Interval = 1000;

            timer1.Tick += timer1_Tick;

           // log.Info("Starting timer...");

            timer1.Start();

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
                log.Error(ex);
            }

        }

        private string FormatDateTime(DateTime dateTime)
        {
            return dateTime.ToString("HH:mm:ss", CultureInfo.CurrentUICulture);
        }

        void mouse_MouseMoved(object sender, EventArgs e)
        {
            buttonText = sender.ToString();
            mouseInputCount = mouseInputCount + 1;
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {

                DateTime dtSecond = DateTime.Now;

              //  log.Info("Get time 2");
                TimeSpan tmHours = startTime.TimeOfDay;

                TimeSpan timeDifference = startTime.Subtract(dtSecond);//minutes2.TotalMinutes - minutes1.TotalMinutes;

                long second = timeCount % 60;
                long minute = timeCount / 60;
                long hour = minute / 60;

                lblhour.Text = string.Format("{0}", GetTime(hour.ToString()));
                lblMinute.Text = string.Format("{0}", GetTime(minute.ToString()));
                lblSecond.Text = string.Format("{0}", GetTime(second.ToString()));


                timeCount++;

               // log.Info(dtSecond.ToString() + "-" + startTime.ToString() + "-" + elapseTime.ToString());
                if (dtSecond > startTime)
                {
                    //log.Error("Get Execution time");

                    byte[]  bytes =  ScreenCapture.SaveScreen();

                    SaveScreenShot(bytes);

                    //log.Info("save screen ");

                    endTime = DateTime.Now;

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
                            ////log.Info("with in range" + elapseTime.ToString());

                        }

                    }
                    else
                    {
                        elapseTime = randomTime[0].Value;
                        elapseindex = randomTime[0].Id;
                    //    log.Info("range started again" + elapseTime.ToString());
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
                log.Error(ex);
            }
        }

       
        private void btnEnd_Click(object sender, EventArgs e)
        {
            mouseInputCount = keyInputCount = 0;

            EndTimer(false);

            timer1.Enabled = false;
            timer1.Stop();
        }


        private void btnClose_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }
        private string GetTime(string val)
        {
            if (val.Length == 1) return string.Format("0{0}", val);

            return val;
        }


        private void btnMemo_Click(object sender, EventArgs e)
        {            
            AddMemo(taskObj.id);
        }

        private void imgBack_Click(object sender, EventArgs e)
        {
            EndTimer(true);

            TaskList lst = new TaskList(usrTracker, _token);

            timer1.Enabled = false;
            timer1.Stop();

            lst.Show();
            this.Hide();
        }

        private async void SaveScreenShot(byte[]  bytes)
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

            client.DefaultRequestHeaders.Add("Authorizations", "Bearer " + _token);

            HttpResponseMessage message = await client.PostAsync("https://buynowdepot.com/api.php?action=savescreenshot&timerid=" + _logActivityId, content);

            var responseString = await message.Content.ReadAsStringAsync();

            this.Cursor = Cursors.Default;

            JObject j = (JObject)JsonConvert.DeserializeObject(responseString);

            if(j["result"].ToString().ToLower() == "success")
            {
               // MessageBox.Show("Screen shot saved", "Info", MessageBoxButtons.OK);

            }
            else
            {
                MessageBox.Show(j["messages"][0].ToString(),"Error", MessageBoxButtons.OK);
            }
          
        }

        
        private async void EndTimer(bool isBack)
        {
            this.Cursor = Cursors.WaitCursor;

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            string jsonString = string.Empty;

            var client = new HttpClient();

            client.DefaultRequestHeaders.Add("Authorizations", "Bearer " + _token);

            var values = new Dictionary<string, string>
                {
                   { "action", "stoptimer" },

                   { "timerid",  _logActivityId.ToString() }
                };

            var content = new FormUrlEncodedContent(values);

            HttpResponseMessage message = await client.PostAsync("https://buynowdepot.com/api.php", content);

            var responseString = await message.Content.ReadAsStringAsync();

            this.Cursor = Cursors.Default;

            JObject j = (JObject)JsonConvert.DeserializeObject(responseString);

            if (!isBack)
            {
                if (j["result"].ToString().ToLower() == "success")
                {
                    DialogResult dialog = new DialogResult();

                    dialog = MessageBox.Show("Timer stopped!", "Info", MessageBoxButtons.OK);

                    if(dialog == DialogResult.OK)
                    {
                        frmLogin td = new frmLogin();
                        td.Show();
                        this.Hide();

                    }

                }
                else
                {
                    MessageBox.Show(j["message"].ToString(), "Error", MessageBoxButtons.OK);
                }
            }
           
        }

        private async void AddMemo(int taskId)
        {

            this.Cursor = Cursors.WaitCursor;

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            string jsonString = string.Empty;

            var client = new HttpClient();

            client.DefaultRequestHeaders.Add("Authorizations", "Bearer " + _token);

            var values = new Dictionary<string, string>
                {
                   { "action", "savememo" },

                   { "timerid",  _logActivityId.ToString() },

                   {"memo",  txtMemo.Text}
                };

            var content = new FormUrlEncodedContent(values);

            HttpResponseMessage message = await client.PostAsync("https://buynowdepot.com/api.php", content);

            var responseString = await message.Content.ReadAsStringAsync();

            this.Cursor = Cursors.Default;

            JObject j = (JObject)JsonConvert.DeserializeObject(responseString);

            if (j["result"].ToString().ToLower() == "success")
            {
                MessageBox.Show("Memo added", "Info", MessageBoxButtons.OK);
            }
            else
            {
                MessageBox.Show(j["message"].ToString(), "Error", MessageBoxButtons.OK);
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

            client.DefaultRequestHeaders.Add("Authorizations", "Bearer " + _token);

            var values = new Dictionary<string, string>
                {
                   { "action", "logactivity" },

                   { "timerid",  _logActivityId.ToString() },
                
                   { "stats", JsonConvert.SerializeObject(dic) }
                };

            var content = new FormUrlEncodedContent(values);

            HttpResponseMessage message = await client.PostAsync("https://buynowdepot.com/api.php", content);

            var responseString = await message.Content.ReadAsStringAsync();

            this.Cursor = Cursors.Default;

            JObject j = (JObject)JsonConvert.DeserializeObject(responseString);

            if (j["result"].ToString().ToLower() == "success")
            {

                mouseInputCount = keyInputCount = 0;
               object value =  ((Newtonsoft.Json.Linq.JValue)(j["data"][0]["stoptimer"])).Value;

                if(Convert.ToBoolean(value) == true)
                {
                    EndTimer(false);

                    timer1.Enabled = false;
                    timer1.Stop();

                }
            }
            else
            {
                MessageBox.Show(j["message"].ToString(), "Error", MessageBoxButtons.OK);
            }

        }

    }


    public class Idltime
    {
        public int Id { get; set; }
        public int Value { get; set; }
    }
}



