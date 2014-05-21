using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack.Redis;

namespace IntellidateLib
{
    /// <summary>
    /// The class for connecting caching db
    /// </summary>

   public static class CachingDbConnector
    {

        /// <summary>
        /// The Caching Db Connector
        /// </summary>
       public static MongoDatabase GetCachingDatabase()
       {
           try
           {
               //Getting connection string from app.config file 
               string _ConnectionString = ConfigurationSettings.AppSettings[Constants.cachingDBConnectionString].ToString();
               var _Client = new MongoClient(_ConnectionString);
               var _Server = _Client.GetServer();
               //Getting db name
               var database = _Server.GetDatabase(ConfigurationSettings.AppSettings[Constants.cachingDB].ToString());
               return database;
           }
           catch(Exception ex)
           {
               new Error().LogError(ex, Constants.cachingDbConnectorClass, Constants.getCachingDatabaseMethod);
               return null;
           }
       }

       public static RedisClient GetRedisDatabase()
       {
           try
           {
                string m_RedisConnector = ConfigurationManager.AppSettings[Constants.redisServerName].ToString();
                RedisClient redisClient = new RedisClient(m_RedisConnector);
                return redisClient;
           }
           catch (Exception  exception)
           {            
              new Error().LogError(exception, Constants.cachingDbConnectorClass, Constants.getRedisDatabaseMethod);
              return null;
           }
       }

    }
}
