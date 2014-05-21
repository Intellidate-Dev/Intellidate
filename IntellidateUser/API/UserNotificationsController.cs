using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using IntellidateLib;

namespace IntellidateUser.API
{
    public class UserNotificationsController : ApiController
    {
        public int Post([FromBody]UserNotifications _ObjNotification)
        {
            try
            {
                //get unread messages count this will talck to signalR
                if (_ObjNotification.MethodName == "GNM")
                {
                    return new Message().GetUserUnReadMessages(_ObjNotification.UserId).Count();
                }
                else
                {
                    
                    return 0;
                }

            }
            catch (Exception ex)
            {

                return 0;
            }
          
        }

        public IHttpActionResult Get(int UserId)
        {
            try
            {
                var Res= new UserNotification().GetNotification(UserId);
                if (Res == null)
                {
                    return NotFound();
                }
                return Ok(Res);
            }
            catch (Exception)
            {
                return null;
            }
        }

    }
    public class UserNotifications
    {
        public string MethodName { get; set; }
        public int UserId { get; set; }

    }
}
