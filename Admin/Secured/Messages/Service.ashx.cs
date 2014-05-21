using IntellidateLib;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace AdminModule.Secured.Messages
{
    /// <summary>
    /// Summary description for MessageMgt
    /// </summary>
    public class MessageMgt : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            HttpContext.Current.Response.ContentType = "application/json";

            if (context.Request.QueryString["method"].ToString() == "SearchUser")
            {               
                context.Response.Write(SearchUser(context));
            }


            if (context.Request.QueryString["method"].ToString() == "SendMessage")
            {
                context.Response.Write(SendMessage(context));
            }


            if (context.Request.QueryString["method"].ToString() == "SendMessageToAllUsers")
            {
                context.Response.Write(SendMessageToAllUsers(context));
            }

            
            if (context.Request.QueryString["method"].ToString() == "GetLocations")
            {
                context.Response.Write(GetLocations(context));
            }


            if (context.Request.QueryString["method"].ToString() == "GetEthnicity")
            {
                context.Response.Write(GetEthnicity(context));
            }


            if (context.Request.QueryString["method"].ToString() == "GetEducationDetails")
            {
                HttpContext.Current.Response.ContentType = "application/json";
                context.Response.Write(GetEducationDetails(context));
            }
            

            if (context.Request.QueryString["method"].ToString() == "GetDrinkDetails")
            {
                context.Response.Write(GetDrinkDetails(context));
            }


            if (context.Request.QueryString["method"].ToString() == "GetReligion")
            {
                context.Response.Write(GetReligion(context));
            }


            if (context.Request.QueryString["method"].ToString() == "GetHaveChildrenDetails")
            {
                HttpContext.Current.Response.ContentType = "application/json";
                context.Response.Write(GetHaveChildrenDetails(context));
            }


            if (context.Request.QueryString["method"].ToString() == "GetSmokeDetails")
            {
                HttpContext.Current.Response.ContentType = "application/json";
                context.Response.Write(GetSmokeDetails(context));
            }


            if (context.Request.QueryString["method"].ToString() == "GetHoroscopeDetails")
            {
                HttpContext.Current.Response.ContentType = "application/json";
                context.Response.Write(GetHoroscopeDetails(context));
            }


            if (context.Request.QueryString["method"].ToString() == "GetBodyTypeDetails")
            {
                HttpContext.Current.Response.ContentType = "application/json";
                context.Response.Write(GetBodyTypeDetails(context));
            }


            if (context.Request.QueryString["method"].ToString() == "GetUsersBasedOnSearch")
            {
                HttpContext.Current.Response.ContentType = "application/json";
                context.Response.Write(GetUsersBasedOnSearch(context));
            }


            if (context.Request.QueryString["method"].ToString() == "GetAllUsers")
            {
                HttpContext.Current.Response.ContentType = "application/json";
                context.Response.Write(GetAllUsers(context));
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
        /// The SearchUser method. The method must returns users
        /// </summary>
        /// <returns>Json string Object</returns>
        private string SearchUser(HttpContext context)
        {
            try
            {
                string _SearchKey = context.Request.QueryString["q"].ToString();
                User[] _res = new User().GetAllUsersByNameSearch(_SearchKey);
                string _return = JsonConvert.SerializeObject(_res);
                return _return;
            }
            catch (Exception ex)
            {
                new Error().LogError(ex, "MessageMgt", "SearchUser");
                return "";
            }
        }



        /// <summary>
        /// The SearchUser method. The method must returns users
        /// </summary>
        /// <returns>Json string Object</returns>
        private string GetAllUsers(HttpContext context)
        {
            try
            {
                User[] _res = new User().GetAllActiveUsers();
                string _return = JsonConvert.SerializeObject(_res);
                return _return;
            }
            catch (Exception ex)
            {
                new Error().LogError(ex, "MessageMgt", "GetAllUsers");
                return "";
            }
        }



        /// <summary>
        /// The SendMessage method. The method must returns true
        /// </summary>
        /// <returns>Json string Object</returns>
        [ValidateInput(false)]
        private string SendMessage(HttpContext context)
        {
            try
            {
              //  int SenderRefID, int RecipientRefID, string MessageText
               
                int  _SenderRefID =Convert.ToInt32(context.Request.Form["AdminId"].ToString());
                string[] _RecipientRefIDs = context.Request.Form["RecipientId"].ToString().Split(',');
                string _Message = context.Request.Form["Message"].ToString();
                string _Subject = context.Request.Form["Subject"].ToString();

                string _return=string.Empty;

                AdminMasterMessage MstObj = new AdminMasterMessage().AddAdminMasterMessage(_Subject, _Message);


                foreach(string id in _RecipientRefIDs)
                {
                    AdminMessages _res = new AdminMessages().SendAdminMessage(_SenderRefID, Convert.ToInt32(id), MstObj.MessageID);
                    _return = JsonConvert.SerializeObject(_res);
                }
              
                return _return;
            }
            catch (Exception ex)
            {
                new Error().LogError(ex, "MessageMgt", "SearchUser");
                return "";
            }
        }



        private string SendMessageToAllUsers(HttpContext context)
        {
            try
            {
              //  int SenderRefID, int RecipientRefID, string MessageText
                int  _SenderRefID =Convert.ToInt32(context.Request.Form["AdminId"].ToString());
                User[] allUsers = new User().GetAllActiveUsers();
                string _Message = context.Request.Form["Message"].ToString();
                string _Subject = context.Request.Form["Subject"].ToString();
                 string _return=string.Empty;
                 AdminMasterMessage MstObj = new AdminMasterMessage().AddAdminMasterMessage(_Subject, _Message);
                int userid= MstObj.MessageID;
                 foreach (User NewUser in allUsers)
                {
                    AdminMessages _res = new AdminMessages().SendAdminMessage(_SenderRefID, NewUser._RefID, userid);
                    _return = JsonConvert.SerializeObject(_res);
                }
              
                return _return;
            }
            catch (Exception ex)
            {
                new Error().LogError(ex, "MessageMgt", "SendMessageToAllUsers");
                return "";
            }
        }



        /// <summary>
        /// The get locations . The method must returns all locations in db
        /// </summary>
        /// <returns>Json string Object</returns>
        private string GetLocations(HttpContext context)
        {
            try
            {
                Location[] _res = new Location().GetLocations();
                string _return = JsonConvert.SerializeObject(_res);
                return _return;
            }
            catch (Exception ex)
            {
                new Error().LogError(ex, "MessageMgt", "GetLocations");
                return "";
            }
        }
        


         /// <summary>
        /// The get Ethnicity . The method must returns all Ethnicities in db
        /// </summary>
        /// <returns>Json string Object</returns>
        private string GetEthnicity(HttpContext context)
        {
            try
            {
                
                Ethnicity[] _res = new Ethnicity().GetEthnicity();
                string _return = JsonConvert.SerializeObject(_res);
                return _return;
            }
            catch (Exception ex)
            {
                new Error().LogError(ex, "MessageMgt", "GetEthnicity");
                return "";
            }
        }
        
        
        
        /// <summary>
        /// The GetEducationDetails. The method must returns all EducationDetails in db
        /// </summary>
        /// <returns>Json string Object</returns>
        private string GetEducationDetails(HttpContext context)
        {
            try
            {
                Education[] _res = new Education().GetEducationDetails();
                string _return = JsonConvert.SerializeObject(_res);
                return _return;
            }
            catch (Exception ex)
            {
                new Error().LogError(ex, "MessageMgt", "GetEducationDetails");
                return "";
            }
        }
       
        
        
        /// <summary>
        /// The  Get DrinkDetails . The method must returns all DrinkDetails in db
        /// </summary>
        /// <returns>Json string Object</returns>
        private string GetDrinkDetails(HttpContext context)
        {
            try
            {
                Drink[] _res = new Drink().GetDrinkDetails();
                string _return = JsonConvert.SerializeObject(_res);
                return _return;
            }
            catch (Exception ex)
            {
                new Error().LogError(ex, "MessageMgt", "GetDrinkDetails");
                return "";
            }
        }
       
        
        
        /// <summary>
        /// The  Get Religion . The method must returns all Religions in db
        /// </summary>
        /// <returns>Json string Object</returns>
        private string GetReligion(HttpContext context)
        {
            try
            {
                Religion[] _res = new Religion().GetReligion();
                string _return = JsonConvert.SerializeObject(_res);
                return _return;
            }
            catch (Exception ex)
            {
                new Error().LogError(ex, "MessageMgt", "GetReligion");
                return "";
            }
        }
       
        
        
        /// <summary>
        /// The  Get HaveChildrenDetails . The method must returns all HaveChildrenDetails in db
        /// </summary>
        /// <returns>Json string Object</returns>
        private string GetHaveChildrenDetails(HttpContext context)
        {
            try
            {
                HaveChildren[] _res = new HaveChildren().GetHaveChildrenDetails();
                string _return = JsonConvert.SerializeObject(_res);
                return _return;
            }
            catch (Exception ex)
            {
                new Error().LogError(ex, "MessageMgt", "GetHaveChildrenDetails");
                return "";
            }
        }
        
        
        
        /// <summary>
        /// The  Get SmokeDetails . The method must returns all SmokeDetails in db
        /// </summary>
        /// <returns>Json string Object</returns>
        private string GetSmokeDetails(HttpContext context)
        {
            try
            {
                Smoke[] _res = new Smoke().GetSmokeDetails();
                string _return = JsonConvert.SerializeObject(_res);
                return _return;
            }
            catch (Exception ex)
            {
                new Error().LogError(ex, "MessageMgt", "GetSmokeDetails");
                return "";
            }
        }
       
        
        
        /// <summary>
        /// The  Get BodyTypeDetails . The method must returns all BodyTypeDetails in db
        /// </summary>
        /// <returns>Json string Object</returns>
        private string GetBodyTypeDetails(HttpContext context)
        {
            try
            {
                BodyType[] _res = new BodyType().GetBodyTypeDetails();
                string _return = JsonConvert.SerializeObject(_res);
                return _return;
            }
            catch (Exception ex)
            {
                new Error().LogError(ex, "MessageMgt", "GetBodyTypeDetails");
                return "";
            }
        }
       
        
        
        /// <summary>
        /// The  Get HoroscopeDetails . The method must returns all HoroscopeDetails in db
        /// </summary>
        /// <returns>Json string Object</returns>
        private string GetHoroscopeDetails(HttpContext context)
        {
            try
            {
                Horoscope[] _res = new Horoscope().GetHoroscopeDetails();
                string _return = JsonConvert.SerializeObject(_res);
                return _return;
            }
            catch (Exception ex)
            {
                new Error().LogError(ex, "MessageMgt", "GetHoroscopeDetails");
                return "";
            }
        }




        /// <summary>
        /// The  Get Users based on user class. The method must returns all HoroscopeDetails in db
        /// </summary>
        /// <returns>Json string Object</returns>
        private string GetUsersBasedOnSearch(HttpContext context)
        {
            try
            {
                string jsonObject = context.Request.Form["SearchData"].ToString();
                List<int> _res = new User().GetUsersBasedOnSearch(jsonObject);
                string _return = JsonConvert.SerializeObject(_res);
                return _return;
            }
            catch (Exception ex)
            {
                new Error().LogError(ex, "MessageMgt", "GetUsersBasedOnAge");
                return "";
            }
        }


       
    }
}