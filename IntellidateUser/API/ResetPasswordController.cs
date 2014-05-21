using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using IntellidateLib;
using System.Web;

namespace IntellidateUser.API
{
    public class ResetPasswordController : ApiController
    {
        public bool Post([FromBody]ResetPassword _ResetPassword)
        {
            int userid = Convert.ToInt32(HttpContext.Current.Response.Cookies[0]);
            return new User().ChangeUserPassword(userid, _ResetPassword.Password);
        }
    }
    public class ResetPassword
    {
        public string Password { get; set; }
    }

}
