using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using IntellidateLib.DB;
using System.Globalization;
using MongoDB.Bson.Serialization.Attributes;
using System.Transactions;

namespace IntellidateLib
{
    /// <summary>
    /// The ProfileSave defines all the generic properties of the saved profiles 
    /// </summary>
    public class ProfileSave
    {

        /// <summary>
        /// The ProfileSave identifier
        /// </summary>
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }

        // add the properties.
        /// <summary>
        /// The reference ID from the MySQL Database if any..
        /// </summary>
        public int _trnID { get; set; }

        /// <summary>
        /// The User ID from the MySQL Database if any..
        /// </summary>
        public int _RefID { get; set; }
    
        /// <summary>
        /// The another user reference ID from the MySQL Database if any..
        /// </summary>
        public int OtherUserRefID { get; set; }

        /// <summary>
        /// The another user details from User class
        /// </summary>
        public User OtherUser
        {
            get
            {
                try
                {
                    return new User().GetUserDetails(OtherUserRefID);
                }
                catch(Exception ex)
                {
                    new Error().LogError(ex, Constants.userClass, Constants.otherUser);
                    return null;
                }
            }
        }

        /// <summary>
        /// The User Profile Saved Time
        /// </summary>
        public DateTime SavedTime{ get; set; }
        
        /// <summary>
        /// The Status is "A" for Active and "I" for Inactive 
        /// </summary>
        public string Status { get; set; }
       



        /// <summary>
        /// Returns true if User Profile is saved else flase
        /// </summary>
        /// <param name="UserRefID">The LoginID of the user</param>
        /// <param name="OtherUserRefID">The LoginID of the another user(recipient) </param>
        /// <returns>true/false</returns>
        public bool SaveProfile(int UserRefID, int OtherUserRefID)
        {
            try
            {
                MongoDatabase _CachingDatabase =CachingDbConnector.GetCachingDatabase();
                var _newCollection = _CachingDatabase.GetCollection<ProfileSave>(Constants.profileSaveClass);
                DateTime _CreatedDate = DateTime.Now;
                int _TrnID = 0;

                // insert the user Profile details in the Main DB and Cache DB
                using (intellidatev2Entities _MainDB = new intellidatev2Entities())
                {
                    using (var transaction = new TransactionScope())
                    {
                        in_userprofilesave_trn _PSObject = new in_userprofilesave_trn
                        {
                            UserID = UserRefID,
                            SaveUserID = OtherUserRefID,
                            SavedTime = DateTime.Now,
                            Status = Constants.activeStatus
                        };
                        _MainDB.in_userprofilesave_trn.Add(_PSObject);
                        _MainDB.SaveChanges();
                        _TrnID = _PSObject.trnID;

                        try
                        {
                            // Once the insert is done in the MainDB, do the insert on the Caching DB
                            ProfileSave _CachingObject = new ProfileSave();

                            _CachingObject._trnID = _TrnID;
                            _CachingObject._RefID = UserRefID;
                            _CachingObject.OtherUserRefID = OtherUserRefID;
                            _CachingObject.SavedTime = DateTime.Now;
                            _CachingObject.Status = Constants.activeStatus;
                            _newCollection.Save(_CachingObject);
                            transaction.Complete();
                            return true;
                        }
                        catch (Exception exception)
                        {
                            new Error().LogError(exception, Constants.profileSaveClass, Constants.saveProfileMethod);
                            return false;
                        }
                       
                    }
                };
            }
            catch (Exception ex)
            {
                new Error().LogError(ex, Constants.profileSaveClass, Constants.saveProfileMethod);
                return false;
            }
        }




