using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using IntellidateLib;

namespace IntellidateUser.API
{
    public class UserNameCheckController : ApiController
    {
        // POST api/UserNameCheck
        public bool Post([FromBody]SimpleModel Model)
        {
            try
            {
                return new User().CheckUserName(Model.value.ToString());
            }
            catch (Exception)
            {
                return false;
            }
        }


    }


    public class SimpleModel
    {
        public string value { get; set; }
    }
}
