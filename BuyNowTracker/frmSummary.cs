using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using MaterialSkin;
using MaterialSkin.Controls;
using System.Windows.Forms;

namespace BuyNowTracker
{
    public partial class frmSummary : MaterialForm
    {
        private ToolTip toolText = null;
        private bool anyButtonClicked = false;
        public TaskList lstTask;
        public string TaskSummary { get; set; }

        public frmSummary()
        {
            InitializeComponent();

            anyButtonClicked = false;

            MaterialSkinManager materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;

            // Configure color schema
            materialSkinManager.ColorScheme = new ColorScheme(
                Primary.Blue400, Primary.Blue500,
                Primary.Blue500, Accent.LightBlue200,
                TextShade.WHITE
            );
        }

        private void btnMemo_Click(object sender, EventArgs e)
        {
            if (txtMemo.Text == string.Empty)
            {
                pnlSummary.BackColor = Color.Red; 
                return;
            }
            pnlSummary.BackColor = Color.White;

            if(toolText != null)
                toolText.Dispose();

            AddMemo();
        }

        private async void AddMemo()
        {
            this.Cursor = Cursors.WaitCursor;

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            string jsonString = string.Empty;

            var client = new HttpClient();

            client.DefaultRequestHeaders.Add("Authorizations", "Bearer " + lstTask.Token);

            var values = new Dictionary<string, string>
                {
                   { "action", "savememo" },

                   { "timerid",  lstTask.LogActivityId.ToString() },

                   {"memo",  txtMemo.Text}
                };

            var content = new FormUrlEncodedContent(values);

            HttpResponseMessage message = await client.PostAsync("https://buynowdepot.com/api.php", content);

            var responseString = await message.Content.ReadAsStringAsync();

            this.Cursor = Cursors.Default;

            JObject j = (JObject)JsonConvert.DeserializeObject(responseString);

            if (j["result"].ToString().ToLower() == "success")
            {
                anyButtonClicked = true;
                this.Close();
                lstTask.EndTaskTimer();

            }
            else
            {
                MessageBox.Show(j["messages"][0].ToString(), "Error", MessageBoxButtons.OK);
            }

        }

        private void btnSkip_Click(object sender, EventArgs e)
        {
            pnlSummary.BackColor = Color.White;

            if (toolText != null)
                toolText.Dispose();

            

            SkipMemo(true);
        }

        private async void SkipMemo(bool isButtonClicked)
        {

            this.Cursor = Cursors.WaitCursor;

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            string jsonString = string.Empty;

            var client = new HttpClient();

            client.DefaultRequestHeaders.Add("Authorizations", "Bearer " + lstTask.Token);

            var values = new Dictionary<string, string>
                {
                   { "action", "skipmemo" },

                   { "timerid",  lstTask.LogActivityId.ToString() },
                };

            var content = new FormUrlEncodedContent(values);

            HttpResponseMessage message = await client.PostAsync("https://buynowdepot.com/api.php", content);

            var responseString = await message.Content.ReadAsStringAsync();

            this.Cursor = Cursors.Default;

            JObject j = (JObject)JsonConvert.DeserializeObject(responseString);

            if (j["result"].ToString().ToLower() == "success")
            {
                anyButtonClicked = true;

                if (isButtonClicked)
                    this.Close();


                lstTask.EndTaskTimer();
            }
            else
            {
                MessageBox.Show(j["messages"][0].ToString(), "Error", MessageBoxButtons.OK);
            }

        }

        private void frmSummary_Load(object sender, EventArgs e)
        {
            this.Text = TaskSummary;
        }

        private void txtMemo_MouseHover(object sender, EventArgs e)
        {

            if(pnlSummary.BackColor == Color.Red)
            {
                if (toolText != null)
                    toolText.Dispose();

                    toolText = new ToolTip();
                toolText.InitialDelay = 0;
                toolText.IsBalloon = true;
                toolText.Show(string.Empty, txtMemo);
                toolText.Show("Required", txtMemo, 0);
            }
        }

        private void frmSummary_FormClosing(object sender, FormClosingEventArgs e)
        {
            pnlSummary.BackColor = Color.White;

            if (toolText != null)
                toolText.Dispose();

            if(!anyButtonClicked)
                SkipMemo(false);
        }
    }
}
