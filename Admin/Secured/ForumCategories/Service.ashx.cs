using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace AdminModule.Secured.ForumCatg
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
                case "A":
                    {
                        _MethodOutput = AddNewCategory(context);
                        break;
                    }
                case "E":
                    {
                        _MethodOutput = EditCategory(context);
                        break;
                    }
                case "D":
                    {
                        _MethodOutput = DeleteCategory(context);
                        break;
                    }
                case "G":
                    {
                        _MethodOutput = GetAllCategories();
                        break;
                    }
            }

            context.Response.ContentType = "text/json";
            context.Response.Write(_MethodOutput);
        }


        private string EditCategory(HttpContext context)
        {
            try
            {
                string _CategoryName = context.Request.Form["CategoryName"].ToString();
                string _CategoryID = context.Request.Form["_id"].ToString();

                var _Return = new IntellidateLib.ForumCategory().EditCategory(_CategoryID,_CategoryName);
                return JsonConvert.SerializeObject(_Return);
            }
            catch (Exception)
            {
                return "";
            }
        }

        private string AddNewCategory(HttpContext context)
        {
            try
            {
                string _CategoryName = context.Request.Form["CategoryName"].ToString();
                var _Return = new IntellidateLib.ForumCategory().AddNewCategory(_CategoryName);
                return JsonConvert.SerializeObject(_Return);
            }
            catch (Exception)
            {
                return "";
            }
        }

        private string DeleteCategory(HttpContext context)
        {
            try
            {
                string _CategoryID = context.Request.Form["_id"].ToString();
                var _Return = new IntellidateLib.ForumCategory().DeleteCategory(_CategoryID);
                return JsonConvert.SerializeObject(_Return);
            }
            catch (Exception)
            {
                return "";
            }
        }

        private string GetAllCategories()
        {
            try
            {
                var _Return = new IntellidateLib.ForumCategory().GetAllForumCategories();
                return JsonConvert.SerializeObject(_Return);
            }
            catch (Exception)
            {
                return "";
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}