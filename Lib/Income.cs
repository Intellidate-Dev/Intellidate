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
    public class Income
    {

        /// <summary>
        /// The Income identifier
        /// </summary>
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }

        public int _RefID { get; set; }

        public string Salary { get; set; }

        public string Status { get; set; }




        public bool AddIncomeDetails(string salary)
        {
            try
            {
                int m_IncomeRefID = 0;
                // Insert the record into the MainDB
                using (intellidatev2Entities mainDB = new intellidatev2Entities())
                {
                    using (var transaction = new TransactionScope())
                    {
                        in_income_mst mstIncomeObj = new in_income_mst
                        {
                            Salary = salary,
                            Status = Constants.activeStatus,
                        };
                        mainDB.in_income_mst.Add(mstIncomeObj);
                        mainDB.SaveChanges();
                        m_IncomeRefID = mstIncomeObj.IncomeId;
                        try
                        {
                            Income cachingObject = new Income
                            {

                                _RefID = m_IncomeRefID,
                                Salary = salary,
                                Status = Constants.activeStatus
                            };
                            //insert the record into cache db
                            MongoDatabase cachingDataBase = CachingDbConnector.GetCachingDatabase();
                            var m_Collection = cachingDataBase.GetCollection<Income>(Constants.incomeClass);
                            m_Collection.Save(cachingObject);
                            transaction.Complete();
                            return true;
                        }
                        catch (Exception exception)
                        {
                            new Error().LogError(exception, Constants.incomeClass, Constants.addIncomeDetailsMethod);
                            return false;
                        }
                        
                    }
                }
            }
            catch (Exception ex)
            {
                new Error().LogError(ex, Constants.incomeClass, Constants.addIncomeDetailsMethod);
                return false;
            }
        }




        public Income[] GetIncomeDetails()
        {
            try
            {
                //connecting to caching db
                MongoDatabase m_CachingDataBase = CachingDbConnector.GetCachingDatabase();
                //getting all active income data from caching db
                var m_Query = Query<Income>.EQ(e => e.Status, Constants.activeStatus);
                var m_Collection = m_CachingDataBase.GetCollection<Income>(Constants.incomeClass);
                return m_Collection.Find(m_Query).ToArray();

            }
            catch (Exception ex)
            {
                new Error().LogError(ex, Constants.incomeClass, Constants.getIncomeDetailsMethod);
                return null;
            }
        }




        public Income GetIncomeDetailsById(int refId)
        {
            try
            {
                //connecting to caching db
                MongoDatabase cachingDataBase = CachingDbConnector.GetCachingDatabase();
                
                var m_Query = Query<Income>.EQ(e => e._RefID, refId);
                var m_Collection = cachingDataBase.GetCollection<Income>(Constants.incomeClass);
                return m_Collection.FindOne(m_Query);

            }
            catch (Exception ex)
            {
                new Error().LogError(ex, Constants.incomeClass, Constants.getIncomeDetailsByIdMethod);
                return null;
            }
        }




        public bool ActivateOrDeactivateIncomeDetails(string status, int refId)
        {
            try
            {
                MongoDatabase cachingDataBase = CachingDbConnector.GetCachingDatabase();
                //Here we are not deleting the Income we just updates the status of Income A(Active) to I(Inactive)
                var m_Query = Query<Income>.EQ(e => e._RefID, refId);
                var m_Collection = cachingDataBase.GetCollection<Income>(Constants.incomeClass);
                var m_ExistingObject = m_Collection.FindOne(m_Query);
                if (m_ExistingObject != null)
                {
                   
                    // If existing already in the database  Update MainDb                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                     return the existing object
                    using (intellidatev2Entities mainDB = new intellidatev2Entities())
                    {
                        using (var transaction = new TransactionScope())
                        {
                            in_income_mst mstIncomeObj = mainDB.in_income_mst.SingleOrDefault(c => c.IncomeId == refId);
                            mstIncomeObj.Status = status;
                            mainDB.SaveChanges();
                            try
                            {
                                //Update CachingDB 
                                Income cachingObject = new Income();
                                var m_UpdateQuery = Query.And(Query.EQ(Constants.refId, refId));
                                var m_Update = Update.Set(Constants.status, status);
                                var m_SortBy = SortBy.Descending(Constants.refId);
                                var m_Result = m_Collection.FindAndModify(m_UpdateQuery, m_SortBy, m_Update, true);
                                transaction.Complete();
                                return true;
                            }
                            catch (Exception exception)
                            {
                                new Error().LogError(exception, Constants.incomeClass, Constants.activateOrDeactivateIncomeDetailsMethod);
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
                new Error().LogError(ex, Constants.incomeClass, Constants.activateOrDeactivateIncomeDetailsMethod);
                return false;
            }
        }




        public bool UpdateIncomeDetails(string salary, int refId)
        {
            try
            {
                MongoDatabase cachingDataBase = CachingDbConnector.GetCachingDatabase();
                var m_SelectQuery = Query<Income>.EQ(e => e._RefID, refId);
                var m_Collection = cachingDataBase.GetCollection<Income>(Constants.incomeClass);
                var m_ExistingObject = m_Collection.FindOne(m_SelectQuery);
                if (m_ExistingObject != null)
                {
                    // If existing already in the database  Update MainDb                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                     return the existing object
                    using (intellidatev2Entities mainDB = new intellidatev2Entities())
                    {
                        using (var transaction = new TransactionScope())
                        {
                            in_income_mst mstIncomeObj = mainDB.in_income_mst.SingleOrDefault(c => c.IncomeId == refId);
                            mstIncomeObj.Salary = salary;
                            mainDB.SaveChanges();
                            try
                            {
                                //Update CachingDB 
                                Income cachingObject = new Income();
                                var m_UpdateQuery = Query.And(Query.EQ(Constants.refId, refId));
                                var m_Update = Update.Set(Constants.salary, salary);
                                var m_SortBy = SortBy.Descending(Constants.refId);
                                var m_Result = m_Collection.FindAndModify(m_UpdateQuery, m_SortBy, m_Update, true);
                                transaction.Complete();
                                return true;
                            }
                            catch (Exception exception)
                            {
                                new Error().LogError(exception, Constants.incomeClass, Constants.UpdateIncomeDetailsMethod);
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
                new Error().LogError(ex, Constants.incomeClass, Constants.UpdateIncomeDetailsMethod);
                return false;
            }
        }




    }
}
