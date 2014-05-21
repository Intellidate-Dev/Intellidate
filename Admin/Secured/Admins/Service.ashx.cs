using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace AdminModule.Secured.Admins
{
    /// <summary>
    /// The Admin Service Handler
    /// </summary>
    public class Service : IHttpHandler
    {
        private const string BlankJson = "[]";

        /// <summary>
        /// The main method for processing the incoming request from ajax
        /// </summary>
        /// <param name="context">HttpContext: The request data along with the User & Form variables</param>
        public void ProcessRequest(HttpContext context)
        {
            string _MethodOutput = "";
            string _MethodCall = "";

            // Check if there is method name
            if (context.Request.QueryString["f"] != null)
            {
                // Assign the method call
                _MethodCall = context.Request.QueryString["f"].ToString();
            }

            switch (_MethodCall)
            {
                case "A":
                    {
                        _MethodOutput = ManageAdminDetails(context);
                        break;
                    }
                case "G":
                    {
                        _MethodOutput = GetAllAdminUsers(context);
                        break;
                    }
                case "D":
                    {
                        _MethodOutput = DeleteAdmin(context);
                        break;
                    }
                case "":
                    {
                        // Return the blank JSON string
                        _MethodOutput = BlankJson;
                        break;
                    }
            }

            context.Response.ContentType = "text/json";
            context.Response.Write(_MethodOutput);
        }

        /// <summary>
        /// Private method for getting all admin users
        /// </summary>
        /// <returns>string</returns>
        private string GetAllAdminUsers(HttpContext Context)
        {
            try
            {
                // Call the method
                IntellidateLib.Admin[] _AllAdminUsers = new IntellidateLib.Admin().GetAllAdminUsers();

                // Must omit the existing Admin's details.
                string _CurrentAdminID = Context.User.Identity.Name.ToString();
                if (_CurrentAdminID.ToUpper() != "SUPERADMIN")
                {
                    // Get the existing Admin's ID
                    _AllAdminUsers = _AllAdminUsers.Where(x => x.AdminRefId.ToString() != _CurrentAdminID).ToArray();
                }

                // Return the converted Json string
                return JsonConvert.SerializeObject(_AllAdminUsers);
            }
            catch (Exception)
            {
                // Return a blank Json string
                return BlankJson;
            }
        }

        /// <summary>
        /// Private method for deleting the admin
        /// </summary>
        /// <param name="context">HttpContext</param>
        /// <returns>string</returns>
        private string DeleteAdmin(HttpContext context)
        {
            try
            {
                // Get the admin ID from Form data
                int _AdminID = Convert.ToInt32(context.Request.Form["AdminID"].ToString());

                // Call the method
                bool _Return = new IntellidateLib.Admin().ActivateOrDeActivateAdmin(_AdminID, "I");

                // Return the converted Json string
                return JsonConvert.SerializeObject(_Return);
            }
            catch (Exception)
            {
                // Return the blank Json string
                return BlankJson;
            }
            
        }

        /// <summary>
        /// This method is to Add/Edit the Admin details.
        /// </summary>
        /// <param name="context">HttpContext: The form data from ajax</param>
        /// <returns>JSON string</returns>
        private string ManageAdminDetails(HttpContext context)
        {
            try
            {
                // Retrieve the form data
                string _LoginName = context.Request.Form["LoginName"].ToString();
                string _AdminName = context.Request.Form["AdminName"].ToString();
                string _AdminPassword = context.Request.Form["AdminPassword"].ToString();
                string _AdminType = context.Request.Form["AdminType"].ToString();
                string _EmailID = context.Request.Form["EmailID"].ToString();
                string[] _AdminPrivileges = context.Request.Form["AdminPrivileges"].ToString().Split(',');
                string[] _ForumPrivileges = context.Request.Form["Forums"].ToString().Split(',');

                // Check if the admin ID is existing
                int _AdminID = (context.Request.Form["AdminID"] == "") ? 0 : Convert.ToInt32(context.Request.Form["AdminID"].ToString());

                // If the admin ID is available, edit the admin details
                if (_AdminID != 0)
                {
                    // Call the method
                    var _Ret = new IntellidateLib.Admin().EditAminUser(_AdminID, _AdminName, _AdminPassword, _AdminType, _LoginName, _EmailID, _AdminPrivileges, _ForumPrivileges);

                    // Return the JSON string
                    return JsonConvert.SerializeObject(_Ret);
                }
                else
                {
                    // Call the add method
                    var _Ret = new IntellidateLib.Admin().AddNewAdminUser(_AdminName, _AdminPassword, _AdminType, _LoginName, _EmailID, _AdminPrivileges, _ForumPrivileges);

                    // Return the JSON string
                    return JsonConvert.SerializeObject(_Ret);
                }
            }
            catch (Exception)
            {
                // Return the blank JSON string
                return BlankJson;
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