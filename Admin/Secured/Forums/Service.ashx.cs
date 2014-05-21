using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace AdminModule.Secured.Forums
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
                        _MethodOutput = AddNewPost(context);
                        break;
                    }
                case "RP":
                    {
                        _MethodOutput = ReplyPost(context);
                        break;
                    }

                case "G":
                    {
                        _MethodOutput = GetAllPosts(context);
                        break;
                    }
                case "U":
                    {
                        _MethodOutput = UploadFile(context);
                        break;
                    }
            }

            context.Response.ContentType = "text/json";
            context.Response.Write(_MethodOutput);
        }

        private string UploadFile(HttpContext context)
        {
            string _out = "";
            try
            {
                HttpPostedFile objFiles;
                if (context.Request.Files[0] != null)
                {
                    objFiles = (HttpPostedFile)context.Request.Files[0];
                    string _FileExtension = objFiles.FileName.Split('.')[objFiles.FileName.Split('.').Length - 1];
                    string _FileName = DateTime.Now.ToString("ddMMyyyyhhmmss") + "." + _FileExtension;
                    string _FilePath = context.Server.MapPath("~") + "\\Secured\\Attachments\\" + _FileName;
                    objFiles.SaveAs(_FilePath);
                    _out = _FileName;
                }
            }
            catch (Exception)
            {
                _out = "";
            }
            return JsonConvert.SerializeObject(_out);
        }

        private string GetAllPosts(HttpContext context)
        {
            int _LastShownID = Convert.ToInt32(context.Request.Form["lastshownid"]);
            var _Ret = new IntellidateLib.Forum().GetMainPosts(_LastShownID);
            return JsonConvert.SerializeObject(_Ret);
        }

        private string ReplyPost(HttpContext context)
        {
            int _adminid = Convert.ToInt32(context.User.Identity.Name);
            string _content = context.Request.Form["content"].ToString();
            int _postid = Convert.ToInt32(context.Request.Form["postid"].ToString());

            _content = _content.Replace("\n","<br />");

            var _Ret = new IntellidateLib.Forum().ReplyPost(_adminid, _postid, _content);

            return JsonConvert.SerializeObject(_Ret);
        }

        private string AddNewPost(HttpContext context)
        {
            try
            {
                int _adminid = Convert.ToInt32(context.User.Identity.Name);
                string _title = context.Request.Form["title"].ToString();
                string _content = context.Request.Form["content"].ToString();

                _content = _content.Replace("\n", "<br />");

                int _category = Convert.ToInt32(context.Request.Form["category"].ToString());
                List<string> _Attachments = new List<string>();
                if (context.Request.Form["attachment"].ToString() != "")
                {
                    _Attachments.Add(context.Request.Form["attachment"].ToString());
                }

                var _Ret = new IntellidateLib.Forum().AddNewPost(_adminid, _category, _title, _content, _Attachments.ToArray());

                return JsonConvert.SerializeObject(_Ret);
            }
            catch (Exception)
            {
                return JsonConvert.SerializeObject("");
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