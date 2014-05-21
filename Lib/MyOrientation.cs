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
    public class MyOrientation
    {

        /// <summary>
        /// The Orientation identifier
        /// </summary>
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }

        public int _RefID { get; set; }

        public string OrientationType { get; set; }

        public string Status { get; set; }




        public bool AddOrientation(string orientationType)
        {
            try
            {
                int m_OrientationRefID = 0;
                // Insert the record into the MainDB
                using (intellidatev2Entities mainDB = new intellidatev2Entities())
                {
                    using (var transaction = new TransactionScope())
                    {

                        in_orientation_mst mstOrientationObject = new in_orientation_mst
                        {
                            OrientationType = orientationType,
                            Status = Constants.activeStatus,
                        };
                        mainDB.in_orientation_mst.Add(mstOrientationObject);
                        mainDB.SaveChanges();
                        m_OrientationRefID = mstOrientationObject.OrientationId;
                        try
                        {
                            MyOrientation cachingObject = new MyOrientation
                            {

                                _RefID = m_OrientationRefID,
                                OrientationType = orientationType,
                                Status = Constants.activeStatus
                            };
                            //insert the record into cache db
                            MongoDatabase cachingDataBase = CachingDbConnector.GetCachingDatabase();
                            var m_Collection = cachingDataBase.GetCollection<MyOrientation>(Constants.orientationClass);
                            m_Collection.Save(cachingObject);
                            transaction.Complete();
                            return true;
                        }
                        catch (Exception exception)
                        {
                            new Error().LogError(exception, Constants.orientationClass, Constants.addOrientationMethod);
                            return false;
                        }
                       
                    }
                }
            }
            catch (Exception ex)
            {
                new Error().LogError(ex, Constants.orientationClass, Constants.addOrientationMethod);
                return false;
            }
        }




        public MyOrientation[] GetOrientation()
        {
            try
            {
                //connecting to caching db
                MongoDatabase cachingDataBase = CachingDbConnector.GetCachingDatabase();
                //getting all active Orientation data from caching db
                var m_Query = Query<MyOrientation>.EQ(e => e.Status, Constants.activeStatus);
                var m_Collection = cachingDataBase.GetCollection<MyOrientation>(Constants.orientationClass);
                return m_Collection.Find(m_Query).ToArray();

            }
            catch (Exception ex)
            {
                new Error().LogError(ex, Constants.orientationClass, Constants.getOrientationMethod);
                return null;
            }
        }




        public MyOrientation GetOrientationById(int refId)
        {
            try
            {
                //connecting to caching db
                MongoDatabase cachingDataBase = CachingDbConnector.GetCachingDatabase();
                
                var m_Query = Query<MyOrientation>.EQ(e => e._RefID, refId);
                var m_Collection = cachingDataBase.GetCollection<MyOrientation>(Constants.orientationClass);
                return m_Collection.FindOne(m_Query);

            }
            catch (Exception ex)
            {
                new Error().LogError(ex, Constants.orientationClass, Constants.getOrientationByIdMethod);
                return null;
            }
        }




        public bool ActivateOrDeactivateOrientation(string status, int refId)
        {
            try
            {
                MongoDatabase cachingDataBase = CachingDbConnector.GetCachingDatabase();
                //Here we are not deleting the Orientation we just updates the status of Orientation A(Active) to I(Inactive)
                var m_SelectQuery = Query<MyOrientation>.EQ(e => e._RefID, refId);
                var m_Collection = cachingDataBase.GetCollection<MyOrientation>(Constants.orientationClass);
                var m_ExistingObject = m_Collection.FindOne(m_SelectQuery);
                if (m_ExistingObject != null)
                {
                    // If existing already in the database  Update MainDb                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                     return the existing object
                    using (intellidatev2Entities mainDB = new intellidatev2Entities())
                    {
                        using (var transaction = new TransactionScope())
                        {

                            in_orientation_mst mstOrientationObject = mainDB.in_orientation_mst.SingleOrDefault(c => c.OrientationId == refId);
                            mstOrientationObject.Status = status;
                            mainDB.SaveChanges();
                            try
                            {
                                //Update CachingDB 
                                MyOrientation cachingObject = new MyOrientation();
                                var m_UpdateQuery = Query.And(Query.EQ(Constants.refId, refId));
                                var m_Update = Update.Set(Constants.status, status);
                                var m_SortBy = SortBy.Descending(Constants.refId);
                                var m_Result = m_Collection.FindAndModify(m_UpdateQuery, m_SortBy, m_Update, true);
                                transaction.Complete();
                                return true;
                            }
                            catch (Exception exception)
                            {
                                new Error().LogError(exception, Constants.orientationClass, Constants.activateOrDeactivateOrientationMethod);
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
                new Error().LogError(ex, Constants.orientationClass, Constants.activateOrDeactivateOrientationMethod);
                return false;
            }
        }




        public bool UpdateOrientation(string orientationType, int refId)
        {
            try
            {
                MongoDatabase cachingDataBase = CachingDbConnector.GetCachingDatabase();
                var m_SelectQuery = Query<MyOrientation>.EQ(e => e._RefID, refId);
                var m_Collection = cachingDataBase.GetCollection<MyOrientation>(Constants.orientationClass);
                var m_ExistingObject = m_Collection.FindOne(m_SelectQuery);
                if (m_ExistingObject != null)
                {
                    // If existing already in the database  Update MainDb                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                     return the existing object
                    using (intellidatev2Entities mainDB = new intellidatev2Entities())
                    {
                        using (var transaction = new TransactionScope())
                        {
                            in_orientation_mst _OrientationObject = mainDB.in_orientation_mst.SingleOrDefault(c => c.OrientationId == refId);
                            _OrientationObject.OrientationType = orientationType;
                            mainDB.SaveChanges();
                            try
                            {
                                //Update CachingDB 
                                MyOrientation cachingObject = new MyOrientation();
                                var m_UpdateQuery = Query.And(Query.EQ(Constants.refId, refId));
                                var m_Update = Update.Set(Constants.orientationType, orientationType);
                                var m_SortBy = SortBy.Descending(Constants.refId);
                                var m_Result = m_Collection.FindAndModify(m_UpdateQuery, m_SortBy, m_Update, true);
                                transaction.Complete();
                                return true;
                            }
                            catch (Exception exception)
                            {
                                new Error().LogError(exception, Constants.orientationClass, Constants.updateOrientationMethod);
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
                new Error().LogError(ex, Constants.orientationClass, Constants.updateOrientationMethod);
                return false;
            }
        }




    }
}
