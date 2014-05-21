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

    /// <summary>
    /// The Album class defines all the genaric properties of user's album and saved photos
    /// </summary>
    public class Album
    {
        /// <summary>
        /// The Album identifier
        /// </summary>
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; } 

        /// <summary>
        /// The reference ID from the album collection
        /// </summary>
        public int AlbumId { get; set; }

        /// <summary>
        /// The Album name from the collection or MySQL Database if any..
        /// </summary>
        public string AlbumName { get; set; }

        /// <summary>
        /// The User ID from the collection or MySQL Database if any..
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// The Created Date from the collection or  MySQL Database if any..
        /// </summary>
        public DateTime CreatedDate{ get; set; }

        /// <summary>
        /// The Status from the collection or MySQL Database if any..
        /// </summary>
        public string Status { get; set; }



        ///<summary>
        /// Save Album method can be saves album collection in caching db and main db
        /// </summary>
        /// <param name="UserID">The User Id of user collection</param>
        /// <param name="ALbumName">Name of the Album</param>
        /// <returns>Integer value</returns>
        public Album SaveAlbum(int UserID, string ALbumName)
        {
            try
            {
                //connection for caching db
                MongoDatabase _CachingDatabase = CachingDbConnector.GetCachingDatabase();
                var _newCollection = _CachingDatabase.GetCollection<Album>(Constants.albumClass);
                DateTime _CreatedDate = DateTime.Now;
                int _AlbumID = 0;

                // insert the album details in the Main DB and Cache DB
                //Insert album details in the Main DB
                using (intellidatev2Entities _MainDB = new intellidatev2Entities())
                {
                    using (var transaction = new TransactionScope())
                    {
                        in_album_mst _AlbumObject = new in_album_mst
                        {
                            UserID = UserID,
                            AlbumName = ALbumName,
                            CreatedDate = DateTime.Now.ToUniversalTime(),
                            Status = Constants.activeStatus
                        };
                        _MainDB.in_album_mst.Add(_AlbumObject);
                        _MainDB.SaveChanges();
                        _AlbumID = _AlbumObject.albumID;
                        try
                        {
                            // Once the insert is done in the MainDB, do the insert on the Caching DB
                            Album _CachingObject = new Album();

                            _CachingObject.AlbumId = _AlbumID;
                            _CachingObject.UserId = UserID;
                            _CachingObject.AlbumName = ALbumName;
                            _CachingObject.CreatedDate = DateTime.Now.ToUniversalTime();
                            _CachingObject.Status = Constants.activeStatus;
                            _newCollection.Save(_CachingObject);
                            transaction.Complete();
                            return _CachingObject;
                        }
                        catch (Exception)
                        {

                            throw;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                new Error().LogError(ex, Constants.albumClass, Constants.saveAlbumMethod);
                return null;
            }
        }



        ///<summary>
        ///  GetAlbums method can be returns album collection from caching db 
        /// </summary>
        /// <param name="UserId">UserId From User Master</param>
        /// <returns>array of album class</returns>
        public Album[] GetAlbums(int UserId)
        {
            try
            {
                //connecting to caching db
                MongoDatabase _CachingDatabase = CachingDbConnector.GetCachingDatabase();
                //getting all active users data from caching db 
                var _Query = Query<Album>.Where(e => e.Status == Constants.activeStatus & e.UserId == UserId);
                var _Collection = _CachingDatabase.GetCollection<Album>(Constants.albumClass);
                return _Collection.Find(_Query).ToArray();
            }
            catch(Exception ex)
            {
                new Error().LogError(ex, Constants.albumClass, Constants.getAlbumsMethod);
                return null;
            }
        }




    }
}
