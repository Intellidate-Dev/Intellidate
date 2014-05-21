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
    public  class Language
    {

        /// <summary>
        /// The Language identifier
        /// </summary>
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }

        public int _RefID { get; set; }

        public string LanguageName { get; set; }

        public string Status { get; set; }




        public bool AddLanguage(string languageName)
        {
            try
            {
                int m_LanRefID = 0;
                // Insert the record into the MainDB
                using (intellidatev2Entities mainDB = new intellidatev2Entities())
                {
                    using (var transaction = new TransactionScope())
                    {
                        in_language_mst mstLanguageObj = new in_language_mst
                        {
                            LanguageName = languageName,
                            Status = Constants.activeStatus,
                        };
                        mainDB.in_language_mst.Add(mstLanguageObj);
                        mainDB.SaveChanges();
                        m_LanRefID = mstLanguageObj.LanguageId;

                        try
                        {
                            Language cachingObject = new Language
                            {

                                _RefID = m_LanRefID,
                                LanguageName = languageName,
                                Status = Constants.activeStatus
                            };
                            //insert the record into cache db
                            MongoDatabase cachingDataBase = CachingDbConnector.GetCachingDatabase();
                            var m_Collection = cachingDataBase.GetCollection<Language>(Constants.languageClass);
                            m_Collection.Save(cachingObject);
                            transaction.Complete();
                            return true;
                        }
                        catch (Exception exception)
                        {
                            new Error().LogError(exception, Constants.languageClass, Constants.addLanguageMethod);
                            return false;
                        }
                        
                    }
                }
            }
            catch (Exception ex)
            {
                new Error().LogError(ex, Constants.languageClass, Constants.addLanguageMethod);
                return false;
            }
        }




        public Language[] GetLanguages()
        {
            try
            {
                //connecting to caching db
                MongoDatabase cachingDataBase = CachingDbConnector.GetCachingDatabase();
                //getting all active Language data from caching db
                var m_Query = Query<Language>.EQ(e => e.Status, Constants.activeStatus);
                var m_Collection = cachingDataBase.GetCollection<Language>(Constants.languageClass);
                return m_Collection.Find(m_Query).ToArray();

            }
            catch (Exception ex)
            {
                new Error().LogError(ex, Constants.languageClass, Constants.getLanguagesMethod);
                return null;
            }
        }




        public Language GetLanguagesById(int refId)
        {
            try
            {
                //connecting to caching db
                MongoDatabase cachingDataBase = CachingDbConnector.GetCachingDatabase();
               
                var m_Query = Query<Language>.EQ(e => e._RefID, refId);
                var m_Collection = cachingDataBase.GetCollection<Language>(Constants.languageClass);
                return m_Collection.FindOne(m_Query);

            }
            catch (Exception ex)
            {
                new Error().LogError(ex, Constants.languageClass, Constants.getLanguagesByIdMethod);
                return null;
            }
        }





        public bool ActivateOrDeactivateLanguage(string status, int refId)
        {
            try
            {
                MongoDatabase cachingDataBase = CachingDbConnector.GetCachingDatabase();
                //Here we are not deleting the Language we just updates the status of Language A(Active) to I(Inactive)
                var m_SelectQuery = Query<Language>.EQ(e => e._RefID, refId);
                var m_Collection = cachingDataBase.GetCollection<Language>(Constants.languageClass);
                var m_ExistingObject = m_Collection.FindOne(m_SelectQuery);
                if (m_ExistingObject != null)
                {
                    // If existing already in the database  Update MainDb                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                     return the existing object
                    using (intellidatev2Entities mainDB = new intellidatev2Entities())
                    {
                        using (var transaction = new TransactionScope())
                        {
                            in_language_mst mstLanguageObj = mainDB.in_language_mst.SingleOrDefault(c => c.LanguageId == refId);
                            mstLanguageObj.Status = status;
                            mainDB.SaveChanges();
                            try
                            {
                                //Update CachingDB 
                                Language cachingObject = new Language();
                                var m_UpdateQuery = Query.And(Query.EQ(Constants.refId, refId));
                                var m_Update = Update.Set(Constants.status, status);
                                var m_sortBy = SortBy.Descending(Constants.refId);
                                var m_Result = m_Collection.FindAndModify(m_UpdateQuery, m_sortBy, m_Update, true);
                                transaction.Complete();
                                return true;
                            }
                            catch (Exception exception)
                            {
                                new Error().LogError(exception, Constants.languageClass, Constants.activateOrDeactivateLanguageMethod);
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
                new Error().LogError(ex, Constants.languageClass, Constants.activateOrDeactivateLanguageMethod);
                return false;
            }
        }




        public bool UpdateLanguage(string languageName, int refId)
        {
            try
            {
                MongoDatabase cachingDataBase = CachingDbConnector.GetCachingDatabase();

                var m_SelectQuery = Query<Language>.EQ(e => e._RefID, refId);
                var m_Collection = cachingDataBase.GetCollection<Language>(Constants.languageClass);
                var m_ExistingObject = m_Collection.FindOne(m_SelectQuery);
                if (m_ExistingObject != null)
                {
                    // If existing already in the database  Update MainDb                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                     return the existing object
                    using (intellidatev2Entities mainDB = new intellidatev2Entities())
                    {
                        using (var transaction = new TransactionScope())
                        {
                            in_language_mst mstLanguageObj = mainDB.in_language_mst.SingleOrDefault(c => c.LanguageId == refId);
                            mstLanguageObj.LanguageName = languageName;
                            mainDB.SaveChanges();
                            try
                            {
                                //Update CachingDB 
                                Language cachingObject = new Language();
                                var m_UpdateQuery = Query.And(Query.EQ(Constants.refId, refId));
                                var m_Update = Update.Set(Constants.languageName, languageName);
                                var m_SortBy = SortBy.Descending(Constants.refId);
                                var m_Result = m_Collection.FindAndModify(m_UpdateQuery, m_SortBy, m_Update, true);
                                transaction.Complete();
                                return true;
                            }
                            catch (Exception exception)
                            {
                                new Error().LogError(exception, Constants.languageClass, Constants.updateLanguagesMethod);
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
                new Error().LogError(ex, Constants.languageClass, Constants.updateLanguagesMethod);
                return false;
            }
        }




    }
}
