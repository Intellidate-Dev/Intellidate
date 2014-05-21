using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntellidateLib.DB;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Transactions;

namespace IntellidateLib
{
    /// <summary>
    /// TheThe LoginDetails class defines all the generic properties of the Login details collection.
    /// </summary>
    public class LoginDetails
    {

        /// <summary>
        /// The user identifier for cache db
        /// </summary>
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }
        /// <summary>
        /// The user identifier for main db
        /// </summary>
        public int _RefID { get; set; }

        /// <summary>
        /// The Login ID of the user
        /// </summary>
        public int _UserRefID { get; set; }

        /// <summary>
        /// The Login time of the user
        /// </summary>
        public DateTime LoginTime { get; set; }

        /// <summary>
        /// The Browser details
        /// </summary>
        public string Browser { get; set; }

        /// <summary>
        /// The User Opereting System details
        /// </summary>
        public string OS { get; set; }

        /// <summary>
        /// The User location Latitude details
        /// </summary>
        public string Latitude { get; set; } 
        
        /// <summary>
        /// The User location Longtitude details
        /// </summary>
        public string Longtitude { get; set; } 
        
        /// <summary>
        /// The Browser Referer details
        /// </summary>
        public string Referer{ get; set; }


        /// <summary>
        /// The User System IP Address details
        /// </summary>
        public string IPAddress { get; set; }

      


        /// <summary>
        /// Adding User login Details to caching DB and MainDb
        /// </summary>
        /// <param name="userRefID">The LoginID of the user.</param>
        /// <param name="browser">The User's Opened Browser  </param>
        /// <param name="operatingSys">The Operating System of the user.</param>
        /// <param name="latitude">The Latitude of the user location </param>
        /// <param name="longtitude">The Longtitude of the user location.</param>
        /// <param name="browser">The EmailAddress </param>
        /// <param name="referer">The Referer of the user.</param>
        /// <param name="ipAddress">The IPAddress of user system </param>
        public bool AddUserLogin(int userRefID, string browser, string operatingSys, string latitude, string longtitude, string referer, string ipAddress)
        {
            try
            {
                //Connecting to caching db
                MongoDatabase cachingDataBase = CachingDbConnector.GetCachingDatabase();

                var m_Query = Query<LoginDetails>.Where(x => x._RefID == userRefID);
                var m_Collection = cachingDataBase.GetCollection<LoginDetails>(Constants.loginDetailsClass);
                var m_ExistingObject = m_Collection.Find(m_Query);
              
                    int m_RefID = 0;

                    using (intellidatev2Entities mainDB = new intellidatev2Entities())
                    {
                        using (var transaction = new TransactionScope())
                        {
                            in_userlogin_trn mstUserLoginObject = new in_userlogin_trn
                            {
                                LoginTime = DateTime.Now.ToUniversalTime(),
                                Browser = browser,
                                OS = operatingSys,
                                Latitude = latitude,
                                Longitude = longtitude,
                                Referer = referer,
                                IPAddress = ipAddress,
                                UserID = userRefID
                            };
                            mainDB.in_userlogin_trn.Add(mstUserLoginObject);
                            mainDB.SaveChanges();
                            m_RefID = Convert.ToInt32(mstUserLoginObject.UserID);
                            try
                            {
                                // Once the insert is done in the MainDB, do the insert on the Caching DB
                                LoginDetails cachingObject = new LoginDetails();
                                cachingObject._RefID = m_RefID;
                                cachingObject._UserRefID = userRefID;
                                cachingObject.LoginTime = DateTime.Now;
                                cachingObject.Browser = browser;
                                cachingObject.OS = operatingSys;
                                cachingObject.Latitude = latitude;
                                cachingObject.Longtitude = longtitude;
                                cachingObject.Referer = referer;
                                cachingObject.IPAddress = ipAddress;
                                m_Collection.Save(cachingObject);
                                transaction.Complete();
                                return true;
                            }
                            catch (Exception exception)
                            {
                                new Error().LogError(exception, Constants.loginDetailsClass, Constants.addUserLoginMethod);
                                return false;
                            }

                           
                        }
                    };
             }
            catch (Exception ex)
            {
                new Error().LogError(ex, Constants.loginDetailsClass, Constants.addUserLoginMethod);
                return false;
            }
        }





        /// <summary>
        /// Getting User login Details as a array from caching DB
        /// </summary>
        /// <param name="userRefId">The LoginID of the user.</param>
        public LoginDetails[] GetUserLoginHistory(int userRefId)
        {
            try
            {
                //Connecting to caching db
                MongoDatabase cachingDataBase = CachingDbConnector.GetCachingDatabase();
                var m_Query = Query<LoginDetails>.Where(x => x._RefID == userRefId);
                //Getting User Login History from caching db
                var m_Collection = cachingDataBase.GetCollection<LoginDetails>(Constants.loginDetailsClass);
                return m_Collection.Find(m_Query).ToArray();
            }
            catch (Exception ex)
            {
                new Error().LogError(ex, Constants.loginDetailsClass, Constants.getUserLoginHistoryMethod);
                return null;
            }
        }





        /// <summary>
        /// Getting User Last(recently) login Details as a array from caching DB
        /// </summary>
        /// <param name="userRefId">The LoginID of the user.</param>
        public LoginDetails GetUserLastLoginDetails(int userRefId)
        {
            try
            {
                //Connecting to caching db
                MongoDatabase cachingDataBase =CachingDbConnector.GetCachingDatabase();
                var m_Query = Query<LoginDetails>.Where(x => x._RefID == userRefId);
                //Getting User Login details from caching db                
                var m_Collection = cachingDataBase.GetCollection<LoginDetails>(Constants.loginDetailsClass);
                var m_Array = m_Collection.Find(m_Query).ToArray().OrderBy(x=>x.LoginTime);
                return m_Array.LastOrDefault();
            }
            catch (Exception ex)
            {
                new Error().LogError(ex, Constants.loginDetailsClass, Constants.getUserLastLoginDetailsMethod);
                return null;
            }
        }
  
    
    
    
    }
}
