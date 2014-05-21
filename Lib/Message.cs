using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Bson;
using MySql.Data;
using MongoDB.Driver.Builders;
using MongoDB.Driver.GridFS;
using MongoDB.Driver.Linq;
using System.Configuration;
using IntellidateLib.DB;
using System.Globalization;
using MongoDB.Bson.Serialization.Attributes;
using System.Transactions;
using ServiceStack.Redis;

namespace IntellidateLib
{
    /// <summary>
    /// The messages class for all the online/offline messages
    /// </summary>
    public class Message
    {

        /// <summary>
        /// The Message class identifier for caching db
        /// </summary>
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }

        /// <summary>
        /// The Message ID from the collection or MySQL Database if any..
        /// </summary>
        public int MessageRefID { get; set; }

        /// <summary>
        /// The User ID from the collection or MySQL Database if any..
        /// </summary>
        public int SenderRefID { get; set; }

        /// <summary>
        /// The Sender property from the collection or MySQL Database if any.. it returns the user information
        /// </summary>
        public User Sender { 
            get{
                return new User().GetUserDetails(SenderRefID);
            }
        }

        /// <summary>
        /// The RecipientRefID from the collection or MySQL Database if any.. the reciver user id
        /// </summary>
        public int RecipientRefID { get; set; }

        /// <summary>
        /// The Recipient property from the collection or MySQL Database if any..it returns the reciver user information
        /// </summary>
        public User Recipient { 
            get{
                return new User().GetUserDetails(RecipientRefID);
            }
        }

        /// <summary>
        /// The Message Text from the collection or MySQL Database if any.. the content of the message
        /// </summary>
        public string MessageText { get; set; }

        /// <summary>
        /// The SentTime from the collection or MySQL Database if any.. the sender message sent time
        /// </summary>
        public DateTime? SentTime { get; set; }

        /// <summary>
        /// The IsMessageViewed from the collection or MySQL Database if any.. the reciver viewed or not viewed=true,not viewed=false
        /// </summary>
        public bool IsMessageViewed { get; set; }

        /// <summary>
        /// The MessageViewedTime from the collection or MySQL Database if any.. the reciver viewed time
        /// </summary>
        public DateTime? MessageViewedTime { get; set; }

        /// <summary>
        /// 
        /// The IsMessageEdited from the collection or MySQL Database if any.. the user message is edited or not. Edited=true,Not Edited=false
        /// </summary>
        public bool IsEdited { get; set; }

        /// <summary>
        /// The MessageLastEditedTime from the collection or MySQL Database if any.. the user message last edited time
        /// </summary>
        public DateTime? LastEdited { get; set; }

        /// <summary>
        /// The MessageDeletedBySender from the collection or MySQL Database if any.. the message is deleted by sender. Deleted=true,Not Deleted=false
        /// </summary>
        public bool MessageDeletedBySender { get; set; }

        /// <summary>
        /// The MessageDeletedByRecipient from the collection or MySQL Database if any.. the message is deleted by Recipient. Deleted=true,Not Deleted=false
        /// </summary>
        public bool MessageDeletedByRecipient { get; set; }

        /// <summary>
        /// The Status from the collection or MySQL Database if any.. 
        /// this message can be sent to sender the status will set to "S" or status is "U"
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// The IsRejectedByAdmin from the collection or MySQL Database if any.. the message is deleted by Recipient. Deleted=true,Not Deleted=false
        /// </summary>
        public bool IsRejectedByAdmin { get; set; }

        public UserMessageReport[] GetUserMessageReport
        {
            get { return new UserMessageReport().GetUserMessageReport(MessageRefID); }
        }

        public string[] KeyWords { get; set; }

