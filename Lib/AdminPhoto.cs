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
    public class AdminPhoto
    {
        /// <summary>
        /// The AdminPhoto identifier
        /// </summary>
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }

        public int _RefID { get; set; }

        public int AdminId { get; set; }

        public int PhotoId { get; set; }

        
        public string Comment { get; set; }

        public DateTime  TimeStamp { get; set; }

        /// <summary>
        /// for approved=true and rejected=false
        /// </summary>
        public bool IsApproved { get; set; }


       
        public AdminPhoto AddAdminPhotoReport(int AdminId, int PhotoId, string Comment, bool IsApproved)
        {
            try
            {
                int _ObjRefID = 0;
                  // Insert the record into the MainDB
                using (intellidatev2Entities _MainDB = new intellidatev2Entities())
                {
                    using (var transaction = new TransactionScope())
                    {

                        in_admin_photo _PhotoObj = new in_admin_photo
                        {
                            AdminId = AdminId,
                            PhotoId = PhotoId,
                            Comment = Comment,
                            IsApproved = IsApproved,
                            TimeStamp = DateTime.Now.ToUniversalTime()
                        };
                        _MainDB.in_admin_photo.Add(_PhotoObj);
                        _MainDB.SaveChanges();
                        _ObjRefID = _PhotoObj.AadminPhotoID;
                        try
                        {
                            AdminPhoto _CachingObject = new AdminPhoto
                            {
                                _RefID = _ObjRefID,
                                AdminId = AdminId,
                                PhotoId = PhotoId,
                                Comment = Comment,
                                IsApproved = IsApproved,
                                TimeStamp = DateTime.Now.ToUniversalTime()
                            };
                            //insert the record into cache db
                            MongoDatabase _CachingDatabase = CachingDbConnector.GetCachingDatabase();
                            var _Collection = _CachingDatabase.GetCollection<AdminPhoto>(Constants.adminPhotoClass);
                            _Collection.Save(_CachingObject);
                            transaction.Complete();
                            return _CachingObject;

                        }
                        catch (Exception exception)
                        {
                            new Error().LogError(exception, Constants.adminPhotoClass, Constants.addAdminPhotoReportMethod);
                            return null;
                        }
                       
                    }
                }
            }
            catch (Exception ex)
            {
                new Error().LogError(ex, Constants.adminPhotoClass, Constants.addAdminPhotoReportMethod);
                return null;
            }
        }


       
        public AdminPhoto GetAdminPhotoReportById(int _RefID)
        {
            try
            {
                //connecting to caching db
                MongoDatabase _CachingDatabase = CachingDbConnector.GetCachingDatabase();

                var _Query = Query<AdminPhoto>.EQ(e => e._RefID, _RefID);
                var _Collection = _CachingDatabase.GetCollection<AdminPhoto>(Constants.adminPhotoClass);
                return _Collection.FindOne(_Query);

            }
            catch (Exception ex)
            {
                new Error().LogError(ex, Constants.adminPhotoClass, Constants.getAdminPhotoReportByIdMethod);
                return null;
            }
        }



        /// <summary>
        /// photo is approved or not by same admin and same photo for check the admin aprovel status
        /// </summary>       
        public AdminPhoto GetAdminPhotoReportByAdminId(int AdminId,int PhotoId)
        {
            try
            {
                //connecting to caching db
                MongoDatabase _CachingDatabase = CachingDbConnector.GetCachingDatabase();
                var _Query = Query<AdminPhoto>.Where(e => e.AdminId == AdminId && e.PhotoId==PhotoId && e.IsApproved==true);
                var _Collection = _CachingDatabase.GetCollection<AdminPhoto>(Constants.adminPhotoClass);
                return _Collection.FindOne(_Query);

            }
            catch (Exception ex)
            {
                new Error().LogError(ex, Constants.adminPhotoClass, Constants.getAdminPhotoReportByAdminIdMethod);
                return null;
            }
        }


        /// <summary>
        /// getting Admin to photo last approved status
        /// </summary>         
        public string GetAdminPhotoLastApprovalStatus(int AdminId, int PhotoId)
        {
            try
            {
                //connecting to caching db
                MongoDatabase _CachingDatabase = CachingDbConnector.GetCachingDatabase();
                var _Query = Query<AdminPhoto>.Where(e => e.AdminId == AdminId && e.PhotoId == PhotoId);
                var _Collection = _CachingDatabase.GetCollection<AdminPhoto>(Constants.adminPhotoClass);
                bool[] _array=_Collection.Find(_Query).OrderBy(e => e._RefID).Select(e => e.IsApproved).ToArray();
                if (_array.Length > 0)
                {
                    return _array[_array.Length - 1].ToString();
                }
                else
                {
                    return "0";
                }

            }
            catch (Exception ex)
            {
                new Error().LogError(ex, Constants.adminPhotoClass, Constants.getAdminPhotoLastApprovalStatusMethod);
                return "";
            }
        }


      
    }
}
