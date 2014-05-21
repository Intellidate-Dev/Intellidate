using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using IntellidateLib;
using Newtonsoft.Json;

namespace IntellidateUser.API
{
    public class WhoSavedMeController : ApiController
    {
        public string GET([FromUri] string inputParam, [FromUri] string ControlName)
        {
            try
            {
                if(ControlName=="WhoViewed")
                {
                    WhoSavedMe[] whoSaveMeArrayObj = new ProfileView().GetWhoViewedMe(Convert.ToInt32(inputParam.ToString())).ToArray();
                    return JsonConvert.SerializeObject(whoSaveMeArrayObj);
                }
                else if (ControlName == "WhoSaved")
                {
                    WhoSavedMe[] whoSaveMeArrayObj = new ProfileSave().WhoSavedMyProfile(Convert.ToInt32(inputParam.ToString())).ToArray();
                    return JsonConvert.SerializeObject(whoSaveMeArrayObj);
                }
                else if (ControlName == "MySaved")
                {
                    WhoSavedMe[] whoSaveMeArrayObj = new ProfileSave().GetSavedProfiles(Convert.ToInt32(inputParam.ToString())).ToArray();
                    return JsonConvert.SerializeObject(whoSaveMeArrayObj);
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
        public class WhoSaved
        {
            public string UserID { get; set; }

        }

    }
}