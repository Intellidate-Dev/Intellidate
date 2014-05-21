using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Bson;
using MySql.Data;
using MongoDB.Driver.Builders;
using MongoDB.Driver.GridFS;
using MongoDB.Driver.Linq;
using System.Configuration;
using IntellidateLib.DB;
using System.Globalization;
using MongoDB.Bson.Serialization.Attributes;
using System.Transactions;

namespace IntellidateLib
{
    public class MessageReply
    {
        /// <summary>
        /// The Message class identifier for caching db
        /// </summary>
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }
        public int _RefID { get; set; }
        public int UserId { get; set; }
        public int RecipientRefId { get; set; }
        public bool IsReply { get; set; }

        public bool AddMessageReply(int UserId, int RecipientRefId, bool IsReply)
        {
            try
            {
                int m_ObjRefID = 0;
                // Insert the record into the MainDB
                using (intellidatev2Entities mainDB = new intellidatev2Entities())
                {
                    using (var transaction = new TransactionScope())
                    {
                        in_message_reply MessageReplyObj = new in_message_reply
                        {
                            UserId = UserId,
                            RecipientId = RecipientRefId,
                            IsReply = IsReply,
                        };

                        mainDB.in_message_reply.Add(MessageReplyObj);
                        mainDB.SaveChanges();
                        m_ObjRefID = MessageReplyObj.ReplyId;
                        try
                        {
                            MessageReply cachingObject = new MessageReply
                            {
                                _RefID = m_ObjRefID,
                                UserId = UserId,
                                RecipientRefId = RecipientRefId,
                                IsReply = IsReply,
                            };
                            //insert the record into cache db
                            MongoDatabase cachingDataBase = CachingDbConnector.GetCachingDatabase();
                            var m_Collection = cachingDataBase.GetCollection<MessageReply>(Constants.messageReplyClass);
                            m_Collection.Save(cachingObject);
                            transaction.Complete();
                            return true;
                        }
                        catch (Exception exception)
                        {
                            new Error().LogError(exception, Constants.messageReplyClass, Constants.addMessageReplyMethod);
                            return false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                new Error().LogError(ex, Constants.messageReplyClass, Constants.addMessageReplyMethod);
                return false;
            }
        }


        public MessageReply GetMessageReplyById(int UserId, int RecipientRefId)
        {
            try
            {
                //connecting to caching db
                MongoDatabase CachingDatabase = CachingDbConnector.GetCachingDatabase();
                var _Query = Query<MessageReply>.Where(e => e.UserId == UserId && e.RecipientRefId == RecipientRefId);
                var _Collection = CachingDatabase.GetCollection<MessageReply>(Constants.messageReplyClass);
                return _Collection.FindOne(_Query);

            }
            catch (Exception ex)
            {
                new Error().LogError(ex, Constants.messageReplyClass, Constants.getMessageReplyByIdMethod);
                return null;
            }
        }


        public bool UpdateMessageReply(int UserId, int RecipientRefId, bool IsReply)
        {
            try
            {
                MongoDatabase cachingDataBase = CachingDbConnector.GetCachingDatabase();
                var m_Query = Query<MessageReply>.Where(e => e.UserId == UserId && e.RecipientRefId == RecipientRefId);
                var m_Collection = cachingDataBase.GetCollection<MessageReply>(Constants.messageReplyClass);
                var m__ExistingObject = m_Collection.FindOne(m_Query);
                if (m__ExistingObject != null)
                {
                    // If existing already in the database  Update MainDb                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                     return the existing object
                    using (intellidatev2Entities mainDB = new intellidatev2Entities())
                    {
                        using (var transaction = new TransactionScope())
                        {
                            in_message_reply MaiDbObj = mainDB.in_message_reply.SingleOrDefault(c => c.UserId == UserId && c.RecipientId == RecipientRefId);
                            MaiDbObj.IsReply = IsReply;
                            mainDB.SaveChanges();
                            try
                            {
                                //Update CachingDB 
                                MessageReply cCachingObject = new MessageReply();
                                var m_Selectquery = Query<MessageReply>.Where(e => e.UserId == UserId && e.RecipientRefId == RecipientRefId);
                                var m_Update = Update.Set(Constants.IsReply, IsReply);
                                var m_SortBy = SortBy.Descending(Constants.refId);
                                var m_Result = m_Collection.FindAndModify(m_Selectquery, m_SortBy, m_Update, true);
                                transaction.Complete();
                                return true;
                            }
                            catch (Exception exception)
                            {
                                new Error().LogError(exception, Constants.messageReplyClass, Constants.updateMessageReplyMethod);
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
                new Error().LogError(ex, Constants.messageReplyClass, Constants.updateMessageReplyMethod);
                return false;
            }
        }


    }
}
