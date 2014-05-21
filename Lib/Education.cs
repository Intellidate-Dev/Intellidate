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
    public class Education
    {


        /// <summary>
        /// The Education identifier
        /// </summary>
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }

        public int _RefID { get; set; }

        public string Qualification { get; set; }

        public string Status { get; set; }




        public bool AddQualification(string qualification)
        {
            try
            {
                int m_EduRefID = 0;
                // Insert the record into the MainDB
                using (intellidatev2Entities mainDB = new intellidatev2Entities())
                {
                    using (var transaction = new TransactionScope())
                    {
                        in_education_mst eduObj = new in_education_mst
                        {
                            Qualification = qualification,
                            Status = Constants.activeStatus,
                        };
                        mainDB.in_education_mst.Add(eduObj);
                        mainDB.SaveChanges();
                        m_EduRefID = eduObj.EduId;
                        try
                        {
                            Education cachingObject = new Education
                            {

                                _RefID = m_EduRefID,
                                Qualification = qualification,
                                Status = Constants.activeStatus
                            };
                            //insert the record into cache db
                            MongoDatabase cachingDataBbase = CachingDbConnector.GetCachingDatabase();
                            var m_Collection = cachingDataBbase.GetCollection<Education>(Constants.educationClass);
                            m_Collection.Save(cachingObject);
                            transaction.Complete();
                            return true;
                        }
                        catch (Exception exception)
                        {
                            new Error().LogError(exception, Constants.educationClass, Constants.addQualificationMethod);
                            return false;
                        }
                        
                    }
                }
            }
            catch (Exception ex)
            {
                new Error().LogError(ex, Constants.educationClass, Constants.addQualificationMethod);
                return false;
            }
        }




        public Education[] GetEducationDetails()
        {
            try
            {
                //connecting to caching db
                MongoDatabase cachingDataBase = CachingDbConnector.GetCachingDatabase();
                //getting all active Education data from caching db
                var m_Query = Query<Education>.EQ(e => e.Status, Constants.activeStatus);
                var m_Collection = cachingDataBase.GetCollection<Education>(Constants.educationClass);
                return m_Collection.Find(m_Query).ToArray();

            }
            catch (Exception ex)
            {
                new Error().LogError(ex, Constants.educationClass, Constants.getEducationDetailsMethod);
                return null;
            }
        }




        public Education GetEducationDetailsById(int refId)
        {
            try
            {
                //connecting to caching db
                MongoDatabase cachingDataBase = CachingDbConnector.GetCachingDatabase();
                //getting all active Education data from caching db
                var m_Query = Query<Education>.EQ(e => e._RefID, refId);
                var m_Collection = cachingDataBase.GetCollection<Education>(Constants.educationClass);
                return m_Collection.FindOne(m_Query);

            }
            catch (Exception ex)
            {
                new Error().LogError(ex, Constants.educationClass, Constants.getEducationDetailsByIdMethod);
                return null;
            }
        }




        public bool ActivateOrDeactivateEducation(string status, int refId)
        {
            try
            {
                MongoDatabase cachingDataBase = CachingDbConnector.GetCachingDatabase();
                //Here we are not deleting the Education we just updates the status of user A(Active) to I(Inactive)
                var m_SelectQuery = Query<Education>.EQ(e => e._RefID, refId);
                var m_Collection = cachingDataBase.GetCollection<Education>(Constants.educationClass);
                var m_ExistingObject = m_Collection.FindOne(m_SelectQuery);
                if (m_ExistingObject != null)
                {
                    // If existing already in the database  Update MainDb                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                     return the existing object
                    using (intellidatev2Entities mainDB = new intellidatev2Entities())
                    {
                        using (var transaction = new TransactionScope())
                        {
                            in_education_mst eduObj = mainDB.in_education_mst.SingleOrDefault(c => c.EduId == refId);
                            eduObj.Status = status;
                            mainDB.SaveChanges();
                            try
                            {
                                //Update CachingDB 
                                Education cachingObject = new Education();
                                var m_UpdateQuery = Query.And(Query.EQ(Constants.refId, refId));
                                var m_Update = Update.Set(Constants.status, status);
                                var m_SortBy = SortBy.Descending(Constants.refId);
                                var m_Result = m_Collection.FindAndModify(m_UpdateQuery, m_SortBy, m_Update, true);
                                transaction.Complete();
                                return true;
                            }
                            catch (Exception exception)
                            {
                                new Error().LogError(exception, Constants.educationClass, Constants.activateOrDeactivateEducationMethod);
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
                new Error().LogError(ex, Constants.educationClass, Constants.activateOrDeactivateEducationMethod);
                return false;
            }
        }




        public bool UpdateEducationDetails(string qualification, int refId)
        {
            try
            {
                MongoDatabase cachingDataBase = CachingDbConnector.GetCachingDatabase();
                //Here we are not deleting the Education we just updates the status of user A(Active) to I(Inactive)
                var m_Query = Query<Education>.EQ(e => e._RefID, refId);
                var m_Collection = cachingDataBase.GetCollection<Education>(Constants.educationClass);
                var m_ExistingObject = m_Collection.FindOne(m_Query);
                if (m_ExistingObject != null)
                {
                    // If existing already in the database  Update MainDb                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                     return the existing object
                    using (intellidatev2Entities mainDB = new intellidatev2Entities())
                    {
                        using (var transaction = new TransactionScope())
                        {
                            in_education_mst educationObj = mainDB.in_education_mst.SingleOrDefault(c => c.EduId == refId);
                            educationObj.Qualification = qualification;
                            mainDB.SaveChanges();
                            try
                            {
                                //Update CachingDB 
                                Education cachingObject = new Education();
                                var m_Newquery = Query.And(Query.EQ(Constants.refId, refId));
                                var m_Update = Update.Set(Constants.qualification, qualification);
                                var m_sortBy = SortBy.Descending(Constants.refId);
                                var m_Result = m_Collection.FindAndModify(m_Newquery, m_sortBy, m_Update, true);
                                transaction.Complete();
                                return true;
                            }
                            catch (Exception exception)
                            {
                                new Error().LogError(exception, Constants.educationClass, Constants.updateEducationDetailsMethod);
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
                new Error().LogError(ex, Constants.educationClass, Constants.updateEducationDetailsMethod);
                return false;
            }
        }




    }
}
