using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using IntellidateLib.DB;
using System.Globalization;
using MongoDB.Bson.Serialization.Attributes;
using System.Transactions;
using System.Data;

namespace IntellidateLib
{

    /// <summary>
    ///  The ProfileView class defines the number of views from other users
    /// </summary>
   public  class ProfileView
    {

        /// <summary>
        /// The ProfileView identifier
        /// </summary>
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }
        /// <summary>
        /// The reference ID from the MySQL Database if any..
        /// </summary>
        public int _RefID { get; set; }

        /// <summary>
        /// The User ID from the MySQL Database if any..
        /// </summary>
        public int UserRefID { get; set; }
        /// <summary>
        /// The Other User User ID from the MySQL Database if any..
        /// </summary>
        public int OtherUserRefID { get; set; }
        /// <summary>
        /// The timestamp when the user was viewed a profile
        /// </summary>
        public DateTime TimeStamp { get; set; }

        /// <summary>
        /// The status of the Profile View record. A=Active, I=Inactive
        /// </summary>
        public string Status { get; set; }

        public User UserDetails
        {
            get
            {
                return new User().GetUserDetails(UserRefID);
            }
        }
       
       
       
       
        /// <summary>
        /// AddNewProfileView The method must insert into both MySQL cache the date into MongoDB
        /// </summary>
        ///<returns>true</returns>
        public bool AddNewProfileView(int UserId,int OtherUserId)
        {
            try
            {
                MongoDatabase _CachingDatabase = CachingDbConnector.GetCachingDatabase();
                int _TrnID = 0;
                var _Collection = _CachingDatabase.GetCollection<ProfileView>(Constants.profileViewClass);
                // else insert the user details in the Main DB and Cache DB
                using (intellidatev2Entities _MainDB = new intellidatev2Entities())
                {
                    using (var transaction = new TransactionScope())
                    {
                        in_userprofileview_trn _PVObject = new in_userprofileview_trn
                        {
                            UserID = UserId,
                            OtherUserID = OtherUserId,
                            TimeStamp = DateTime.Now,
                            Status = Constants.activeStatus
                        };
                        _MainDB.in_userprofileview_trn.Add(_PVObject);
                        _MainDB.SaveChanges();
                        _TrnID = _PVObject.trnID;
                        try
                        {
                            // Once the insert is done in the MainDB, do the insert on the Caching DB
                            ProfileView _CachingObject = new ProfileView();

                            _CachingObject._RefID = _TrnID;
                            _CachingObject.UserRefID = UserId;
                            _CachingObject.OtherUserRefID = OtherUserId;
                            _CachingObject.TimeStamp = DateTime.Now;
                            _CachingObject.Status = Constants.activeStatus;
                            _Collection.Save(_CachingObject);
                            transaction.Complete();
                            return true;
                        }
                        catch (Exception exception)
                        {
                            new Error().LogError(exception, Constants.profileViewClass, Constants.addNewProfileViewMethod);
                            return false;
                        }

                     
                    }
                };
            }
            catch(Exception ex)
            {
                new Error().LogError(ex, Constants.profileViewClass, Constants.addNewProfileViewMethod);
                return false;
            }
        }




        /// <summary>
        /// Getting count of viewed profiles from MongoDB
        /// </summary>
        ///<returns>int</returns>
        public int GetNoOfProfileViews(int UserId)
        {
            try
            {
                MongoDatabase _CachingDatabase = CachingDbConnector.GetCachingDatabase();
                // Get number of profile views
                var _Query = Query<ProfileView>.EQ(e => e.UserRefID, UserId);
                var _Collection = _CachingDatabase.GetCollection<ProfileView>(Constants.profileViewClass);
                return Convert.ToInt32(_Collection.Find(_Query).Count());
            }
            catch (Exception ex)
            {
                new Error().LogError(ex, Constants.profileViewClass, Constants.getNoOfProfileViewsMethod);
                return 0;
            }
        }

        public int GetIndividualCount(int UserId,int OtherUserID)
        {
            try
            {
                MongoDatabase _CachingDatabase = CachingDbConnector.GetCachingDatabase();
                // Get number of profile views
                var _Query = Query<ProfileView>.Where(e => e.OtherUserRefID==OtherUserID && e.UserRefID==UserId);
                var _Collection = _CachingDatabase.GetCollection<ProfileView>(Constants.profileViewClass);
                return Convert.ToInt32(_Collection.Find(_Query).Count());
            }
            catch (Exception ex)
            {
                new Error().LogError(ex, Constants.profileViewClass, Constants.getNoOfProfileViewsMethod);
                return 0;
            }
        }

