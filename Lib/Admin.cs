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

/* -----------------------------------------------
 Name       :       Admin.cs
 Version    :       1.0
 Author     :       Baseer Siddiqui - Baseer
 Purpose    :       The admin site and end-user site methods for the admin collection
  -----------------------------------------------*/

namespace IntellidateLib
{
    /// <summary>
    /// The Admin class defines all the generic properties of the Admin
    /// </summary> 
    public class Admin
    {
        /// <summary>
        /// The admin identifier for the caching DB collection
        /// </summary>
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }

        /// <summary>
        /// The Admin ID from the collection or MySQL Database
        /// </summary>
        public int AdminRefId { get; set; }

        /// <summary>
        /// The Admin name (Max: 200)
        /// </summary>
        public string AdminName { get; set; }

        /// <summary>
        /// The Admin login name. (Max: 20)
        /// </summary>
        public string LoginName { get; set; }

        /// <summary>
        /// The Admin login EmailID (Max: 50)
        /// </summary>
        public string EmailID { get; set; }

        /// <summary>
        /// The Admin Password (Max: 20)
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// The Admin Created Date
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// the status of the Admin record. A=Active, I=Inactive (Max: 1)
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// the Type of the Admin is set here. M=MasterAdmin, S=SubAdmin
        /// </summary>
        public string AdminType { get; set; }

        /// <summary>
        /// The Admin Privileges. 1=User Management; 2= Photos Management; 3= Videos Management; 4= Questions Management
        /// </summary>
        public string[] AdminPrivileges { get; set; }

