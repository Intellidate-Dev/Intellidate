using IntellidateLib.DB;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace IntellidateLib
{
    /// <summary>
    /// To maintain the history of user reported messages
    /// </summary>
    public class UserMessageReport
    {

        /// <summary>
        /// The UserMessageReport class identifier for caching db
        /// </summary>
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }

        /// <summary>
        /// The _RefID from maindb uniqid
        /// </summary>
        public int _RefID { get; set; }

        /// <summary>
        /// recipient or reported user id
        /// </summary>
        public int UserRefID { get; set; }

        /// <summary>
        /// The GetUser property from the collection or MySQL Database if any..it returns the reciver user information
        /// </summary>
        public User GetUser
        {
            get
            {
                return new User().GetUserDetails(UserRefID);
            }
        }

        /// <summary>
        /// MessageId property from the collection or MySQL Database if any.. it returns the user information
        /// </summary>   
        public int MessageId { get; set; }

        /// <summary>
        /// ReportText property from the collection or MySQL Database if any.. it returns the user information
        /// </summary>   
        public string ReportText { get; set; }

        /// <summary>
        /// TimeStamp property from the collection or MySQL Database if any.. it returns the user information
        /// </summary>   
        public DateTime TimeStamp { get; set; }



        /// <summary>
        /// inserting user reported messages
        /// </summary>        
        public UserMessageReport AddUserMessageReport(int UserRefID, int MessageId, string ReportText)
        {
            try
            {
                int _ObjRefID = 0;
                // Insert the record into the MainDB
                using (intellidatev2Entities _MainDB = new intellidatev2Entities())
                {
                    using (var transaction = new TransactionScope())
                    {
                        in_usermessagereport_mst _USRObj = new in_usermessagereport_mst
                        {
                            MessageId = MessageId,
                            ReportText = ReportText,
                            UserId = UserRefID,
                            TimeStamp = DateTime.Now.ToUniversalTime()
                        };
                        _MainDB.in_usermessagereport_mst.Add(_USRObj);
                        _MainDB.SaveChanges();
                        _ObjRefID = _USRObj.ReportId;
                        try
                        {
                            UserMessageReport _CachingObject = new UserMessageReport
                            {
                                _RefID = _ObjRefID,
                                UserRefID = UserRefID,
                                MessageId = MessageId,
                                ReportText = ReportText,
                                TimeStamp = DateTime.Now.ToUniversalTime()
                            };
                            //insert the record into cache db
                            MongoDatabase _CachingDatabase = CachingDbConnector.GetCachingDatabase();
                            var _newCollection = _CachingDatabase.GetCollection<UserMessageReport>(Constants.userMessageReportClass);
                            _newCollection.Save(_CachingObject);
                            transaction.Complete();
                            return _CachingObject;
                        }
                        catch (Exception exception)
                        {
                            new Error().LogError(exception, Constants.userMessageReportClass, Constants.addUserMessageReportMethod);
                            return null; 
                            
                        }
                       
                    }
                }
            }
            catch (Exception ex)
            {
                new Error().LogError(ex, Constants.userMessageReportClass, Constants.addUserMessageReportMethod);
                return null;
            }
        }
        



        /// <summary>
        /// getting user message reports based on message id
        /// </summary>        
        public UserMessageReport[] GetUserMessageReport(int MessageId)
        {
            try
            {
                MongoDatabase CachingDatabase = CachingDbConnector.GetCachingDatabase();
                //getting UserMessageReport data from caching db
                var _Query = Query<UserMessageReport>.EQ(e => e.MessageId, MessageId);
                var _Collection = CachingDatabase.GetCollection<UserMessageReport>(Constants.userMessageReportClass);
                return _Collection.Find(_Query).ToArray();
            }
            catch (Exception ex)
            {
                new Error().LogError(ex, Constants.userMessageReportClass, Constants.getUserMessageReportMethod);
                return null;
            }
        }




    }
    public static partial class Constants 
    {
        //class
        public static string userMessageReportClass = "UserMessageReport";

        //methods
        public static string getUserMessageReportMethod = "GetUserMessageReport";
        public static string addUserMessageReportMethod = "AddUserMessageReport";
      
    }
}