        public ProfileView GetProfileView(int UserId)
        {
            try
            {
                MongoDatabase CachingDatabase = CachingDbConnector.GetCachingDatabase();
                var _Query = Query<ProfileView>.EQ(e => e.OtherUserRefID, UserId);
                var _Collection = CachingDatabase.GetCollection<ProfileView>("ProfileView");
                var __ExistingObject = _Collection.FindOne(_Query);
                return __ExistingObject;
            }
            catch (Exception ex)
            {
                new Error().LogError(ex, Constants.profileViewClass, Constants.getNoOfProfileViewsMethod);
                return null;
            }
        }

        public List<WhoSavedMe> GetWhoViewedMe(int UserId)
        {
            try
            {
                List<WhoSavedMe> lstWhoViewed = new List<WhoSavedMe>();
                MongoDatabase _CachingDatabase = CachingDbConnector.GetCachingDatabase();
                // Get number of profile views
                var m_Query = Query<ProfileView>.EQ(e => e.OtherUserRefID, UserId);
                var m_Collection = _CachingDatabase.GetCollection<ProfileView>(Constants.profileViewClass);
                var m_Result = m_Collection.Find(m_Query).ToArray();

                var counts = from m in m_Result
                             group m by new { m.UserRefID,m.OtherUserRefID} into grp
                             where grp.Count() > 1
                             select grp.Key;

                var result = (from x in m_Result
                      group x by x.UserRefID into g
                      select new
                      {
                          g.Key,
                          timestamp = g.Max(x => x.TimeStamp),
                          g //This will return everything in g
                      });
                DataTable dt = new DataTable();
                dt.Columns.Add("ID");
                dt.Columns.Add("TimeStamp");

                foreach (var m_Item1 in result)
                {
                    DataRow dr = dt.NewRow();
                    dr["ID"] = m_Item1.Key;
                    dr["TimeStamp"] = m_Item1.timestamp;
                    dt.Rows.Add(dr);
                }

                foreach (var m_item in counts)
                {
                    ProfileView profViewObj = new ProfileView().GetProfileView(m_item.UserRefID);
                    User usObj = new User().GetUserDetails(m_item.UserRefID);
                    UserProfile usProfObj = new UserProfile().GetUserProfile(m_item.UserRefID);
                    if (usObj != null && usProfObj != null)
                    {
                        WhoSavedMe whoViewMeObj = new WhoSavedMe();
                        whoViewMeObj.FullName = usObj.LoginName;
                        whoViewMeObj.Age = whoViewMeObj.GetAge(usObj.DateOfBirth);
                        whoViewMeObj.LastOnlineTime = usObj.LastOnlineTime;
                        DataRow dataRow = dt.AsEnumerable().FirstOrDefault(r => Convert.ToInt32(r["ID"]) == Convert.ToInt32(m_item.UserRefID));
                        if (dataRow != null)
                        {
                            whoViewMeObj.SavedTime = Convert.ToDateTime(dataRow.ItemArray[1].ToString());
                        }
                        whoViewMeObj.Religion = new Religion().GetReligionById(usProfObj.ReligionId).ReligionType.ToString();
                        whoViewMeObj.Location = new Location().GetLocationsById(usProfObj.LocationId).LocationName.ToString();
                        whoViewMeObj.Job = new JobType().GetJobDetailsById(usProfObj.JobId).JobTitle.ToString();
                        whoViewMeObj.Height = usProfObj.Height;
                        whoViewMeObj.MatchPercentage = 75;
                        whoViewMeObj.CountOfSaved = counts.Count();
                        whoViewMeObj.ViewedTimes = GetIndividualCount(m_item.UserRefID,m_item.OtherUserRefID).ToString();
                        lstWhoViewed.Add(whoViewMeObj);
                    }
                }
                return lstWhoViewed;
            }
            catch (Exception ex)
            {
                new Error().LogError(ex, Constants.profileViewClass, Constants.getNoOfProfileViewsMethod);
                return null;
            }
        }

