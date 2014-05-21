using IntellidateLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace IntellidateUser.API
{
    public class EmailCheckController : ApiController
    {
        // POST api/EmeailCheck
        public bool Post([FromBody]string value)
        {
            try
            {
                return new User().CheckEmailAddress(value,0,"");
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
