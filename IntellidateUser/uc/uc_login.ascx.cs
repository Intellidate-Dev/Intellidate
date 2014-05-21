using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IntellidateLib;
using System.Web.Security;
using System.Configuration;

namespace IntellidateUser.uc
{
    public partial class uc_login : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["Counter"] == null)
                {
                    Session["Counter"] = 0;
                }
            }
        }
       
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                User _UserObject = new User().AuthenticateUser(txtUserName.Value, txtPassword.Value);


                if (!(Convert.ToInt32(Session["Counter"]) >= 3))
                {
                    if (_UserObject != null)
                    {
                        //remember me check box
                        if (this.chkRemember != null && this.chkRemember.Checked == true)
                        {
                            // Clear any other tickets that are already in the response
                            Response.Cookies.Clear();
                            FormsAuthentication.SetAuthCookie(_UserObject._RefID.ToString(), true);
                        }
                        else
                        {
                            // Authenticate user
                            FormsAuthentication.SetAuthCookie(_UserObject._RefID.ToString(), false);
                        }

                        FormsAuthenticationTicket intellidateTicket = new FormsAuthenticationTicket(1, _UserObject._RefID.ToString(), DateTime.Now, DateTime.Now.AddDays(30), true, "");
                        HttpCookie intellidateCookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(intellidateTicket));
                        Response.Cookies.Add(intellidateCookie);

                        //insert user login details
                        var m_Location = new System.Device.Location.GeoCoordinateWatcher();

                        //Redirect to Home page
                        Response.Redirect(ConfigurationManager.AppSettings["SiteUrl"].ToString() + "web/home");

                    }
                    else
                    {
                        int m_Count = Convert.ToInt32(Session["Counter"]);
                        m_Count = m_Count + 1;
                        Session["Counter"] = m_Count;
                        Session["UserName"] = txtUserName.Value;
                        lblErrorMessage.Visible = true;
                        lblErrorMessage.Text = "Invalid credentials";
                    }
                }
                else
                {
                    lblErrorMessage.Visible = true;
                    lblErrorMessage.Text = "User has been locked, please try some other time.";
                    if (Session["UserName"] != null)
                    {
                        if (Session["UserName"].ToString().Trim() != txtUserName.Value.Trim())
                        {
                            Session["Counter"] = 0;
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                new Error().LogError(exception, "uc_login", "btnLogin_Click");
            }
           
        }
       
    }
}