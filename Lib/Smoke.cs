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
    public class Smoke
    {

        /// <summary>
        /// The Smoke identifier
        /// </summary>
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }

        public int _RefID { get; set; }

        public string SmokeDetails { get; set; }

        public string Status { get; set; }




        public bool AddSmokeDetails(string SmokeDetails)
        {
            try
            {
                int _SmokeRefID = 0;
                // Insert the record into the MainDB
                using (intellidatev2Entities _MainDB = new intellidatev2Entities())
                {
                    using (var transaction = new TransactionScope())
                    {
                        in_smoke_mst _SmokeObj = new in_smoke_mst
                        {
                            SmokeDetails = SmokeDetails,
                            Status = Constants.activeStatus,
                        };
                        _MainDB.in_smoke_mst.Add(_SmokeObj);
                        _MainDB.SaveChanges();
                        _SmokeRefID = _SmokeObj.SmokeId;
                        try
                        {
                            Smoke _CachingObject = new Smoke
                            {

                                _RefID = _SmokeRefID,
                                SmokeDetails = SmokeDetails,
                                Status = Constants.activeStatus
                            };
                            //insert the record into cache db
                            MongoDatabase _CachingDatabase = CachingDbConnector.GetCachingDatabase();
                            var _newCollection = _CachingDatabase.GetCollection<Smoke>(Constants.smokeClass);
                            _newCollection.Save(_CachingObject);
                            transaction.Complete();
                            return true;
                        }
                        catch (Exception exception)
                        {
                            new Error().LogError(exception, Constants.smokeClass, Constants.addSmokeDetailsMethod);
                            return false;
                        }
                       
                    }
                }
            }
            catch (Exception ex)
            {
                new Error().LogError(ex, Constants.smokeClass, Constants.addSmokeDetailsMethod);
                return false;
            }
        }




        public Smoke[] GetSmokeDetails()
        {
            try
            {
                //connecting to caching db
                MongoDatabase CachingDatabase = CachingDbConnector.GetCachingDatabase();
                //getting all active Smoke data from caching db
                var _Query = Query<Smoke>.EQ(e => e.Status, Constants.activeStatus);
                var _Collection = CachingDatabase.GetCollection<Smoke>(Constants.smokeClass);
                return _Collection.Find(_Query).ToArray();

            }
            catch (Exception ex)
            {
                new Error().LogError(ex, Constants.smokeClass, Constants.getSmokeDetailsMethod);
                return null;
            }
        }




        public Smoke GetSmokeDetailsById(int _RefID)
        {
            try
            {
                //connecting to caching db
                MongoDatabase CachingDatabase = CachingDbConnector.GetCachingDatabase();
                
                var _Query = Query<Smoke>.EQ(e => e._RefID, _RefID);
                var _Collection = CachingDatabase.GetCollection<Smoke>(Constants.smokeClass);
                return _Collection.FindOne(_Query);

            }
            catch (Exception ex)
            {
                new Error().LogError(ex, Constants.smokeClass, Constants.getSmokeDetailsByIdMethod);
                return null;
            }
        }




        public bool UpdateSmokeDetails(string SmokeDetails, int _RefID)
        {
            try
            {
                MongoDatabase _CachingDatabase = CachingDbConnector.GetCachingDatabase();
               
                var _SelectQuery = Query<Smoke>.EQ(e => e._RefID, _RefID);
                var _Collection = _CachingDatabase.GetCollection<Smoke>(Constants.smokeClass);
                var _ExistingObject = _Collection.FindOne(_SelectQuery);
                if (_ExistingObject != null)
                {
                    // If existing already in the database  Update MainDb                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                     return the existing object
                    using (intellidatev2Entities _MainDB = new intellidatev2Entities())
                    {
                        using (var transaction = new TransactionScope())
                        {
                            in_smoke_mst _SmokeObj = _MainDB.in_smoke_mst.SingleOrDefault(c => c.SmokeId == _RefID);
                            _SmokeObj.SmokeDetails = SmokeDetails;
                            _MainDB.SaveChanges();
                            try
                            {
                                //Update CachingDB 
                                Smoke _CachingObject = new Smoke();
                                var _UpdateQuery = Query.And(Query.EQ(Constants.refId, _RefID));
                                var _Update = Update.Set(Constants.smokeDetails, SmokeDetails);
                                var _sortBy = SortBy.Descending(Constants.refId);
                                var _Result = _Collection.FindAndModify(_UpdateQuery, _sortBy, _Update, true);
                                transaction.Complete();
                                return true;
                            }
                            catch (Exception exception)
                            {
                                new Error().LogError(exception, Constants.smokeClass, Constants.updateSmokeDetailsMethod);
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
                new Error().LogError(ex, Constants.smokeClass, Constants.updateSmokeDetailsMethod);
                return false;
            }
        }




        public bool ActivateOrDeactivateSmokeDetails(string Status, int _RefID)
        {
            try
            {
                MongoDatabase _CachingDatabase = CachingDbConnector.GetCachingDatabase();
                //Here we are not deleting the Smoke we just updates the status of Smoke A(Active) to I(Inactive)
                var _SelectQuery = Query<Smoke>.EQ(e => e._RefID, _RefID);
                var _Collection = _CachingDatabase.GetCollection<Smoke>(Constants.smokeClass);
                var _ExistingObject = _Collection.FindOne(_SelectQuery);
                if (_ExistingObject != null)
                {
                    // If existing already in the database  Update MainDb                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                     return the existing object
                    using (intellidatev2Entities _MainDB = new intellidatev2Entities())
                    {
                        using (var transaction = new TransactionScope())
                        {
                            in_smoke_mst _SmokeObj = _MainDB.in_smoke_mst.SingleOrDefault(c => c.SmokeId == _RefID);
                            _SmokeObj.Status = Status;
                            _MainDB.SaveChanges();

                            try
                            {
                                //Update CachingDB 
                                Smoke _CachingObject = new Smoke();
                                var _UpdateQuery = Query.And(Query.EQ(Constants.refId, _RefID));
                                var _Update = Update.Set(Constants.status, Status);
                                var _sortBy = SortBy.Descending(Constants.refId);
                                var _Result = _Collection.FindAndModify(_UpdateQuery, _sortBy, _Update, true);
                                transaction.Complete();
                                return true;
                            }
                            catch (Exception exception)
                            {
                                new Error().LogError(exception, Constants.smokeClass, Constants.activateOrDeactivateSmokeDetailsMethod);
                                return false;
                            }

                        }
                    };
                }
                else
                {
                    return false;
                }


            }
            catch (Exception ex)
            {
                new Error().LogError(ex, Constants.smokeClass, Constants.activateOrDeactivateSmokeDetailsMethod);
                return false;
            }
        }




    }
}
