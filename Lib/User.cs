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
using System.Data.SqlClient;
using System.Globalization;
using MongoDB.Bson.Serialization.Attributes;
using System.Transactions;


namespace IntellidateLib
{

    /// <summary>
    /// The User class defines all the generic properties of the user
    /// </summary>
    public class User
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
        /// The user login name. Min=6 characters, Max=20 characters
        /// </summary>
        public string LoginName { get; set; }

        /// <summary>
        /// The full name of the user. Max=100 characters, Min=10 characters
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// The email address of the user. Min=6 characters, Max=100 characters
        /// </summary>
        public string EmailAddress { get; set; }

        /// <summary>
        /// The password of the user. Min=8 characters, Max=20 characters
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// The user gender. Taken from the enum Gender
        /// </summary>
        public int UserGender { get; set; }

        /// <summary>
        /// The date of birth of the user
        /// </summary>
        public DateTime DateOfBirth { get; set; }

        /// <summary>
        /// The computed value of user's age.
        /// </summary>
        public int UserAgeInYears { get; set; }

        /// <summary>
        /// The timestamp when the user was created.
        /// </summary>
        public DateTime UserCreatedDate { get; set; }

        /// <summary>
        /// The timestamp when the user was last online.
        /// </summary>
        public DateTime LastOnlineTime { get; set; }

        /// <summary>
        /// The status of the User record. A=Active, I=Inactive 
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// The connection string of  Cache DB
        /// </summary>
        private int GetAge(DateTime DateOfBirth)
        {
            TimeSpan UserAge = DateTime.Now - DateOfBirth;
            return (UserAge.Days/365);
        }

        public UserProfile GetUserProfile
        {
            get { return new UserProfile().GetUserProfile(_RefID); }
        }


        public User RegisterUser(string LoginName, string EmailAddress, string Password)
        {
            try
            {
                MongoDatabase _CachingDatabase = CachingDbConnector.GetCachingDatabase();
                // check if the User email is already existing. and also checks the User's login is alredy existing.
                var _Query = Query<User>.Where(x => x.EmailAddress == EmailAddress && x.LoginName == LoginName);
                var _Collection = _CachingDatabase.GetCollection<User>(Constants.userClass);
                var _ExistingObject = _Collection.FindOne(_Query);

                if (_ExistingObject != null)
                {
                    // If existing already in the database 

                    return null;
                }
                else
                {
                    DateTime _CreatedDate = DateTime.Now.ToUniversalTime();
                    int _RefID = 0;

                    // else insert the user details in the Main DB and Cache DB
                    using (intellidatev2Entities _MainDB = new intellidatev2Entities())
                    {
                        using (var transaction = new TransactionScope())
                        {
                            in_user_mst _UserObject = new in_user_mst
                            {
                                LoginName = LoginName,
                                FullName = "",
                                EmailAddress = EmailAddress,
                                Password = Password,
                                DateOfBirth = DateTime.Now,
                                CreatedDate = _CreatedDate,
                                Gender = 0,
                                LastOnlineTime = _CreatedDate,
                                UserAgeInYears = GetAge(DateOfBirth),
                                Status = Constants.inActiveStatus,
                            };
                            _MainDB.in_user_mst.Add(_UserObject);
                            _MainDB.SaveChanges();
                            _RefID = _UserObject.UserId;
                            try
                            {
                                // Once the insert is done in the MainDB, do the insert on the Caching DB
                                User _CachingObject = new User();
                                _CachingObject._RefID = _RefID;
                                _CachingObject.UserCreatedDate = _CreatedDate;
                                _CachingObject.UserGender = 0;
                                _CachingObject.LoginName = LoginName;
                                _CachingObject.Password = Password;
                                _CachingObject.LastOnlineTime = _CreatedDate;
                                _CachingObject.FullName = "";
                                _CachingObject.EmailAddress = EmailAddress;
                                _CachingObject.DateOfBirth = DateTime.Now;
                                _CachingObject.UserAgeInYears = GetAge(DateOfBirth);
                                _CachingObject.Status = Constants.inActiveStatus;
                                _Collection.Save(_CachingObject);
                                transaction.Complete();
                                return _CachingObject;
                            }
                            catch (Exception exception)
                            {
                                new Error().LogError(exception, Constants.userClass, Constants.registerUserMethod);
                                return null;
                            }
                        }
                    };
                }
            }
            catch (Exception ex)
            {
                new Error().LogError(ex, Constants.userClass, Constants.registerUserMethod);
                return null;
            }
        }


