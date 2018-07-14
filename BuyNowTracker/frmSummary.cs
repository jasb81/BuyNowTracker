using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BuyNowTrackerBIZ;
using System.Globalization;
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
        public static frmSummary Current;

        public TaskList lstTask;

        public string TaskSummary { get; set; }
        public frmSummary()
        {
            InitializeComponent();

            // this.Parent = lstTask;


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
            SkipMemo();
        }


        private async void SkipMemo()
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

            // frmSummary.Current.Text = ((TaskList)lstTask).TaskTitle;

        }
    }
}
