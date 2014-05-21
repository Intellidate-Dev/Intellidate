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


namespace IntellidateLib
{
    public class UserCriteria
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }

        public string UserID { get; set; }

        public string CriteriaID { get; set; }
        public Criteria Criteria { get { return new Criteria().GetCriteria(this.CriteriaID); } }

        public object UserAnswer { get; set; }

        public double[] PreferenceAnswer { get; set; }

        public decimal Points { get; set; }

        public Criteria GetNextCriteria(string UserID)
        {
            try
            {
                
                Criteria[] _AllCriterias = new Criteria().GetActiveCriterias();

                // First get the answered criteria question by the given user
                
                MongoDatabase _CachingDatabase = CachingDbConnector.GetCachingDatabase();
                var _UserCriteriaCollection = _CachingDatabase.GetCollection<UserCriteria>("UserCriteria");

                var querybyuser = Query<UserCriteria>.Where(x => x.UserID == UserID);
                UserCriteria[] _AnsweredCriterias = _UserCriteriaCollection.Find(querybyuser).ToArray();
                List<string> _AnsweredCriteriaIDs = new List<string>();
                foreach (var _Each in _AnsweredCriterias)
                {
                    _AnsweredCriteriaIDs.Add(_Each.CriteriaID);
                }

                Criteria _UnAnsweredCriteria = _AllCriterias.Where(x => _AnsweredCriteriaIDs.Contains(x._id) == false).FirstOrDefault();

                if (_UnAnsweredCriteria.RangeType == 4)
                {
                    _UnAnsweredCriteria = SetHeightOptions(_UnAnsweredCriteria);
                }
                return _UnAnsweredCriteria;
                
            }
            catch (Exception ex)
            {
                new Error().LogError(ex, "UserCriteria", "GetNextCriteria");
                return null;
            }
        }

        private Criteria SetHeightOptions(Criteria _Input)
        {
            List<string> _Heights = new List<string>();
            for (int i = (int)_Input.MinValue; i <= (int)_Input.MaxValue; i++)
            {
                for (int j = 0; j < 12; j++)
                {
                    _Heights.Add(i.ToString() + "." + j.ToString());
                }
            }

            List<CriteriaOptionIndependent> _Options = new List<CriteriaOptionIndependent>();
            foreach (string _EachHeight in _Heights)
            {
                _Options.Add(new CriteriaOptionIndependent { OptionText = _EachHeight, RefID = 0 });
            }

            _Input.UserOptions = _Options;
            return _Input;
        }

        public Criteria SaveUserCriteria(string UserID, string CriteriaID, object UserAnswer, double[] PreferenceAnswer)
        {
            try
            {
                MongoDatabase _CachingDatabase = CachingDbConnector.GetCachingDatabase();
                var _UserCriteriaCollection = _CachingDatabase.GetCollection<UserCriteria>("UserCriteria");
                UserCriteria _UserCriteria = new UserCriteria
                {
                    CriteriaID = CriteriaID,
                    UserID= UserID,
                    UserAnswer = UserAnswer,
                    PreferenceAnswer = PreferenceAnswer
                };
                _UserCriteriaCollection.Insert(_UserCriteria);

                // Return the next criteria
                Criteria _NextCriteria = GetNextCriteria(UserID);

                return _NextCriteria;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public UserCriteria[] GetUserAnsweredCriterias(string UserID)
        {
            try
            {   
                MongoDatabase _CachingDatabase = CachingDbConnector.GetCachingDatabase();
                var _UserCriteriaCollection = _CachingDatabase.GetCollection<UserCriteria>("UserCriteria");

                var querybyuser = Query<UserCriteria>.Where(x => x.UserID == UserID);
                UserCriteria[] _AnsweredCriterias = _UserCriteriaCollection.Find(querybyuser).ToArray();
                return _AnsweredCriterias;

            }
            catch (Exception ex)
            {
                new Error().LogError(ex, "UserCriteria", "GetUserAnsweredCriterias");
                return null;
            }
        }

        public bool AssignPoints(string UserID, string CriteriaID, decimal Points)
        {
            try
            {
                MongoDatabase _CachingDatabase = CachingDbConnector.GetCachingDatabase();
                var _UserCriteriaCollection = _CachingDatabase.GetCollection<UserCriteria>("UserCriteria");
                var query = Query<UserCriteria>.Where(x => x.CriteriaID == CriteriaID && x.UserID == UserID);
                var update = Update<UserCriteria>.Set(x => x.Points, Points);

                _UserCriteriaCollection.Update(query, update);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        
    }

    
}