        /// <summary>
        /// Returns true if User Profile is deleted else it returns flase 
        /// here only status is updated A(Active) to (I)Inactive
        /// </summary>
        /// <param name="UserRefID">The LoginID of the user</param>
        /// <param name="OtherUserRefID">The LoginID of the another user(recipient) </param>
        /// <returns>true/false</returns>
        public bool DeleteSavedProfile(int UserRefID, int OtherUserRefID)
        {
            try
            {
                //Connecting to caching db
                MongoDatabase _CachingDatabase = CachingDbConnector.GetCachingDatabase();
                //Here we are not deleting the ProfileSave we just updates the status of user A(Active) to I(Inactive)
                var _SelectQuery = Query<ProfileSave>.Where(e => e._RefID==UserRefID && e.OtherUserRefID==OtherUserRefID);
                var _Collection = _CachingDatabase.GetCollection<ProfileSave>(Constants.profileSaveClass);
                var _ExistingObject = _Collection.FindOne(_SelectQuery);
                if (_ExistingObject != null)
                {
                    // If existing already in the database  Update MainDb                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                     return the existing object
                    using (intellidatev2Entities _MainDB = new intellidatev2Entities())
                    {
                        using (var transaction = new TransactionScope())
                        {
                            in_userprofilesave_trn _UPSObj = _MainDB.in_userprofilesave_trn.SingleOrDefault(c => c.UserID == UserRefID && c.SaveUserID == OtherUserRefID);
                            _UPSObj.Status = Constants.inActiveStatus;
                            _MainDB.SaveChanges();
                            try
                            {
                                //Update CachingDB 
                                User _CachingObject = new User();
                                var _UpdateQuery = Query<ProfileSave>.Where(x => x._RefID == UserRefID && x.OtherUserRefID == OtherUserRefID);
                                var _Update = Update.Set(Constants.status, Constants.inActiveStatus);
                                var _sortBy = SortBy.Descending(Constants.trnId);
                                var _Result = _Collection.FindAndModify(_UpdateQuery, _sortBy, _Update, true);
                                transaction.Complete();
                                return true;
                            }
                            catch (Exception exception)
                            {
                                new Error().LogError(exception, Constants.profileSaveClass, Constants.deleteSavedProfileMethod);
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
                new Error().LogError(ex, Constants.profileSaveClass, Constants.deleteSavedProfileMethod);
                return false;
            }
        }




        /// <summary>
        /// Returns Array of ProfileSave class
        /// </summary>
        /// <param name="RefID">The LoginID of the user</param>
        /// <returns>Array</returns>
        public ProfileSave [] GetAllSavedProfiles(int UserRefID)
        {
            try
            {
                //Connecting to caching db
                MongoDatabase _CachingDatabase = CachingDbConnector.GetCachingDatabase();
                // check if the User email is already existing. and also checks the User's login is alredy existing.
                var _Query = Query<ProfileSave>.Where(x => x._RefID == _RefID && x.Status == Constants.activeStatus);
                 var _Collection = _CachingDatabase.GetCollection<ProfileSave>(Constants.profileSaveClass);
                 return _Collection.Find(_Query).ToArray();              
            }
            catch (Exception ex)
            {
                new Error().LogError(ex, Constants.profileSaveClass, Constants.getAllSavedProfilesMethod);
                return null;
            }
        }

        /// <summary>
        /// Method to return all the profiles saved by me
        /// </summary>
        /// <param name="RefID"></param>
        /// <returns></returns>
        public List<WhoSavedMe> GetSavedProfiles(int RefID)
        {
            try
            {
                List<WhoSavedMe> userLst = new List<WhoSavedMe>();
                //Connecting to caching db
                MongoDatabase cachingDataBase = CachingDbConnector.GetCachingDatabase();
                // check if the User email is already existing. and also checks the User's login is alredy existing.
                var m_Query = Query<ProfileSave>.Where(x => x._RefID == RefID && x.Status == Constants.activeStatus);
                var m_Collection = cachingDataBase.GetCollection<ProfileSave>(Constants.profileSaveClass);
                var m_Result = m_Collection.Find(m_Query).ToArray();
                if(m_Result!=null)
                {
                    foreach(ProfileSave prfSaveObj in m_Result)
                    {
                        User usObj = new User().GetUserDetails(prfSaveObj.OtherUserRefID);
                        UserProfile usProfObj = new UserProfile().GetUserProfile(prfSaveObj.OtherUserRefID);
                        if (usObj != null && usProfObj != null)
                        {
                            WhoSavedMe whoSavedMeObj = new WhoSavedMe();
                            whoSavedMeObj.FullName = usObj.LoginName.ToUpperInvariant();
                            whoSavedMeObj.Age = whoSavedMeObj.GetAge(usObj.DateOfBirth);
                            whoSavedMeObj.LastOnlineTime = usObj.LastOnlineTime;
                            whoSavedMeObj.SavedTime = prfSaveObj.SavedTime;
                            whoSavedMeObj.Religion = new Religion().GetReligionById(usProfObj.ReligionId).ReligionType.ToString();
                            whoSavedMeObj.Location = new Location().GetLocationsById(usProfObj.LocationId).LocationName.ToString();
                            whoSavedMeObj.Job = new JobType().GetJobDetailsById(usProfObj.JobId).JobTitle.ToString();
                            whoSavedMeObj.Height = usProfObj.Height;
                            whoSavedMeObj.MatchPercentage = 75;
                            whoSavedMeObj.CountOfSaved = m_Result.Count();
                            whoSavedMeObj.ViewedTimes = "0";
                            userLst.Add(whoSavedMeObj);
                        }
                    }

                }
                return userLst;
            }
            catch (Exception ex)
            {
                new Error().LogError(ex, Constants.profileSaveClass, Constants.getAllSavedProfilesMethod);
                return null;
            }
        }

        /// <summary>
        /// Method to return the profiles who saved me 
        /// </summary>
        /// <param name="RefID"></param>
        /// <returns></returns>
        public List<WhoSavedMe> WhoSavedMyProfile(int RefID)
        {
            try
            {
                List<WhoSavedMe> userLst = new List<WhoSavedMe>();
                //Connecting to caching db
                MongoDatabase _CachingDatabase = CachingDbConnector.GetCachingDatabase();
                // check if the User email is already existing. and also checks the User's login is alredy existing.
                var m_Query = Query<ProfileSave>.Where(x => x.OtherUserRefID == RefID);
                var m_Collection = _CachingDatabase.GetCollection<ProfileSave>(Constants.profileSaveClass);
                var m_Result= m_Collection.Find(m_Query).ToArray();
                foreach (ProfileSave profSaveObj in m_Result)
                {
                    User usObj = new User().GetUserDetails(profSaveObj._RefID);
                    UserProfile usProfObj = new UserProfile().GetUserProfile(profSaveObj._RefID);
                    if (usObj != null && usProfObj!=null)
                    {
                        WhoSavedMe whoSavedMeObj = new WhoSavedMe();
                        whoSavedMeObj.FullName = usObj.LoginName.ToUpperInvariant();
                        whoSavedMeObj.Age = whoSavedMeObj.GetAge(usObj.DateOfBirth);
                        whoSavedMeObj.LastOnlineTime = usObj.LastOnlineTime;
                        whoSavedMeObj.SavedTime = profSaveObj.SavedTime;
                        whoSavedMeObj.Religion = new Religion().GetReligionById(usProfObj.ReligionId).ReligionType.ToString();
                        whoSavedMeObj.Location = new Location().GetLocationsById(usProfObj.LocationId).LocationName.ToString();
                        whoSavedMeObj.Job = new JobType().GetJobDetailsById(usProfObj.JobId).JobTitle.ToString();
                        whoSavedMeObj.Height = usProfObj.Height;
                        whoSavedMeObj.MatchPercentage = 75;
                        whoSavedMeObj.CountOfSaved = m_Result.Count();
                        whoSavedMeObj.ViewedTimes = "0";
                        userLst.Add(whoSavedMeObj);
                    }
                }

                return userLst;
            }
            catch (Exception ex)
            {
                new Error().LogError(ex, Constants.profileSaveClass, Constants.getWhoSavedMyProfile);
                return null;
            }
        }


        /// <summary>
        /// Returns this mounth Saved rofiles as an Array
        /// </summary>
        /// <returns>ProfileSave Class Array</returns>
        public ProfileSave[] GetThisMonthSavedProfiles()
        {

            try
            {
                //Connecting to caching db
                MongoDatabase CachingDatabase = CachingDbConnector.GetCachingDatabase();
                //Getting data from caching db using defferences between currentdate and current month start date
                var _Query = Query<ProfileSave>.Where(x => x.SavedTime >= DateTime.Today.AddDays(1 - DateTime.Today.Day) & x.SavedTime <= DateTime.Now);
                var _Collection = CachingDatabase.GetCollection<ProfileSave>(Constants.profileSaveClass);
                return _Collection.Find(_Query).ToArray();
            }
            catch (Exception ex)
            {
                new Error().LogError(ex, Constants.profileSaveClass, Constants.getThisMonthSavedProfilesMethod);
                return null;
            }
        }


        /// <summary>
        /// Returns this Week Saved Profiles as an Array
        /// </summary>
        /// <returns>ProfileSave Class Array</returns>
        public ProfileSave[] GetThisWeekSavedProfiles()
        {
            try
            {
                //Connecting to caching db
                MongoDatabase CachingDatabase = CachingDbConnector.GetCachingDatabase();
                //Getting Current week start date
                DateTime _WeekStart = DateTime.Now.AddDays(CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek - DateTime.Now.DayOfWeek);
                //Getting this week saved profiles data from caching db using 'deffrences between current week start date and current date
                var _Query = Query<ProfileSave>.Where(x => x.SavedTime >= _WeekStart & x.SavedTime <= DateTime.Now);
                var _Collection = CachingDatabase.GetCollection<ProfileSave>(Constants.profileSaveClass);
                return _Collection.Find(_Query).ToArray();

            }
            catch (Exception ex)
            {
                new Error().LogError(ex, Constants.profileSaveClass, Constants.getThisWeekSavedProfilesMethod);
                return null;
            }
        }

        /// <summary>
        /// Returns today Saved Profiles as an Array
        /// </summary>
        /// <returns>ProfileSave Class Array</returns>
        public ProfileSave[] GetThisDaySavedProfiles()
        {
            try
            {
                //Connecting to caching db
                MongoDatabase CachingDatabase = CachingDbConnector.GetCachingDatabase();
                //Getting Todays Saved Profiles details from caching db 
                var _Query = Query<ProfileSave>.Where(x => x.SavedTime >= DateTime.Today & x.SavedTime <= DateTime.Now);
                var _Collection = CachingDatabase.GetCollection<ProfileSave>(Constants.profileSaveClass);
                return _Collection.Find(_Query).ToArray();
            }
            catch (Exception ex)
            {
                new Error().LogError(ex, Constants.profileSaveClass, Constants.getThisDaySavedProfilesMethod);
                return null;
            }
        }




    }
}
