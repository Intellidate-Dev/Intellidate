using IntellidateLib;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdminModule.Secured.Settings
{
    /// <summary>
    /// Summary description for Service
    /// </summary>
    public class Service : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string _MethodOutput = "";
            string _MethodCall = "";

            if (context.Request.QueryString["f"] != null)
            {
                _MethodCall = context.Request.QueryString["f"].ToString();
            }


            switch (_MethodCall)
            {
                case "CAP":
                    {
                        _MethodOutput = ChangeAdminPassword(context);
                        break;
                    }
                case "CAE":
                    {
                        _MethodOutput = ChangeAdminEmailId(context);
                        break;
                    }
                case "GAD":
                    {
                        _MethodOutput = GetAdminDetails(context);
                        break;
                    }
            }


            context.Response.ContentType = "text/json";
            context.Response.Write(_MethodOutput);


        }



        public bool IsReusable
        {
            get
            {
                return false;
            }
        }




        /// <summary>
        /// The ChangeAdminPassword method. The method must returns true/false
        /// </summary>
        /// <returns>Json string Object</returns>
        private string ChangeAdminPassword(HttpContext context)
        {
            try
            {
                int _adminid = Convert.ToInt32(context.User.Identity.Name);
                string _password = context.Request.Form["Password"].ToString();
                bool _res = new Admin().ChangeAdminPassword(_adminid, _password);
                string _return = JsonConvert.SerializeObject(_res);
                return _return;
            }
            catch (Exception ex)
            {
                new Error().LogError(ex, "AdminSettings Service", "ChangeAdminPassword");
                return "";
            }
        }




        /// <summary>
        /// The ChangeAdminPassword method. The method must returns true/false
        /// </summary>
        /// <returns>Json string Object</returns>
        private string ChangeAdminEmailId(HttpContext context)
        {
            try
            {
                int _adminid = Convert.ToInt32(context.User.Identity.Name);
                string _emailid = context.Request.Form["EmailId"].ToString();
                bool _res = new Admin().ChangeAdminEmailAddress(_adminid, _emailid);
                string _return = JsonConvert.SerializeObject(_res);
                return _return;
            }
            catch (Exception ex)
            {
                new Error().LogError(ex, "AdminSettings Service", "ChangeAdminEmailId");
                return "";
            }
        }

        /// <summary>
        /// The GetAdminDetails method. The method must returns Admin
        /// </summary>
        /// <returns>Json string Object</returns>
        private string GetAdminDetails(HttpContext context)
        {
            try
            {
                int _adminid = Convert.ToInt32(context.User.Identity.Name);
                Admin _res = new Admin().GetAdminDetails(_adminid);
                string _return = JsonConvert.SerializeObject(_res);
                return _return;
            }
            catch (Exception ex)
            {
                new Error().LogError(ex, "AdminSettings Service", "ChangeAdminEmailId");
                return "";
            }
        }


    }
}