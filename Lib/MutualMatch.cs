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
    public class MutualMatch
    {
        public string UserID { get; set; }

        public string OtherUserID { get; set; }

        public Criteria Criteria { get; set; }

        public string CriteriaName { get; set; }

        public double[] UserPreference { get; set; }
        public string UserPreferenceText { get; set; }

        public double OtherUserValue { get; set; }
        public string OtherUserValueText { get; set; }

        public decimal PointsAssigned { get; set; }

        public decimal PointsAwarded { get; set; }

        public int MatchSuccess { get; set; }

        public double[] GetUserPreferences(string userId, string criteriaId)
        {
            try
            {
                
                // First get the answered criteria question by the given user

                MongoDatabase cachingDataBase = CachingDbConnector.GetCachingDatabase();
                var m_UserCriteriaCollection = cachingDataBase.GetCollection<UserCriteria>("UserCriteria");
                var m_QueryByUser = Query<UserCriteria>.Where(x => x.UserID == userId && x.CriteriaID==criteriaId);
                var m_Result = m_UserCriteriaCollection.FindOne(m_QueryByUser);
                if (m_Result != null)
                {
                    return m_Result.PreferenceAnswer;
                }
                else
                {
                    return new List<double>().ToArray();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public double GetUserValue(string userId, string criteriaId)
        {
            try
            {

                // First get the answered criteria question by the given user

                MongoDatabase cachingDataBase = CachingDbConnector.GetCachingDatabase();
                var m_UserCriteriaCollection = cachingDataBase.GetCollection<UserCriteria>("UserCriteria");
                var m_QueryByUser = Query<UserCriteria>.Where(x => x.UserID == userId && x.CriteriaID == criteriaId);
                var m_Result = m_UserCriteriaCollection.FindOne(m_QueryByUser);
                if (m_Result != null)
                {
                    return Convert.ToDouble(m_Result.UserAnswer);
                }
                else
                {
                    return (double)0;
                }
            }
            catch (Exception)
            {
                return (double)0;
            }
        }

        public decimal GetPointsAssigned(string userId, string criteriaId)
        {
            try
            {

                // First get the answered criteria question by the given user

                MongoDatabase cachingDataBase = CachingDbConnector.GetCachingDatabase();
                var m_UserCriteriaCollection = cachingDataBase.GetCollection<UserCriteria>("UserCriteria");
                var m_QueryByUser = Query<UserCriteria>.Where(x => x.UserID == userId && x.CriteriaID == criteriaId);
                var m_Result = m_UserCriteriaCollection.FindOne(m_QueryByUser);
                if (m_Result != null)
                {
                    return m_Result.Points;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public string GetPreferenceName(string criteriaId, double preferenceId)
        {
            try
            {

                // First get the answered criteria question by the given user

                MongoDatabase cachingDataBase = CachingDbConnector.GetCachingDatabase();
                var m_CriteriaCollection = cachingDataBase.GetCollection<Criteria>("Criteria");
                var m_Query = Query<Criteria>.Where(x => x._id == criteriaId);
                Criteria _Result = m_CriteriaCollection.FindOne(m_Query);

                List<CriteriaOptionIndependent> lstCriteriaOptions = new List<CriteriaOptionIndependent>();
                lstCriteriaOptions = (List<CriteriaOptionIndependent>)_Result.UserOptions;

                string m_PreferenceText = lstCriteriaOptions.Where(x => x.RefID == preferenceId).Select(x=>x.OptionText).SingleOrDefault();

                return m_PreferenceText;

            }
            catch (Exception)
            {
                return preferenceId.ToString();
            }
        }

        public MutualMatch[] GetMutualMatch(string userId, string otherUserId)
        {
            try
            {
                // Get all criterias
                Criteria[] allCriteriasArray = new Criteria().GetActiveCriterias();

                List<MutualMatch> lstAllMutualMatches = new List<MutualMatch>();

                MutualMatch mutualMatchObj;

                foreach (Criteria eachCriteria in allCriteriasArray)
                {
                    mutualMatchObj = new MutualMatch();

                    mutualMatchObj.CriteriaName = eachCriteria.CriteriaName;

                    // Get User Preferences
                    mutualMatchObj.UserPreference = GetUserPreferences(userId, eachCriteria._id);
                    mutualMatchObj.UserPreferenceText = "";
                    if (eachCriteria.CriteriaType == 1 || eachCriteria.CriteriaType == 2)
                    {
                        foreach (var m_item in mutualMatchObj.UserPreference)
                        {
                            mutualMatchObj.UserPreferenceText = mutualMatchObj.UserPreferenceText + ", " + GetPreferenceName(eachCriteria._id, m_item);
                        }
                        mutualMatchObj.UserPreferenceText = mutualMatchObj.UserPreferenceText.Trim();
                        mutualMatchObj.UserPreferenceText = mutualMatchObj.UserPreferenceText.Substring(1);
                    }
                    else
                    {
                        switch (eachCriteria.RangeType)
                        {
                            case 1:
                                {
                                    mutualMatchObj.UserPreferenceText = "$" + Convert.ToInt32(mutualMatchObj.UserPreference[0]).ToString() + " to $" + Convert.ToInt32(mutualMatchObj.UserPreference[1]).ToString();
                                    break;
                                }
                            case 2:
                                {
                                    mutualMatchObj.UserPreferenceText = Convert.ToInt32(mutualMatchObj.UserPreference[0]).ToString() + " Mile(s) to " + Convert.ToInt32(mutualMatchObj.UserPreference[1]).ToString() +" Mile(s)";
                                    break;
                                }
                            case 3:
                                {
                                    mutualMatchObj.UserPreferenceText = Convert.ToInt32(mutualMatchObj.UserPreference[0]).ToString() + " Year(s) to " + Convert.ToInt32(mutualMatchObj.UserPreference[1]).ToString() + " Year(s)";
                                    break;
                                }
                            case 4:
                                {
                                    mutualMatchObj.UserPreferenceText = mutualMatchObj.UserPreference[0].ToString() + " Feet(s) to " + mutualMatchObj.UserPreference[1].ToString() + " Feet(s)";
                                    break;
                                }
                        }
                        
                        
                    }
                    

                    


                    // Get Other User Preferences
                    mutualMatchObj.OtherUserValue = GetUserValue(otherUserId, eachCriteria._id);
                    mutualMatchObj.OtherUserValueText = GetPreferenceName(eachCriteria._id, mutualMatchObj.OtherUserValue);

                    if (eachCriteria.CriteriaType == 3)
                    {

                        switch (eachCriteria.RangeType)
                        {
                            case 1:
                                {
                                    mutualMatchObj.OtherUserValueText = "$" + mutualMatchObj.OtherUserValueText;
                                    break;
                                }
                            case 2:
                                {
                                    mutualMatchObj.OtherUserValueText = mutualMatchObj.OtherUserValueText + " Mile(s)";
                                    break;
                                }
                            case 3:
                                {
                                    mutualMatchObj.OtherUserValueText = mutualMatchObj.OtherUserValueText + " Year(s)";
                                    break;
                                }
                            case 4:
                                {
                                    mutualMatchObj.OtherUserValueText = mutualMatchObj.OtherUserValueText + " Feet(s)";
                                    break;
                                }
                        }
                    }


                    // points assigned 
                    mutualMatchObj.PointsAssigned = GetPointsAssigned(userId, eachCriteria._id);


                    if (eachCriteria.CriteriaType == 1 || eachCriteria.CriteriaType == 2)
                    {
                        if (mutualMatchObj.UserPreference.Contains(mutualMatchObj.OtherUserValue))
                        {
                            mutualMatchObj.PointsAwarded = mutualMatchObj.PointsAssigned;
                            mutualMatchObj.MatchSuccess = 1;
                        }
                        else
                        {
                            mutualMatchObj.MatchSuccess = 0;
                        }
                    }
                    else
                    {
                        // Height
                        if (eachCriteria.RangeType > 0)
                        {
                            if (mutualMatchObj.OtherUserValue >= mutualMatchObj.UserPreference[0] && mutualMatchObj.OtherUserValue <= mutualMatchObj.UserPreference[1])
                            {
                                mutualMatchObj.PointsAwarded = mutualMatchObj.PointsAssigned;
                                mutualMatchObj.MatchSuccess = 1;
                            }
                            else
                            {
                                mutualMatchObj.MatchSuccess = 0;
                            }
                        }
                    }
                

                    lstAllMutualMatches.Add(mutualMatchObj);
                }

                lstAllMutualMatches = lstAllMutualMatches.OrderByDescending(x => x.PointsAssigned).ToList();
                return lstAllMutualMatches.ToArray();
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
