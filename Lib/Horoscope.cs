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
    public class Horoscope
    {

        /// <summary>
        /// The Horoscope identifier
        /// </summary>
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }

        public int _RefID { get; set; }

        public string Description { get; set; }

        public string Status { get; set; }




        public bool AddHoroscopeDetails(string Description)
        {
            try
            {
               
                int _HoroscopeRefID = 0;
                // Insert the record into the MainDB
                using (intellidatev2Entities _MainDB = new intellidatev2Entities())
                {
                    using (var transaction = new TransactionScope())
                    {
                        in_horoscope_mst _NewGender = new in_horoscope_mst
                        {
                            Description = Description,
                            Status = Constants.activeStatus,
                        };
                        _MainDB.in_horoscope_mst.Add(_NewGender);
                        _MainDB.SaveChanges();
                        _HoroscopeRefID = _NewGender.HoroscopeId;
                        try
                        {
                            Horoscope _CachingObj = new Horoscope
                            {

                                _RefID = _HoroscopeRefID,
                                Description = Description,
                                Status = Constants.activeStatus
                            };
                            //insert the record into cache db
                            MongoDatabase _CachingDatabase = CachingDbConnector.GetCachingDatabase();
                            var _Collection = _CachingDatabase.GetCollection<Horoscope>(Constants.horoscopeClass);
                            _Collection.Save(_CachingObj);
                            transaction.Complete();
                            return true;
                        }
                        catch (Exception exception)
                        {
                            new Error().LogError(exception, Constants.horoscopeClass, Constants.addHoroscopeDetailsMethod);
                            return false;
                        }
                       
                    }
                }
            }
            catch (Exception ex)
            {
                new Error().LogError(ex, Constants.horoscopeClass, Constants.addHoroscopeDetailsMethod);
                return false;
            }
        }




        public Horoscope[] GetHoroscopeDetails()
        {
            try
            {
                //connecting to caching db
                MongoDatabase _CachingDatabase = CachingDbConnector.GetCachingDatabase();
                //getting all active Horoscope data from caching db
                var _Query = Query<Horoscope>.EQ(e => e.Status, Constants.activeStatus);
                var _Collection = _CachingDatabase.GetCollection<Horoscope>(Constants.horoscopeClass);
                return _Collection.Find(_Query).ToArray();

            }
            catch (Exception ex)
            {
                new Error().LogError(ex, Constants.horoscopeClass, Constants.getHoroscopeDetailsMethod);
                return null;
            }
        }




        public Horoscope GetHoroscopeDetailsById(int _RefID)
        {
            try
            {
                //connecting to caching db
                MongoDatabase _CachingDatabase = CachingDbConnector.GetCachingDatabase();
                //getting all active Horoscope data from caching db
                var _Query = Query<Horoscope>.EQ(e => e._RefID, _RefID);
                var _Collection = _CachingDatabase.GetCollection<Horoscope>(Constants.horoscopeClass);
                return _Collection.FindOne(_Query);

            }
            catch (Exception ex)
            {
                new Error().LogError(ex, Constants.horoscopeClass, Constants.getHoroscopeDetailsByIdMethod);
                return null;
            }
        }




        public bool ActivateOrDeactivateHoroscopeDetails(string Status, int _RefID)
        {
            try
            {
                MongoDatabase _CachingDatabase = CachingDbConnector.GetCachingDatabase();
                //Here we are not deleting the Horoscope we just updates the status of Horoscope A(Active) to I(Inactive)
                var _SelectQuery = Query<Horoscope>.EQ(e => e._RefID, _RefID);
                var collection = _CachingDatabase.GetCollection<Horoscope>(Constants.horoscopeClass);
                var _ExistingObject = collection.FindOne(_SelectQuery);
                if (_ExistingObject != null)
                {
                    // If existing already in the database  Update MainDb                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                     return the existing object
                    using (intellidatev2Entities _MainDB = new intellidatev2Entities())
                    {
                        using (var transaction = new TransactionScope())
                        {
                            in_horoscope_mst _HoroscopeObj = _MainDB.in_horoscope_mst.SingleOrDefault(c => c.HoroscopeId == _RefID);
                            _HoroscopeObj.Status = Status;
                            _MainDB.SaveChanges();
                            try
                            {
                                //Update CachingDB 
                                Horoscope _CachingObject = new Horoscope();
                                var _UpdateQuery = Query.And(Query.EQ(Constants.refId, _RefID));
                                var _Update = Update.Set(Constants.status, Status);
                                var _sortBy = SortBy.Descending(Constants.refId);
                                var _Result = collection.FindAndModify(_UpdateQuery, _sortBy, _Update, true);
                                transaction.Complete();
                                return true;
                            }
                            catch (Exception exception)
                            {
                                new Error().LogError(exception, Constants.horoscopeClass, Constants.activateOrDeactivateHoroscopeDetailsMethod);
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
                new Error().LogError(ex, Constants.horoscopeClass, Constants.activateOrDeactivateHoroscopeDetailsMethod);
                return false;
            }
        }




        public bool UpdateHoroscopeDetails(string Description, int _RefID)
        {
            try
            {
                MongoDatabase _CachingDatabase = CachingDbConnector.GetCachingDatabase();
                var _SelectQuery = Query<Horoscope>.EQ(e => e._RefID, _RefID);
                var _Collection = _CachingDatabase.GetCollection<Horoscope>(Constants.horoscopeClass);
                var __ExistingObject = _Collection.FindOne(_SelectQuery);
                if (__ExistingObject != null)
                {
                    // If existing already in the database  Update MainDb                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                     return the existing object
                    using (intellidatev2Entities _MainDB = new intellidatev2Entities())
                    {
                        using (var transaction = new TransactionScope())
                        {
                            in_horoscope_mst _HoroscopeObj = _MainDB.in_horoscope_mst.SingleOrDefault(c => c.Description == Description);
                            _HoroscopeObj.Description = Description;
                            _MainDB.SaveChanges();
                            try
                            {
                                //Update CachingDB 
                                Horoscope _CachingObject = new Horoscope();
                                var _UpdateQuery = Query.And(Query.EQ(Constants.refId, _RefID));
                                var _Update = Update.Set(Constants.desc, Description);
                                var _SortBy = SortBy.Descending(Constants.refId);
                                var _Result = _Collection.FindAndModify(_UpdateQuery, _SortBy, _Update, true);
                                transaction.Complete();
                                return true;
                            }
                            catch (Exception exception)
                            {
                                new Error().LogError(exception, Constants.horoscopeClass, Constants.updateHoroscopeDetailsMethod);
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
                new Error().LogError(ex, Constants.horoscopeClass, Constants.updateHoroscopeDetailsMethod);
                return false;
            }
        }




    }
}
