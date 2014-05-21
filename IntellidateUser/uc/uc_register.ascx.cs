using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using IntellidateLib;

namespace IntellidateUser.uc
{
    public partial class uc_register : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //FillDates();
            }
        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            // Register user
            string _UserName = txtUserName.Value;
            string _EmailAddress = txtEmail.Value;
            string _Password = txtPassword.Value;

            var _UserObject = new IntellidateLib.User().RegisterUser(_UserName, _EmailAddress, _Password);

            // Authenticate user
            FormsAuthentication.SetAuthCookie(_UserObject._RefID.ToString(), true);

            FormsAuthenticationTicket intellidateTicket = new FormsAuthenticationTicket(1,_UserObject._RefID.ToString(),DateTime.Now,DateTime.Now.AddDays(30),true,"");
            HttpCookie intellidateCookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(intellidateTicket));
            Response.Cookies.Add(intellidateCookie);

            //Redirect to Home page
            Response.Redirect(ConfigurationManager.AppSettings["SiteUrl"].ToString() + "web/Home");
        }

    }
}