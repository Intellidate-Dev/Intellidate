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
    public class Attachments
    {



        /// <summary>
        /// The Attachments identifier
        /// </summary>
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }

        public int _RefID { get; set; }

        public string FolderNaame { get; set; }

        public string AttachmentPath { get; set; }




        public Attachments AddAttachment(string FolderNaame,string AttachmentPath)
        {
            try
            {

                MongoDatabase _CachingDatabase = CachingDbConnector.GetCachingDatabase();
                var _Collection = _CachingDatabase.GetCollection<Attachments>(Constants.attachmentsClass);
                var _Query = Query<Attachments>.Where(x => x.AttachmentPath == AttachmentPath);
                var _Result = _Collection.FindOne(_Query);
                if (_Result == null)
                {
                    int _AttRefID = 0;
                    // Insert the record into the MainDB
                    using (intellidatev2Entities _MainDB = new intellidatev2Entities())
                    {
                        using (var transaction = new TransactionScope())
                        {
                            in_attachment_mst _AttObj = new in_attachment_mst
                            {
                                FolderNaame = FolderNaame,
                                AttachmentPath = AttachmentPath,
                            };
                            _MainDB.in_attachment_mst.Add(_AttObj);
                            _MainDB.SaveChanges();
                            _AttRefID = _AttObj.AttachmentId;
                            try
                            {
                                Attachments _CachingObject = new Attachments
                                {

                                    _RefID = _AttRefID,
                                    FolderNaame = FolderNaame,
                                    AttachmentPath = AttachmentPath,

                                };
                                //insert the record into cache db
                                _Collection.Save(_CachingObject);
                                transaction.Complete();
                                return _CachingObject;
                            }
                            catch (Exception exception)
                            {
                                new Error().LogError(exception, Constants.attachmentsClass, Constants.addAttachmentMethod);
                                return null;
                            }

                        }
                    }
                }
                else
                {
                    return _Result;
                }

            }
            catch (Exception ex)
            {
                new Error().LogError(ex, Constants.attachmentsClass, Constants.addAttachmentMethod);
                return null;
            }
        }





        public Attachments GetAttachmentById(int _RefID)
        {
            try
            {
                //connecting to caching db
                MongoDatabase _CachingDatabase = CachingDbConnector.GetCachingDatabase();
                var _Query = Query<Attachments>.EQ(e => e._RefID, _RefID);
                var _Collection = _CachingDatabase.GetCollection<Attachments>(Constants.attachmentsClass);
                return _Collection.FindOne(_Query);

            }
            catch (Exception ex)
            {
                new Error().LogError(ex, Constants.attachmentsClass, Constants.getAttachmentByIdMethod);
                return null;
            }
        }

    }
}
