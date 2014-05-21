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
    public  class AbusiveReport
    {
       [BsonRepresentation(BsonType.ObjectId)]
       public string _id { get; set; }

       public int _RefID { get; set; }

       public int UserId { get; set; }

       public int PhotoId { get; set; }

       public int AbusiveReportId { get; set; }

       public DateTime TimeStamp { get; set; }



      public AbusiveReport AddAbusiveReport(int userId,int abusiveReportId,int photoId)
       {
           try
           {
               int m_ObjRefID = 0;
               // Insert the record into the MainDB
               using (intellidatev2Entities mainDB = new intellidatev2Entities())
               {
                   using (var transaction = new TransactionScope())
                   {
                       in_abusivereport_mst abusiveReportObj = new in_abusivereport_mst
                       {
                           AbusiveReportId = abusiveReportId,
                           PhotoId = photoId,
                           UserId = userId,
                           TimeStamp = DateTime.Now.ToUniversalTime()
                       };

                       mainDB.in_abusivereport_mst.Add(abusiveReportObj);
                       mainDB.SaveChanges();
                       m_ObjRefID = abusiveReportObj.AbusiveId;
                       try
                       {
                           AbusiveReport cachingObject = new AbusiveReport
                           {
                               _RefID = m_ObjRefID,
                               UserId = userId,
                               PhotoId = photoId,
                               AbusiveReportId = abusiveReportId,
                               TimeStamp = DateTime.Now.ToUniversalTime()
                           };
                           //insert the record into cache db
                           MongoDatabase cachingDataBase = CachingDbConnector.GetCachingDatabase();
                           var m_Collection = cachingDataBase.GetCollection<AbusiveReport>(Constants.abusiveReportClass);
                           m_Collection.Save(cachingObject);
                           transaction.Complete();
                           return cachingObject;
                       }
                       catch (Exception ex2)
                       {
                           new Error().LogError(ex2, Constants.abusiveReportClass, Constants.addAbusiveReportMethod);
                           return null;
                       }
                   }
               }
           }
           catch (Exception ex)
           {
               new Error().LogError(ex, Constants.abusiveReportClass, Constants.addAbusiveReportMethod);
               return null;
           }
       }




      public AbusiveReport[] GetAbusiveReport(int photoId)
      {
          try
          {
              //connecting to caching db
              MongoDatabase cachingDataBase = CachingDbConnector.GetCachingDatabase();
              //getting all  AbusiveReport data from caching db 
              var m_Query = Query<AbusiveReport>.Where(e => e.PhotoId == photoId);
              var m_Collection = cachingDataBase.GetCollection<AbusiveReport>(Constants.abusiveReportClass);
              return m_Collection.Find(m_Query).ToArray();
          }
          catch (Exception ex)
          {
              new Error().LogError(ex, Constants.abusiveReportClass, Constants.getAbusiveReportMethod);
              return null;
          }
      }

         


      public AbusiveReport[] GetAllAbusiveReports()
      {
          try
          {
              //connecting to caching db
              MongoDatabase cachingDataBase = CachingDbConnector.GetCachingDatabase();
              //getting all active users data from caching db 
              var m_Collection = cachingDataBase.GetCollection<AbusiveReport>(Constants.abusiveReportClass);
              return m_Collection.FindAll().ToArray();
          }
          catch (Exception ex)
          {
              new Error().LogError(ex, Constants.abusiveReportClass, Constants.getAllAbusiveReportsMethod);
              return null;
          }
      }



    }
}
