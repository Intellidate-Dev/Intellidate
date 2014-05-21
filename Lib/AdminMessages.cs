using IntellidateLib.DB;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace IntellidateLib
{
    public class AdminMessages
    {
        /// <summary>
        /// The Message class identifier for caching db
        /// </summary>
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }

        /// <summary>
        /// The unque and prymary key of AdminMessages  collection or MySQL Database if any..
        /// </summary>
        public int MessageRefID { get; set; }


        /// <summary>
        /// The Message ref ID from the AdminMasterMessages collection or MySQL Database if any..
        /// </summary>
        public int MessageID { get; set; }



        /// <summary>
        /// The User ID from the collection or MySQL Database if any..
        /// </summary>
        public int AdminRefID { get; set; }
        /// The RecipientRefID from the collection or MySQL Database if any.. the reciver user id
        /// </summary>
        public int RecipientRefID { get; set; }

        /// <summary>
        /// The Recipient property from the collection or MySQL Database if any..it returns the reciver user information
        /// </summary>
        public User Recipient
        {
            get
            {
                return new User().GetUserDetails(RecipientRefID);
            }
        }

        /// <summary>
        /// The SentTime from the collection or MySQL Database if any.. the sender message sent time
        /// </summary>
        public DateTime SentTime { get; set; }

        /// <summary>
        /// The IsMessageViewed from the collection or MySQL Database if any.. the reciver viewed or not viewed=true,not viewed=false
        /// </summary>
        public bool IsMessageViewed { get; set; }

        /// <summary>
        /// The MessageViewedTime from the collection or MySQL Database if any.. the reciver viewed time
        /// </summary>
        public DateTime MessageViewedTime { get; set; }

        /// <summary>
        /// 
        /// The IsMessageEdited from the collection or MySQL Database if any.. the user message is edited or not. Edited=true,Not Edited=false
        /// </summary>
        public bool IsMessageEdited { get; set; }

        /// <summary>
        /// The MessageLastEditedTime from the collection or MySQL Database if any.. the user message last edited time
        /// </summary>
        public DateTime MessageLastEditedTime { get; set; }

        /// <summary>
        /// The MessageDeletedBySender from the collection or MySQL Database if any.. the message is deleted by Admin. Deleted=true,Not Deleted=false
        /// </summary>
        public bool MessageDeletedByAdmin { get; set; }

        /// <summary>
        /// The MessageDeletedByRecipient from the collection or MySQL Database if any.. the message is deleted by Recipient. Deleted=true,Not Deleted=false
        /// </summary>
        public bool MessageDeletedByRecipient { get; set; }

        /// <summary>
        /// The Status from the collection or MySQL Database if any.. Incative=I and Active=A
        /// </summary>
        public string Status { get; set; }




        /// <summary>
        ///  The Send Message. The method must insert into both MySQL cache the date into MongoDB
        /// </summary>
        /// <param name="SenderRefID">The LoginID of the user</param>
        /// <param name="RecipientRefID">The LoginID of the another user(recipient) </param>
        /// <param name="MessageText">The Sender MessageText   </param>
        /// <returns>Message class</returns>
        public AdminMessages SendAdminMessage(int AdminRefID, int RecipientRefID, int MessageID)
        {
            try
            {
                int _MessageRefID = 0;
                // Insert the record into the MainDB
                using (intellidatev2Entities _MainDB = new intellidatev2Entities())
                {
                    using (var transaction = new TransactionScope())
                    {
                        in_admin_message_trn _MessageObj = new in_admin_message_trn
                        {
                            AdminID = AdminRefID,
                            RecipientID = RecipientRefID,
                            MessageID = MessageID,
                            SentTime = DateTime.Now.ToUniversalTime(),
                            IsMessageViewed = false,
                            IsEdited = false,
                            Status = Constants.activeStatus,
                        };
                        _MainDB.in_admin_message_trn.Add(_MessageObj);
                        _MainDB.SaveChanges();
                        _MessageRefID = _MessageObj.MessageTrnID;
                        try
                        {
                            AdminMessages _CachingObject = new AdminMessages
                            {
                                MessageRefID = _MessageRefID,
                                MessageID = MessageID,
                                AdminRefID = AdminRefID,
                                RecipientRefID = RecipientRefID,
                                SentTime = DateTime.Now.ToUniversalTime(),
                                IsMessageViewed = false,
                                IsMessageEdited = false,
                                Status = Constants.activeStatus
                            };
                            //insert the record into cache db
                            MongoDatabase _CachingDatabase = CachingDbConnector.GetCachingDatabase();
                            var _Collection = _CachingDatabase.GetCollection<AdminMessages>(Constants.adminMessageClass);
                            _Collection.Save(_CachingObject);
                            transaction.Complete();
                            return _CachingObject;
                        }
                        catch (Exception exception)
                        {
                            new Error().LogError(exception, Constants.adminMessageClass, Constants.sendAdminMessageMethod);
                            return null;
                        }
                       
                    }
                }
            }
            catch (Exception ex)
            {
                new Error().LogError(ex, Constants.adminMessageClass, Constants.sendAdminMessageMethod);
                return null;
            }
        }





    }
    
}
