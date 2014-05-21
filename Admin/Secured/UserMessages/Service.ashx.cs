using IntellidateLib;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdminModule.Secured.UserMessages
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
                case "GTMRM":
                    {
                        _MethodOutput = GetThisMonthReportedMessages(context);
                        break;
                    }
                case "GTWRM":
                    {
                        _MethodOutput = GetThisWeekReportedMessages(context);
                        break;
                    }
                case "GTDRM":
                    {
                        _MethodOutput = GetToDayReportedMessages(context);
                        break;
                    }
                case "GRMBTD":
                    {
                        _MethodOutput = GetReportedMessagesBetweenToDates(context);
                        break;
                    }
                case "GRML":
                    {
                        _MethodOutput = GetRejectedMessageList(context);
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
        /// The GetThisMonthReportedMessages method. The method must returns Messages
        /// </summary>
        /// <returns>Json string Object</returns>
        private string GetThisMonthReportedMessages(HttpContext context)
        {
            try
            {
                Message[] _res = new Message().GetThisMonthReportedMessages();
                string _return = JsonConvert.SerializeObject(_res);
                return _return;
            }
            catch (Exception ex)
            {
                new Error().LogError(ex, "UserMessages Service", "GetThisMonthReportedMessages");
                return "";
            }
        }



        /// <summary>
        /// The GetThisWeekReportedMessages method. The method must returns Messages
        /// </summary>
        /// <returns>Json string Object</returns>
        private string GetThisWeekReportedMessages(HttpContext context)
        {
            try
            {
                Message[] _res = new Message().GetThisWeekReportedMessages();
                string _return = JsonConvert.SerializeObject(_res);
                return _return;
            }
            catch (Exception ex)
            {
                new Error().LogError(ex, "UserMessages Service", "GetThisWeekReportedMessages");
                return "";
            }
        }


        /// <summary>
        /// The GetToDayReportedMessages method. The method must returns Messages
        /// </summary>
        /// <returns>Json string Object</returns>
        private string GetToDayReportedMessages(HttpContext context)
        {
            try
            {
                Message[] _res = new Message().GetTodayReportedMessages();
                string _return = JsonConvert.SerializeObject(_res);
                return _return;
            }
            catch (Exception ex)
            {
                new Error().LogError(ex, "UserMessages Service", "GetToDayReportedMessages");
                return "";
            }
        }




        /// <summary>
        /// The GetReportedMessagesBetweenToDates method. The method must returns Messages
        /// </summary>
        /// <returns>Json string Object</returns>
        private string GetReportedMessagesBetweenToDates(HttpContext context)
        {
            try
            {
                DateTime _From = Convert.ToDateTime(context.Request.Form["FromDate"]);  
                DateTime _To = Convert.ToDateTime(context.Request.Form["ToDate"]);                            
                Message[] _res = new Message().GetReportedMessagesBetweenTwoDates(_From,_To);
                string _return = JsonConvert.SerializeObject(_res);
                return _return;
            }
            catch (Exception ex)
            {
                new Error().LogError(ex, "UserMessages Service", "GetReportedMessagesBetweenToDates");
                return "";
            }
        }




        /// <summary>
        /// The GetRejectedMessageList method. The method must returns Messages
        /// </summary>
        /// <returns>Json string Object</returns>
        private string GetRejectedMessageList(HttpContext context)
        {
            try
            {
                Message[] _res = new Message().GetRejectedMessageList();
                string _return = JsonConvert.SerializeObject(_res);
                return _return;
            }
            catch (Exception ex)
            {
                new Error().LogError(ex, "UserMessages Service", "GetRejectedMessageList");
                return "";
            }
        }




        /// <summary>
        /// The RejectMessage method. The method must returns true/false
        /// </summary>
        /// <returns>Json string Object</returns>
        private string RejectOrApproveReportedMessage(HttpContext context)
        {
            try
            {
                int _AdminId = Convert.ToInt32(context.Request.Form["AdminId"]);  
                int _MessageRefID = Convert.ToInt32(context.Request.Form["MessageId"]);  
                bool _IsRejected = Convert.ToBoolean(context.Request.Form["IsRejected"]);
                string _Comment = context.Request.Form["Comment"].ToString();
                bool _res = new Message().RejectOrApproveReportedMessage(_MessageRefID, _IsRejected, _AdminId, _Comment);
                string _return = JsonConvert.SerializeObject(_res);
                return _return;
            }
            catch (Exception ex)
            {
                new Error().LogError(ex, "UserMessages Service", "RejectOrApproveReportedMessage");
                return "";
            }
        }

    }
}