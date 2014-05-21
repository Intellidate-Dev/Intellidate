using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace IntellidateUser
{
    public partial class Default : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                // Setting the site title
                //htmlHead.Title = ConfigurationManager.AppSettings["SiteTitle"].ToString();

            }
            catch (Exception)
            {
                return;
            }
        }
        public void SetTitle(string Title)
        {
            htmlHead.Title = Title;
        }
    }
}