        /// <summary>
        ///  The Send Message. The method must insert into both MySQL cache the date into MongoDB
        /// </summary>
        /// <param name="senderRefId">The LoginID of the user</param>
        /// <param name="recipientRefId">The LoginID of the another user(recipient) </param>
        /// <param name="messageText">The Sender MessageText   </param>
        /// <returns>Message class</returns>
        public Message SendMessage(int senderRefId, int recipientRefId, string messageText)
        {
            try
            {
              bool IsReply = IsUserAbleToSendMessage(senderRefId, recipientRefId);
              Message _ResObj = new Message();
              if (IsReply)
                {
                    int m_MessageRefId = 0;
                    MongoDatabase _MongoDB = CachingDbConnector.GetCachingDatabase();
                    var _MongoCollection = _MongoDB.GetCollection<User>(Constants.messageClass);
                    // Insert the record into the MainDB
                    using (intellidatev2Entities mainDB = new intellidatev2Entities())
                    {
                        using (var transaction = new TransactionScope())
                        {
                            in_message_trn trnMessageObj = new in_message_trn
                            {
                                SenderID = senderRefId,
                                RecipientID = recipientRefId,
                                MessageText = messageText,
                                IsMessageViewed = false,
                                IsEdited = false,
                                MessageDeletedBySender = false,
                                MessageDeletedByRecipient = false,
                                Status = Constants.successStatus,
                                SentTime = DateTime.Now.ToUniversalTime(),
                                IsRejected = false,
                            };
                            mainDB.in_message_trn.Add(trnMessageObj);
                            mainDB.SaveChanges();
                            m_MessageRefId = trnMessageObj.messageID;
                            try
                            {
                                Message cachingObject = new Message
                                {
                                    MessageRefID = m_MessageRefId,
                                    SenderRefID = senderRefId,
                                    RecipientRefID = recipientRefId,
                                    MessageText = messageText,
                                    SentTime = DateTime.Now.ToUniversalTime(),
                                    IsMessageViewed = false,
                                    IsEdited = false,
                                    LastEdited = null,
                                    MessageViewedTime = null,
                                    MessageDeletedBySender = false,
                                    MessageDeletedByRecipient = false,
                                    Status = Constants.successStatus,
                                    IsRejectedByAdmin = false,
                                    KeyWords = TextToWords.ConvertTextToWords(messageText),
                                };
                                //insert the record into cache db
                                MongoDatabase cachingDataBase = CachingDbConnector.GetCachingDatabase();
                                var m_Collection = cachingDataBase.GetCollection<User>(Constants.messageClass);
                                m_Collection.Save(cachingObject);
                                transaction.Complete();
                                _ResObj = cachingObject;
                              
                                MessageReply _MessageReplySR = new MessageReply().GetMessageReplyById(senderRefId, recipientRefId);
                                if (_MessageReplySR != null)
                                {
                                    if (_MessageReplySR.IsReply)
                                    {
                                        new MessageReply().UpdateMessageReply(recipientRefId, senderRefId, _MessageReplySR.IsReply);
                                    }
                                }
                            }
                            catch (Exception exception)
                            {
                                new Error().LogError(exception, Constants.messageClass, Constants.sendMessageMethod);
                                _ResObj = null;
                            }

                        }
                    }
                    return _ResObj;
                }
              else
              {
                  return null;
              }
             
            }
            catch (Exception ex)
            {
                new Error().LogError(ex, Constants.messageClass, Constants.sendMessageMethod);
                return null;
            }
       
        }


        //check user able to send secound message
        public bool IsUserAbleToSendMessage(int senderRefId, int recipientRefId)
        {
            try
            {
                bool IsReply = false;
                MessageReply _MessageReplySR = new MessageReply().GetMessageReplyById(senderRefId, recipientRefId);
               if (_MessageReplySR == null)
                {

                    bool m_sender = new MessageReply().AddMessageReply(senderRefId, recipientRefId, false);
                    bool m_recipient = new MessageReply().AddMessageReply(recipientRefId, senderRefId, true);
                    if (m_sender == true && m_recipient == true)
                    {
                        IsReply = true;
                    }
                    else
                    {
                        IsReply = false;
                    }
                }
                if (_MessageReplySR !=null)
                {
                    if (_MessageReplySR.IsReply)
                    {
                        IsReply = _MessageReplySR.IsReply;
                    }       
                }
                return IsReply;
            }
            catch (Exception ex)
            {
                new Error().LogError(ex, Constants.messageClass, "IsUserAbleToSendMessage");
                return false;
            }
        }



        /// <summary>
        ///  The GetConversation. The method must gets the conversation of two users from cache db
        /// </summary>
        /// <param name="userRefId">The LoginID of the user</param>
        /// <param name="otherUserRefId">The LoginID of the another user(recipient) </param>
        /// <returns>Message class array</returns>
        public Message[] GetConversation(int userRefId, int otherUserRefId)
        {
            try
            {
                //get connection string from cache db
                MongoDatabase cachingDataBase = CachingDbConnector.GetCachingDatabase();
                var m_Query = Query<Message>.Where(x=> (x.SenderRefID==userRefId && x.RecipientRefID==otherUserRefId) || (x.SenderRefID==otherUserRefId && x.RecipientRefID==userRefId));
                var m_Collection = cachingDataBase.GetCollection<Message>(Constants.messageClass);
               //pass query to collection object
                var m_Conversation = m_Collection.Find(m_Query);
                return m_Conversation.ToArray();
            }
            catch (Exception ex)
            {
                new Error().LogError(ex, Constants.messageClass, Constants.getConversationMethod);
                return null;
            }
        }


