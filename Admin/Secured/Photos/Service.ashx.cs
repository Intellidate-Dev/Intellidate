using IntellidateLib;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace AdminModule.Secured.Photos
{
    /// <summary>
    /// Summary description for PhotosMgt
    /// </summary>
    public class PhotosMgt : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {

            HttpContext.Current.Response.ContentType = "application/json";

            if (context.Request.QueryString["method"].ToString() == "GetPhotosBasedOnStatus")
            {
                context.Response.Write(GetPhotosBasedOnStatus(context));
            }


            if (context.Request.QueryString["method"].ToString() == "ApproveOrRejectPhoto")
            {
                context.Response.Write(ApproveOrRejectPhoto(context));
            }


            if (context.Request.QueryString["method"].ToString() == "GetReportedPhotos")
            {
                context.Response.Write(GetReportedPhotos(context));
            }


            if (context.Request.QueryString["method"].ToString() == "ApproveOrRejectBulkPhotos")
            {
                context.Response.Write(ApproveOrRejectBulkPhotos(context));
            }


            if (context.Request.QueryString["method"].ToString() == "GetPhotosCount")
            {
                context.Response.Write(GetPhotosCount(context));
            }


            if (context.Request.QueryString["method"].ToString() == "GetPhotosBasedOnUserName")
            {
                context.Response.Write(GetPhotosBasedOnUserName(context));
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
        /// The GetPhotosBasedOnStatus method. The method must returns Photos
        /// </summary>
        /// <returns>Json string Object</returns>
        private string GetPhotosBasedOnStatus(HttpContext context)
        {
            try
            {
                int _status = Convert.ToInt32(context.Request.Form["Status"].ToString());
             //   int AdminId=Convert.ToInt32(context.Request.Form["AdminId"].ToString());
          
                Photo[] _res = new Photo().GetPhotosBasedOnStatus(_status);
                
               string _return = JsonConvert.SerializeObject(_res);
                return _return;
            }
            catch (Exception ex)
            {
                new Error().LogError(ex, "PhotosMgt", "GetPhotosBasedOnStatus");
                return "";
            }
        }



        /// <summary>
        /// The GetReportedPhotos method. The method must returns Photos
        /// </summary>
        /// <returns>Json string Object</returns>
        private string GetReportedPhotos(HttpContext context)
        {
            try
            {
                Photo[] _res = new Photo().GetAbusiveReportedPhotos();
                string _return = JsonConvert.SerializeObject(_res);
                return _return;
            }
            catch (Exception ex)
            {
                new Error().LogError(ex, "PhotosMgt", "GetReportedPhotos");
                return "";
            }
        }



        /// <summary>
        /// The GetReportedPhotos method. The method must returns Photos
        /// </summary>
        /// <returns>Json string Object</returns>
        private string GetPhotosBasedOnUserName(HttpContext context)
        {
            try
            {
                string _UserName = context.Request.Form["UserName"].ToString();
                Photo[] _res = new Photo().GetPhotosBasedOnUserName(_UserName);
                string _return = JsonConvert.SerializeObject(_res);
                return _return;
            }
            catch (Exception ex)
            {
                new Error().LogError(ex, "PhotosMgt", "GetPhotosBasedOnUserName");
                return "";
            }
        }



        private string ApproveOrRejectPhoto(HttpContext context)
        {
            try
            {
                bool _status=Convert.ToBoolean(context.Request.Form["Status"].ToString());
                int _AdminId=Convert.ToInt32(context.Request.Form["AdminId"].ToString());
                int _PhotoId=Convert.ToInt32(context.Request.Form["PhotoId"].ToString());
                string _comment=context.Request.Form["Comment"].ToString();
                bool IsRejected = Convert.ToBoolean(context.Request.Form["IsRejected"].ToString());
                var _return = ApproveRejectPhotos(_status, _AdminId, _PhotoId, _comment, IsRejected);
                if (_return==null)
                {
                    return "0";
                }
                else
                {
                    return JsonConvert.SerializeObject(_return);
                }
               
            }
            catch (Exception ex)
            {
                new Error().LogError(ex, "PhotosMgt", "ApproveOrRejectPhoto");
                return "";
            }
        }



        private string ApproveOrRejectBulkPhotos(HttpContext context)
        {
            try
            {
                int _AdminId = Convert.ToInt32(context.Request.Form["AdminId"].ToString());
                string[] _PhotoIds =context.Request.Form["PhotoId"].ToString().Split(',');
                string _comment = context.Request.Form["Comment"].ToString();
                bool _status = Convert.ToBoolean(context.Request.Form["Status"].ToString());
                bool IsRejected = Convert.ToBoolean(context.Request.Form["IsRejected"].ToString());
                List<Photo> obj = new List<Photo>();
                foreach (string PhotoId in _PhotoIds)
                {
                    int _id = Convert.ToInt32(PhotoId);
                    var res = ApproveRejectPhotos(_status, _AdminId, _id, _comment,IsRejected);
                    if (res != null)
                    {
                        obj.Add(res);
                    }
                }
                var _data= JsonConvert.SerializeObject(obj); ;
                return _data;
            }
            catch (Exception ex)
            {
                new Error().LogError(ex, "PhotosMgt", "ApproveOrRejectPhoto");
                return "";
            }
        }



        public Photo ApproveRejectPhotos(bool Status,int AdminId,int PhotoId,string comment,bool isRejected)
        {
            try
            {
                Photo _return = new Photo();
                string _getLast = new AdminPhoto().GetAdminPhotoLastApprovalStatus(AdminId, PhotoId);
                if (_getLast != "0")
                {
                    bool adminLastApproval = Convert.ToBoolean(_getLast);
                    if (adminLastApproval != Status || isRejected==false)
                    {
                        Photo _res = new Photo().ApproveOrRejectPhoto(Status, PhotoId, AdminId, comment);
                        _return = _res;
                    }
                }
                else
                {
                    Photo _res = new Photo().ApproveOrRejectPhoto(Status, PhotoId, AdminId, comment);
                    _return = _res;
                }

                return _return;
            }
            catch(Exception ex)
            {
                new Error().LogError(ex, "PhotosMgt", "ApproveRejectPhotos");
                return null;
            }
        }



        public string GetPhotosCount(HttpContext context)
        {
            try
            {
                Dictionary<string, int> dicObj = new Dictionary<string, int>();
                int RequestedCount = new Photo().GetPhotosBasedOnStatusCount(0);
                int ReportedCount = new Photo().GetAbusiveReportedPhotos().Count();
                int RejectedCount = new Photo().GetPhotosBasedOnStatusCount(-1);
                int PendingCount = new Photo().GetPhotosBasedOnStatusCount(1);
                int ApprovedCount = new Photo().GetPhotosBasedOnStatusCount(2);
                //adding photos count to dictionary..
                dicObj.Add("Requested", RequestedCount);
                dicObj.Add("Reported", ReportedCount);
                dicObj.Add("Rejected", RejectedCount);
                dicObj.Add("Pending", PendingCount);
                dicObj.Add("Approved", ApprovedCount);

                string _return = JsonConvert.SerializeObject(dicObj);
                return _return;
            }
            catch (Exception ex)
            {
                new Error().LogError(ex, "PhotosMgt", "GetPhotosCount");
                return "";
            }
        }



    }
}