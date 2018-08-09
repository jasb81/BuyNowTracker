using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using BuyNowTrackerBIZ;
using System.Text.RegularExpressions;


namespace BuyNowTracker
{
    public partial class frmLogin : Form
    {
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(typeof(frmLogin));
        public frmLogin()
        {
            InitializeComponent();
        }

        #region Events

        private void frmLogin_Load(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.email != string.Empty)
            {
                txtUserName.Text = Properties.Settings.Default.email;
                txtPassword.Text = Properties.Settings.Default.password;
                txtUserName.ForeColor = Color.Black;
                txtPassword.ForeColor = Color.Black;
                chkRemember.Checked = true;

            }

        }
        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                string userName = txtUserName.Text;
                string password = txtPassword.Text;

                if (userName.ToLower() != "your email addresss" && password.ToLower() != "password")
                {
                    if (!ValidateEmail())
                    {
                        lblStatus.Text = "invalid email address";

                        return;
                    }

                    if (chkRemember.Checked)
                    {
                        Properties.Settings.Default.email = userName;
                        Properties.Settings.Default.password = password;
                        Properties.Settings.Default.Save();
                    }
                    else
                    {
                        Properties.Settings.Default.Reset();
                    }

                    LoginUser(userName, password);

                }
                else
                {
                    lblStatus.Text = "Please enter your email or password";
                }
            }
            catch (Exception ex)
            {

               // logger.Info("btnLogin_Click(object sender, EventArgs e) " + ex.Message);
            }
        }

        private void txtUserName_Enter(object sender, EventArgs e)
        {
            if (txtUserName.Text.ToLower() == "your email address")
            {
                txtUserName.Text = "";

                txtUserName.ForeColor = Color.Black;
            }
        }

        private void txtUserName_Leave(object sender, EventArgs e)
        {
            if (txtUserName.Text == "")
            {
                txtUserName.Text = "Your email address";

                txtUserName.ForeColor = Color.Silver;
            }
        }

        private void txtPassword_Enter(object sender, EventArgs e)
        {
            if (txtPassword.Text.ToLower() == "password")
            {
                txtPassword.Text = "";

                txtPassword.ForeColor = Color.Black;
            }
        }

        private void txtPassword_Leave(object sender, EventArgs e)
        {
            if (txtPassword.Text == "")
            {
                txtPassword.Text = "Password";

                txtPassword.ForeColor = Color.Silver;
            }

        }

        #endregion

        public async void LoginUser(string userName,string passowrd)
        {

            string jsonString = string.Empty;
            string token = string.Empty;

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            var client = new HttpClient();

            this.Cursor = Cursors.WaitCursor;

            var values = new Dictionary<string, string>
            {
               { "action", "login" },
               { "username", userName },
               { "password", passowrd },
            };

            var content = new FormUrlEncodedContent(values);

            HttpResponseMessage message = await client.PostAsync("https://www.buynowdepot.com/api.php", content);

            var responseString = await message.Content.ReadAsStringAsync();

            this.Cursor = Cursors.Default;

            JObject obj = (JObject)JsonConvert.DeserializeObject(responseString);

            if (obj != null && obj["result"].ToString().ToLower() == "success")
            {
                if (obj["_token"].ToString() != string.Empty)
                {
                    token = obj["_token"].ToString();

                    var jwtHandler = new JwtSecurityTokenHandler();

                    var readableToken = jwtHandler.CanReadToken(token);

                    if (readableToken == true)
                    {
                        var tk = jwtHandler.ReadJwtToken(token);

                        var headers = tk.Header;

                        var claims = tk.Claims;

                        foreach (Claim c in claims)
                        {
                            if (c.Type == "data")
                            {
                                jsonString = c.Value;
                            }
                        }
                    }

                }
                else
                {
                    lblStatus.Text = "Email or password invalid";
                }
            }
            else
                lblStatus.Text = obj["messages"][0].ToString();



            if (jsonString != string.Empty)
            {
                User u = JsonConvert.DeserializeObject<User>(jsonString);

                this.Hide();

                TaskList tskList = new TaskList(u, token);
                tskList.Show();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(1);
        }


        private bool ValidateEmail()
        {
            bool isValid = true;

            var mRegxExpression = new Regex(@"^([a-zA-Z0-9_\-])([a-zA-Z0-9_\-\.]*)@(\[((25[0-5]|2[0-4][0-9]|1[0-9][0-9]|[1-9][0-9]|[0-9])\.){3}|((([a-zA-Z0-9\-]+)\.)+))([a-zA-Z]{2,}|(25[0-5]|2[0-4][0-9]|1[0-9][0-9]|[1-9][0-9]|[0-9])\])$");

            if (!mRegxExpression.IsMatch(txtUserName.Text.Trim()))
            {
                isValid = false;
            }

            return isValid;
        }

    } 
}
