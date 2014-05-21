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
    public class UserLanguages
    {

        /// <summary>
        /// The UserLanguages identifier
        /// </summary>
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }

        /// <summary>
        /// The UserLanguages maindb refid
        /// </summary>
        public int _RefID { get; set; }

        /// <summary>
        /// The User id
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// The UserDetails property from the collection or MySQL Database if any.. it returns the user information
        /// </summary>
        public User UserDetails
        {
            get
            {
                return new User().GetUserDetails(UserId);
            }
        }

        /// <summary>
        /// The LanguageId property from the collection or MySQL Database if any.. it returns the user information
        /// </summary>
        public int LanguageId { get; set; }

        /// <summary>
        /// The GetLanguage property from the collection or MySQL Database if any.. it returns the user information
        /// </summary>
        public Language GetLanguage
        {
            get
            {
                return new Language().GetLanguagesById(LanguageId);
            }
        }

        /// <summary>
        /// The Proficiency property from the collection or MySQL Database if any.. it returns the user information
        /// </summary>
        public string Proficiency { get; set; }
        
        /// <summary>
        /// The Status property from the collection or MySQL Database if any.. it returns the user information
        /// </summary>
        public string Status { get; set; }




        public bool AddUserLanguages(int  UserId,int LanguageId,string Proficiency)
        {
            try
            {
                int _LanRefID = 0;
                // Insert the record into the MainDB
                using (intellidatev2Entities _MainDB = new intellidatev2Entities())
                {
                    using (var transaction = new TransactionScope())
                    {
                        in_userlanguage_trn _ULObj = new in_userlanguage_trn
                        {
                            LanguageId = LanguageId,
                            UserId = UserId,
                            Proficiency = Proficiency,
                            Status = Constants.activeStatus,
                        };
                        _MainDB.in_userlanguage_trn.Add(_ULObj);
                        _MainDB.SaveChanges();
                        _LanRefID = _ULObj.trnId;
                        try
                        {
                            UserLanguages _CachingObject = new UserLanguages
                            {

                                _RefID = _LanRefID,
                                LanguageId = LanguageId,
                                UserId = UserId,
                                Proficiency = Proficiency,
                                Status = Constants.activeStatus
                            };
                            //insert the record into cache db
                            MongoDatabase _CachingDatabase = CachingDbConnector.GetCachingDatabase();
                            var _newCollection = _CachingDatabase.GetCollection<UserLanguages>(Constants.userLanguagesClass);
                            _newCollection.Save(_CachingObject);
                            transaction.Complete();
                            return true;
                        }
                        catch (Exception exception)
                        {
                            new Error().LogError(exception, Constants.userLanguagesClass, Constants.addUserLanguagesMethod);
                            return false;
                        }
                        
                    }
                }
            }
            catch (Exception ex)
            {
                new Error().LogError(ex, Constants.userLanguagesClass, Constants.addUserLanguagesMethod);
                return false;
            }
        }




        public UserLanguages[] GetUserLanguagesById(int UserId)
        {
            try
            {
                //connecting to caching db
                MongoDatabase CachingDatabase = CachingDbConnector.GetCachingDatabase();
                //getting all active UserLanguages data from caching db
                var _Query = Query<UserLanguages>.EQ(e => e.UserId, UserId);
                var _Collection = CachingDatabase.GetCollection<UserLanguages>(Constants.userLanguagesClass);
                return _Collection.Find(_Query).ToArray();

            }
            catch (Exception ex)
            {
                new Error().LogError(ex, Constants.userLanguagesClass, Constants.getUserLanguagesByIdMethod);
                return null;
            }
        }




        public bool ActivateOrDeactivateUserLanguages(string Status, int _RefID)
        {
            try
            {
                MongoDatabase _CachingDatabase = CachingDbConnector.GetCachingDatabase();
                
                var _SelectQuery = Query<UserLanguages>.EQ(e => e._RefID, _RefID);
                var _Collection = _CachingDatabase.GetCollection<UserLanguages>(Constants.userLanguagesClass);
                var _ExistingObject = _Collection.FindOne(_SelectQuery);
                if (_ExistingObject != null)
                {
                    // If existing already in the database  Update MainDb                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                     return the existing object
                    using (intellidatev2Entities _MainDB = new intellidatev2Entities())
                    {
                        using (var transaction = new TransactionScope())
                        {
                            in_userlanguage_trn _ULObj = _MainDB.in_userlanguage_trn.SingleOrDefault(c => c.trnId == _RefID);
                            _ULObj.Status = Status;
                            _MainDB.SaveChanges();
                            try
                            {
                                //Update CachingDB 
                                UserLanguages _CachingObject = new UserLanguages();
                                var _UpdateQuery = Query.And(Query.EQ(Constants.refId, _RefID));
                                var _Update = Update.Set(Constants.status, Status);
                                var _SortBy = SortBy.Descending(Constants.refId);
                                var _Result = _Collection.FindAndModify(_UpdateQuery, _SortBy, _Update, true);
                                transaction.Complete();
                                return true;
                            }
                            catch (Exception exception)
                            {
                                new Error().LogError(exception, Constants.userLanguagesClass, Constants.activateOrDeactivateUserLanguagesMethod);
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
                new Error().LogError(ex, Constants.userLanguagesClass, Constants.activateOrDeactivateUserLanguagesMethod);
                return false;
            }
        }




        public bool UpdateUserLanguages(string Proficiency, int _RefID)
        {
            try
            {
                MongoDatabase _CachingDatabase = CachingDbConnector.GetCachingDatabase();

                var _Query = Query<UserLanguages>.EQ(e => e._RefID, _RefID);
                var _Collection = _CachingDatabase.GetCollection<UserLanguages>(Constants.userLanguagesClass);
                var _ExistingObject = _Collection.FindOne(_Query);
                if (_ExistingObject != null)
                {
                    // If existing already in the database  Update MainDb                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                     return the existing object
                    using (intellidatev2Entities _MainDB = new intellidatev2Entities())
                    {
                        using (var transaction = new TransactionScope())
                        {
                            in_userlanguage_trn _ULObj = _MainDB.in_userlanguage_trn.SingleOrDefault(c => c.trnId == _RefID);
                            _ULObj.Proficiency = Proficiency;
                            _MainDB.SaveChanges();

                            //Update CachingDB 
                            UserLanguages _CachingObject = new UserLanguages();
                            var _UpdateQuery = Query.And(Query.EQ(Constants.refId, _RefID));
                            var _Update = Update.Set(Constants.proficiency, Proficiency);
                            var _SortBy = SortBy.Descending(Constants.refId);
                            var _Result = _Collection.FindAndModify(_UpdateQuery, _SortBy, _Update, true);
                            transaction.Complete();
                            return true;
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
                new Error().LogError(ex, Constants.userLanguagesClass, Constants.updateUserLanguagesMethod);
                return false;
            }
        }




    }
}
