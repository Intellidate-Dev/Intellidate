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
    //to manage the user notifications
    public  class UserNotification
    {
        /// <summary>
        /// The UserNotification class identifier for caching db
        /// </summary>
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }

        /// <summary>
        /// The User ID from the MySQL Database if any..
        /// </summary>
        public int UserRefID { get; set; }

        /// <summary>
        /// The User property from the collection or MySQL Database if any.. it returns the user information
        /// </summary>
        public User User
        {
            get
            {
                return new User().GetUserDetails(UserRefID);
            }
        }

        /// <summary>
        /// The Other User User ID from the MySQL Database if any..
        /// </summary>
        public int OtherUserRefID { get; set; }

        /// <summary>
        /// The Other User property from the collection or MySQL Database if any.. it returns the user information
        /// </summary>
        public User OtherUser
        {
            get
            {
                return new User().GetUserDetails(OtherUserRefID);
            }
        }

        /// <summary>
        /// The NotificationType from the MySQL Database if any..
        /// </summary>
        public string NotificationType { get; set; }

        /// <summary>
        /// The NotificationText from the MySQL Database if any..
        /// </summary>
        public string NotificationText { get; set; }

        /// <summary>
        /// The timestamp when the user was viewed a notificaion
        /// </summary>
        public DateTime TimeStamp { get; set; }

        /// <summary>
        /// The IsViewed from the MySQL Database if any..
        /// </summary>
        public bool IsViewed { get; set; }

        public bool AddNotification(int UserRefID, int OtherUserRefID, string NotificationType, string NotificationText)
        {
            try
            {
                 int _LanRefID = 0;
                // Insert the record into the MainDB
                 using (intellidatev2Entities _MainDB = new intellidatev2Entities())
                 {
                     using (var transaction = new TransactionScope())
                     {
                         in_user_notification _ULObj = new in_user_notification
                         {
                             UserId = UserRefID,
                             OtherUserId = OtherUserRefID,
                             NotificationText = NotificationText,
                             NotificationType = NotificationType,
                             TimeSpan = DateTime.Now.ToUniversalTime(),
                             IsViewed = false,
                         };
                         _MainDB.in_user_notification.Add(_ULObj);
                         _MainDB.SaveChanges();
                         _LanRefID = _ULObj.NotificationId;
                         try
                         {
                             UserNotification _CachingObject = new UserNotification
                             {

                                 UserRefID = UserRefID,
                                 OtherUserRefID = OtherUserRefID,
                                 NotificationText = NotificationText,
                                 NotificationType = NotificationType,
                                 TimeStamp = DateTime.Now.ToUniversalTime(),
                                 IsViewed = false,
                             };
                             //insert the record into cache db
                             MongoDatabase _CachingDatabase = CachingDbConnector.GetCachingDatabase();
                             var _newCollection = _CachingDatabase.GetCollection<UserNotification>(Constants.userNotificationClass);
                             _newCollection.Save(_CachingObject);
                             transaction.Complete();
                             return true;
                         }
                         catch (Exception exception)
                         {
                             new Error().LogError(exception, Constants.userNotificationClass, Constants.AddNotificationMethod);
                             return false;
                         }
                     }
                 }
         
            }
            catch (Exception ex)
            {
                new Error().LogError(ex, Constants.userNotificationClass, Constants.AddNotificationMethod);
                return false;
            }
        }


        public UserNotification[] GetNotification(int UserRefID)
        {
            try
            {
                //connecting to caching db
                MongoDatabase CachingDatabase = CachingDbConnector.GetCachingDatabase();
                //getting all UserNotification data from caching db                
                var _Query = Query<UserNotification>.Where(e => e.UserRefID==UserRefID);
                var _Collection = CachingDatabase.GetCollection<UserNotification>(Constants.userNotificationClass);
                return _Collection.Find(_Query).ToArray();
            }
            catch (Exception ex)
            {
                new Error().LogError(ex, Constants.userNotificationClass, Constants.getNotificationMethod);
                return null;
            }
        }

    }

    


}
