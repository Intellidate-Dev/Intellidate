using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntellidateLib.DB;
using System.Transactions;


namespace IntellidateLib
{

    /// <summary>
    /// This class for user reported messages
    /// To maintain the history of admin rejected messages.
    /// </summary>
    public class AdminMessageHistory
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }

        public int _RefID { get; set; }

        public int AdminId { get; set; }

        public int MessageId { get; set; }

        public string Comment { get; set; }

        public DateTime TimeStamp { get; set; }

        //for rejected=true and approved=false
        public bool IsRejected { get; set; }




        public AdminMessageHistory AddAdminMessageHistory(int AdminId, int MessageId, string Comment, bool IsRejected)
        {
            try
            {
                int _ObjRefID = 0;
                // Insert the record into the MainDB
                using (intellidatev2Entities _MainDB = new intellidatev2Entities())
                {
                    using (var transaction = new TransactionScope())
                    {
                        in_adminmessagehistory_mst _HistoryObj = new in_adminmessagehistory_mst
                        {
                            AdminId = AdminId,
                            MessageId = MessageId,
                            Comment = Comment,
                            IsRejected = IsRejected,
                            TimeStamp = DateTime.Now.ToUniversalTime()
                        };
                        _MainDB.in_adminmessagehistory_mst.Add(_HistoryObj);
                        _MainDB.SaveChanges();
                        _ObjRefID = _HistoryObj.HistoryId;
                        try
                        {
                            AdminMessageHistory _CachingObject = new AdminMessageHistory
                            {
                                _RefID = _ObjRefID,
                                AdminId = AdminId,
                                MessageId = MessageId,
                                Comment = Comment,
                                IsRejected = IsRejected,
                                TimeStamp = DateTime.Now.ToUniversalTime()
                            };
                            //insert the record into cache db
                            MongoDatabase _CachingDatabase = CachingDbConnector.GetCachingDatabase();
                            var _Collection = _CachingDatabase.GetCollection<AdminMessageHistory>(Constants.adminMessageHistoryClass);
                            _Collection.Save(_CachingObject);
                            transaction.Complete();
                            return _CachingObject;
                            
                        }
                        catch (Exception exception)
                        {
                            new Error().LogError(exception, Constants.adminMessageHistoryClass, Constants.addAdminMessageHistoryMethod);
                            return null;
                        }
                      
                    }
                }
            }
            catch (Exception ex)
            {
                new Error().LogError(ex, Constants.adminMessageHistoryClass, Constants.addAdminMessageHistoryMethod);
                return null;
            }
        }




        public AdminMessageHistory GetAdminMessageHistory(int RefID)
        {
            try
            {
                //connecting to caching db
                MongoDatabase _CachingDatabase = CachingDbConnector.GetCachingDatabase();

                var _Query = Query<AdminMessageHistory>.EQ(e => e._RefID, RefID);
                var _Collection = _CachingDatabase.GetCollection<AdminMessageHistory>(Constants.adminMessageHistoryClass);
                return _Collection.FindOne(_Query);

            }
            catch (Exception ex)
            {
                new Error().LogError(ex, Constants.adminMessageHistoryClass, Constants.getAdminMessageHistoryMethod);
                return null;
            }
        }




        public AdminMessageHistory[] GetAdminMessageHistoryByMessageId(int MessageId)
        {
            try
            {
                //connecting to caching db
                MongoDatabase _CachingDatabase = CachingDbConnector.GetCachingDatabase();

                var _Query = Query<AdminMessageHistory>.EQ(e => e.MessageId, MessageId);
                var _Collection = _CachingDatabase.GetCollection<AdminMessageHistory>(Constants.adminMessageHistoryClass);
                return _Collection.Find(_Query).ToArray();

            }
            catch (Exception ex)
            {
                new Error().LogError(ex, Constants.adminMessageHistoryClass, Constants.getAdminMessageHistoryByMessageIdMethod);
                return null;
            }
        }




        public AdminMessageHistory[] GetAdminRejectedMessagesHistory()
        {
            try
            {
                //connecting to caching db
                MongoDatabase _CachingDatabase = CachingDbConnector.GetCachingDatabase();

                var _Query = Query<AdminMessageHistory>.EQ(e => e.IsRejected, true);
                var _Collection = _CachingDatabase.GetCollection<AdminMessageHistory>(Constants.adminMessageHistoryClass);
                return _Collection.Find(_Query).ToArray();

            }
            catch (Exception ex)
            {
                new Error().LogError(ex, Constants.adminMessageHistoryClass, Constants.getAdminRejectedMessagesHistoryMethod);
                return null;
            }
        }


    }
}
