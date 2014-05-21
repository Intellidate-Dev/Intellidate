using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using IntellidateLib;

namespace IntellidateUser.API
{
    public class ForgotPasswordController : ApiController
    {
        public bool Post([FromBody]ForgotPassword _ForgotPassword)
        {
            string _Password = new User().GetPassword(_ForgotPassword.EmailAddress);
            if (_Password == "")
            {
                return false;
            }
            else
            {
                return true;
            }
            
        }
    }

    public class ForgotPassword
    {
        public string EmailAddress { get; set; }
    }
}