        /// <summary>
        ///  The Get current user unread/not seen messages
        /// </summary>
        /// <param name="userRefId">The LoginID of the user</param>
        /// <returns>Message class array</returns>
        public Message[] GetUserUnReadMessages(int userRefId)
        {
            try
            {
                //get connection string from cache db
                MongoDatabase cachingDataBase = CachingDbConnector.GetCachingDatabase();
                var m_Query = Query<Message>.Where(x => (x.SenderRefID == userRefId && x.IsMessageViewed==false));
                var m_Collection = cachingDataBase.GetCollection<Message>(Constants.messageClass);
                //pass query to collection object
                var m_Conversation = m_Collection.Find(m_Query);
                return m_Conversation.ToArray();
            }
            catch (Exception ex)
            {
                new Error().LogError(ex, Constants.messageClass, Constants.getUserUnReadMessagesMethod);
                return null;
            }
        }


        /// <summary>
        ///  The Get current user read/seen messages
        /// </summary>
        /// <param name="userRefId">The LoginID of the user</param>
        /// <returns>Message class array</returns>
        public Message[] GetUserReadMessages(int userRefId)
        {
            try
            {
                //get connection string from cache db
                MongoDatabase cachingDataBase = CachingDbConnector.GetCachingDatabase();
                var m_Query = Query<Message>.Where(x => (x.SenderRefID == userRefId && x.IsMessageViewed == true));
                var m_Collection = cachingDataBase.GetCollection<Message>(Constants.messageClass);
                //pass query to collection object
                var m_Conversation = m_Collection.Find(m_Query);
                return m_Conversation.ToArray();
            }
            catch (Exception ex)
            {
                new Error().LogError(ex, Constants.messageClass, Constants.getUserReadMessagesMethod);
                return null;
            }
        }



        /// <summary>
        ///  The EditMessage. The method must updates the both MySQL cache the date into MongoDB
        /// </summary>
        /// <param name="messageRefId">The Message Id </param>
        /// <param name="userRefId">The LoginID of the user (sender)</param>
        /// <param name="newMessageText">The Sender newly entered Text</param>
        /// <returns>true</returns>
        public bool EditMessage(int messageRefId, int userRefId, string newMessageText)
        {
            try
            {
                //insert record into maindb
                using (intellidatev2Entities mainDB = new intellidatev2Entities())
                {
                    using (var transaction = new TransactionScope())
                    {
                        in_message_trn trnMessageObject = mainDB.in_message_trn.Where(x => x.messageID == messageRefId && x.SenderID == userRefId).FirstOrDefault();
                        trnMessageObject.IsEdited = true;
                        trnMessageObject.LastEdited = DateTime.Now.ToUniversalTime();
                        trnMessageObject.MessageText = newMessageText;
                        mainDB.SaveChanges();
                        try
                        {
                            //insert record into  caching db
                            MongoDatabase cachingDataBase = CachingDbConnector.GetCachingDatabase();
                            var m_Query = Query<Message>.Where(x => x.SenderRefID == userRefId && x.MessageRefID == messageRefId);
                            var m_Collection = cachingDataBase.GetCollection<Message>(Constants.messageClass);
                            var m_Update = Update.Set(Constants.isEdited, true)
                                                .Set(Constants.lastEdited, DateTime.Now)
                                                .Set(Constants.messageText, newMessageText);
                            var m_SortBy = SortBy.Null;
                            m_Collection.FindAndModify(m_Query, m_SortBy, m_Update);
                            transaction.Complete();
                            return true;
                        }
                        catch (Exception exception)
                        {
                            new Error().LogError(exception, Constants.messageClass, Constants.editMessageMethod);
                            return false;
                        }
                       
                    }
                }
            }
            catch (Exception ex)
            {
                new Error().LogError(ex, Constants.messageClass, Constants.editMessageMethod);
                return false;
            }
        }