        /// <summary>
        /// The forums the admin has access to
        /// </summary>
        public string[] ForumPrivileges { get; set; }

        
        /// <summary>
        /// The Add new Admin method. The method must insert into both MySQL cache the date into MongoDB
        /// </summary>
        /// <param name="adminName">string: The name of the admin</param>
        /// <param name="adminPassword">string: The password of the admin</param>
        /// <param name="adminType">string: The type of admin (not used for now)</param>
        /// <param name="loginName">string: The login name of the admin</param>
        /// <param name="emailId">string: The email address of the admin</param>
        /// <param name="adminPrivileges">string[]: The collection of the privileges of the admin</param>
        /// <param name="forumIds">string[]: The collection of the forum privileges</param>
        /// <returns>Admin</returns>
        public Admin AddNewAdminUser(string adminName, string adminPassword, string adminType, string loginName, string emailId, string[] adminPrivileges, string[] forumIds)
        {
            try
            {
                // The caching DB object
                MongoDatabase cachingDataBase = CachingDbConnector.GetCachingDatabase();

                // Query to find the caching collection object with the same admin Login Name
                var m_Query = Query<Admin>.Where(x => x.LoginName == loginName);

                // The caching collection
                var m_Collection = cachingDataBase.GetCollection<Admin>(Constants.c_ADMIN);

                // Running the query
                var m_ExistingObject = m_Collection.FindOne(m_Query);

                // If the object is found
                if (m_ExistingObject != null)
                {
                    // Logging the failure report
                    string m_ErrorMessage = "Attempted to create " + loginName + " second time";
                    new Error().LogError(m_ErrorMessage, Constants.c_ADMIN, Constants.m_ADDNEWADMINUSER);

                    // Return null
                    return null;
                }
                else
                {
                    // The current time
                    DateTime m_CreatedDate = DateTime.Now.ToUniversalTime();

                    // The reference id for Main DB
                    int m_MainDBAdminReferenceID = 0;

                    // Updating the main DB
                    using (intellidatev2Entities mainDB = new intellidatev2Entities())
                    {
                        using (var transaction = new TransactionScope())
                        {
                            // Creating a new Main DB object
                            in_adminuser_mst adminUserObject = new in_adminuser_mst
                            {
                                AdminName = adminName,
                                LoginName = loginName,
                                Password = adminPassword,
                                CreatedDate = m_CreatedDate,
                                AdminType = adminType,
                                EmailID = emailId,
                                Status = Constants.c_STATUSACTIVE,
                            };

                            mainDB.in_adminuser_mst.Add(adminUserObject);
                            mainDB.SaveChanges();

                            // Getting the reference ID from the Main DB
                            m_MainDBAdminReferenceID = adminUserObject.AdminID;

                            // Adding the Admin Privileges in the MainDB
                            foreach (string m_EachPrivilege in adminPrivileges)
                            {
                                in_adminprivileges_mst _Privileges = new in_adminprivileges_mst
                                {
                                    adminid = m_MainDBAdminReferenceID,
                                    privilegeid = m_EachPrivilege
                                };
                                mainDB.in_adminprivileges_mst.Add(_Privileges);
                                mainDB.SaveChanges();
                            }


                            // Addming the Admin's forum privileges
                            foreach (string m_EachForumId in forumIds)
                            {
                                int m_forumRefID = new ForumCategory().GetForumRefID(m_EachForumId);
                                in_forumprivileges_trn forumPrivilegesObj = new in_forumprivileges_trn
                                {
                                    adminid = m_MainDBAdminReferenceID,
                                    forumcategoryid = m_forumRefID
                                };

                                mainDB.in_forumprivileges_trn.Add(forumPrivilegesObj);
                                mainDB.SaveChanges();
                            }

                            try
                            {
                                // Once the insert is done in the MainDB, performing the insert on the Caching DB
                                Admin cachingObject = new Admin();
                                cachingObject.AdminRefId = m_MainDBAdminReferenceID;
                                cachingObject.AdminName = adminName;
                                cachingObject.LoginName = loginName;
                                cachingObject.EmailID = emailId;
                                cachingObject.Password = adminPassword;
                                cachingObject.CreatedDate = m_CreatedDate;
                                cachingObject.AdminType = adminType;
                                cachingObject.Status = Constants.c_STATUSACTIVE;
                                cachingObject.AdminPrivileges = adminPrivileges;
                                cachingObject.ForumPrivileges = forumIds;

                                m_Collection.Save(cachingObject);
                                transaction.Complete();
                                return cachingObject;
                            }
                            catch (Exception exception)
                            {
                                // Logging the exception
                                new Error().LogError(exception, Constants.c_ADMIN, Constants.m_ADDNEWADMINUSER);

                                // Returning Null
                                return null;
                            }
                        }
                    };
                }
            }
            catch (Exception ex)
            {
                // Logging the exception
                new Error().LogError(ex, Constants.c_ADMIN, Constants.m_ADDNEWADMINUSER);

                // Returning Null
                return null;
            }

        }

