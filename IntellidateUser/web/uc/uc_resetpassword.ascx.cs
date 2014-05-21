using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IntellidateLib;
using System.Web.Security;

namespace IntellidateUser.uc
{
    public partial class uc_resetpassword : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnResetPwd_Click(object sender, EventArgs e)
        {
            try
            {
               
                if (txtPassword.Value != string.Empty && txtRePassword.Value != string.Empty)
                {
                    int m_Userid = 0;
                    HttpCookie authCookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
                    if (authCookie != null)
                    {
                        string encTicket = authCookie.Value;

                        if (!String.IsNullOrEmpty(encTicket))
                        {
                            // decrypt the ticket if possible.
                            FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(encTicket);

                            m_Userid = Convert.ToInt32(ticket.Name);
                        }
                    }
                    if (m_Userid != 0)
                    {
                        var m_Res = new User().ChangeUserPassword(m_Userid, txtRePassword.Value);
                        if (m_Res)
                        {
                            lblMsg.Visible = true;
                            lblMsg.Text = "Your password has been changed.";
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                new Error().LogError(exception, "uc_resetpassword", "btnResetPwd_Click");
            }
           
        }
    }
}