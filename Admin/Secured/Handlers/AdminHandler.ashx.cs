using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using IntellidateLib;
using System.Web.Mvc;
namespace AdminModule.Secured
{
    /// <summary>
    /// Summary description for AdminHandler
    /// </summary>
    public class AdminHandler : IHttpHandler
    {
      
        public void ProcessRequest(HttpContext context)
        {
            HttpResponse response = context.Response;
            response.ContentType = "text/json";
            //check the all method requests
            if (context.Request.QueryString["method"].ToString() == "AddNewAdmin")
            {
                HttpContext.Current.Response.ContentType = "application/json";
                context.Response.Write(AddNewAdmin(context));
            }

            if (context.Request.QueryString["method"].ToString() == "GetAllAdmin")
            {
                HttpContext.Current.Response.ContentType = "application/json";
                context.Response.Write(GetAllAdmins(context));
            }
            if (context.Request.QueryString["method"].ToString() == "ActivateOrDeActivateAdmin")
            {
                HttpContext.Current.Response.ContentType = "application/json";
                context.Response.Write(ActivateOrDeActivateAdmin(context));
            }

            if (context.Request.QueryString["method"].ToString() == "SearchAdmins")
            {
                HttpContext.Current.Response.ContentType = "application/json";
                context.Response.Write(SearchAdmins(context));
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// The Add New User method. The method must returns true
        /// </summary>
        /// <returns>Json string Object</returns>
        private string AddNewAdmin(HttpContext context)
        {
            try
            {
                //call Add New User method from User class
                string AdminName = context.Request.QueryString["AdminName"].ToString();
                string Password = context.Request.QueryString["Password"].ToString();
                string AdminType = context.Request.QueryString["AdminType"].ToString();
                string EmailID = context.Request.QueryString["EmailID"].ToString();
                bool _res = true;// new Admin().AddNewAdminUser(AdminName, Password, AdminType, EmailID, null);     
                string _return = JsonConvert.SerializeObject(_res);
                return _return;
            }
            catch (Exception ex)
            {
                new Error().LogError(ex, "AdminHandler", "AddNewAdmin");
                return "";
            }
        }
        private string GetAllAdmins(HttpContext context)
        {
            try
            {
                Admin[] _res = new Admin().GetAllAdminUsers();           
                string _return = JsonConvert.SerializeObject(_res);        
                return _return;
            }
            catch (Exception ex)
            {
                new Error().LogError(ex, "AdminHandler", "GetAllAdmins");
                return "";
            }
        }

        private string ActivateOrDeActivateAdmin(HttpContext context)
        {
            try
            {
                string AdminId = context.Request.QueryString["AdminId"].ToString();
                string Status=context.Request.QueryString["Status"].ToString();
                bool _res = new Admin().ActivateOrDeActivateAdmin(Convert.ToInt32(AdminId), Status);
                string _return = JsonConvert.SerializeObject(_res);
                return _return;
            }
            catch (Exception ex)
            {
                new Error().LogError(ex, "AdminHandler", "ActivateOrDeActivateAdmin");
                return "";
            }
        }


        private string SearchAdmins(HttpContext context)
        {
            try
            {
                //SearchKey must be admin user login name
                string SearchKey = context.Request.QueryString["SearchKey"].ToString();
                Admin[] _res = new Admin().GetAllAdminUsers(SearchKey);
                string _return = JsonConvert.SerializeObject(_res);
                return _return;
            }
            catch (Exception ex)
            {
                new Error().LogError(ex, "AdminHandler", "SearchAdmins");
                return "";
            }
        }
    }
 
}