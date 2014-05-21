using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IntellidateLib;
namespace AdminModule.Secured.Messages
{
    public partial class addcollection : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
         
        }

        protected void Button1_Click(object sender, EventArgs e)
        {

            new Message().SendMessage(28, 29, "Hello Good Morning");
            new Message().SendMessage(29, 30, "Hello Good afternoon");
            new Message().SendMessage(30, 31, "Hello Good Morning");
            new Message().SendMessage(31, 32, "Hello Good Morning");
            new Message().SendMessage(32, 33, "Hello Good afternoon");
            new Message().SendMessage(33, 34, "Hello Good Morning");



        }
    }
}