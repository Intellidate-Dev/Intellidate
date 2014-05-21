using IntellidateLib;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace IntellidateUser.API
{
    public class UserDetailsController : ApiController
    {
        public string GET([FromUri] string inputParam, [FromUri] string ControlName)
        {
            try
            {
                if (ControlName == "UserDetails")
                {
                    UserDetails userDetArrObj = new UserDetails().GetUserDetails(inputParam);
                    return JsonConvert.SerializeObject(userDetArrObj);
                }
                else
                {
                    return "";
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
