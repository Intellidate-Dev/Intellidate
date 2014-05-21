using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using IntellidateLib;

namespace IntellidateUser.API
{
    public class ComposeBoxController : ApiController
    {
        public bool Post([FromBody]ComposeBox _ObjCompose)
        {
            try
             {
                 bool m_Res = false;
                //send message to mongo db
                if (_ObjCompose.MethodName == "SMM")
                {
                    Message _SendMsg = new Message().SendMessage(_ObjCompose.SenderId, _ObjCompose.RecipientId, _ObjCompose.MessageText);
                    if (_SendMsg != null)
                    {
                        m_Res= true;
                        //if message is sent then delete redis data 
                        string m_RMsgKey = _ObjCompose.SenderId + "-" + _ObjCompose.RecipientId;
                        new RedisMessage().DeleteRedisDraftMessage(m_RMsgKey);
                        
                    }
                    else
                    {
                        m_Res= false;
                    }
                }

                //validate user able to send message to recipeant.
                if (_ObjCompose.MethodName == "VSM")
                {
                    m_Res = new Message().IsUserAbleToSendMessage(_ObjCompose.SenderId, _ObjCompose.RecipientId);
                }
                //save message to redis db
                if (_ObjCompose.MethodName == "SMR")
                {
                    RedisMessage _RedisMessage=new RedisMessage();
                    _RedisMessage.UserID = _ObjCompose.SenderId;
                    _RedisMessage.OtherUserID = _ObjCompose.RecipientId;
                    _RedisMessage.MessageText = _ObjCompose.MessageText;
                    m_Res = _RedisMessage.SaveRedisDraftMessage(_RedisMessage);
                }
                //delete draft message if discard is clicked
                if (_ObjCompose.MethodName == "DSM")
                {
                    RedisMessage _RedisMessage = new RedisMessage();
                    string m_RMsgKey = _ObjCompose.SenderId + "-" + _ObjCompose.RecipientId;
                    long m_Result = _RedisMessage.DeleteRedisDraftMessage(m_RMsgKey);
                    if (m_Result != null || m_Result > 0)
                    {
                        m_Res = true;
                    }
                }
                return m_Res;
            }
            catch (Exception exception)
            {
                new Error().LogError(exception, "ComposeBoxController", "Post");
                return false;
            }         
        }

        

    }

    public class ComposeBox
    { 
        public int SenderId { get; set; }
        public int RecipientId { get; set; }
        public string MessageText { get; set; }
        public string MethodName { get; set; }
    }
}
