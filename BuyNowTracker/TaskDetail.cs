using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BuyNowTrackerBIZ;

namespace BuyNowTracker
{
    public partial class TaskDetail : Form
    {
        private static readonly log4net.ILog log =
         log4net.LogManager.GetLogger(typeof(TaskDetail));

        private static System.Timers.Timer aTimer;
        private static string Status = "A";
        public string Msg = "";
        private static int t = 0;

        private static int elapseTime = 0;
        private static int elapseindex = 0;

        public long timeCount = 1;

        List<Idltime> randomTime = new List<Idltime>();
        DateTime dtFirst;

        private FindInputCtrl lastInput;


        private DateTime startTime; 

        private DateTime endTime;
        public TaskDetail(UserTask obj)
        {
            InitializeComponent();

            lblTaskName.Text = obj.title;
            lblStart.Text ="Task Start Time :";
            lblStop.Text = "Task Stop Time :";

            aTimer = new System.Timers.Timer();
            aTimer.Interval = 10000;
            aTimer.Elapsed += OnTimedEvent;

            btnStop.Visible = false;
            lblRun.Visible = false;


            randomTime.Add(new Idltime { Id = 1,Value =2});
            randomTime.Add(new Idltime { Id = 2, Value = 4 });
            randomTime.Add(new Idltime { Id = 3, Value = 1 });
            randomTime.Add(new Idltime { Id = 4, Value = 2});
            randomTime.Add(new Idltime { Id = 5, Value = 3 });
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                Status = "I";
                btnStop.Visible = false;
                btnStart.Visible = true;
                //lblStop.Text = "Task Stop Time :" + DateTime.Now.TimeOfDay.ToString();
                //lblStop.Visible = true;
                timer1.Stop();
                //lblRun.Visible = false; 
                
            }
            catch (Exception ex)
            {

                log.Error(ex.Message);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Status = "I";
                TaskList.Current.Show();
                this.Close();
            }
            catch (Exception ex)
            {

                log.Error(ex.Message);
            }
        
        }

        private async void btnStart_Click(object sender, EventArgs e)
        {
            try
            {
                Status = "A";
                startTime = DateTime.Now;
                elapseTime = randomTime[0].Value;
                elapseindex = randomTime[0].Value;
                dtFirst = DateTime.Now.AddMinutes(elapseTime);
                //lblStart.Text = "Task start Time :" + DateTime.Now.TimeOfDay.ToString();
                //lblStop.Visible = false;
                btnStart.Visible = false;
                btnStop.Visible = true;
                lastInput = new FindInputCtrl();
                lblRun.Visible = true;
                timer1.Start();


                Task t = new Task(showTime);
                t.Start();

                await t;
            }
            catch (Exception ex)
            {

                log.Error(ex);
            }
        }

        private void showTime()
        {
            try
            {
                aTimer.Start();
            }
            catch (Exception ex)
            {

                log.Error(ex);
            }



        }


        private void OnTimedEvent(Object source, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                if (Status.Equals("A"))
                {
                    t++;
                    Msg = "Status is Active" + t.ToString();

                    string[] arr = new string[1];


                    
                    log.Info("Get time 1");
                   
                    DateTime dtSecond = DateTime.Now;

                   


                    log.Info("Get time 2");
                    

                    log.Info(dtSecond.ToString() + "-" + dtFirst.ToString() +"-"+ elapseTime.ToString());
                    if (dtSecond > dtFirst)
                    {
                        log.Error("Get Execution time");
                         ScreenCapture.SaveScreen();
                        log.Info("save screen ");

                        endTime = DateTime.Now;

                        int _arrIndex = randomTime.FindIndex(a=>a.Id == elapseindex);
                        if(_arrIndex < randomTime.Count)
                        {
                            elapseTime = randomTime[_arrIndex + 1].Value;
                            elapseindex = randomTime[_arrIndex + 1].Id;
                            log.Info("with in range" + elapseTime.ToString());
                            log.Info(_arrIndex.ToString());
                        }
                        else
                        {
                            elapseTime = randomTime[0].Value;
                            elapseindex = randomTime[0].Id;
                            log.Info("range started again" + elapseTime.ToString());
                        }
                        dtFirst = DateTime.Now.AddMinutes(elapseTime);
                    }
                    else
                    {
                        log.Info("Time difference is less then ellapse time");
                    }

                }
                else
                {
                    Msg = "Task Completed";
                                       
                    log.Info("Task Stop Time :" + DateTime.Now.TimeOfDay.ToString());
                   
                    aTimer.Enabled = false;
                    aTimer.Stop();
                }
            }
            catch (Exception ex)
            {

                log.Error(ex.Message);
            }
        }

        public class Idltime
        {
            public int Id { get; set; }
            public int Value { get; set; }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            long second = timeCount % 60;
            long minute = timeCount / 60 ;
            long hour = minute / 60;

            lblRun.Text = string.Format("{0}:{1}:{2}", GetTime(hour.ToString())
                , GetTime(minute.ToString()), GetTime(second.ToString()));
            timeCount++;
        }
       
        private string GetTime(string val)
        {
            if (val.Length == 1) return string.Format("0{0}", val);

            return val;
        }
        private void TaskDetail_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Status = "I";
            TaskList.Current.Show();
            this.Close();
        }
    }
}
