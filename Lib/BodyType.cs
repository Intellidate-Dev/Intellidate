using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntellidateLib.DB;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.Transactions;

namespace IntellidateLib
{
  public class BodyType
    {
        /// <summary>
        /// The BodyType identifier
        /// </summary>
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }

        public int _RefID { get; set; }

        public string Description { get; set; }

        public string Status { get; set; }




        public bool AddBodyTypeDetails(string Discription)
        {
            try
            {
                int _BodyRefID = 0;
                // Insert the record into the MainDB
                using (intellidatev2Entities _MainDB = new intellidatev2Entities())
                {
                    using (var transaction = new TransactionScope())
                    {
                        in_bodytype_mst _BodyTypeObj = new in_bodytype_mst
                        {
                            Description = Discription,
                            Status = Constants.activeStatus,
                        };
                        _MainDB.in_bodytype_mst.Add(_BodyTypeObj);
                        _MainDB.SaveChanges();
                        _BodyRefID = _BodyTypeObj.BodyTypeId;
                        try
                        {
                            BodyType _CachingObject = new BodyType
                            {
                                _RefID = _BodyRefID,
                                Description = Discription,
                                Status = Constants.activeStatus
                            };
                            //insert the record into cache db
                            MongoDatabase _CachingDatabase = CachingDbConnector.GetCachingDatabase();
                            var _Collection = _CachingDatabase.GetCollection<BodyType>(Constants.bodyTypeClass);
                            _Collection.Save(_CachingObject);
                            transaction.Complete();
                            return true;
                        }
                        catch (Exception exception)
                        {

                            new Error().LogError(exception, Constants.bodyTypeClass, Constants.addBodyTypeDetailsMethod);
                            return false;
                        }
                        
                    }
                }
            }
            catch (Exception ex)
            {
                new Error().LogError(ex, Constants.bodyTypeClass, Constants.addBodyTypeDetailsMethod);
                return false;
            }
        }





        public BodyType[] GetBodyTypeDetails()
        {
            try
            {
                //connecting to caching db
                MongoDatabase _CachingDatabase = CachingDbConnector.GetCachingDatabase();
                //getting all active BodyType data from caching db
                var _Query = Query<BodyType>.EQ(e => e.Status, Constants.activeStatus);
                var _Collection = _CachingDatabase.GetCollection<BodyType>(Constants.bodyTypeClass);
                return _Collection.Find(_Query).ToArray();

            }
            catch (Exception ex)
            {
                new Error().LogError(ex, Constants.bodyTypeClass, Constants.getBodyTypeDetailsMethod);
                return null;
            }
        }




        public BodyType GetBodyTypeDetailsById(int _RefID)
        {
            try
            {
                //connecting to caching db
                MongoDatabase CachingDatabase = CachingDbConnector.GetCachingDatabase();
              
                var _Query = Query<BodyType>.EQ(e => e._RefID, _RefID);
                var _Collection = CachingDatabase.GetCollection<BodyType>(Constants.bodyTypeClass);
                return _Collection.FindOne(_Query);

            }
            catch (Exception ex)
            {
                new Error().LogError(ex, Constants.bodyTypeClass, Constants.getBodyTypeDetailsByIdMethod);
                return null;
            }
        }





        public bool ActivateOrDeactivateBodyTypeDetails(string Status, int _RefID)
        {
            try
            {
                MongoDatabase _CachingDatabase = CachingDbConnector.GetCachingDatabase();
                //Here we are not deleting the Distance we just updates the status of Distance A(Active) to I(Inactive)
                var _Query = Query<BodyType>.EQ(e => e._RefID, _RefID);
                var _Collection = _CachingDatabase.GetCollection<BodyType>(Constants.bodyTypeClass);
                var _ExistingObject = _Collection.FindOne(_Query);
                if (_ExistingObject != null)
                {
                    // If existing already in the database  Update MainDb                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                     return the existing object
                    using (intellidatev2Entities _MainDB = new intellidatev2Entities())
                    {
                        using (var transaction = new TransactionScope())
                        {
                            in_bodytype_mst _BodytypeObj = _MainDB.in_bodytype_mst.SingleOrDefault(c => c.BodyTypeId == _RefID);
                            _BodytypeObj.Status = Status;
                            _MainDB.SaveChanges();
                            try
                            {
                                //Update CachingDB 
                                BodyType _CachingObject = new BodyType();
                                var _Newquery = Query.And(Query.EQ(Constants.refId, _RefID));
                                var _Update = Update.Set(Constants.status, Status);
                                var _sortBy = SortBy.Descending(Constants.refId);
                                var _Result = _Collection.FindAndModify(_Newquery, _sortBy, _Update, true);
                                transaction.Complete();
                                return true;
                            }
                            catch (Exception exception)
                            {
                                new Error().LogError(exception, Constants.bodyTypeClass, Constants.activateOrDeactivateBodyTypeDetailsMethod);
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
                new Error().LogError(ex, Constants.bodyTypeClass, Constants.activateOrDeactivateBodyTypeDetailsMethod);
                return false;
            }
        }





        public bool UpdateBodyTypeDetails(string Description, int _RefID)
        {
            try
            {
                MongoDatabase _CachingDatabase = CachingDbConnector.GetCachingDatabase();
              
                var _SelectQuery = Query<BodyType>.EQ(e => e._RefID, _RefID);
                var _Collection = _CachingDatabase.GetCollection<BodyType>(Constants.bodyTypeClass);
                var _ExistingObject = _Collection.FindOne(_SelectQuery);
                if (_ExistingObject != null)
                {
                    // If existing already in the database  Update MainDb                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                     return the existing object
                    using (intellidatev2Entities _MainDB = new intellidatev2Entities())
                    {
                        using (var transaction = new TransactionScope())
                        {
                            in_bodytype_mst _BodyTypeObj = _MainDB.in_bodytype_mst.SingleOrDefault(c => c.BodyTypeId == _RefID);
                            _BodyTypeObj.Description = Description;
                            _MainDB.SaveChanges();
                            try
                            {
                                //Update CachingDB 
                                BodyType _CachingObject = new BodyType();
                                var _UpdateQuery = Query.And(Query.EQ(Constants.refId, _RefID));
                                var _Update = Update.Set(Constants.desc, Description);
                                var _sortBy = SortBy.Descending(Constants.refId);
                                var _Result = _Collection.FindAndModify(_UpdateQuery, _sortBy, _Update, true);
                                transaction.Complete();
                                return true;
                            }
                            catch (Exception exception)
                            {
                                new Error().LogError(exception, Constants.bodyTypeClass, Constants.updateBodyTypeDetailsMethod);
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
                new Error().LogError(ex, Constants.bodyTypeClass, Constants.updateBodyTypeDetailsMethod);
                return false;
            }
        }





    }
}