        /// <summary>
        /// The Add new user method. The method must insert into both MySQL cache the date into MongoDB
        /// </summary>
        /// <param name="LoginName">The Login Name of the user</param>
        /// <param name="FullName">The Full Name of the user</param>
        /// <param name="EmailAddress">The Email Address  of the user</param>
        /// <param name="Password">The Password  of the user</param>
        /// <param name="UserGender">The User Gender of the user</param>
        /// <param name="DateOfBirth">The Date Of Birth of the user</param>
        /// <returns>recently added user collection</returns>
        /// </summary>
        public User AddNewUser(string LoginName,string FullName, string EmailAddress, string Password, int UserGender, DateTime DateOfBirth)
        {
            try
            {
                MongoDatabase _CachingDatabase = CachingDbConnector.GetCachingDatabase();
                // check if the User email is already existing. and also checks the User's login is alredy existing.
                var _Query = Query<User>.Where(x=>x.EmailAddress==EmailAddress && x.LoginName==LoginName);
                var _Collection = _CachingDatabase.GetCollection<User>(Constants.userClass);
                var _ExistingObject = _Collection.FindOne(_Query);

                if (_ExistingObject != null)
                {
                    // If existing already in the database 

                    return null;
                }
                else
                {
                    DateTime _CreatedDate = DateTime.Now.ToUniversalTime();
                    int _RefID = 0;

                    // else insert the user details in the Main DB and Cache DB
                    using (intellidatev2Entities _MainDB = new intellidatev2Entities())
                    {

                        using (var transaction = new TransactionScope())
                        {

                            in_user_mst _UserObject = new in_user_mst
                            {
                                LoginName = LoginName,
                                FullName = FullName,
                                EmailAddress = EmailAddress,
                                Password = Password,
                                DateOfBirth = DateOfBirth,
                                CreatedDate = _CreatedDate,
                                Gender = UserGender,
                                LastOnlineTime = _CreatedDate,
                                UserAgeInYears = GetAge(DateOfBirth),
                                Status = Constants.inActiveStatus,
                            };
                            _MainDB.in_user_mst.Add(_UserObject);
                            _MainDB.SaveChanges();
                            _RefID = _UserObject.UserId;
                            try
                            {
                                // Once the insert is done in the MainDB, do the insert on the Caching DB
                                User _CachingObject = new User();
                                _CachingObject._RefID = _RefID;
                                _CachingObject.UserCreatedDate = _CreatedDate;
                                _CachingObject.UserGender = UserGender;
                                _CachingObject.LoginName = LoginName;
                                _CachingObject.Password = Password;
                                _CachingObject.LastOnlineTime = _CreatedDate;
                                _CachingObject.FullName = FullName;
                                _CachingObject.EmailAddress = EmailAddress;
                                _CachingObject.DateOfBirth = DateOfBirth;
                                _CachingObject.UserAgeInYears = GetAge(DateOfBirth);
                                _CachingObject.Status = Constants.inActiveStatus;
                                _Collection.Save(_CachingObject);
                                transaction.Complete();
                                return _CachingObject;
                            }
                            catch (Exception exception)
                            {
                                new Error().LogError(exception, Constants.userClass, Constants.addNewUserMethod);
                                return null;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                new Error().LogError(ex, Constants.userClass, Constants.addNewUserMethod);
                return null;
            }
        }




        /// <summary>
        /// The EditUserDetails Method is update the particular user collection. The method must update into both MySQL cache the date into MongoDB
        /// </summary>
        /// <param name="UserRefID">The LoginID of the user.</param>
        /// <param name="LoginName">The Login Name of the user</param>
        /// <param name="FullName">The Full Name of the user</param>
        /// <param name="EmailAddress">The Email Address  of the user</param>
        /// <param name="Password">The Password  of the user</param>
        /// <param name="UserGender">The User Gender of the user</param>
        /// <param name="DateOfBirth">The Date Of Birth of the user</param>
        /// <returns>recently added user collection</returns>
        /// 
        public User EditUserDetails(int UserRefID, string LoginName, string FullName, string EmailAddress, string Password, int UserGender, DateTime DateOfBirth)
        {
            try
            {
                MongoDatabase _CachingDatabase = CachingDbConnector.GetCachingDatabase();
                var collection = _CachingDatabase.GetCollection<User>(Constants.userClass);
                //check user is exist or not 
                var _Query = Query<User>.EQ(e => e._RefID, UserRefID);
                var __ExistingObject = collection.FindOne(_Query);

                if (__ExistingObject != null)
                {
                    User _CachingObject = new User();
                    // Edit user details in MainDB and CachingDB
                        // If existing already in the database  Update MainDb                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                     return the existing object
                    using (intellidatev2Entities _MainDB = new intellidatev2Entities())
                    {
                        using (var transaction = new TransactionScope())
                        {
                            in_user_mst _UserObj = _MainDB.in_user_mst.SingleOrDefault(c => c.UserId == UserRefID);
                            _UserObj.LoginName = LoginName;
                            _UserObj.FullName = FullName;
                            _UserObj.EmailAddress = EmailAddress;
                            _UserObj.Password = Password;
                            _UserObj.Gender = UserGender;
                            _UserObj.DateOfBirth = DateOfBirth;
                            _UserObj.UserAgeInYears = GetAge(DateOfBirth);
                            _MainDB.SaveChanges();

                            try
                            {
                                //update CachingDB 
                                var _UpdateQuery = Query.And(Query.EQ(Constants.refId, UserRefID));
                                var _Update = Update.Set(Constants.loginName, LoginName)
                                                    .Set(Constants.fullName, FullName)
                                                    .Set(Constants.password, Password)
                                                    .Set(Constants.emailAddress, EmailAddress)
                                                    .Set(Constants.userGender, UserGender)
                                                    .Set(Constants.dateOfBirth, DateOfBirth)
                                                    .Set(Constants.userAgeInYears, GetAge(DateOfBirth));
                                var _sortBy = SortBy.Descending(Constants.refId);
                                var _Result = collection.FindAndModify(_UpdateQuery, _sortBy, _Update, true);


                                _CachingObject._RefID = UserRefID;
                                _CachingObject.UserCreatedDate = UserCreatedDate;
                                _CachingObject.UserGender = UserGender;
                                _CachingObject.LoginName = LoginName;
                                _CachingObject.Password = Password;
                                _CachingObject.FullName = FullName;
                                _CachingObject.EmailAddress = EmailAddress;
                                _CachingObject.DateOfBirth = DateOfBirth;
                                _CachingObject.UserAgeInYears = GetAge(DateOfBirth);
                                transaction.Complete();
                                return _CachingObject;
                            }
                            catch (Exception exception)
                            {
                                new Error().LogError(exception, Constants.userClass, Constants.editUserDetailsMethod);
                                return null;
                            }
                           
                        }
                    };
                }
                else
                {
                    return null;
                }
            }
            
            catch(Exception ex)
            {
                new Error().LogError(ex, Constants.userClass, Constants.editUserDetailsMethod);
                return null;
            }
        }




        /// <summary>
        /// Deleting single user based on userid it will not delete entaire record it will update the status as InActive mode.
        /// </summary>
        /// <param name="UserRedfID">The ID from the MainDB</param>
        /// <returns>true or false</returns>
        public bool DeleteUser(int UserRefID)
        {
            try
            {
                MongoDatabase _CachingDatabase = CachingDbConnector.GetCachingDatabase();
                //Here we are not deleting the user we just updates the status of user A(Active) to I(Inactive)
                var _SelectQuery = Query<User>.EQ(e => e._RefID, UserRefID);
                var _Collection = _CachingDatabase.GetCollection<User>(Constants.userClass);
                var __ExistingObject = _Collection.FindOne(_SelectQuery);
                if (__ExistingObject != null)
                {
                    // If existing already in the database  Update MainDb                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                     return the existing object
                    using (intellidatev2Entities _MainDB = new intellidatev2Entities())
                    {
                        using (var transaction = new TransactionScope())
                        {

                            in_user_mst _UserObj
                                = _MainDB.in_user_mst.SingleOrDefault(c => c.UserId == UserRefID);
                            _UserObj.Status = Constants.inActiveStatus;
                            _MainDB.SaveChanges();
                            try
                            {
                                //Update CachingDB 
                                User _CachingObject = new User();
                                var _UpdateQuery = Query.And(Query.EQ(Constants.refId, UserRefID));
                                var _Update = Update.Set(Constants.status, Constants.inActiveStatus);
                                var _sortBy = SortBy.Descending(Constants.refId);
                                var _Result = _Collection.FindAndModify(_UpdateQuery, _sortBy, _Update, true);
                                transaction.Complete();
                                return true;
                            }
                            catch (Exception)
                            {
                                
                                throw;
                            }
                            
                        }
                    };
                }
                else
                {
                    return false;
                }

                
            }
            catch(Exception ex)
            {
                new Error().LogError(ex, Constants.userClass, Constants.deleteUserMethod);
                return false;
            }
        }




        /// <summary>
        /// Activating single user based on userid it  will update the status as Active mode.
        /// while user clicks the activation link this method will call
        /// the admin also able to activate the users
        /// </summary>
        /// <param name="UserRedfID">The ID from the MainDB</param>
        /// <returns>true or false</returns>
        public bool ReActivateUser(int UserRefID)
        {
            try
            {
                MongoDatabase _CachingDatabase = CachingDbConnector.GetCachingDatabase();
                //Here we are not deleting the user we just updates the status of user A(Active) to I(Inactive)
                var _SelectQuery = Query<User>.EQ(e => e._RefID, UserRefID);
                var _Collection = _CachingDatabase.GetCollection<User>(Constants.userClass);
                var _ExistingObject = _Collection.FindOne(_SelectQuery);
                if (_ExistingObject != null)
                {
                    // If existing already in the database  Update MainDb                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                     return the existing object
                    using (intellidatev2Entities _MainDB = new intellidatev2Entities())
                    {
                        using (var transaction = new TransactionScope())
                        {
                            in_user_mst _UserObj = _MainDB.in_user_mst.SingleOrDefault(c => c.UserId == UserRefID);
                            _UserObj.Status = Constants.activeStatus;
                            _MainDB.SaveChanges();
                            try
                            {
                                //Update CachingDB 
                                User _CachingObject = new User();
                                var _UpdateQuery = Query.And(Query.EQ(Constants.refId, UserRefID));
                                var _Update = Update.Set(Constants.status, Constants.activeStatus);
                                var _SortBy = SortBy.Descending(Constants.refId);
                                var _Result = _Collection.FindAndModify(_UpdateQuery, _SortBy, _Update, true);
                                transaction.Complete();
                                return true;
                            }
                            catch (Exception exception)
                            {
                                new Error().LogError(exception, Constants.userClass, Constants.deleteUserMethod);
                                return false;
                            }
                           
                        }
                    };
                }
                else
                {
                    return false;
                }


            }
            catch (Exception ex)
            {
                new Error().LogError(ex, Constants.userClass, Constants.deleteUserMethod);
                return false;
            }
        }




        /// <summary>
        /// validate email address is unique for each user 
        /// </summary>
        /// <param name="EmailAddress">The EmailAddress from the cache db user collection</param>
        /// <param name="_RefID">The _RefID from the cache db user collection</param>
        /// <param name="type">The type from the cache db user collection type = "C"  for create user method and type="U" for update user method</param>
        /// <returns>true or false</returns>
        public bool CheckEmailAddress(string EmailAddress, int _RefID,string _Type)
        {
            try
            {
                //while user update details it will tack email and refid of user
                if (_Type == "U")
                {
                    MongoDatabase _CachingDatabase = CachingDbConnector.GetCachingDatabase();
                    //just create a query
                    var query = Query.And(Query<User>.NE(e => e._RefID, _RefID), Query<User>.EQ(e => e.EmailAddress, EmailAddress));
                    var collection = _CachingDatabase.GetCollection<User>(Constants.userClass);
                    var _ExistingObject = collection.Find(query);
                    if (_ExistingObject.Count() < 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                    //while new user cretion it will not tacks refid it checks only email id of existing users
                else
                {
                    MongoDatabase _CachingDatabase = CachingDbConnector.GetCachingDatabase();
                    //just create a query
                    var query = Query<User>.EQ(e => e.EmailAddress, EmailAddress);
                    var collection = _CachingDatabase.GetCollection<User>(Constants.userClass);
                    var _ExistingObject = collection.Find(query);
                    if (_ExistingObject.Count() < 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                new Error().LogError(ex, Constants.userClass, Constants.checkEmailAddressMethod);
                return false;
            }
        }

        public bool CheckEmailAddress(string EmailAddress)
        {
            try
            {
                MongoDatabase _CachingDatabase = CachingDbConnector.GetCachingDatabase();
                //just create a query
                var query = Query.And(Query<User>.Matches(e => e.EmailAddress, EmailAddress));
                var collection = _CachingDatabase.GetCollection<User>(Constants.userClass);
                var _ExistingObject = collection.Find(query);
                if (_ExistingObject.Count() > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                new Error().LogError(ex, Constants.userClass, Constants.checkEmailAddressMethod);
                return false;
            }
        }

         


        /// <summary>
        /// validate User Name is unique for each user 
        /// </summary>
        /// <param name="UserName">The loginName from the cache db user collection</param>
        /// <returns>true or false</returns>
        public bool CheckUserName(string UserName)
        {
            try
            {
               
                    MongoDatabase _CachingDatabase = CachingDbConnector.GetCachingDatabase();
                    //just create a query
                    var query = Query<User>.EQ(e => e.LoginName, UserName);
                    var collection = _CachingDatabase.GetCollection<User>(Constants.userClass);
                    var _ExistingObject = collection.Find(query);
                    if (_ExistingObject.Count() >0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
            }
            catch (Exception ex)
            {
                new Error().LogError(ex, Constants.userClass, Constants.checkUserNameMethod);
                return false;
            }
        } 



        /// <summary>
        /// Returns the User Details
        /// </summary>
        /// <param name="UserID">The ID from the MainDB</param>
        /// <returns>it returns the user collection from cache db</returns>
        public User GetUserDetails(int UserID)
        {
            try
            {
                //getting data from caching db
                MongoDatabase CachingDatabase = CachingDbConnector.GetCachingDatabase();
                var _Query = Query<User>.EQ(e => e._RefID, UserID);
                var _Collection = CachingDatabase.GetCollection<User>(Constants.userClass);
                var __ExistingObject = _Collection.FindOne(_Query);
                return __ExistingObject;
            }
            catch (Exception ex)
            {
                new Error().LogError(ex, Constants.userClass, Constants.getUserDetailsMethod);
                return null;
            }
        }
        
        
        
        
        /// <summary>
        /// Returns the User Password
        /// </summary>
        /// <param name="EmailAddress">The EmailAddress from the MainDB</param>
        /// <returns></returns>
        public string GetPassword(string EmailAddress)
        {

            try
            {
                //connecting to caching db
                MongoDatabase _CachingDatabase =CachingDbConnector.GetCachingDatabase();
                //getting data from caching db
                var _Query = Query<User>.EQ(e => e.EmailAddress, EmailAddress);
                var _Collection = _CachingDatabase.GetCollection<User>(Constants.userClass);
                var __ExistingObject = _Collection.FindOne(_Query);
                if (__ExistingObject != null)
                {
                    return __ExistingObject.Password;
                }
                else
                {
                    return Constants.emptyString;
                }
            }
            catch (Exception ex)
            {
                new Error().LogError(ex, Constants.userClass, Constants.getPasswordMethod);
                return Constants.emptyString;
            }
        }
        
        
        
        
        /// <summary>
        /// ChangeUserPassword  changes the user password
        /// </summary>
        /// <param name="UserID">The UserID from the User</param>
        /// <param name="Password">The Password from the User</param>
        /// <returns>true</returns>
        public bool ChangeUserPassword(int UserID, string Password)
        {
            try
            {
                MongoDatabase _CachingDatabase = CachingDbConnector.GetCachingDatabase();
                // change user password in MainDB and CachingDB
                var _SelectQuery = Query<User>.EQ(e => e._RefID, UserID);
                var _Collection = _CachingDatabase.GetCollection<User>(Constants.userClass);
                var _ExistingObject = _Collection.FindOne(_SelectQuery);
                if (_ExistingObject != null)
                {
                    // If existing already in the database  Update MainDb                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                     return the existing object
                    using (intellidatev2Entities _MainDB = new intellidatev2Entities())
                    {

                        using (var transaction = new TransactionScope())
                        {
                            in_user_mst _UserObj = _MainDB.in_user_mst.SingleOrDefault(c => c.UserId == UserID);
                            _UserObj.Password = Password;
                            _MainDB.SaveChanges();
                            try
                            {

                                //update CachingDB 
                                User _CachingObject = new User();
                                var _UpdateQuery = Query.And(Query.EQ(Constants.refId, UserID));
                                var _Update = Update.Set(Constants.password, Password);
                                var _sortBy = SortBy.Descending(Constants.refId);
                                var _Result = _Collection.FindAndModify(_UpdateQuery, _sortBy, _Update, true);
                                transaction.Complete();
                                return true;
                            }
                            catch (Exception exception)
                            {

                                new Error().LogError(exception, Constants.userClass, Constants.changeUserPasswordMethod);
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
                new Error().LogError(ex, Constants.userClass, Constants.changeUserPasswordMethod);
                return false;
            }
        }
        



        /// <summary>
        /// ChangeUserPassword  changes the user password based on email Address
        /// </summary>
        /// <param name="EmailId">The EmailId from the User</param>
        /// <param name="Password">The Password from the User</param>
        /// <returns>true</returns>
        public bool ChangeUserPasswordBasedOnEmailId(string EmailId, string Password)
        {
            try
            {
                MongoDatabase _CachingDatabase = CachingDbConnector.GetCachingDatabase();
                // change user password in MainDB and CachingDB
                var _SelectQuery = Query<User>.EQ(e => e.EmailAddress, EmailId);
                var _Collection = _CachingDatabase.GetCollection<User>(Constants.userClass);
                var _ExistingObject = _Collection.FindOne(_SelectQuery);
                if (_ExistingObject != null)
                {
                    // If existing already in the database  Update MainDb                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                     return the existing object
                    using (intellidatev2Entities _MainDB = new intellidatev2Entities())
                    {
                        using (var transaction = new TransactionScope())
                        {

                            in_user_mst _UserObj = _MainDB.in_user_mst.SingleOrDefault(c => c.EmailAddress == EmailId);
                            _UserObj.Password = Password;
                            _MainDB.SaveChanges();
                            try
                            {

                                //update CachingDB 
                                User _CachingObject = new User();
                                var _UpdateQuery = Query.And(Query.EQ(Constants.emailAddress, EmailId));
                                var _Update = Update.Set(Constants.password, Password);
                                var _sortBy = SortBy.Descending(Constants.refId);
                                var _Result = _Collection.FindAndModify(_UpdateQuery, _sortBy, _Update, true);
                                transaction.Complete();
                                return true;
                            }
                            catch (Exception exception)
                            {
                                new Error().LogError(exception, Constants.userClass, Constants.changeUserPasswordBasedOnEmailIdMethod);
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
                new Error().LogError(ex, Constants.userClass, Constants.changeUserPasswordBasedOnEmailIdMethod);
                return false;
            }
        }
        
        
        
        /// <summary>
        /// Returns the all activated Users in a Array
        /// </summary>
        /// <returns>User Class sArray</returns>
        public User[] GetAllActiveUsers()
        {
            try
            {
                //connecting to caching db
                MongoDatabase CachingDatabase =CachingDbConnector.GetCachingDatabase();
                //getting all active users data from caching db
                var _Query = Query<User>.EQ(e=>e.Status, Constants.activeStatus);                           
                var _Collection = CachingDatabase.GetCollection<User>(Constants.userClass);
                return _Collection.Find(_Query).SetLimit(10).ToArray();
              
            }
            catch(Exception ex)
            {
                new Error().LogError(ex, Constants.userClass, Constants.getAllActiveUsersMethod);
                return null;
            }
        }




        /// <summary>
        /// Returns the all Users based on loginname as input(like query in sql) with limit
        /// </summary>
        /// <returns>User Class sArray</returns>
        public User[] GetAllUsersByNameSearch(string LoginName)
        {
            try
            {
                //connecting to caching db
                MongoDatabase CachingDatabase = CachingDbConnector.GetCachingDatabase();
                //getting all active users data from caching db
                var _Query = Query.Matches(Constants.loginName, LoginName);
                var _Collection = CachingDatabase.GetCollection<User>(Constants.userClass);
                return _Collection.Find(_Query).SetLimit(10).ToArray();

            }
            catch (Exception ex)
            {
                new Error().LogError(ex, Constants.userClass, Constants.getAllActiveUsersMethod);
                return null;
            }
        }




        /// <summary>
        /// Returns the all De-Activated Users in a Array
        /// </summary>
        /// <returns>User Class sArray</returns>
        public User[] GetAllDeActiveUsers()
        {
            try
            {
                //connecting to caching db
                MongoDatabase CachingDatabase = CachingDbConnector.GetCachingDatabase();
                //getting all de-active users data from caching db
                var _Query = Query<User>.EQ(e => e.Status, Constants.inActiveStatus);
                var _Collection = CachingDatabase.GetCollection<User>(Constants.userClass);
                return _Collection.Find(_Query).ToArray();

            }
            catch (Exception ex)
            {
                new Error().LogError(ex, Constants.userClass, Constants.getAllActiveUsersMethod);
                return null;
            }
        }




        /// <summary>
        /// Returns this mounth adeded Users as an Array
        /// </summary>
        /// <returns>User Class Array</returns>
        public User[] GetThisMonthUsers()
        {

            try
            {
                //Connecting to caching db
                MongoDatabase CachingDatabase =CachingDbConnector.GetCachingDatabase();
                //Getting data from caching db using defferences between currentdate and current month start date
                var _Query = Query<User>.Where(x => x.UserCreatedDate >= DateTime.Today.AddDays(1 - DateTime.Today.Day) & x.UserCreatedDate <= DateTime.Now);
                var _Collection = CachingDatabase.GetCollection<User>(Constants.userClass);
                return _Collection.Find(_Query).ToArray();
            }
            catch (Exception ex)
            {
                new Error().LogError(ex, Constants.userClass, Constants.getThisMonthUsersMethod);
                return null;
            }
        }




        /// <summary>
        /// Returns this Week adeded Users as an Array
        /// </summary>
        /// <returns>User Class Array</returns>
        public User[] GetThisWeekUsers()
        {
            try
            {
                //Connecting to caching db
                MongoDatabase CachingDatabase = CachingDbConnector.GetCachingDatabase();
                //Getting Current week start date
                DateTime _WeekStart = DateTime.Now.AddDays(CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek - DateTime.Now.DayOfWeek);
                //Getting this week users data from caching db using 'deffrences between current week start date and current date
                var _Query = Query<User>.Where(x => x.UserCreatedDate >= _WeekStart & x.UserCreatedDate <= DateTime.Now);
                var _Collection = CachingDatabase.GetCollection<User>(Constants.userClass);
                var _result = _Collection.Find(_Query).ToArray();
                if(_result.Count()>0)
                {
                    return _result;
                }
                else
                {
                    var _Query1 = Query<User>.Where(x => x.UserCreatedDate >= DateTime.Today & x.UserCreatedDate <= DateTime.Now);
                    var _Collection1 = CachingDatabase.GetCollection<User>(Constants.userClass);
                    return _Collection.Find(_Query1).ToArray();
                }

            }
            catch (Exception ex)
            {
                new Error().LogError(ex, Constants.userClass, Constants.getThisWeekUsersMethod);
                return null;
            }
        }




        /// <summary>
        /// Returns today adeded Users as an Array
        /// </summary>
        /// <returns>User Class Array</returns>
        public User[] GetThisDayUsers()
        {
           try
            {
               //Connecting to caching db
                MongoDatabase CachingDatabase = CachingDbConnector.GetCachingDatabase();
               //Getting Todays user details from caching db 
                var _Query = Query<User>.Where(x => x.UserCreatedDate >= DateTime.Today & x.UserCreatedDate <= DateTime.Now);
                var _Collection = CachingDatabase.GetCollection<User>(Constants.userClass);
                return _Collection.Find(_Query).ToArray();
            }
            catch (Exception ex)
            {
                new Error().LogError(ex, Constants.userClass, Constants.getThisDayUsersMethod);
                return null;
            }
        }
        
        
        
        
        /// <summary>
        /// to check the user login credentials 
        /// </summary>
        /// <param name="UserName">The User Name as Login Name</param>
        /// <param name="Password">The User Password</param>
        /// <returns>User Class </returns>
        public User AuthenticateUser(string UserName,string Password)
        {
            try
            {
                //Connecting to caching db
                MongoDatabase CachingDatabase = CachingDbConnector.GetCachingDatabase();
                //Getting user details from caching db 
                var _Query = Query<User>.Where(x => x.LoginName == UserName & x.Password == Password);
                var _Collection = CachingDatabase.GetCollection<User>(Constants.userClass);
                return _Collection.FindOne(_Query);
            }
            catch (Exception ex)
            {
                new Error().LogError(ex, Constants.userClass, Constants.authenticateUserMethod);
                return null;
            }
        }




        /// <summary>
        /// getting remining users data where scroll down 
        /// </summary>
        /// <param name="Count">Count of loaded users</param>
        /// <param name="searchKey">SearchKey input search</param>
        /// <param name="LastDocumentIds">LastDocumentIds lodated document ids</param>
        /// <returns>User Class array</returns>
        public User[]  GetNextScrollDown(int Count, string searchKey,int [] LastDocumentIds)
        {
            try
            {
                //Connecting to caching db
                MongoDatabase CachingDatabase = CachingDbConnector.GetCachingDatabase();
                //Getting user details from caching db 
                var _Query = Query<User>.NotIn( e=>e._RefID, LastDocumentIds);
                var _Collection = CachingDatabase.GetCollection<User>(Constants.userClass);
                return _Collection.Find(_Query).SetLimit(Count).ToArray();
            }
            catch (Exception ex)
            {
                new Error().LogError(ex, Constants.userClass, Constants.authenticateUserMethod);
                return null;
            }
        }




        /// <summary>
        /// Returns usersids based on admin search creteria
        /// </summary>
        /// <returns>int Array list</returns>
        public List<int> GetUsersBasedOnSearch(string SearchData)
        {
            try
            {
                string _Age = string.Empty;
                var _Location = new BsonArray();
                var _Ethnicity = new BsonArray();
                var _Religion = new BsonArray();
                var _Education = new BsonArray();
                var _Children = new BsonArray();
                var _Drink = new BsonArray();
                var _Smoke = new BsonArray();
                var _BodyType = new BsonArray();
                var _Horoscope = new BsonArray();
                List<int> _dinamicData = new List<int>();
                //here we are searching the data based on bson input. 
                BsonDocument document = BsonDocument.Parse(SearchData);
                if (document.Contains("Age"))
                {
                    MongoDatabase CachingDatabase = CachingDbConnector.GetCachingDatabase();
                    _Age = document["Age"].AsString;
                    var _Query = Query<User>.Where(x => x.UserAgeInYears > Convert.ToInt32(_Age.Split('-')[0]) && x.UserAgeInYears < Convert.ToInt32(_Age.Split('-')[1])&& x.Status==Constants.activeStatus);
                    var _Collection = CachingDatabase.GetCollection<User>(Constants.userClass);
                    var array1 = _Collection.Find(_Query);
                    foreach (var x in array1)
                    {
                        _dinamicData.Add(x._RefID);
                    }
                }
                if (document.Contains("Location"))
                {
                    MongoDatabase CachingDatabase = CachingDbConnector.GetCachingDatabase();
                    _Location = document["Location"].AsBsonArray;
                    var _Query = Query.And(Query<UserProfile>.In(e => e.LocationId, BsonArraytoIntArray(_Location)),
                                           Query<UserProfile>.EQ(e => e.Status,Constants.activeStatus));
                       
                    var _Collection = CachingDatabase.GetCollection<UserProfile>(Constants.userProfileClass);
                    var array = _Collection.Find(_Query).ToArray();
                    foreach (var i in array)
                    {
                        _dinamicData.Add(i.UserId);
                    }
                }
                if (document.Contains("Premium"))
                {
                    MongoDatabase CachingDatabase = CachingDbConnector.GetCachingDatabase();
                    var _Query = Query.And(Query<UserProfile>.EQ(e => e.IsUserType, true),
                                           Query<UserProfile>.EQ(e => e.Status,Constants.activeStatus));
                    var _Collection = CachingDatabase.GetCollection<UserProfile>(Constants.userProfileClass);
                    var array = _Collection.Find(_Query).ToArray();
                    foreach (var i in array)
                    {
                        _dinamicData.Add(i.UserId);
                    }
                }
                if (document.Contains("Free"))
                {
                    MongoDatabase CachingDatabase = CachingDbConnector.GetCachingDatabase();
                    var _Query = Query.And(Query<UserProfile>.EQ(e => e.IsUserType, false),
                                           Query<UserProfile>.EQ(e => e.Status,Constants.activeStatus));
                    var _Collection = CachingDatabase.GetCollection<UserProfile>(Constants.userProfileClass);
                    var array = _Collection.Find(_Query).ToArray();
                    foreach (var i in array)
                    {
                        _dinamicData.Add(i.UserId);
                    }
                }
                if (document.Contains("Ethnicity"))
                {
                    MongoDatabase CachingDatabase = CachingDbConnector.GetCachingDatabase();
                    _Ethnicity = document["Ethnicity"].AsBsonArray;
                    var _Query =Query.And(Query<UserProfile>.In(e => e.EthnicityId, BsonArraytoIntArray(_Ethnicity)),
                                          Query<UserProfile>.EQ(e => e.Status,Constants.activeStatus));
                    var _Collection = CachingDatabase.GetCollection<UserProfile>(Constants.userProfileClass);
                    var array = _Collection.Find(_Query).ToArray();
                    foreach (var i in array)
                    {
                        _dinamicData.Add(i.UserId);
                    }
                }
                if (document.Contains("Religion"))
                {
                    MongoDatabase CachingDatabase = CachingDbConnector.GetCachingDatabase();
                    _Religion = document["Religion"].AsBsonArray;
                    var _Query =Query.And(Query<UserProfile>.In(e => e.ReligionId, BsonArraytoIntArray(_Religion)),
                                          Query<UserProfile>.EQ(e => e.Status,Constants.activeStatus));
                    var _Collection = CachingDatabase.GetCollection<UserProfile>(Constants.userProfileClass);
                    var array = _Collection.Find(_Query).ToArray();
                    foreach (var i in array)
                    {
                        _dinamicData.Add(i.UserId);
                    }
                }
                if (document.Contains("Education"))
                {
                    MongoDatabase CachingDatabase = CachingDbConnector.GetCachingDatabase();
                    _Education = document["Education"].AsBsonArray;
                    var _Query =Query.And(Query<UserProfile>.In(e => e.EduId, BsonArraytoIntArray(_Education)),
                                          Query<UserProfile>.EQ(e => e.Status,Constants.activeStatus));
                    var _Collection = CachingDatabase.GetCollection<UserProfile>(Constants.userProfileClass);
                    var array = _Collection.Find(_Query).ToArray();
                    foreach (var i in array)
                    {
                        _dinamicData.Add(i.UserId);
                    }
                }
                if (document.Contains("HaveChildren"))
                {
                    MongoDatabase CachingDatabase = CachingDbConnector.GetCachingDatabase();
                    _Children = document["HaveChildren"].AsBsonArray;
                    var _Query =Query.And(Query<UserProfile>.In(e => e.HaveChildrenId, BsonArraytoIntArray(_Children)),
                                          Query<UserProfile>.EQ(e => e.Status, Constants.activeStatus));
                    var _Collection = CachingDatabase.GetCollection<UserProfile>(Constants.userProfileClass);
                    var array = _Collection.Find(_Query).ToArray();
                    foreach (var i in array)
                    {
                        _dinamicData.Add(i.UserId);
                    }
                }
                if (document.Contains("Drink"))
                {
                    MongoDatabase CachingDatabase = CachingDbConnector.GetCachingDatabase();
                    _Drink = document["Drink"].AsBsonArray;
                    var _Query =Query.And(Query<UserProfile>.In(e => e.DrinkId, BsonArraytoIntArray(_Drink)),
                                          Query<UserProfile>.EQ(e => e.Status, Constants.activeStatus));
                    var _Collection = CachingDatabase.GetCollection<UserProfile>(Constants.userProfileClass);
                    var array = _Collection.Find(_Query).ToArray();
                    foreach (var i in array)
                    {
                        _dinamicData.Add(i.UserId);
                    }
                }
                if (document.Contains("Smoke"))
                {
                    MongoDatabase CachingDatabase = CachingDbConnector.GetCachingDatabase();
                    _Smoke = document["Smoke"].AsBsonArray;
                    var _Query = Query.And(Query<UserProfile>.In(e => e.SmokeId, BsonArraytoIntArray(_Smoke)),
                                           Query<UserProfile>.EQ(e => e.Status, Constants.activeStatus));
                    var _Collection = CachingDatabase.GetCollection<UserProfile>(Constants.userProfileClass);
                    var array = _Collection.Find(_Query).ToArray();
                    foreach (var i in array)
                    {
                        _dinamicData.Add(i.UserId);
                    }
                }
                if (document.Contains("BodyType"))
                {
                    MongoDatabase CachingDatabase = CachingDbConnector.GetCachingDatabase();
                    _BodyType = document["BodyType"].AsBsonArray;
                    var _Query = Query.And(Query<UserProfile>.In(e => e.BodyTypeId, BsonArraytoIntArray(_BodyType)),
                                           Query<UserProfile>.EQ(e => e.Status, Constants.activeStatus));
                    var _Collection = CachingDatabase.GetCollection<UserProfile>(Constants.userProfileClass);
                    var array = _Collection.Find(_Query).ToArray();
                    foreach (var i in array)
                    {
                        _dinamicData.Add(i.UserId);
                    }
                }
                if (document.Contains("Horoscope"))
                {
                    MongoDatabase CachingDatabase = CachingDbConnector.GetCachingDatabase();
                    _Horoscope = document["Horoscope"].AsBsonArray;
                    var _Query =Query.And(Query<UserProfile>.In(e => e.HoroscopeId, BsonArraytoIntArray(_Horoscope)),
                                Query<UserProfile>.EQ(e => e.Status, Constants.activeStatus));
                    var _Collection = CachingDatabase.GetCollection<UserProfile>(Constants.userProfileClass);
                    var array = _Collection.Find(_Query).ToArray();
                    foreach (var i in array)
                    {
                        _dinamicData.Add(i.UserId);
                        //if (_dinamicData.Exists(x => x != i.UserId))
                        //{
                        //    _dinamicData.Add(i.UserId);
                        //}
                    }
                }
                return _dinamicData.Distinct().ToList();
            }
            catch (Exception ex)
            {
                new Error().LogError(ex, Constants.userClass, Constants.getUsersBasedOnSearchMethod);
                return null;
            }

        }



        /// <summary>
        /// converting bson array to int array 
        /// </summary>
        /// <returns>int Array </returns>
        public int [] BsonArraytoIntArray(BsonArray BArray)
        {
            try
            {
                String[] strings = BArray.ToString().Split('[')[1].Split(']')[0].Split(',');
                int[] ints = new int[strings.Length];
                for (int i = 0; i < strings.Length; i++)
                {
                    ints[i] = int.Parse(strings[i]);
                }
                return ints;
            }
            catch(Exception ex)
            {
                new Error().LogError(ex, Constants.userClass, Constants.bsonArraytoIntArrayMethod);
                return null;
            }
        }




        /// <summary>
        /// Returns the all Users based on loginname as input(like query in sql) with out limit
        /// </summary>
        /// <returns>User Class sArray</returns>
        public User[] GetAllUsersByNameSearchNoLimit(string LoginName)
        {
            try
            {
                //connecting to caching db
                MongoDatabase CachingDatabase = CachingDbConnector.GetCachingDatabase();
                //getting all active users data from caching db
                var _Query = Query.Matches(Constants.loginName, LoginName);
                var _Collection = CachingDatabase.GetCollection<User>(Constants.userClass);
                return _Collection.Find(_Query).ToArray();

            }
            catch (Exception ex)
            {
                new Error().LogError(ex, Constants.userClass, Constants.getAllUsersByNameSearchNoLimitMethod);
                return null;
            }
        }

        
    }


}
