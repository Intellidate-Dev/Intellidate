using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IntellidateLib;
using System.Web.Security;
using System.Configuration;

namespace AdminModule
{
    public partial class AdminLogin : System.Web.UI.Page
    {
        Admin adminObj;
        protected void Page_Load(object sender, EventArgs e)
        {
            //if admin is already login he redirect to home page
            if (Session["AdminName"] != null)
            {
                Response.Redirect("/Secured/Forums/");
            }
        }

        protected void objLogin_Authenticate(object sender, AuthenticateEventArgs e)
        {
            string _LoginID = objLogin.UserName;
            string _Password = objLogin.Password;

            // check if he is super admin
            if (_LoginID.ToUpper().Trim() == ConfigurationManager.AppSettings["SuperAdminLogin"].ToString())
            {
                if (_Password == ConfigurationManager.AppSettings["SuperAdminPassword"].ToString())
                {
                    // Success
                    if (Roles.FindUsersInRole("SuperAdmin", _LoginID).Length == 0)
                    {
                        Roles.AddUserToRole(_LoginID, "SuperAdmin");
                    }
                    FormsAuthentication.SetAuthCookie(_LoginID, objLogin.RememberMeSet);
                    e.Authenticated = true;
                    Response.Redirect(objLogin.DestinationPageUrl);
                }
                else
                {
                    e.Authenticated = false;
                }
            }
            else
            {
                adminObj = new Admin().AuthanticateAdmin(_LoginID, _Password);

                if (adminObj != null)
                {
                    if (adminObj.ForumPrivileges != null)
                    {
                        Session["ForumPrivileges"] = adminObj.ForumPrivileges;
                    }

                    // Getting the priveleges
                    foreach (string _eachPrivilege in adminObj.AdminPrivileges)
                    {
                        if (Roles.IsUserInRole(adminObj.AdminRefId.ToString(), _eachPrivilege)==false)
                        {
                            Roles.AddUserToRole(adminObj.AdminRefId.ToString(), _eachPrivilege);
                        }
                    }
                    FormsAuthentication.SetAuthCookie(adminObj.AdminRefId.ToString(), objLogin.RememberMeSet);
                    e.Authenticated = true;
                    Response.Redirect(objLogin.DestinationPageUrl);
                }
                else
                {
                    e.Authenticated = false;
                }
            }


        }

    
    }
}