using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IntellidateLib;
using Newtonsoft.Json;
namespace AdminModule.Secured.Users
{
    /// <summary>
    /// Summary description for UserMgt
    /// </summary>
    public class UserMgt : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            HttpResponse response = context.Response;
            response.ContentType = "text/json";

            //check the all method requests
            if (context.Request.QueryString["method"].ToString() == "AddNewUser")
            {
                HttpContext.Current.Response.ContentType = "application/json";
                context.Response.Write(AddNewUser(context));
            }


            if (context.Request.QueryString["method"].ToString() == "GetAllUsers")
            {
                HttpContext.Current.Response.ContentType = "application/json";
                context.Response.Write(GetAllUsers(context));
            }


            if (context.Request.QueryString["method"].ToString() == "GetAllDeActivatedUsers")
            {
                HttpContext.Current.Response.ContentType = "application/json";
                context.Response.Write(GetAllDeActivatedUsers(context));
            }


            if (context.Request.QueryString["method"].ToString() == "DeleteUser")
            {
                HttpContext.Current.Response.ContentType = "application/json";
                context.Response.Write(DeleteUser(context));
            }


            if (context.Request.QueryString["method"].ToString() == "ActivateUser")
            {
                HttpContext.Current.Response.ContentType = "application/json";
                context.Response.Write(ActivateUser(context));
            }


            if (context.Request.QueryString["method"].ToString() == "SearchUser")
            {
                HttpContext.Current.Response.ContentType = "application/json";
                context.Response.Write(SearchUser(context));
            }


            if (context.Request.QueryString["method"].ToString() == "SearchRecentlyAddedUser")
            {
                HttpContext.Current.Response.ContentType = "application/json";
                context.Response.Write(SearchRecentlyAddedUser(context));
            }


            if (context.Request.QueryString["method"].ToString() == "EditUserDetails")
            {
                HttpContext.Current.Response.ContentType = "application/json";
                context.Response.Write(EditUserDetails(context));
            }


            if (context.Request.QueryString["method"].ToString() == "GetNextScrollDown")
            {
                HttpContext.Current.Response.ContentType = "application/json";
                context.Response.Write(GetNextScrollDown(context));
            }