        public List<ProfilesViewedByMe> GetMyViewedProfiles(int UserId)
        {
            try
            {
                List<ProfilesViewedByMe> lstWhoViewedByMe = new List<ProfilesViewedByMe>();
                MongoDatabase _CachingDatabase = CachingDbConnector.GetCachingDatabase();
                // Get number of profile views
                var m_Query = Query<ProfileView>.EQ(e => e.UserRefID, UserId);
                var m_Collection = _CachingDatabase.GetCollection<ProfileView>(Constants.profileViewClass);
                var m_Result = m_Collection.Find(m_Query).ToArray();

                foreach (var m_item in m_Result)
                {
                    User usObj = new User().GetUserDetails(m_item.OtherUserRefID);
                    if(usObj!=null)
                    {
                        ProfilesViewedByMe profViewObj = new ProfilesViewedByMe();
                        profViewObj._id = m_item._id;
                        profViewObj.FullName = usObj.LoginName;
                        profViewObj.ImagePath = "";
                        lstWhoViewedByMe.Add(profViewObj);
                    }
                }
                return lstWhoViewedByMe;
            }
            catch (Exception ex)
            {
                new Error().LogError(ex, Constants.profileViewClass, "GetMyViewedProfiles");
                return null;
            }
        }
       
       
       
        /// <summary>
        /// Getting all users viewed profiles 
        /// </summary>
        ///<returns>int</returns>
       public ProfileView [] GetAllProfileViews()
        {
            MongoDatabase _CachingDatabase = CachingDbConnector.GetCachingDatabase();
            // Get number of profile views
            var _Query = Query<ProfileView>.EQ(e => e.Status, Constants.activeStatus);
            var _Collection = _CachingDatabase.GetCollection<ProfileView>(Constants.profileViewClass);
            return _Collection.Find(_Query).ToArray();
        }




       /// <summary>
       /// Returns this mounth Profile Views as an Array
       /// </summary>
       /// <returns>ProfileView Class Array</returns>
       public ProfileView [] GetThisMonthProfileViews()
       {

           try
           {
               //Connecting to caching db
               MongoDatabase CachingDatabase = CachingDbConnector.GetCachingDatabase();
               //Getting data from caching db using defferences between currentdate and current month start date
               var _Query = Query<ProfileView>.Where(x => x.TimeStamp >= DateTime.Today.AddDays(1 - DateTime.Today.Day) & x.TimeStamp <= DateTime.Now);
               var _Collection = CachingDatabase.GetCollection<ProfileView>(Constants.profileViewClass);
               return _Collection.Find(_Query).ToArray();
           }
           catch (Exception ex)
           {
               new Error().LogError(ex, Constants.profileViewClass, Constants.getThisMonthProfileViewsMethod);
               return null;
           }
       }




       /// <summary>
       /// Returns this Week  Profile Views as an Array
       /// </summary>
       /// <returns>ProfileView Class Array</returns>
       public ProfileView[] GetThisWeekProfileViews()
       {
           try
           {
               //Connecting to caching db
               MongoDatabase CachingDatabase = CachingDbConnector.GetCachingDatabase();
               //Getting Current week start date
               DateTime _WeekStart = DateTime.Now.AddDays(CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek - DateTime.Now.DayOfWeek);
               //Getting this week ProfileViews data from caching db using 'deffrences between current week start date and current date
               var _Query = Query<ProfileView>.Where(x => x.TimeStamp >= _WeekStart & x.TimeStamp <= DateTime.Now);
               var _Collection = CachingDatabase.GetCollection<ProfileView>(Constants.profileViewClass);
               return _Collection.Find(_Query).ToArray();

           }
           catch (Exception ex)
           {
               new Error().LogError(ex, Constants.profileViewClass, Constants.getThisWeekProfileViewsMethod);
               return null;
           }
       }




       /// <summary>
       /// Returns today Profile Views as an Array
       /// </summary>
       /// <returns>ProfileView Class Array</returns>
       public ProfileView[] GetThisDayProfileViews()
       {
           try
           {
               //Connecting to caching db
               MongoDatabase CachingDatabase = CachingDbConnector.GetCachingDatabase();
               //Getting Todays ProfileViews from caching db 
               var _Query = Query<ProfileView>.Where(x => x.TimeStamp >= DateTime.Today & x.TimeStamp <= DateTime.Now);
               var _Collection = CachingDatabase.GetCollection<ProfileView>(Constants.profileViewClass);
               return _Collection.Find(_Query).ToArray();
           }
           catch (Exception ex)
           {
               new Error().LogError(ex, Constants.profileViewClass, Constants.getThisDayProfileViewsMethod);
               return null;
           }
       }

       
   }
}