        /// <summary>
        ///  The Update Read and Unread messages. The method must updates the both MySQL cache the date into MongoDB
        /// </summary>
        /// <param name="messageRefId">The Message Id </param>
        /// <param name="newMessageText">The Sender newly entered Text</param>
        /// <returns>true</returns>
        public bool UpdateReadMessage(int MessageID)
        {
            try
            {
                //insert record into maindb
                using (intellidatev2Entities mainDB = new intellidatev2Entities())
                {
                    using (var transaction = new TransactionScope())
                    {
                        in_message_trn trnMessageObject = mainDB.in_message_trn.Where(x => x.messageID == MessageID).FirstOrDefault();
                        trnMessageObject.IsMessageViewed = true;
                        mainDB.SaveChanges();
                        try
                        {
                            //insert record into  caching db
                            MongoDatabase cachingDataBase = CachingDbConnector.GetCachingDatabase();
                            var m_Query = Query<Message>.Where(x => x.MessageRefID == MessageID);
                            var m_Collection = cachingDataBase.GetCollection<Message>(Constants.messageClass);
                            var m_Update = Update.Set(Constants.isMessageViewed, true);
                            var m_SortBy = SortBy.Null;
                            m_Collection.FindAndModify(m_Query, m_SortBy, m_Update);
                            transaction.Complete();
                            return true;
                        }
                        catch (Exception exception)
                        {
                            new Error().LogError(exception, Constants.messageClass, Constants.updateReadMessageMethod);
                            return false;
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                new Error().LogError(ex, Constants.messageClass, Constants.editMessageMethod);
                return false;
            }
        }




        /// <summary>
        ///  The DeleteMessage. The method must updates the (Status) both MySQL cache the date into MongoDB
        /// </summary>
        /// <param name="messageRefId">The Message Id </param>
        /// <param name="UserRefID">The LoginID of the user (sender)</param>
        /// <returns>true</returns>
        public bool DeleteMessage(int messageRefId)
        {
            try
            {
                MongoDatabase cachingDataBase = CachingDbConnector.GetCachingDatabase();
                //Here we are not deleting the message we just updates the status of video A(Active) to I(Inactive)
                var m_Query = Query<Message>.Where(e => e.MessageRefID == messageRefId);
                var m_Collection = cachingDataBase.GetCollection<Message>(Constants.messageClass);
                var m_ExistingObject = m_Collection.FindOne(m_Query);
                if (m_ExistingObject != null)
                {
                    // If existing already in the database  Update MainDb                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                     return the existing object
                    using (intellidatev2Entities mainDB = new intellidatev2Entities())
                    {
                        using (var transaction = new TransactionScope())
                        {
                            in_message_trn trnMessageObj = mainDB.in_message_trn.SingleOrDefault(c => c.messageID == messageRefId);
                            trnMessageObj.Status = Constants.inActiveStatus;
                            mainDB.SaveChanges();
                            try
                            {
                                //Update CachingDB 
                                Message cachingObject = new Message();
                                var m_Newquery = Query<Message>.Where(e => e.MessageRefID == messageRefId);
                                var m_Update = Update.Set(Constants.status, Constants.inActiveStatus);
                                var m_SortBy = SortBy.Descending(Constants.messageRefID);
                                var m_Result = m_Collection.FindAndModify(m_Newquery, m_SortBy, m_Update, true);
                                transaction.Complete();
                                return true;
                            }
                            catch (Exception exception)
                            {
                                new Error().LogError(exception, Constants.messageClass, Constants.deleteMessageMethod);
                                return false;
                            }
                            
                        }
                    }
                }
                else
                {
                    return false;
                }


            }
            catch (Exception ex)
            {
                new Error().LogError(ex, Constants.messageClass, Constants.deleteMessageMethod);
                return false;
            }
        }




        /// <summary>
        /// Returns this mounth sent Messages as an Array
        /// </summary>
        /// <returns>Message Class Array</returns>
        public Message[] GetThisMonthSentMessages(int senderRefId)
        {

            try
            {
                //Connecting to caching db
                MongoDatabase cachingDataBase = CachingDbConnector.GetCachingDatabase();
                //Getting data from caching db using defferences between currentdate and current month start date
                var m_Query = Query<Message>.Where(x => x.SentTime >= DateTime.Today.AddDays(1 - DateTime.Today.Day) & x.SentTime <= DateTime.Now & x.SenderRefID==senderRefId);
                var m_Collection = cachingDataBase.GetCollection<Message>(Constants.messageClass);
                return m_Collection.Find(m_Query).ToArray();
            }
            catch (Exception ex)
            {
                new Error().LogError(ex, Constants.messageClass, Constants.getThisMonthSentMessagesMethod);
                return null;
            }
        }




        /// <summary>
        /// Returns this Week sent message as an Array
        /// </summary>
        /// <returns>message Class Array</returns>
        public Message[] GetThisWeekSentMessages(int senderRefId)
        {
            try
            {
                //Connecting to caching db
                MongoDatabase cachingDataBase = CachingDbConnector.GetCachingDatabase();
                //Getting Current week start date
                DateTime m_WeekStart = DateTime.Now.AddDays(CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek - DateTime.Now.DayOfWeek);
                //Getting this week sent Messages details from caching db using 'deffrences between current week start date and current date
                var m_Query = Query<Message>.Where(x => x.SentTime >= m_WeekStart & x.SentTime <= DateTime.Now & x.SenderRefID == senderRefId);
                var m_Collection = cachingDataBase.GetCollection<Message>(Constants.messageClass);
                return m_Collection.Find(m_Query).ToArray();

            }
            catch (Exception ex)
            {
                new Error().LogError(ex, Constants.messageClass, Constants.getThisWeekSentMessagesMethod);
                return null;
            }
        }




        /// <summary>
        /// Returns today sent message as an Array
        /// </summary>
        /// <returns>Messsage Class Array</returns>
        public Message[] GetThisDaySentMessages(int senderRefId)
        {
            try
            {
                //Connecting to caching db
                MongoDatabase cachingDataBase = CachingDbConnector.GetCachingDatabase();
                //Getting Todays sent message details from caching db 
                var m_Query = Query<Message>.Where(x => x.SentTime >= DateTime.Today & x.SentTime <= DateTime.Now & x.SenderRefID == senderRefId);
                var m_Collection = cachingDataBase.GetCollection<Message>(Constants.messageClass);
                return m_Collection.Find(m_Query).ToArray();
            }
            catch (Exception ex)
            {
                new Error().LogError(ex, Constants.messageClass, Constants.getThisDaySentMessagesMethod);
                return null;
            }
        }




        /// <summary>
        /// Returns this mounth reported Messages as an Array
        /// </summary>
        /// <returns>Message Class Array</returns>
        public Message[] GetThisMonthReportedMessages()
        {
            try
            {
                //Connecting to caching db
                MongoDatabase cachingDataBase = CachingDbConnector.GetCachingDatabase();
                //Getting data from caching db using defferences between currentdate and current month start date
                var m_Query = Query<UserMessageReport>.Where(x => x.TimeStamp >= DateTime.Today.AddDays(1 - DateTime.Today.Day) & x.TimeStamp <= DateTime.Now);
                var m_Collection = cachingDataBase.GetCollection<UserMessageReport>(Constants.userMessageReportClass);
                int[] m_MessageIds = m_Collection.Find(m_Query).Select(e => e.MessageId).ToArray();
                var m_NewQuery = Query<Message>.In(e => e.MessageRefID, m_MessageIds);
                var m_NewCollection = cachingDataBase.GetCollection<Message>(Constants.messageClass);
                return  m_NewCollection.Find(m_NewQuery).ToArray();
            }
            catch (Exception ex)
            {
                new Error().LogError(ex, Constants.messageClass, Constants.getThisMonthReportedMessagesMethod);
                return null;
            }
        }



        /// <summary>
        /// Returns this week reported Messages as an Array
        /// </summary>
        /// <returns>Message Class Array</returns>
         public Message[] GetThisWeekReportedMessages()
        {
            try
            {
                //Connecting to caching db
                MongoDatabase cachingDataBase = CachingDbConnector.GetCachingDatabase();

                DateTime m_WeekStart = DateTime.Now.AddDays(CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek - DateTime.Now.DayOfWeek);
                
                var m_Query = Query<UserMessageReport>.Where(x => x.TimeStamp >=m_WeekStart & x.TimeStamp <= DateTime.Now);
                var m_Collection = cachingDataBase.GetCollection<UserMessageReport>(Constants.userMessageReportClass);
                int[] m_MessageIds = m_Collection.Find(m_Query).Select(e => e.MessageId).ToArray();
                var m_NewQuery = Query<Message>.In(e => e.MessageRefID, m_MessageIds);
                var m_newCollection = cachingDataBase.GetCollection<Message>(Constants.messageClass);
                return  m_newCollection.Find(m_NewQuery).ToArray();
            }
            catch (Exception ex)
            {
                new Error().LogError(ex, Constants.messageClass, Constants.getThisWeekReportedMessagesMethod);
                return null;
            }
        }




         /// <summary>
         /// Returns this day reported Messages as an Array
         /// </summary>
         /// <returns>Message Class Array</returns>
         public Message[] GetTodayReportedMessages()
         {
             try
             {
                 //Connecting to caching db
                 MongoDatabase cachingDataBase = CachingDbConnector.GetCachingDatabase();
                 var m_Query = Query<UserMessageReport>.Where(x => x.TimeStamp >= DateTime.Today & x.TimeStamp <= DateTime.Now);
                 var m_Collection = cachingDataBase.GetCollection<UserMessageReport>(Constants.userMessageReportClass);
                 int[] m_MessageIds = m_Collection.Find(m_Query).Select(e => e.MessageId).ToArray();
                 var m_NewQuery = Query<Message>.In(e => e.MessageRefID, m_MessageIds);
                 var m_NewCollection = cachingDataBase.GetCollection<Message>(Constants.messageClass);
                 return m_NewCollection.Find(m_NewQuery).ToArray();
             }
             catch (Exception ex)
             {
                 new Error().LogError(ex, Constants.messageClass, Constants.getTodayReportedMessagesMethod);
                 return null;
             }
         }




         /// <summary>
         /// Returns this  reported Messages between two dates as an Array
         /// </summary>
         /// <returns>Message Class Array</returns>
         public Message[] GetReportedMessagesBetweenTwoDates(DateTime fromDate,DateTime toDate)
         {
             try
             {
                 //Connecting to caching db
                 MongoDatabase cachingDataBase = CachingDbConnector.GetCachingDatabase();
                 var m_Query = Query<UserMessageReport>.Where(x => x.TimeStamp >= fromDate & x.TimeStamp <= toDate);
                 var m_Collection = cachingDataBase.GetCollection<UserMessageReport>(Constants.userMessageReportClass);
                 int[] m_MessageIds = m_Collection.Find(m_Query).Select(e => e.MessageId).ToArray();
                 var m_NewQuery = Query<Message>.In(e => e.MessageRefID, m_MessageIds);
                 var m_NewCollection = cachingDataBase.GetCollection<Message>(Constants.messageClass);
                 return m_NewCollection.Find(m_NewQuery).ToArray();
             }
             catch (Exception ex)
             {
                 new Error().LogError(ex, Constants.messageClass, Constants.getReportedMessagesBetweenTwoDatesMethod);
                 return null;
             }
         }




         /// <summary>
         /// Returns this rejected Messages between two dates as an Array
         /// </summary>
         /// <returns>Message Class Array</returns>
         public Message[] GetRejectedMessageList()
         {
             try
             {
                  //Connecting to caching db
                 MongoDatabase cachingDataBase = CachingDbConnector.GetCachingDatabase();
                 var m_Query = Query<Message>.EQ(e => e.IsRejectedByAdmin, true);
                 var m_Collection = cachingDataBase.GetCollection<Message>(Constants.messageClass);
                 return m_Collection.Find(m_Query).ToArray();
             }
             catch (Exception ex)
             {
                 new Error().LogError(ex, Constants.messageClass, Constants.getRejectedMessageListMethod);
                 return null;
             }
         }




         public bool RejectOrApproveReportedMessage(int messageRefId,bool isRejected,int adminId,string comment)
         {
             try
             {
                 //Adding approve and rejected history into AdminMessageHistory  collection
                 AdminMessageHistory adminMsgHistoryObj = new AdminMessageHistory().AddAdminMessageHistory(adminId, messageRefId, comment, isRejected);

                 //updating IsRejectedByAdmin into true.

                 MongoDatabase cachingDataBase = CachingDbConnector.GetCachingDatabase();
                 //Here we are not deleting the message we just updates the status of video A(Active) to I(Inactive)
                 var m_Query = Query<Message>.Where(e => e.MessageRefID == messageRefId);
                 var m_Collection = cachingDataBase.GetCollection<Message>(Constants.messageClass);
                 var m_ExistingObject = m_Collection.FindOne(m_Query);
                 if (m_ExistingObject != null)
                 {
                     // If existing already in the database  Update MainDb                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                     return the existing object
                     using (intellidatev2Entities mainDB = new intellidatev2Entities())
                     {
                         using (var transaction = new TransactionScope())
                         {
                             in_message_trn trnMessageObj = mainDB.in_message_trn.SingleOrDefault(c => c.messageID == messageRefId);
                             trnMessageObj.IsRejected = isRejected;
                             mainDB.SaveChanges();

                             try
                             {
                                 //Update CachingDB 
                                 Message cachingObject = new Message();
                                 var m_UpdateQuery = Query<Message>.Where(e => e.MessageRefID == messageRefId);
                                 var m_Update = Update.Set(Constants.isRejectedByAdmin, isRejected);
                                 var m_SortBy = SortBy.Descending(Constants.messageRefID);
                                 var m_Result = m_Collection.FindAndModify(m_UpdateQuery, m_SortBy, m_Update, true);
                                 transaction.Complete();
                                 return true;
                             }
                             catch (Exception exception)
                             {
                                 new Error().LogError(exception, Constants.messageClass, Constants.rejectOrApproveReportedMessageMethod);
                                 return false;
                             }
                            
                         }
                     }
                 }
                 else
                 {
                     return false;
                 }


             }
             catch (Exception ex)
             {
                 new Error().LogError(ex, Constants.messageClass, Constants.rejectOrApproveReportedMessageMethod);
                 return false;
             }
         }


      

    }


    public class RedisMessage
    {
       
        public int UserID { get; set; }
       
        public int OtherUserID { get; set; }

        public string MessageText { get; set; }

      

        /// <summary>
        /// Save user message redis db
        /// </summary>
        /// <returns>bool value</returns>
        public bool SaveRedisDraftMessage(RedisMessage m_Rmsg)
        {
            try
            {
                string m_key = m_Rmsg.UserID + "-" + m_Rmsg.OtherUserID;
                RedisClient m_RedisClient = CachingDbConnector.GetRedisDatabase();
                RedisMessage m_RedisMsg = m_RedisClient.Get<RedisMessage>(m_key);

                if (m_RedisMsg == null)
                {
                    m_RedisClient.Add<RedisMessage>(m_key, m_Rmsg);
                    return true;
                }
                else
                {
                    m_RedisClient.Set<RedisMessage>(m_key, m_Rmsg);
                    return true;
                }

            }
            catch (Exception ex)
            {
                new Error().LogError(ex, Constants.redisMessageClass, Constants.saveRedisDraftMessageMethod);
                return false;
            }
        }

        /// <summary>
        /// get user draft messages from redis db
        /// </summary>
        /// <returns>RedisMessage Class Array</returns>
        public RedisMessage[] GetRedisDraftMessages(int UserId)
        {
            try
            {
                RedisClient m_RedisClient = CachingDbConnector.GetRedisDatabase();
                var m_Res = m_RedisClient.GetAll<RedisMessage>().ToArray().Where(x => x.UserID == UserID).ToArray();
                return m_Res;
            }
            catch (Exception ex)
            {
                new Error().LogError(ex, Constants.redisMessageClass, Constants.getRedisDraftMessagesMethod);
                return null;
            }
        }


        /// <summary>
        /// get user draft messages from redis db
        /// </summary>
        /// <returns>RedisMessage Class Array</returns>
        public RedisMessage GetRedisDraftMessageByKey(string key)
        {
            try
            {
                RedisClient m_RedisClient = CachingDbConnector.GetRedisDatabase();
                var m_Res = m_RedisClient.Get<RedisMessage>(key);
                return m_Res;
            }
            catch (Exception ex)
            {
                new Error().LogError(ex, Constants.redisMessageClass, Constants.getRedisDraftMessagesMethod);
                return null;
            }
        }


        /// <summary>
        ///delete message if user message was sent.
        /// </summary>
        /// <returns>RedisMessage Class Array</returns>
        public long DeleteRedisDraftMessage(string m_RKey)
        {
            try
            {
                RedisClient m_RedisClient = CachingDbConnector.GetRedisDatabase();
                long m_Res = m_RedisClient.Del(m_RKey);
                return m_Res;
            }
            catch (Exception ex)
            {
                new Error().LogError(ex, Constants.redisMessageClass, "DeleteRedisDraftMessage");
                return 0;
            }
        }



    }

    


}