            if (context.Request.QueryString["method"].ToString() == "CheckEmailAddress")
            {
                HttpContext.Current.Response.ContentType = "application/json";
                context.Response.Write(CheckEmailAddress(context));
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
        private string AddNewUser(HttpContext context)
        {
            try
            {
                //call Add New User method from User class
                string _loginName = context.Request.Form["LoginName"].ToString();
                string _fullName = context.Request.Form["FullName"].ToString();
                string _emailAddress = context.Request.Form["EmailAddress"].ToString();
                string _password = context.Request.Form["Password"].ToString();
                int _gender = Convert.ToInt32(context.Request.Form["Gender"].ToString());
                DateTime _dob = new DateTime(Convert.ToInt32(context.Request.Form["Dob"].ToString().Split('/')[2]), Convert.ToInt32(context.Request.Form["Dob"].ToString().Split('/')[0]), Convert.ToInt32(context.Request.Form["Dob"].ToString().Split('/')[1]));
                User _res = new User().AddNewUser(_loginName, _fullName, _emailAddress, _password, _gender, _dob);
                string _return = JsonConvert.SerializeObject(_res);
                return _return;
            }
            catch (Exception ex)
            {
                new Error().LogError(ex, "UserHandler", "AddNewUser");
                return "";
            }
        }



        /// <summary>
        /// The GetAllUsers method. The method must returns no of users
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
                new Error().LogError(ex, "UserHandler", "GetAllUsers");
                return "";
            }
        }



        /// <summary>
        /// The GetAllDeActivatedUsers method. The method must returns deactivated users
        /// </summary>
        /// <returns>Json string Object</returns>
        private string GetAllDeActivatedUsers(HttpContext context)
        {
            try
            {
                User[] _res = new User().GetAllDeActiveUsers();

                string _return = JsonConvert.SerializeObject(_res);
                return _return;
            }
            catch (Exception ex)
            {
                new Error().LogError(ex, "UserHandler", "GetAllDeActivatedUsers");
                return "";
            }
        }



        /// <summary>
        /// The DeleteUser method. The method must returns deactivate the users
        /// </summary>
        /// <returns>Json string Object</returns>
        private string DeleteUser(HttpContext context)
        {
            try
            {
                string _UserId = context.Request.Form["UserId"].ToString();
                bool _res = new User().DeleteUser(Convert.ToInt32(_UserId));
                string _return = JsonConvert.SerializeObject(_res);
                return _return;
            }
            catch (Exception ex)
            {
                new Error().LogError(ex, "UserHandler", "DeleteUser");
                return "";
            }
        }



        /// <summary>
        /// The ActivateUser method. The method must returns true to activate the user
        /// </summary>
        /// <returns>Json string Object</returns>
        private string ActivateUser(HttpContext context)
        {
            try
            {
                string _UserId = context.Request.Form["UserId"].ToString();
                bool _res = new User().ReActivateUser(Convert.ToInt32(_UserId));
                string _return = JsonConvert.SerializeObject(_res);
                return _return;
            }
            catch (Exception ex)
            {
                new Error().LogError(ex, "UserHandler", "ActivateUser");
                return "";
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
                string _SearchKey = context.Request.Form["SearchKey"].ToString();
                User[] _res = new User().GetAllUsersByNameSearch(_SearchKey);
                string _return = JsonConvert.SerializeObject(_res);
                return _return;
            }
            catch (Exception ex)
            {
                new Error().LogError(ex, "UserHandler", "SearchUser");
                return "";
            }
        }



        /// <summary>
        /// The SearchRecentlyAddedUser method. The method must returns users by mounth,week and day
        /// </summary>
        /// <returns>Json string Object</returns>
        private string SearchRecentlyAddedUser(HttpContext context)
        {
            try
            {
                string _return = string.Empty;
                string SearchKey = context.Request.Form["SearchKey"].ToString();
                if (SearchKey == "1")
                {
                    User[] _res = new User().GetThisMonthUsers();
                    _return = JsonConvert.SerializeObject(_res);
                }
                if (SearchKey == "2")
                {
                    User[] _res = new User().GetThisWeekUsers();
                    _return = JsonConvert.SerializeObject(_res);
                }
                if (SearchKey == "3")
                {
                    User[] _res = new User().GetThisDayUsers();
                    _return = JsonConvert.SerializeObject(_res);
                }
                return _return;
            }
            catch (Exception ex)
            {
                new Error().LogError(ex, "UserHandler", "SearchUser");
                return "";
            }
        }



        /// <summary>
        /// The EditUserDetails method. The method must returns true for update users
        /// </summary>
        /// <returns>Json string Object</returns>
        private string EditUserDetails(HttpContext context)
        {
            try
            {
                //call Add New User method from User class
                int _userId = Convert.ToInt32(context.Request.Form["UserId"].ToString());
                string _loginName = context.Request.Form["LoginName"].ToString();
                string _fullName = context.Request.Form["FullName"].ToString();
                string _emailAddress = context.Request.Form["EmailAddress"].ToString();
                string _password = context.Request.Form["Password"].ToString();
                int _gender = Convert.ToInt32(context.Request.Form["Gender"].ToString());
                DateTime _dob = new DateTime(Convert.ToInt32(context.Request.Form["Dob"].ToString().Split('/')[2]), Convert.ToInt32(context.Request.Form["Dob"].ToString().Split('/')[0]), Convert.ToInt32(context.Request.Form["Dob"].ToString().Split('/')[1]));
                User _res = new User().EditUserDetails(_userId, _loginName, _fullName, _emailAddress, _password, _gender, _dob);
                string _return = JsonConvert.SerializeObject(_res);
                return _return;
            }
            catch (Exception ex)
            {
                new Error().LogError(ex, "UserHandler", "AddNewUser");
                return "";
            }
        }



        /// <summary>
        /// The GetNextScrollDown method. The method must returns users while scroll down 
        /// </summary>
        /// <returns>Json string Object</returns>
        private string GetNextScrollDown(HttpContext context)
        {
            try
            {
                string _userIds = context.Request.Form["UserId"].ToString();
                int[] RefIds = Array.ConvertAll(_userIds.Split(','), s => int.Parse(s));
                int _count = Convert.ToInt32(context.Request.Form["Count"].ToString());
                string _searchKey = context.Request.Form["Key"].ToString();
                User[] _res = new User().GetNextScrollDown(_count, _searchKey, RefIds);

                string _return = JsonConvert.SerializeObject(_res);
                return _return;
            }
            catch (Exception ex)
            {
                new Error().LogError(ex, "UserHandler", "GetAllUsers");
                return "";
            }
        }



        /// <summary>
        /// The CheckEmailAddress method. The method must returns true,false for check the existing email
        /// </summary>
        /// <returns>Json string Object</returns>
        private string CheckEmailAddress(HttpContext context)
        {
            try
            {
                int _userId = Convert.ToInt32(context.Request.Form["UserId"].ToString());
                string _EmailId = context.Request.Form["EmailId"].ToString();
                string _type = context.Request.Form["Type"].ToString();
                bool _res = new User().CheckEmailAddress(_EmailId, _userId, _type);
                string _return = JsonConvert.SerializeObject(_res);
                return _return;
            }
            catch (Exception ex)
            {
                new Error().LogError(ex, "UserHandler", "GetAllUsers");
                return "";
            }
        }



    }
}