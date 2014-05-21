using IntellidateLib.DB;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using MongoDB.Driver.Builders;
namespace IntellidateLib
{
    public class AdminMasterMessage
    {
        /// <summary>
        /// The Message class identifier for caching db
        /// </summary>
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }

        public int MessageID { get; set; }

        public string Subject { get; set; }

        public string MessageText { get; set; }

        public DateTime CreatedDate { get; set; }

        public string Status { get; set; }

        public bool IsDeleted { get; set; }



        /// <summary>
        /// Add new message
        ///<param name="subject">The  subject of message</param>
        ///<param name="messageText">Message Text</param>
        /// </summary>
        public AdminMasterMessage AddAdminMasterMessage(string subject, string messageText)
        {
            try
            {
                int m_MessageRefID = 0;

                // Insert the record into the MainDB
                using (intellidatev2Entities mainDB = new intellidatev2Entities())
                {
                    using (var transaction = new TransactionScope())
                    {
                        in_admin_message_mst adminMessageObj = new in_admin_message_mst
                        {
                            Subject = subject,
                            MessageText = messageText,
                            CreatedDate = DateTime.Now.ToUniversalTime(),
                            Status = Constants.successStatus,
                            IsDeleted = false
                        };
                        mainDB.in_admin_message_mst.Add(adminMessageObj);
                        mainDB.SaveChanges();
                        m_MessageRefID = adminMessageObj.MessageID;

                        try
                        {
                            AdminMasterMessage cachingObject = new AdminMasterMessage
                            {
                                MessageID = m_MessageRefID,
                                Subject = subject,
                                MessageText = messageText,
                                CreatedDate = DateTime.Now.ToUniversalTime(),
                                Status = Constants.activeStatus,
                                IsDeleted = false,
                            };

                            //insert the record into cache db
                            MongoDatabase cachingDataBase = CachingDbConnector.GetCachingDatabase();
                            var m_Collection = cachingDataBase.GetCollection<AdminMasterMessage>(Constants.adminMasterMessageClass);
                            m_Collection.Save(cachingObject);
                            transaction.Complete();
                            return cachingObject;
                        }
                        catch (Exception exception)
                        {
                            new Error().LogError(exception, Constants.adminMasterMessageClass, Constants.addAdminMasterMessageMethod);
                            return null;
                        }

                       
                    }
                }
            }
            catch (Exception ex)
            {
                new Error().LogError(ex, Constants.adminMasterMessageClass, Constants.addAdminMasterMessageMethod);
                return null;
            }
        }

        public AdminMasterMessage GetAdminMasterMessage(int RefId)
        {
            try
            {
               //connecting to caching db
              MongoDatabase cachingDataBase = CachingDbConnector.GetCachingDatabase();
              //getting AdminMasterMessage  data from caching db 
              var m_Query = Query<AdminMasterMessage>.Where(e => e.MessageID == RefId);
              var m_Collection = cachingDataBase.GetCollection<AdminMasterMessage>(Constants.abusiveReportClass);
              return m_Collection.FindOne(m_Query);
            }
            catch (Exception ex)
            {
               new Error().LogError(ex, Constants.adminMasterMessageClass, Constants.addAdminMasterMessageMethod);
                return null;
            }
        }


    }
}
