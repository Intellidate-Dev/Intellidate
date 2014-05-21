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
    public class Criteria
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }

        public int RefID { get; set; }

        public string CriteriaName { get; set; }

        public string OptionQuestion { get; set; }

        public string PreferenceQuestion { get; set; }

        public int CriteriaType { get; set; }

        public object UserOptions { get; set; }

        public object PreferenceOptions { get; set; }

        public double MinValue { get; set; }

        public double MaxValue { get; set; }

        public int RangeType { get; set; }

        // For the criteria results page
        public string MismatchText { get; set; }

        // For the criteria results page
        public bool ShowMatch { get; set; }

        public bool IncludeAllInPreference { get; set; }

        public string IncludeAllInPreferenceText { get; set; }

        public bool HideFromOtherUserPreferences { get; set; }

        public Criteria GetCriteria(string criteriaId)
        {
            try
            {
                MongoDatabase cachingDataBase = CachingDbConnector.GetCachingDatabase();
                var m_CriteriaCollection = cachingDataBase.GetCollection<Criteria>("Criteria");
                var m_Query = Query<Criteria>.EQ(x => x._id, criteriaId);
                var m_Criteria = m_CriteriaCollection.FindOne(m_Query);
                return m_Criteria;
            }
            catch (Exception ex)
            {
                new Error().LogError(ex, "Criteria", "GetCriteria");
                return null;
            }
        }

        public Criteria[] GetActiveCriterias()
        {
            try
            {
                MongoDatabase cachingDataBase = CachingDbConnector.GetCachingDatabase();
                var m_CriteriaCollection = cachingDataBase.GetCollection<Criteria>("Criteria");

                var m_Criterias = m_CriteriaCollection.FindAll().ToArray();
                return m_Criterias;
            }
            catch (Exception ex)
            {
                new Error().LogError(ex, "Criteria", "GetActiveCriterias");
                return null;
            }
        }

        public Criteria AddNewCriteria(string criteriaName, string optionQuestion, string  preferenceQuestion, int criteriaType, string[] options, string mismatchText, bool includeAllInPreference, string includeAllInPreferenceText, bool showMatch, out int errorCode)
        {
            try
            {
                errorCode = 0;

                MongoDatabase cachingDataBase = CachingDbConnector.GetCachingDatabase();
                var m_CriteriaCollection = cachingDataBase.GetCollection<Criteria>("Criteria");


                // Check is the Criteria already exists
                var m_CheckQuery = Query<Criteria>.Where(x => x.CriteriaName.ToUpperInvariant() == criteriaName.ToUpper());
                var m_ExistingObject = m_CriteriaCollection.FindOne(m_CheckQuery);
                if (m_ExistingObject != null)
                {
                    errorCode = 1;
                    return null;
                }

                int m_RefID = 0;
                List<CriteriaOptionIndependent> criteriaOptionIndependent = new List<CriteriaOptionIndependent>();
                using (intellidatev2Entities mainDB = new intellidatev2Entities())
                {
                    using (var transaction = new TransactionScope())
                    {
                        in_criteria_mst newCriteria = new in_criteria_mst
                        {
                            criterianame = criteriaName,
                            criteriatype = criteriaType,
                            optionquestion = optionQuestion,
                            preferencequestion = preferenceQuestion,
                            includeallInpreference = includeAllInPreference,
                            MismatchText = mismatchText,
                            ShowMatch = showMatch,
                            timestamp = DateTime.Now,
                            status = Constants.activeStatus,
                            includeallInpreferencetext = includeAllInPreferenceText
                        };
                        mainDB.in_criteria_mst.Add(newCriteria);
                        mainDB.SaveChanges();
                        m_RefID = newCriteria.criteriaid;

                        // Create Options

                        int m_OptionRefId = 0;
                        if (options.Length > 0)
                        {
                            foreach (string m_EachOption in options)
                            {
                                in_criteriaoptions_trn criteriaOption = new in_criteriaoptions_trn
                                {
                                    optiontext = m_EachOption,
                                    status = Constants.activeStatus,
                                    criteriaid = m_RefID
                                };
                                mainDB.in_criteriaoptions_trn.Add(criteriaOption);
                                mainDB.SaveChanges();
                                m_OptionRefId = criteriaOption.trnid;
                                criteriaOptionIndependent.Add(new CriteriaOptionIndependent { OptionText = m_EachOption, RefID = m_OptionRefId });
                            }
                        }


                        Criteria criteria = new Criteria
                        {
                            CriteriaName = criteriaName,
                            RefID = m_RefID,
                            OptionQuestion = optionQuestion,
                            PreferenceQuestion = preferenceQuestion,
                            CriteriaType = criteriaType,
                            UserOptions = criteriaOptionIndependent,
                            PreferenceOptions = criteriaOptionIndependent,
                            MismatchText = mismatchText,
                            ShowMatch = showMatch,
                            IncludeAllInPreference = includeAllInPreference,
                            IncludeAllInPreferenceText = includeAllInPreferenceText
                        };

                        m_CriteriaCollection.Insert(criteria);
                        transaction.Complete();
                        return criteria;
                    }
                };
            }
            catch (Exception ex)
            {
                new Error().LogError(ex, "Criteria", "AddNewCriteria");
                errorCode = -1;
                return null;
            }
        }


        public Criteria AddNewCriteria(string criteriaName, string optionQuestion, string preferenceQuestion, int criteriaType, int rangeType, double minValue, double maxValue, string mismatchText, bool showMatch, out int errorCode)
        {
            try
            {
                errorCode = 0;

                MongoDatabase cachingDataBase = CachingDbConnector.GetCachingDatabase();
                var m_CriteriaCollection = cachingDataBase.GetCollection<Criteria>("Criteria");


                // Check is the Criteria already exists
                var m_CheckQuery = Query<Criteria>.Where(x => x.CriteriaName.ToUpperInvariant() == criteriaName.ToUpper());
                var m_ExistingObject = m_CriteriaCollection.FindOne(m_CheckQuery);
                if (m_ExistingObject != null)
                {
                    errorCode = 1;
                    return null;
                }

                int m_RefID = 0;
                List<CriteriaOptionIndependent> criteriaOptionIndependent = new List<CriteriaOptionIndependent>();
                using (intellidatev2Entities mainDB = new intellidatev2Entities())
                {
                    using (var transaction = new TransactionScope())
                    {
                        in_criteria_mst newCriteria = new in_criteria_mst
                        {
                            criterianame = criteriaName,
                            criteriatype = criteriaType,
                            optionquestion = optionQuestion,
                            preferencequestion = preferenceQuestion,
                            MismatchText = mismatchText,
                            ShowMatch = showMatch,
                            timestamp = DateTime.Now,
                            status = Constants.activeStatus

                        };
                        mainDB.in_criteria_mst.Add(newCriteria);
                        mainDB.SaveChanges();
                        m_RefID = newCriteria.criteriaid;

                        try
                        {
                            // Create Options


                            Criteria criteria = new Criteria
                            {
                                CriteriaName = criteriaName,
                                RefID = m_RefID,
                                OptionQuestion = optionQuestion,
                                PreferenceQuestion = preferenceQuestion,
                                CriteriaType = criteriaType,
                                MismatchText = mismatchText,
                                ShowMatch = showMatch,
                                MinValue = minValue,
                                MaxValue = maxValue,
                                RangeType = rangeType
                            };

                            m_CriteriaCollection.Insert(criteria);
                            transaction.Complete();
                            return criteria;
                        }
                        catch (Exception exception)
                        {
                            new Error().LogError(exception, "Criteria", "AddNewCriteria");
                            errorCode = -1;
                            return null;
                        }
                       
                    }
                };
            }
            catch (Exception ex)
            {
                new Error().LogError(ex, "Criteria", "AddNewCriteria");
                errorCode = -1;
                return null;
            }
        }
    }

    public class CriteriaOptionIndependent
    {
        public int RefID { get; set; }

        public string OptionText { get; set; }

    }

}
