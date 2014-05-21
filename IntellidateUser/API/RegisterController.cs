using IntellidateLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace IntellidateUser.API
{
    public class RegisterController : ApiController
    {

        public bool Post([FromBody]RegisterCheckModel CheckModel)
        {
            if (CheckModel.type == "uname")
            {
                return new User().CheckUserName(CheckModel.value.ToString());
            }
            else
            {
                return new User().CheckEmailAddress(CheckModel.value.ToString());
            }
        }

    }
    public class RegisterCheckModel
    {
        public string value { get; set; }
        public string type { get; set; }
    }
}
