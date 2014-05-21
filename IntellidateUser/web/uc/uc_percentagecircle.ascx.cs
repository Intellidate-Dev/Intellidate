using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IntellidateUser.web.uc
{
    public partial class uc_percentagecircle : System.Web.UI.UserControl
    {
        private string name;

        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
    }
}