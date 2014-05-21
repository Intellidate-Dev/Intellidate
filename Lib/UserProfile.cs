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
    public class UserProfile
    {

        /// <summary>
        /// The UserProfile cache db identifier
        /// </summary>
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }

        /// <summary>
        /// The UserProfile maindb identifier
        /// </summary>
        public int _RefID { get; set; }

        /// <summary>
        /// The User RefId
        /// </summary>
        public int UserId { get; set; }
      
        /// <summary>
        /// The BodyType  RefId
        /// </summary>
        public int BodyTypeId { get; set; }
        
        /// <summary>
        /// The GetBodyType property from the collection or MySQL Database if any.. it returns the user information
        /// </summary>
        public BodyType GetBodyType
        {
            get
            {
                return new BodyType().GetBodyTypeDetailsById(BodyTypeId);
            }
        }

        /// <summary>
        /// The Drink RefID
        /// </summary>
        public int DrinkId { get; set; }
      
        /// <summary>
        /// The GetDrink property from the collection or MySQL Database if any.. it returns the user information
        /// </summary>
        public Drink GetDrink
        {
            get
            {
                return new Drink().GetDrinkDetailsById(DrinkId);
            }
        }

        /// <summary>
        /// The Education RefId
        /// </summary>
        public int EduId { get; set; }
       
        /// <summary>
        /// The GetEducation property from the collection or MySQL Database if any.. it returns the user information
        /// </summary>
        public Education GetEducation
        {
            get
            {
                return new Education().GetEducationDetailsById(EduId);
            }
        }

        /// <summary>
        /// The Ethnicity collection RefID
        /// </summary>
        public int EthnicityId { get; set; }
      
        /// <summary>
        /// The GetEthnicity property from the collection or MySQL Database if any.. it returns the user information
        /// </summary>
        public Ethnicity GetEthnicity
        {
            get
            {
                return new Ethnicity().GetEthnicityById(EthnicityId);
            }
        }

        /// <summary>
        /// The Drug collection RefID
        /// </summary>
        public int DrugId { get; set; }
       
        /// <summary>
        /// The GetDrug property from the collection or MySQL Database if any.. it returns the user information
        /// </summary>
        public Drugs GetDrug
        {
            get
            {
                return new Drugs().GetDrugDetailsById(DrugId);
            }
        }

        /// <summary>
        /// The HaveChildren collection RefID
        /// </summary>
        public int HaveChildrenId { get; set; }
        
        /// <summary>
        /// The GetHaveChildren property from the collection or MySQL Database if any.. it returns the user information
        /// </summary>
        public HaveChildren GetHaveChildren
        {
            get
            {
                return new HaveChildren().GetHaveChildrenDetailsById(HaveChildrenId);
            }
        }

        /// <summary>
        /// The Horoscope collection RefID
        /// </summary>
        public int HoroscopeId { get; set; }
       
        /// <summary>
        /// The GetHoroscope property from the collection or MySQL Database if any.. it returns the user information
        /// </summary>
        public Horoscope GetHoroscope
        {
            get
            {
                return new Horoscope().GetHoroscopeDetailsById(HoroscopeId);
            }
        }

        /// <summary>
        /// The Location collection RefID
        /// </summary>
        public int LocationId { get; set; }
      
        /// <summary>
        /// The GetLocation property from the collection or MySQL Database if any.. it returns the user information
        /// </summary>
        public Location GetLocation
        {
            get
            {
                return new Location().GetLocationsById(LocationId);
            }
        }

        /// <summary>
        /// The Religion RefID
        /// </summary>
        public int ReligionId { get; set; }
  
        /// <summary>
        /// The GetReligion property from the collection or MySQL Database if any.. it returns the user information
        /// </summary>
        public Religion GetReligion
        {
            get
            {
                return new Religion().GetReligionById(ReligionId);
            }
        }

        /// <summary>
        /// The Smoke collection RefId
        /// </summary>
        public int SmokeId { get; set; }
     
        /// <summary>
        /// The GetSmoke property from the collection or MySQL Database if any.. it returns the user information
        /// </summary>
        public Smoke GetSmoke
        {
            get
            {
                return new Smoke().GetSmokeDetailsById(SmokeId);
            }
        }

        /// <summary>
        /// The JobType collection RefId
        /// </summary>
        public int JobId { get; set; }
    
        /// <summary>
        /// The GetJobType property from the collection or MySQL Database if any.. it returns the user information
        /// </summary>
        public JobType GetJobType
        {
            get
            {
                return new JobType().GetJobDetailsById(JobId);
            }
        }

        /// <summary>
        /// The MyOrientation collection _RefId
        /// </summary>
        public int OrientationId { get; set; }
        
        /// <summary>
        /// The GetOrientation property from the collection or MySQL Database if any.. it returns the user information
        /// </summary>
        public MyOrientation GetOrientation
        {
            get
            {
                return new MyOrientation().GetOrientationById(OrientationId);
            }
        }

        /// <summary>
        /// The UserProfile Is Vesible To straight People . 0(false)=not vesible gay or bisexual users to stright people and 1(true)= vesible gay or bisexual users to stright people
        /// </summary>
        public bool IsVesibleToStraightPeople { get; set; }

        /// <summary>
        /// The Weight collection user Weight in Kgs
        /// </summary>
        public decimal UserWeight { get; set; }
       
        /// <summary>
        /// The GetLanguage property from the collection or MySQL Database if any.. it returns the user information
        /// </summary>
        public UserLanguages[] GetLanguages
        {
            get
            {
                return new UserLanguages().GetUserLanguagesById(UserId);
            }
        }

        /// <summary>
        /// The Income  collection RefId
        /// </summary>
        public int IncomeId { get; set; }
        
        /// <summary>
        /// The GetIncome property from the collection or MySQL Database if any.. it returns the user information
        /// </summary>
        public Income GetIncome
        {
            get
            {
                return new Income().GetIncomeDetailsById(IncomeId);
            }
        }

        /// <summary>
        /// The User Height in cms
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// The User Profile created date or edited date
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// The UserProfile last updated date
        /// </summary>
        public DateTime LastEdited { get; set; }

        /// <summary>
        /// The UserProfile Status A = Active and I=Inactive
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// The UserProfile User Type 0(false)=Free users 1(true)=Premium users 
        /// </summary>
        public bool  IsUserType { get; set; }

        /// <summary>
        /// The UserProfile user BloodGroup A,B,AB,O
        /// </summary>
        public string BloodGroup { get; set; }

        /// <summary>
        /// The UserProfile Religion Importance(A static string array) like {Very serious about it, Somewhat serious about it,Not too serious about it}
        /// </summary>
        public string ReligionImportance { get; set; }

        /// <summary>
        /// The UserProfile Horoscope Importance(A static string array) like  {it doesn't matter,it matters a lot,it's fun to think about}
        /// </summary>
        public string HoroscopeImportance { get; set; }




        public bool InsertUserProfile(int UserId, int BodyTypeId, int DrinkId, int EduId, int EthnicityId, int DrugId, int HaveChildrenId, int HoroscopeId, int LocationId, int ReligionId, int SmokeId, int JobId, int OrientationId, decimal Weight, int IncomeId, int Height, string ReligionImportance, string HoroscopeImportance,string BloodGroup,bool IsVesibleToStraightPeople)
        {
            try
            {
                MongoDatabase _CachingDatabase = CachingDbConnector.GetCachingDatabase();
                var _Collection = _CachingDatabase.GetCollection<UserProfile>(Constants.userProfileClass);
                DateTime _CreatedDate = DateTime.Now;
                int _TrnID = 0;

                // insert the user Profile details in the Main DB and Cache DB
                using (intellidatev2Entities _MainDB = new intellidatev2Entities())
                {
                    using (var transaction = new TransactionScope())
                    {
                        in_userprofile_trn _UPObject = new in_userprofile_trn
                        {
                            UserId = UserId,
                            BodyTypeId = BodyTypeId,
                            DrinkId = DrinkId,
                            EduId = EduId,
                            EthnicityId = EthnicityId,
                            DrugId = DrugId,
                            HaveChildrenId = HaveChildrenId,
                            HoroscopeId = HoroscopeId,
                            LocationId = LocationId,
                            ReligionId = ReligionId,
                            SmokeId = SmokeId,
                            JobId = JobId,
                            OrientationId = OrientationId,
                            Weight = Weight,
                            IncomeId = IncomeId,
                            Height = Height,
                            CreatedDate = DateTime.Now.ToUniversalTime(),
                            LastEdited = DateTime.Now.ToUniversalTime(),
                            Status = Constants.activeStatus,
                            IsUserType = false,
                            BloodGroup = BloodGroup,
                            HoroscopeImportance = HoroscopeImportance,
                            ReligionImportance = ReligionImportance
                        };
                        _MainDB.in_userprofile_trn.Add(_UPObject);
                        _MainDB.SaveChanges();
                        _TrnID = _UPObject.ProfileId;
                        try
                        {
                            // Once the insert is done in the MainDB, do the insert on the Caching DB
                            UserProfile _CachingObject = new UserProfile();
                            _CachingObject._RefID = _TrnID;
                            _CachingObject.UserId = UserId;
                            _CachingObject.BodyTypeId = BodyTypeId;
                            _CachingObject.DrinkId = DrinkId;
                            _CachingObject.EduId = EduId;
                            _CachingObject.EthnicityId = EthnicityId;
                            _CachingObject.DrugId = DrugId;
                            _CachingObject.HaveChildrenId = HaveChildrenId;
                            _CachingObject.LocationId = LocationId;
                            _CachingObject.ReligionId = ReligionId;
                            _CachingObject.SmokeId = SmokeId;
                            _CachingObject.JobId = JobId;
                            _CachingObject.OrientationId = OrientationId;
                            _CachingObject.UserWeight = Weight;
                            _CachingObject.IncomeId = IncomeId;
                            _CachingObject.Height = Height;
                            _CachingObject.CreatedDate = DateTime.Now;
                            _CachingObject.LastEdited = DateTime.Now;
                            _CachingObject.Status = Constants.activeStatus;
                            _CachingObject.IsUserType = false;
                            _CachingObject.BloodGroup = BloodGroup;
                            _CachingObject.ReligionImportance = ReligionImportance;
                            _CachingObject.IsVesibleToStraightPeople = IsVesibleToStraightPeople;
                            _CachingObject.HoroscopeImportance = HoroscopeImportance;
                            _Collection.Save(_CachingObject);
                            transaction.Complete();
                            return true;
                        }
                        catch (Exception exception)
                        {
                            new Error().LogError(exception, Constants.userProfileClass, Constants.insertUserProfileMethod);
                            return false;
                        }
                      
                    }
                }
            }
            catch (Exception ex)
            {
                new Error().LogError(ex, Constants.userProfileClass, Constants.insertUserProfileMethod);
                return false;
            }
        }




        public UserProfile GetUserProfile(int UserId)
        {
            try
            {
                //Connecting to caching db
                MongoDatabase _CachingDatabase = CachingDbConnector.GetCachingDatabase();
                var _Query = Query<UserProfile>.Where(x => x.UserId == UserId);
                var _Collection = _CachingDatabase.GetCollection<UserProfile>(Constants.userProfileClass);
                return _Collection.FindOne(_Query);
            }
            catch (Exception ex)
            {
                new Error().LogError(ex, Constants.userProfileClass, Constants.getUserProfileMethod);
                return null;
            }
        }




        public UserProfile UpdateUserProfile(int UserId, int BodyTypeId, int DrinkId, int EduId, int EthnicityId, int DrugId, int HaveChildrenId, int HoroscopeId, int LocationId, int ReligionId, int SmokeId, int JobId, int OrientationId, decimal UserWeight, int IncomeId, int Height, string ReligionImportance, string HoroscopeImportance, string BloodGroup, bool IsVesibleToStraightPeople)
        {
            try
            {
                MongoDatabase _CachingDatabase = CachingDbConnector.GetCachingDatabase();
                var collection = _CachingDatabase.GetCollection<UserProfile>(Constants.userProfileClass);
                //check user is exist or not 
                var _SelectQuery = Query<UserProfile>.EQ(e => e.UserId, UserId);
                var _ExistingObject = collection.FindOne(_SelectQuery);

                if (_ExistingObject != null)
                {
                    UserProfile _CachingObject = new UserProfile();
                    // Edit user details in MainDB and CachingDB
                    // If existing already in the database  Update MainDb                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                     return the existing object
                    using (intellidatev2Entities _MainDB = new intellidatev2Entities())
                    {

                        using (var transaction = new TransactionScope())
                        {
                            in_userprofile_trn _UPObj = _MainDB.in_userprofile_trn.SingleOrDefault(c => c.UserId == UserId);
                            _UPObj.BodyTypeId = BodyTypeId;
                            _UPObj.DrinkId = DrinkId;
                            _UPObj.EduId = EduId;
                            _UPObj.EthnicityId = EthnicityId;
                            _UPObj.DrugId = DrugId;
                            _UPObj.HaveChildrenId = HaveChildrenId;
                            _UPObj.HoroscopeId = HoroscopeId;
                            _UPObj.LocationId = LocationId;
                            _UPObj.ReligionId = ReligionId;
                            _UPObj.SmokeId = SmokeId;
                            _UPObj.JobId = JobId;
                            _UPObj.OrientationId = OrientationId;
                            _UPObj.Weight = UserWeight;
                            _UPObj.IncomeId = IncomeId;
                            _UPObj.Height = Height;
                            _UPObj.HoroscopeImportance = HoroscopeImportance;
                            _UPObj.ReligionImportance = ReligionImportance;
                            _UPObj.BloodGroup = BloodGroup;
                            _UPObj.IsVesibleToStraightPeople = IsVesibleToStraightPeople;
                            _MainDB.SaveChanges();

                            try
                            {
                                //update CachingDB 

                                var _UpdateQuery = Query<UserProfile>.EQ(e => e.UserId, UserId);
                                var _Update = Update.Set(Constants.sUserId, UserId)
                                                    .Set(Constants.bodyTypeId, BodyTypeId)
                                                    .Set(Constants.drinkId, DrinkId)
                                                    .Set(Constants.eduId, EduId)
                                                    .Set(Constants.ethnicityId, EthnicityId)
                                                    .Set(Constants.drugId, DrugId)
                                                    .Set(Constants.haveChildrenId, HaveChildrenId)
                                                    .Set(Constants.locationId, LocationId)
                                                    .Set(Constants.religionId, ReligionId)
                                                    .Set(Constants.smokeId, SmokeId)
                                                    .Set(Constants.jobId, JobId)
                                                    .Set(Constants.orientationId, OrientationId)
                                                    .Set(Constants.incomeId, IncomeId)
                                                    .Set(Constants.userWeight, UserWeight.ToString())
                                                    .Set(Constants.height, Height)
                                                    .Set(Constants.horoscopeImportance, HoroscopeImportance)
                                                    .Set(Constants.religionImportance, ReligionImportance)
                                                    .Set(Constants.bloodGroup, BloodGroup)
                                                    .Set(Constants.isVesibleToStraightPeople, IsVesibleToStraightPeople);
                                var _SortBy = SortBy.Descending(Constants.refId);
                                var _Result = collection.FindAndModify(_UpdateQuery, _SortBy, _Update, true);
                                _CachingObject._RefID = _RefID;
                                _CachingObject.UserId = UserId;
                                _CachingObject.BodyTypeId = BodyTypeId;
                                _CachingObject.DrinkId = DrinkId;
                                _CachingObject.EduId = EduId;
                                _CachingObject.EthnicityId = EthnicityId;
                                _CachingObject.DrugId = DrugId;
                                _CachingObject.HaveChildrenId = HaveChildrenId;
                                _CachingObject.HoroscopeId = HoroscopeId;
                                _CachingObject.LocationId = LocationId;
                                _CachingObject.ReligionId = ReligionId;
                                _CachingObject.SmokeId = SmokeId;
                                _CachingObject.JobId = JobId;
                                _CachingObject.OrientationId = OrientationId;
                                _CachingObject.UserWeight = UserWeight;
                                _CachingObject.IncomeId = IncomeId;
                                _CachingObject.Height = Height;
                                _CachingObject.HoroscopeImportance = HoroscopeImportance;
                                _CachingObject.ReligionImportance = ReligionImportance;
                                _CachingObject.BloodGroup = BloodGroup;
                                _CachingObject.IsVesibleToStraightPeople = IsVesibleToStraightPeople;
                                transaction.Complete();
                                return _CachingObject;
                            }
                            catch (Exception exception)
                            {
                                new Error().LogError(exception, Constants.userProfileClass, Constants.updateUserProfileMethod);
                                return null;
                            }

                            
                        }
                    }
                }
                else
                {
                    return null;
                }
            }

            catch (Exception ex)
            {
                new Error().LogError(ex, Constants.userProfileClass, Constants.updateUserProfileMethod);
                return null;
            }
        }




    }
}
