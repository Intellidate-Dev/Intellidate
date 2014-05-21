using IntellidateLib.DB;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntellidateLib
{
    //this class for block or hide the user 
    public class UserManagement
    {
        /// <summary>
        /// The user identifier
        /// </summary>
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }

        /// <summary>
        /// The reference ID from the collection or MySQL Database if any..
        /// </summary>
        public int _RefID { get; set; }

        /// <summary>
        ///login user id
        /// </summary>
        public int UserRefId { get; set; }

        /// <summary>
        ///blocked or hided user id
        /// </summary>
        public int OtherUserRefId { get; set; }
        /// <summary>
        ///comments for hide or blocking the user
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        ///Blocked=true and UnBlocked=false
        /// </summary>
        public bool IsBlocked { get; set; }

        /// <summary>
        ///  hide =true and show=false
        /// </summary>
        public bool IsHide { get; set; }

        /// <summary>
        /// Time stamp to maintain the history 
        /// </summary>
        public DateTime TimeStamp { get; set; }



        public bool AddUserManagementDetails(int UserRefId, int OtherUserRefId, string Comment, bool IsBlocked, bool IsHide)
        {
            try
            {
                int _UMRefID = 0;
                // Insert the record into the MainDB
                using (intellidatev2Entities _MainDB = new intellidatev2Entities())
                {

                }
                UserManagement _CachingObj = new UserManagement
                {

                    _RefID = _UMRefID,
                    UserRefId = UserRefId,
                    OtherUserRefId = OtherUserRefId,
                    Comment = Comment,
                    IsBlocked = IsBlocked,
                    IsHide = IsHide,
                    TimeStamp = DateTime.Now.ToUniversalTime(),

                };
                //insert the record into cache db
                MongoDatabase _CachingDatabase = CachingDbConnector.GetCachingDatabase();
                var _Collection = _CachingDatabase.GetCollection<UserManagement>("UserManagement");
                _Collection.Save(_CachingObj);
                return true;
            }
            catch (Exception ex)
            {
                new Error().LogError(ex, "UserManagement", "AddUserManagementDetails");
                return false;
            }
        }
    }
}
