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
using System.Data.Common;
using System.Transactions;
using System.Data;

namespace IntellidateLib
{
    public class JobType
    {

        /// <summary>
        /// The JobType identifier
        /// </summary>
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }

        public int _RefID { get; set; }

        public string JobTitle { get; set; }

        public string Status { get; set; }




        public bool AddJobDetails(string jobTitle)
        {
            try
            {
                MongoDatabase cachingDataBase = CachingDbConnector.GetCachingDatabase();
                var m_Collection = cachingDataBase.GetCollection<JobType>(Constants.jobTypeClass);
                int m_JobRefId = 0;
                bool m_Res = false;
                // Insert the record into the MainDB
            
                using (intellidatev2Entities mainDB = new intellidatev2Entities())
                {
                    using (var transaction = new TransactionScope())
                        {
                            in_jobtype_mst mstJobTypeObj = new in_jobtype_mst
                            {
                                JobTitle = jobTitle,
                                Status = Constants.activeStatus,
                            };
                            mainDB.in_jobtype_mst.Add(mstJobTypeObj);
                            mainDB.SaveChanges();
                            m_JobRefId = mstJobTypeObj.JobId;
                            try
                            {
                                JobType cachingObject = new JobType
                                {
                                    _RefID = m_JobRefId,
                                    JobTitle = jobTitle,
                                    Status = Constants.activeStatus
                                };
                                //insert the record into cache db
                                m_Collection.Save(cachingObject);
                                transaction.Complete();
                                return true;
                            }
                            catch (Exception exception)
                            {
                                new Error().LogError(exception, Constants.jobTypeClass, Constants.addJobDetailsMethod);
                                return false;
                            }
                         
                        }
                }
            }
            catch (Exception ex)
            {
                new Error().LogError(ex, Constants.jobTypeClass, Constants.addJobDetailsMethod);
                return false;
            }
        }




        public JobType[] GetJobDetails()
        {
            try
            {
                //connecting to caching db
                MongoDatabase cachingDataBase = CachingDbConnector.GetCachingDatabase();
                //getting all active JobType data from caching db
                var m_Query = Query<JobType>.EQ(e => e.Status, Constants.activeStatus);
                var m_Collection = cachingDataBase.GetCollection<JobType>(Constants.jobTypeClass);
                return m_Collection.Find(m_Query).ToArray();

            }
            catch (Exception ex)
            {
                new Error().LogError(ex, Constants.jobTypeClass, Constants.getJobDetailsMethod);
                return null;
            }
        }




        public JobType GetJobDetailsById(int refId)
        {
            try
            {
                //connecting to caching db
                MongoDatabase cachingDataBase = CachingDbConnector.GetCachingDatabase();
                var m_Query = Query<JobType>.EQ(e => e._RefID, refId);
                var m_Collection = cachingDataBase.GetCollection<JobType>(Constants.jobTypeClass);
                return m_Collection.FindOne(m_Query);

            }
            catch (Exception ex)
            {
                new Error().LogError(ex, Constants.jobTypeClass, Constants.getJobDetailsByIdMethod);
                return null;
            }
        }




        public bool ActivateOrDeactivateJobDetails(string status, int refId)
        {
            try
            {
                MongoDatabase cachingDataBase = CachingDbConnector.GetCachingDatabase();
                //Here we are not deleting the JobType we just updates the status of JobType A(Active) to I(Inactive)
                var m_SelectQuery = Query<JobType>.EQ(e => e._RefID, refId);
                var m_Collection = cachingDataBase.GetCollection<JobType>(Constants.jobTypeClass);
                var m_ExistingObject = m_Collection.FindOne(m_SelectQuery);

             
                if (m_ExistingObject != null)
                {
                    // If existing already in the database  Update MainDb                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                     return the existing object
                    using (intellidatev2Entities mainDB = new intellidatev2Entities())
                    {
                        using (var transaction = new TransactionScope())
                        {
                            in_jobtype_mst mstJobTypeObj = mainDB.in_jobtype_mst.SingleOrDefault(c => c.JobId == refId);
                            mstJobTypeObj.Status = status;
                            mainDB.SaveChanges();                    
                            try
                            {
                                //Update CachingDB 
                                JobType cachingObject = new JobType();
                                var m_UpdateQuery = Query.And(Query.EQ(Constants.refId, refId));
                                var m_Update = Update.Set(Constants.status, status);
                                var m_SortBy = SortBy.Descending(Constants.refId);
                                var m_Result = m_Collection.FindAndModify(m_UpdateQuery, m_SortBy, m_Update, true);
                                transaction.Complete();
                                return true;
                            }
                            catch (Exception exception)
                            {
                                new Error().LogError(exception, Constants.jobTypeClass, Constants.activateOrDeactivateJobDetailsMethod);
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
                new Error().LogError(ex, Constants.jobTypeClass, Constants.activateOrDeactivateJobDetailsMethod);
                return false;
            }
        }




        public bool UpdateJobDetails(string jobTitle, int refId)
        {
            try
            {
                MongoDatabase cachingDataBase = CachingDbConnector.GetCachingDatabase();
                var m_SelectQuery = Query<JobType>.EQ(e => e._RefID, refId);
                var m_Collection = cachingDataBase.GetCollection<JobType>(Constants.jobTypeClass);
                var m_ExistingObject = m_Collection.FindOne(m_SelectQuery);
                if (m_ExistingObject != null)
                {
                    // If existing already in the database  Update MainDb                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                     return the existing object
                    using (intellidatev2Entities mainDB = new intellidatev2Entities())
                    {
                        using (var transaction = new TransactionScope())
                        {
                            in_jobtype_mst mstJobTypeObj = mainDB.in_jobtype_mst.SingleOrDefault(c => c.JobId == refId);
                            mstJobTypeObj.JobTitle = jobTitle;
                            mainDB.SaveChanges();
                            try
                            {
                                //Update CachingDB 
                                JobType cachingObject = new JobType();
                                var m_UpdateQuery = Query.And(Query.EQ(Constants.refId, refId));
                                var m_Update = Update.Set(Constants.jobTitle, jobTitle);
                                var m_SortBy = SortBy.Descending(Constants.refId);
                                var m_Result = m_Collection.FindAndModify(m_UpdateQuery, m_SortBy, m_Update, true);
                                transaction.Complete();
                                return true;
                            }
                            catch (Exception exception)
                            {
                                new Error().LogError(exception, Constants.jobTypeClass, Constants.updateJobDetailsMethod);
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
                new Error().LogError(ex, Constants.jobTypeClass, Constants.updateJobDetailsMethod);
                return false;
            }
        }




    }
}
