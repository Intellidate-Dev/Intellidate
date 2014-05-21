using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace IntellidateUser.js
{
    public partial class jsBackendData : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.ContentType = "application/x-javascript";
            StringBuilder _JsString = new StringBuilder();

            foreach (string _EachKey in ConfigurationManager.AppSettings.AllKeys)
            {
                if (_EachKey.StartsWith("JS-"))
                {
                    _JsString.Append("var " + _EachKey.Replace("JS-","") + " = " + "'" +  ConfigurationManager.AppSettings[_EachKey].ToString() + "';");
                }
            }

            Response.Write(_JsString.ToString());
            
        }
    }
}