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
    public class HaveChildren
    {

        /// <summary>
        /// The HaveChildren identifier
        /// </summary>
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }

        public int _RefID { get; set; }
        
        public string Description { get; set; }

        public string Status { get; set; }




        public bool AddHaveChildrenDetails(string description)
        {
            try
            {
                int m_ChildrenRefID = 0;
                // Insert the record into the MainDB
                using (intellidatev2Entities mainDB = new intellidatev2Entities())
                {
                    using (var transaction = new TransactionScope())
                    {
                        in_havechildren_mst mstHaveChildrenObj = new in_havechildren_mst
                        {
                            Description = description,
                            Status = Constants.activeStatus,
                        };
                        mainDB.in_havechildren_mst.Add(mstHaveChildrenObj);
                        mainDB.SaveChanges();
                        m_ChildrenRefID = mstHaveChildrenObj.HaveChildrenId;
                        try
                        {
                            HaveChildren haveChildrenObject = new HaveChildren
                            {
                                _RefID = m_ChildrenRefID,
                                Description = description,
                                Status = Constants.activeStatus
                            };
                            //insert the record into cache db
                            MongoDatabase cachingDataBase = CachingDbConnector.GetCachingDatabase();
                            var m_Collection = cachingDataBase.GetCollection<HaveChildren>(Constants.haveChildrenClass);
                            m_Collection.Save(haveChildrenObject);
                            transaction.Complete();
                            return true;
                        }
                        catch (Exception exception )
                        {
                            new Error().LogError(exception, Constants.haveChildrenClass, Constants.addHaveChildrenDetailsMethod);
                            return false;
                        }
                       
                    }
                }
            }
            catch (Exception ex)
            {
                new Error().LogError(ex, Constants.haveChildrenClass, Constants.addHaveChildrenDetailsMethod);
                return false;
            }
        }




        public HaveChildren[] GetHaveChildrenDetails()
        {
            try
            {
                //connecting to caching db
                MongoDatabase cachingDataBase = CachingDbConnector.GetCachingDatabase();
                //getting all active HaveChildren data from caching db
                var m_Query = Query<HaveChildren>.EQ(e => e.Status, Constants.activeStatus);
                var m_Collection = cachingDataBase.GetCollection<HaveChildren>(Constants.haveChildrenClass);
                return m_Collection.Find(m_Query).ToArray();

            }
            catch (Exception ex)
            {
                new Error().LogError(ex, Constants.haveChildrenClass, Constants.getHaveChildrenDetailsMethod);
                return null;
            }
        }




        public HaveChildren GetHaveChildrenDetailsById(int refId)
        {
            try
            {
                //connecting to caching db
                MongoDatabase cachingDataBase = CachingDbConnector.GetCachingDatabase();
                //getting  HaveChildren data from caching db
                var m_Query = Query<HaveChildren>.EQ(e => e._RefID,refId);
                var m_Collection = cachingDataBase.GetCollection<HaveChildren>(Constants.haveChildrenClass);
                return m_Collection.FindOne(m_Query);

            }
            catch (Exception ex)
            {
                new Error().LogError(ex, Constants.haveChildrenClass, Constants.getHaveChildrenDetailsByIdMethod);
                return null;
            }
        }




        public bool ActivateOrDeactivateHaveChildrenDetails(string status, int refId)
        {
            try
            {
                MongoDatabase cachingDataBase = CachingDbConnector.GetCachingDatabase();
                //Here we are not deleting the HaveChildren we just updates the status of HaveChildren A(Active) to I(Inactive)
                var m_SelectQuery = Query<HaveChildren>.EQ(e => e._RefID, refId);
                var m_Collection = cachingDataBase.GetCollection<HaveChildren>(Constants.haveChildrenClass);
                var m_ExistingObject = m_Collection.FindOne(m_SelectQuery);
                if (m_ExistingObject != null)
                {
                    // If existing already in the database  Update MainDb                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                     return the existing object
                    using (intellidatev2Entities mainDB = new intellidatev2Entities())
                    {
                        using (var transaction = new TransactionScope())
                        {
                            in_havechildren_mst haveChildrenObj = mainDB.in_havechildren_mst.SingleOrDefault(c => c.HaveChildrenId == refId);
                            haveChildrenObj.Status = status;
                            mainDB.SaveChanges();
                            try
                            {
                                //Update CachingDB 
                                HaveChildren cachingObject = new HaveChildren();
                                var m_UpdateQuery = Query.And(Query.EQ(Constants.refId, refId));
                                var m_Update = Update.Set(Constants.status, status);
                                var m_SortBy = SortBy.Descending(Constants.refId);
                                var m_Result = m_Collection.FindAndModify(m_UpdateQuery, m_SortBy, m_Update, true);
                                transaction.Complete();
                                return true;
                            }
                            catch (Exception exception)
                            {
                                new Error().LogError(exception, Constants.haveChildrenClass, Constants.activateOrDeactivateHaveChildrenDetailsMethod);
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
                new Error().LogError(ex, Constants.haveChildrenClass, Constants.activateOrDeactivateHaveChildrenDetailsMethod);
                return false;
            }
        }




        public bool UpdateHaveChildrenDetails(string description, int refId)
        {
            try
            {
                MongoDatabase cachingDataBbase = CachingDbConnector.GetCachingDatabase();
                var m_Query = Query<HaveChildren>.EQ(e => e._RefID, refId);
                var m_Collection = cachingDataBbase.GetCollection<HaveChildren>(Constants.haveChildrenClass);
                var m_ExistingObject = m_Collection.FindOne(m_Query);
                if (m_ExistingObject != null)
                {
                    // If existing already in the database  Update MainDb                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                     return the existing object
                    using (intellidatev2Entities mainDB = new intellidatev2Entities())
                    {
                        using (var transaction = new TransactionScope())
                        {
                            in_havechildren_mst haveChildrenObj = mainDB.in_havechildren_mst.SingleOrDefault(c => c.HaveChildrenId == refId);
                            haveChildrenObj.Description = description;
                            mainDB.SaveChanges();
                            try
                            {
                                //Update CachingDB 
                                HaveChildren cachingObject = new HaveChildren();
                                var m_UpdateQuery = Query.And(Query.EQ(Constants.refId, refId));
                                var m_Update = Update.Set(Constants.desc, description);
                                var m_sortBy = SortBy.Descending(Constants.refId);
                                var m_Result = m_Collection.FindAndModify(m_UpdateQuery, m_sortBy, m_Update, true);
                                transaction.Complete();
                                return true;
                            }
                            catch (Exception exception)
                            {
                                new Error().LogError(exception, Constants.haveChildrenClass, Constants.updateHaveChildrenDetailsMethod);
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
                new Error().LogError(ex, Constants.haveChildrenClass, Constants.updateHaveChildrenDetailsMethod);
                return false;
            }
        }




    }
}