        /// <summary>
        /// Method to edit the admin details
        /// </summary>
        /// <param name="adminRefId">int: The main DB reference ID of the admin</param>
        /// <param name="adminName">string: The name of the admin</param>
        /// <param name="adminPassword">string: The password of the admin</param>
        /// <param name="adminType">string: The type of admin (not used for now)</param>
        /// <param name="loginName">string: The login name of the admin</param>
        /// <param name="emailId">string: The email address of the admin</param>
        /// <param name="adminPrivileges">string[]: The collection of the privileges of the admin</param>
        /// <param name="forumIds">string[]: The collection of the forum privileges</param>
        /// <returns>Admin</returns>
        public Admin EditAminUser(int adminRefId, string adminName, string adminPassword, string adminType, string loginName, string emailId, string[] adminPrivileges, string[] forumIds)
        {
            try
            {
                // The caching DB object
                MongoDatabase cachingDataBase = CachingDbConnector.GetCachingDatabase();

                // Query to find the caching collection object
                var m_Query = Query<Admin>.EQ(x => x.AdminRefId, adminRefId);

                // The caching collection
                var m_Collection = cachingDataBase.GetCollection<Admin>(Constants.c_ADMIN);

                // Running the query
                var m_ExistingObject = m_Collection.FindOne(m_Query);

                // If the object is found
                if (m_ExistingObject != null)
                {
                    // The curent date
                    DateTime m_CreatedDate = DateTime.Now.ToUniversalTime();

                    // Updating the main DB
                    using (intellidatev2Entities mainDB = new intellidatev2Entities())
                    {
                        using (var transaction = new TransactionScope())
                        {
                            // Searching and getting the Main DB object
                            var m_ExistingMainDBObject = mainDB.in_adminuser_mst.Where(x => x.AdminID == m_ExistingObject.AdminRefId).FirstOrDefault();

                            // If the object exists in the Main DB collection
                            if (m_ExistingMainDBObject != null)
                            {
                                m_ExistingMainDBObject.AdminName = adminName;
                                m_ExistingMainDBObject.Password = adminPassword;
                                m_ExistingMainDBObject.AdminName = adminName;
                                m_ExistingMainDBObject.EmailID = emailId;

                                // Saving the changes
                                mainDB.SaveChanges();
                            }
                            else
                            {
                                // Logging the failure report
                                string m_ErrorMessage = "The admin object with adminID  " + adminRefId.ToString() + " not found in the Main DB collection.";
                                new Error().LogError(m_ErrorMessage, Constants.c_ADMIN, Constants.m_EDITAMINUSER);
                            }

                            // Deleting the existing Admin privileges
                            var m_ExistingPrivileges = mainDB.in_adminprivileges_mst.Where(x => x.adminid == m_ExistingObject.AdminRefId).ToArray();
                            if (m_ExistingPrivileges != null)
                            {
                                foreach (var m_EachPrevilage in m_ExistingPrivileges)
                                {
                                    mainDB.in_adminprivileges_mst.Remove(m_EachPrevilage);
                                    mainDB.SaveChanges();
                                }

                            }

                            // Adding the Admin privileges
                            foreach (string m_EachPrivilege in adminPrivileges)
                            {
                                in_adminprivileges_mst adminPrivilages = new in_adminprivileges_mst
                                {
                                    adminid = m_ExistingObject.AdminRefId,
                                    privilegeid = m_EachPrivilege
                                };
                                mainDB.in_adminprivileges_mst.Add(adminPrivilages);
                                mainDB.SaveChanges();
                            }


                            var m_ExistingForumPrivileges = mainDB.in_forumprivileges_trn.Where(x => x.adminid == m_ExistingObject.AdminRefId).ToArray();
                            if (m_ExistingForumPrivileges != null)
                            {
                                foreach (var m_Item in m_ExistingForumPrivileges)
                                {
                                    mainDB.in_forumprivileges_trn.Remove(m_Item);
                                    mainDB.SaveChanges();
                                }

                            }

                            // Editing the admin Forum privieges
                            foreach (string m_EachForumId in forumIds)
                            {
                                // Get the forum's mySQL id
                                int m_ForumRefID = new ForumCategory().GetForumRefID(m_EachForumId);
                                in_forumprivileges_trn forumPrivileges = new in_forumprivileges_trn
                                {
                                    adminid = m_ExistingObject.AdminRefId,
                                    forumcategoryid = m_ForumRefID
                                };
                                mainDB.in_forumprivileges_trn.Add(forumPrivileges);
                                mainDB.SaveChanges();
                            }

                            try
                            {
                                // Find and update the Admin collection object in the caching DB
                                m_Query = Query<Admin>.EQ(x => x.AdminRefId, adminRefId);
                                var m_Update = Update<Admin>.Set(a => a.AdminName, adminName)
                                                                .Set(x => x.EmailID, emailId)
                                                                .Set(x => x.Password, adminPassword)
                                                                .Set(x => x.AdminPrivileges, adminPrivileges)
                                                                .Set(x => x.ForumPrivileges, forumIds);

                                m_Collection.Update(m_Query, m_Update);
                                transaction.Complete();

                                // Return the object
                                return m_ExistingObject;
                            }
                            catch (Exception exception)
                            {
                                // Logging the exception
                                new Error().LogError(exception, Constants.c_ADMIN, Constants.m_EDITAMINUSER);

                                // Returning Null
                                return null;
                            }
                           
                        }
                    };
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                // Logging the exception
                new Error().LogError(ex, Constants.c_ADMIN, Constants.m_EDITAMINUSER);

                // Returning Null
                return null;
            }

        }

        /// <summary>
        /// The Activate Or DeActivate Admin method. The method must updates into both MySQL cache the date into MongoDB
        /// </summary>
        /// <param name="adminRefId">int: The reference ID for the Main DB admin collection</param>
        /// <param name="status">string: The status for the admin collection. "A" = Active "I" = Inactive</param>
        /// <returns>Boolean</returns>
        public bool ActivateOrDeActivateAdmin(int adminRefId, string status)
        {
            try
            {
                // The caching DB object
                MongoDatabase cachingDataBase = CachingDbConnector.GetCachingDatabase();

                // Query to find the caching collection object
                var m_Query = Query<Admin>.EQ(e => e.AdminRefId, adminRefId);

                // The caching collection
                var m_Collection = cachingDataBase.GetCollection<Admin>(Constants.c_ADMIN);

                // Running the query
                var m_ExistingObject = m_Collection.FindOne(m_Query);

                // If the object is found
                if (m_ExistingObject != null)
                {
                    // Updating the main DB                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                     return the existing object
                    using (intellidatev2Entities mainDB = new intellidatev2Entities())
                    {
                        using (var transaction = new TransactionScope())
                        {
                            // Finding the the object in the Main DB Collection
                            in_adminuser_mst adminUserObject = mainDB.in_adminuser_mst.SingleOrDefault(c => c.AdminID == adminRefId);

                            // If the object is found, updateing the status
                            if (adminUserObject != null)
                            {
                                adminUserObject.Status = status;
                                mainDB.SaveChanges();
                            }

                            try
                            {
                                // Finding and updating the object in the Caching DB Collection
                                var m_Update = Update<Admin>.Set(x => x.Status, status);
                                m_Collection.Update(m_Query, m_Update);
                                transaction.Complete();
                                // Returning success
                                return true;
                            }
                            catch (Exception exception)
                            {
                                // Logging the exception
                                new Error().LogError(exception, Constants.c_ADMIN, Constants.m_ACTIVATEORDEACTIVATEADMIN);

                                // Returning success 
                                return false;
                            }

                           
                        }
                    };
                }
                else
                {
                    // Returning failure
                    return false;
                }


            }
            catch (Exception ex)
            {
                // Logging the exception
                new Error().LogError(ex, Constants.c_ADMIN, Constants.m_ACTIVATEORDEACTIVATEADMIN);

                // Returning success 
                return false;
            }
        }

        /// <summary>
        /// Method for authenticating the admin's login credentials
        /// </summary>
        /// <param name="loginName">string: The Login Name of the admin</param>
        /// <param name="password">string: The password of the admin</param>
        /// <returns>Admin</returns>
        public Admin AuthanticateAdmin(string loginName, string password)
        {
            try
            {
                // The caching DB object
                MongoDatabase cachingDataBase = CachingDbConnector.GetCachingDatabase();

                //Getting user details from caching db 
                var m_Query = Query<Admin>.Where(x => x.LoginName.ToUpperInvariant() == loginName.ToUpper() && x.Password == password && x.Status == Constants.c_STATUSACTIVE);

                // Caching DB Collection
                var m_Collection = cachingDataBase.GetCollection<Admin>(Constants.c_ADMIN);

                // Running the query
                Admin adminObject = m_Collection.FindOne(m_Query);

                if (adminObject == null)
                {
                    // The authentication has failed.
                    return null;
                }

                // Return the admin object, authentication succeeded.
                return adminObject;
            }
            catch (Exception ex)
            {
                // Log the exception
                new Error().LogError(ex, Constants.c_ADMIN, Constants.m_AUTHANTICATEADMIN);

                // returning null
                return null;
            }
        }

        /// <summary>
        /// The method to get the admin details.
        /// </summary>
        /// <param name="aAdminRefId">int: Admin Ref ID from the Main DB</param>
        /// <returns>Admnin</returns>
        public Admin GetAdminDetails(int aAdminRefId)
        {
            try
            {
                // The caching DB object
                MongoDatabase cachingDataBase = CachingDbConnector.GetCachingDatabase();

                // Query to search from the Caching DB caollection
                var m_Query = Query<Admin>.EQ(x => x.AdminRefId, aAdminRefId);

                // The caching DB Collection
                var m_Collection = cachingDataBase.GetCollection<Admin>(Constants.c_ADMIN);

                Admin adminDetailsObj = m_Collection.FindOne(m_Query);

                if (adminDetailsObj == null)
                {
                    // If ther are no admin details, return NULL
                    return null;
                }

                // Returning the admin object
                return m_Collection.FindOne(m_Query);
            }
            catch (Exception ex)
            {
                // Logging the exception
                new Error().LogError(ex, Constants.c_ADMIN, Constants.m_GETADMINDETAILS);

                // Returning null
                return null;
            }
        }

        /// <summary>
        /// The method to get the admin details with the given email address
        /// </summary>
        /// <param name="emailId">string: Email address</param>
        /// <returns>Admin</returns>
        public Admin GetAdminDetails(string emailId)
        {
            try
            {
                // The caching DB object
                MongoDatabase cachingDataBase = CachingDbConnector.GetCachingDatabase();

                // Query to search from the Caching DB caollection
                var m_Query = Query<Admin>.Where(x => x.EmailID.ToUpperInvariant() == emailId.ToUpper());

                // The caching DB Collection
                var m_Collection = cachingDataBase.GetCollection<Admin>(Constants.c_ADMIN);

                Admin adminDetailsObj = m_Collection.FindOne(m_Query);

                if (adminDetailsObj == null)
                {
                    // If ther are no admin details, return NULL
                    return null;
                }

                // Returning the admin object
                return m_Collection.FindOne(m_Query);
            }
            catch (Exception ex)
            {
                // Logging the exception
                new Error().LogError(ex, Constants.c_ADMIN, Constants.m_GETADMINDETAILS);

                // Returning null
                return null;
            }
        }

        /// <summary>
        /// The method to retrieve the Admin Main DB ID given the Caching DB ID
        /// </summary>
        /// <param name="adminId">string: AdminID</param>
        /// <returns>int</returns>
        public int GetAdminRefID(string adminId)
        {
            try
            {
                // Caching DB object
                MongoDatabase cachingDataBase = CachingDbConnector.GetCachingDatabase();

                // Query to search the Admin collection
                var m_Query = Query<Admin>.EQ(x => x._id, adminId);

                // The admin collection
                var m_Collection = cachingDataBase.GetCollection<Admin>(Constants.c_ADMIN);

                // Running the query
                Admin adminDetailsObj = m_Collection.FindOne(m_Query);

                // if the admin details are not found, return 0
                if (adminDetailsObj == null)
                {
                    return 0;
                }

                // Returning the Admin ID from main DB upon success
                return adminDetailsObj.AdminRefId;
            }
            catch (Exception ex)
            {
                // Logging the exception
                new Error().LogError(ex, Constants.c_ADMIN, Constants.m_GETADMINREFID);

                // Returning 0
                return 0;
            }
        }

        /// <summary>
        /// Method to change the admin password
        /// </summary>
        /// <param name="adminId">int: The admin ID from the Main Database</param>
        /// <param name="password">string: The new password to change</param>
        /// <returns>Boolean</returns>
        public bool ChangeAdminPassword(int adminId, string password)
        {
            try
            {
                // The caching database object
                MongoDatabase cachingDataBase = CachingDbConnector.GetCachingDatabase();

                // Query to find the admin object in caching database
                var m_Query = Query<Admin>.EQ(e => e.AdminRefId, adminId);

                // The caching collection
                var m_Collection = cachingDataBase.GetCollection<Admin>(Constants.c_ADMIN);

                // Finding the admin object in the caching database
                var m_AdminObject = m_Collection.FindOne(m_Query);

                // If admin object is found in the caching database
                if (m_AdminObject != null)
                {
                    // Update the Main DB
                    using (intellidatev2Entities mainDB = new intellidatev2Entities())
                    {
                        using (var transaction = new TransactionScope())
                        {
                            // Find and update admin object in the Main DB
                            in_adminuser_mst adminObjectMainDB = mainDB.in_adminuser_mst.SingleOrDefault(c => c.AdminID == adminId);
                            adminObjectMainDB.Password = password;
                            mainDB.SaveChanges();
                            try
                            {
                                // Updating the caching object
                                var m_Update = Update<Admin>.Set(x => x.Password, password);
                                m_Collection.Update(m_Query, m_Update);
                                transaction.Complete();
                                return true;
                            }
                            catch (Exception exception)
                            {
                                // Logging the exception
                                new Error().LogError(exception, Constants.c_ADMIN, Constants.m_CHANGEADMINPASSWORD);

                                // Returning failure
                                return false;
                            }

                          
                        }
                    }
                }
                else
                {
                    // Returning failure if the admin object is not found
                    return false;
                }
            }
            catch (Exception ex)
            {
                // Logging the exception
                new Error().LogError(ex, Constants.c_ADMIN, Constants.m_CHANGEADMINPASSWORD);

                // Returning failure
                return false;
            }
        }




        /// <summary>
        /// Method to change the admin Emailid
        /// </summary>
        /// <param name="adminId">int: The admin ID from the Main Database</param>
        /// <param name="Password">string: The new Emailid to change</param>
        /// <returns>Boolean</returns>
        public bool ChangeAdminEmailAddress(int adminId, string emailId)
        {
            try
            {
                // The caching database object
                MongoDatabase cachingDataBase = CachingDbConnector.GetCachingDatabase();

                // Query to find the admin object in caching database
                var m_Query = Query<Admin>.EQ(e => e.AdminRefId, adminId);

                // The caching collection
                var m_Collection = cachingDataBase.GetCollection<Admin>(Constants.c_ADMIN);

                // Finding the admin object in the caching database
                var m_AdminObject = m_Collection.FindOne(m_Query);

                // If admin object is found in the caching database
                if (m_AdminObject != null)
                {
                   
                    // Update the Main DB
                    using (intellidatev2Entities mainDB = new intellidatev2Entities())
                    {
                        using (var transaction = new TransactionScope())
                        {
                            // Find and update admin object in the Main DB
                            in_adminuser_mst adminObjectMainDB = mainDB.in_adminuser_mst.SingleOrDefault(c => c.AdminID == adminId);
                            adminObjectMainDB.EmailID = emailId;
                            mainDB.SaveChanges();

                            try
                            {
                                // Updating the caching object
                                var m_Update = Update<Admin>.Set(x => x.EmailID, emailId);
                                m_Collection.Update(m_Query, m_Update);
                                transaction.Complete();
                                return true;
                            }
                            catch (Exception exception)
                            {
                                // Logging the exception
                                new Error().LogError(exception, Constants.c_ADMIN, Constants.m_CHANGEADMINEMILADDRESS);

                                // Returning failure
                                return false;
                            }
                           
                        }
                    };
                }
                else
                {
                    // Returning failure if the admin object is not found
                    return false;
                }
            }
            catch (Exception ex)
            {
                // Logging the exception
                new Error().LogError(ex, Constants.c_ADMIN, Constants.m_CHANGEADMINEMILADDRESS);

                // Returning failure
                return false;
            }
        }






        /// <summary>
        /// The method to return all the admin details
        /// </summary>
        /// <returns></returns>
        public Admin[] GetAllAdminUsers()
        {
            try
            {
                // The caching DB object
                MongoDatabase cachingDataBase = CachingDbConnector.GetCachingDatabase();

                // Query to search the admins with active status
                var m_Query = Query<Admin>.EQ(e => e.Status, Constants.c_STATUSACTIVE);

                // The admin collection in the caching DB
                var m_Collection = cachingDataBase.GetCollection<Admin>(Constants.c_ADMIN);

                // Running the query
                List<Admin> m_AllAdminUsers = new List<Admin>();

                m_AllAdminUsers = m_Collection.Find(m_Query).ToList();

                if (m_AllAdminUsers != null)
                {
                    // Sorting the admins
                    m_AllAdminUsers = m_AllAdminUsers.OrderBy(x => x.AdminName).ToList();

                    // returning details
                    return m_AllAdminUsers.ToArray();
                }
                else
                {
                    // Logging the error

                    // Returning null
                    return null;
                }

            }
            catch (Exception ex)
            {
                // Logging the exception
                new Error().LogError(ex, Constants.c_ADMIN, Constants.m_GETALLADMINUSERS);

                // Returning null
                return null;
            }
        }

        /// <summary>
        /// Get all the admins with the given login name
        /// </summary>
        /// <param name="loginName">string: Login name</param>
        /// <returns>Array of Admins</returns>
        public Admin[] GetAllAdminUsers(string loginName)
        {
            try
            {
                // The caching DB object
                MongoDatabase cachingDataBase = CachingDbConnector.GetCachingDatabase();

                // Query to search the admins with matching login name
                var m_Query = Query<Admin>.Matches(e => e.LoginName, loginName);

                // The admin collection in the caching DB
                var m_Collection = cachingDataBase.GetCollection<Admin>(Constants.c_ADMIN);

                // Running the query
                List<Admin> m_AllAdminUsers = new List<Admin>();
                m_AllAdminUsers = m_Collection.Find(m_Query).ToList();

                if (m_AllAdminUsers != null)
                {
                    // Sorting the admins
                    m_AllAdminUsers = m_AllAdminUsers.OrderBy(x => x.AdminName).ToList();

                    // returning details
                    return m_AllAdminUsers.ToArray();
                }
                else
                {
                    // Logging the error

                    // Returning null
                    return null;
                }

            }
            catch (Exception ex)
            {
                // Logging the exception
                new Error().LogError(ex, Constants.c_ADMIN, Constants.m_GETALLADMINUSERS);

                // Returning null
                return null;
            }
        }

    }

    /// <summary>
    /// The partial class for the contants
    /// </summary>
    public static partial class Constants
    {
        public static string c_ADMIN = "Admin";

        public static string m_ADDNEWADMINUSER = "AddNewAdminUser";
        public static string m_EDITAMINUSER = "EditAminUser";
        public static string m_ACTIVATEORDEACTIVATEADMIN = "ActivateOrDeActivateAdmin";
        public static string m_AUTHANTICATEADMIN = "AuthanticateAdmin";
        public static string m_GETADMINDETAILS = "GetAdminDetails";
        public static string m_GETADMINREFID = "GetAdminRefID";
        public static string m_CHANGEADMINPASSWORD = "ChangeAdminPassword";
        public static string m_GETALLADMINUSERS = "GetAllAdminUsers";
        public static string m_CHANGEADMINEMILADDRESS = "ChangeAdminEmailAddress";


        public static string c_STATUS = "S";
        public static string c_STATUSACTIVE = "A";

    }
}
