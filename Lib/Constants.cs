using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntellidateLib
{
    public static partial class Constants
    {
        public static string EventLogName = "Intellidate";
       
        
        //All Class Names or caching db collection names
        
        public static string advertisementsClass = "Advertisements";
        public static string albumClass = "Album";
        public static string cachingDbConnectorClass = "CachingDbConnector";
        public static string commentsClass = "Comments";
        public static string errorClass = "Error";
        public static string loginDetailsClass = "LoginDetails";
        public static string messageClass = "Message";
        public static string adminMessageClass = "AdminMessages";
        public static string photoClass = "Photo";
        public static string profileSaveClass = "ProfileSave";
        public static string profileViewClass = "ProfileView";
        public static string userClass = "User";
        public static string videoClass = "Video";
        public static string drugsClass = "Drugs";
        public static string religionClass = "Religion";
        public static string ethnicityClass = "Ethnicity";
        public static string educationClass = "Education";
        public static string smokeClass = "Smoke";
        public static string drinkClass = "Drink";
        public static string haveChildrenClass = "HaveChildren";
        public static string bodyTypeClass = "BodyType";
        public static string horoscopeClass = "Horoscope";
        public static string locationClass = "Location";
        public static string incomeClass = "Income";
        public static string jobTypeClass = "JobType";
        public static string languageClass = "Language";
        public static string orientationClass = "MyOrientation";
        public static string userProfileClass = "UserProfile";
        public static string userLanguagesClass = "UserLanguages";
        public static string reportedPhotoOptionsClass = "ReportedPhotoOptions";
        public static string adminMasterMessageClass = "AdminMasterMessage";
        public static string attachmentsClass = "Attachments";
        public static string adminPhotoClass = "AdminPhoto";
        public static string abusiveReportClass = "AbusiveReport";
        public static string adminMessageHistoryClass = "AdminMessageHistory";
        public static string messageReplyClass = "MessageReply";
        public static string redisMessageClass = "RedisMessage";
        public static string userNotificationClass = "UserNotification";
        public static string textToWordsClass = "TextToWords";

        // Forums
        public static string forumCategoryClass = "ForumCategory";
        public static string forumClass = "Forum";
        public static string getAllForumCategoriesMethod = "GetAllForumCategories";
        public static string addNewCategoryMethod = "AddNewCategory";
      
        //values
        public static string c_True = "True";
        public static string c_False= "False";


        // Options
        public static string criteriaClass = "Criteria";
        public static int criteriaTypeOptions = 1;

        //All Method Names And Used Variable Names
        //Admin Class Methods 

        public static string addNewAdminUserMethod = "AddNewAdminUser";
        public static string editAdminUserMethod = "AddNewAdminUser"; 

        public static string activateOrDeActivateAdminMethod = "ActivateOrDeActivateAdmin";
        public static string authanticateAdminMethod = "AuthanticateAdmin";
        public static string deleteAdminMethod = "DeleteAdmin";
        public static string changeAdminUserPasswordMethod = "ChangeAdminUserPassword";
        public static string getAllAdminUsersMethod = "GetAllAdminUsers";
        public static string getAdminPasswordMethod = "GetAdminPassword";

        //Properties
        public static string adminRefId = "AdminRefId";
        public static string adminId = "AdminId";
        public static string status = "Status";
        public static string password = "Password";
        public static string mongoId = "_id";
        //Constants
        public static string activeStatus = "A";
        public static string inActiveStatus = "I";
        public static string pendingStatus = "P";
        public static string successStatus = "S";
        public static string unSuccessStatus = "U";
        public static string emptyString = "";

        //Advertisements Class Methods
        public static string addNewAdvertisementMethod = "AddNewAdvertisement";
        public static string deleteAdvertisementMethod = "DeleteAdvertisement";
        public static string getAdvertisementsMethod = "GetAdvertisements";
        //Properties
        public static string advertisementId = "AdvertisementID";

        //Album Class Methods
        public static string saveAlbumMethod = "SaveAlbum";
        public static string getAlbumsMethod = "GetAlbums";

        //CachingDbConnector Class Methods
        public static string getCachingDatabaseMethod = "GetCachingDatabase";      
        public static string getRedisDatabaseMethod = "GetRedisDatabase";
        
        //Constants
        public static string cachingDBConnectionString = "CachingDBConnectionString";
        public static string cachingDB = "CachingDB";
        public static string redisServerName = "RedisDbServer";

        //Comments Class Methods
        public static string addNewCommentMethod = "AddNewComment";
        public static string getUserPhotoCommentsMethod = "GetUserPhotoComments";
        public static string deleteCommentMethod = "DeleteComment";
        public static string getThisMonthCommentsMethod = "GetThisMonthComments";
        public static string getThisWeekCommentsMethod = "GetThisWeekComments";
        public static string getThisDayCommentsMethod = "GetThisDayComments";
        public static string getPhotosBasedOnStatusMethod = "GetPhotosBasedOnStatus";
        //Properties
        public static string userId = "UserID";
        public static string commentId = "CommentID";

        //Error Class Methods
        public static string logErrorMethod = "LogError";

        //Constants
        public static string errorMsg = "Method Name is ";
        public static string nextLine = " \n";

        //LoginDetails Class Methods
        public static string addUserLoginMethod = "AddUserLogin";
        public static string getUserLoginHistoryMethod = "GetUserLoginHistory";
        public static string getUserLastLoginDetailsMethod = "GetUserLastLoginDetails";


        //Message Class Methods
        public static string sendMessageMethod = "SendMessage";
        public static string getConversationMethod = "GetConversation";
        public static string editMessageMethod = "EditMessage";
        public static string deleteMessageMethod = "DeleteMessage";
        public static string getThisMonthSentMessagesMethod = "GetThisMonthSentMessages";
        public static string getThisWeekSentMessagesMethod = "GetThisWeekSentMessages";
        public static string getThisDaySentMessagesMethod = "GetThisDaySentMessages";

        public static string getThisMonthReportedMessagesMethod = "GetThisMonthReportedMessages";
        public static string getThisWeekReportedMessagesMethod = "GetThisWeekReportedMessages";
        public static string getTodayReportedMessagesMethod = "GetTodayReportedMessages";
        public static string getReportedMessagesBetweenTwoDatesMethod = "GetReportedMessagesBetweenTwoDates";
        public static string getRejectedMessageListMethod = "GetRejectedMessageList";
        public static string rejectOrApproveReportedMessageMethod = "RejectOrApproveReportedMessage";


        public static string updateReadMessageMethod = "UpdateReadMessage";
        public static string getUserUnReadMessagesMethod = "GetUserUnReadMessages";
        public static string getUserReadMessagesMethod = "GetUserReadMessages";



        
        //Redis db message class methods
        public static string saveRedisDraftMessageMethod = "SaveRedisDraftMessage";
        public static string getRedisDraftMessagesMethod = "GetRedisDraftMessages";
        public static string getRedisDraftMessageByKeyMethod = "GetRedisDraftMessageByKey";
        public static string deleteRedisDraftMessageMethod = "deleteRedisDraftMessageMethod";
    


        //Properties
        public static string isEdited = "IsEdited";
        public static string lastEdited = "LastEdited";
        public static string messageText = "MessageText";
        public static string messageRefID = "MessageRefID";
        public static string isRejectedByAdmin = "IsRejectedByAdmin";
        public static string isMessageViewed = "IsMessageViewed";


        //Photo Class Methods
        public static string savePhotoMethod = "SavePhoto";
        public static string deletePhotosMethod = "DeletePhotos";
        public static string getPhotosMethod = "GetPhotos";
        public static string getAlbumPhotoMethod = "GetAlbumPhoto";
        public static string getProfilePhotoMethod = "GetProfilePhoto";
        public static string getAllPhotosMethod = "GetAllPhotos";
        public static string getUserUsedSpaceForPhotosMethod = "GetUserUsedSpaceForPhotos";
        public static string approveOrRejectPhotoMethod = "ApproveOrRejectPhoto";

        public static string getAbusiveReportedPhotosMethod = "GetAbusiveReportedPhotos";
        public static string getPhotosBasedOnStatusCountMethod = "GetPhotosBasedOnStatusCount";
        public static string getAdminLastApprovedPhotosMethod = "GetAdminLastApprovedPhotos";
        public static string getPhotosBasedOnUserNameMethod = "GetPhotosBasedOnUserName";
        

        //Properties
        public static string photoId = "PhotoId";
        public static string videoId = "VideoId";


        //ProfileSave Class Methods
        public static string saveProfileMethod = "SaveProfile";
        public static string deleteSavedProfileMethod = "DeleteSavedProfile";
        public static string getAllSavedProfilesMethod = "GetAllSavedProfiles";
        public static string getWhoSavedMyProfile = "GetWhoSavedMyProfile";
        public static string getThisMonthSavedProfilesMethod = "GetThisMonthSavedProfiles";
        public static string getThisWeekSavedProfilesMethod = "GetThisWeekSavedProfiles";
        public static string getThisDaySavedProfilesMethod = "GetThisDaySavedProfiles";

        //Properties
        public static string trnId = "_trnID";
        public static string otherUser = "OtherUser property";

        //ProfileView Class Methods
        public static string addNewProfileViewMethod = "AddNewProfileView";
        public static string getNoOfProfileViewsMethod = "GetNoOfProfileViews";
        public static string getAllProfileViewsMethod = "GetAllProfileViews";
        public static string getThisMonthProfileViewsMethod = "GetThisMonthProfileViews";
        public static string getThisWeekProfileViewsMethod = "GetThisWeekProfileViews";
        public static string getThisDayProfileViewsMethod = "GetThisDayProfileViews";
       
        //User Class Methods
        public static string registerUserMethod = "RegisterUser";
        public static string addNewUserMethod = "AddNewUser";
        public static string editUserDetailsMethod = "EditUserDetails";
        public static string deleteUserMethod = "DeleteUser";
        public static string getPasswordMethod = "GetPassword";
        public static string changeUserPasswordMethod = "ChangeUserPassword";
        public static string changeUserPasswordBasedOnEmailIdMethod = "ChangeUserPasswordBasedOnEmailId";
        public static string getAllActiveUsersMethod = "GetAllActiveUsers";
        public static string getUserDetailsMethod = "GetUserDetails";
        public static string getThisMonthUsersMethod = "GetThisMonthUsers";
        public static string getThisWeekUsersMethod = "GetThisWeekUsers";
        public static string getThisDayUsersMethod = "GetThisDayUsers";
        public static string authenticateUserMethod = "AuthenticateUser";
        public static string checkEmailAddressMethod = "CheckEmailAddress";
        public static string checkUserNameMethod = "CheckUserName";
        public static string getUsersBasedOnSearchMethod = "GetUsersBasedOnSearch";

        public static string bsonArraytoIntArrayMethod = "BsonArraytoIntArray";
        public static string getAllUsersByNameSearchNoLimitMethod = "GetAllUsersByNameSearchNoLimit";


        //Properties
        public static string refId = "_RefID";
        public static string loginName = "LoginName";
        public static string fullName = "FullName";
        public static string emailAddress = "EmailAddress";
        public static string userGender = "UserGender";
        public static string dateOfBirth = "DateOfBirth";
        public static string userCreatedDate = "UserCreatedDate";
        public static string lastOnlineTime = "LastOnlineTime";
        public static string userAgeInYears = "UserAgeInYears";
        

        //Video Class Methods

        public static string addNewVideosMethod = "AddNewVideos";
        public static string getUserVideosMethod = "GetUserVideos";
        public static string deleteVideosMethod = "DeleteVideos";
        public static string getThisMonthVideosMethod = "GetThisMonthVideos";
        public static string getThisWeekVideosMethod = "GetThisWeekVideos";
        public static string getThisDayVideosMethod = "GetThisDayVideos";
        public static string getUserUsedSpaceForVideosMethod = "GetUserUsedSpaceForVideos";
       
        //Admin Messages class methods
        public static string sendAdminMessageMethod = "SendAdminMessage";
        

        //Drugs class methos
        public static string getDrugDetailsMethod = "GetDrugDetails";
        public static string addDrugDetailsMethod = "AddDrugDetails";
        public static string activateOrDeactivateDrugDetailsMethod = "ActivateOrDeactivateDrugDetails";
        public static string getDrugDetailsByIdMethod = "GetDrugDetailsById";
        public static string updateDrugDetailsMethod = "UpdateDrugDetails";

        // Photo status
        public static string photoStatusPending = "P"; //if one admin approve status is pending
        public static string photoStatusRejected = "R"; //if admin rejects the photo
        public static string photoStatusReported= "U";  //user reported photo status
        public static string photoStatusApproved = "A"; //if two continue approves status will be changed to approved
        public static string photoStatusNewRequest = "N"; //if user uploads new photo

        public static string photoStatusPrimary = "StatusPrimary";
        public static string photoStatusSecondary = "StatusSecondary"; 

        //Religion class methos
        public static string addReligionTypeMethod = "AddReligionType";
        public static string getReligionMethod = "GetReligion";
        public static string activateOrDeactivateReligionMethod = "ActivateOrDeactivateReligion";
        public static string getReligionByIdMethod = "GetReligionById";
        public static string updateReligionMethod = "UpdateReligion";

        //Ethnicity class methos
        public static string addEthnicityMethod = "AddEthnicity";
        public static string getEthnicityMethod = "GetEthnicity";
        public static string activateOrDeactivateEthnicityMethod = "ActivateOrDeactivateEthnicity";
        public static string getEthnicityByIdMethod = "GetEthnicityById";
        public static string updateEthnicityMethod = "UpdateEthnicity";

        //Education class methos
        public static string addQualificationMethod = "AddQualification";
        public static string getEducationDetailsMethod = "GetEducationDetails";
        public static string activateOrDeactivateEducationMethod = "ActivateOrDeactivateEducation";
        public static string getEducationDetailsByIdMethod = "GetEducationDetailsById";
        public static string updateEducationDetailsMethod = "UpdateEducationDetails";

        //Smoke Class methods
        public static string addSmokeDetailsMethod = "AddSmokeDetails";
        public static string getSmokeDetailsMethod = "GetSmokeDetails";
        public static string activateOrDeactivateSmokeDetailsMethod = "ActivateOrDeactivateSmokeDetails";
        public static string getSmokeDetailsByIdMethod = "GetSmokeDetailsById";
        public static string updateSmokeDetailsMethod = "UpdateSmokeDetails";

        //Drink Class methods
        public static string addDrinkDetailsMethod = "AddDrinkDetails";
        public static string getDrinkDetailsMethod = "GetDrinkDetails";
        public static string activateOrDeactivateDrinkDetailsMethod = "ActivateOrDeactivateDrinkDetails";
        public static string getDrinkDetailsByIdMethod = "GetDrinkDetailsById";
        public static string updateDrinkDetailsMethod = "UpdateDrinkDetails";

      

        //HaveChildren Class methods
        public static string addHaveChildrenDetailsMethod = "AddHaveChildrenDetails";
        public static string getHaveChildrenDetailsMethod = "GetHaveChildrenDetails";
        public static string activateOrDeactivateHaveChildrenDetailsMethod = "ActivateOrDeactivateHaveChildrenDetails";
        public static string getHaveChildrenDetailsByIdMethod = "GetHaveChildrenDetailsById";
        public static string updateHaveChildrenDetailsMethod = "UpdateHaveChildrenDetails";


        //BodyType Class methods
        public static string addBodyTypeDetailsMethod = "AddBodyTypeDetails";
        public static string getBodyTypeDetailsMethod = "GetBodyTypeDetails";
        public static string activateOrDeactivateBodyTypeDetailsMethod = "ActivateOrDeactivateBodyTypeDetails";
        public static string getBodyTypeDetailsByIdMethod = "GetBodyTypeDetailsById";
        public static string updateBodyTypeDetailsMethod = "UpdateBodyTypeDetails";

        //Horoscope Class methods
        public static string addHoroscopeDetailsMethod = "AddHoroscopeDetails";
        public static string getHoroscopeDetailsMethod = "GetHoroscopeDetails";
        public static string activateOrDeactivateHoroscopeDetailsMethod = "ActivateOrDeactivateHoroscopeDetails";
        public static string getHoroscopeDetailsByIdMethod = "GetHoroscopeDetailsById";
        public static string updateHoroscopeDetailsMethod = "UpdateHoroscopeDetails";

        //Location Class methods
        public static string addLocationMethod = "AddLocation";
        public static string getLocationsMethod = "GetLocations";
        public static string activateOrDeactivateLocationMethod = "ActivateOrDeactivateLocation";
        public static string getLocationsByIdMethod = "GetLocationsById";
        public static string updateLocationsMethod = "UpdateLocations";

        //Income Class methods
        public static string addIncomeDetailsMethod = "AddIncomeDetails";
        public static string getIncomeDetailsMethod = "GetIncomeDetails";
        public static string activateOrDeactivateIncomeDetailsMethod = "ActivateOrDeactivateIncomeDetails";
        public static string getIncomeDetailsByIdMethod = "GetIncomeDetailsById";
        public static string UpdateIncomeDetailsMethod = "UpdateIncomeDetails";

        //JobType Class methods
        public static string addJobDetailsMethod = "AddJobDetails";
        public static string getJobDetailsMethod = "GetJobDetails";
        public static string activateOrDeactivateJobDetailsMethod = "ActivateOrDeactivateJobDetails";
        public static string getJobDetailsByIdMethod = "GetJobDetailsById";
        public static string updateJobDetailsMethod = "UpdateJobDetails";

        //Language Class methods
        public static string addLanguageMethod = "AddLanguage";
        public static string getLanguagesMethod = "GetLanguages";
        public static string activateOrDeactivateLanguageMethod = "ActivateOrDeactivateLanguage";
        public static string getLanguagesByIdMethod = "GetLanguagesById";
        public static string updateLanguagesMethod = "UpdateLanguages";

        //Orientation Class methods
        public static string addOrientationMethod = "AddOrientation";
        public static string getOrientationMethod = "GetOrientation";
        public static string activateOrDeactivateOrientationMethod = "ActivateOrDeactivateOrientation";
        public static string getOrientationByIdMethod = "GetOrientationById"; 
        public static string updateOrientationMethod = "UpdateOrientation";        

        //static class collections properties
        public static string desc = "Description";
        public static string qualification = "Qualification";
        public static string ethnicityName = "EthnicityName";
        public static string jobTitle = "JobTitle";
        public static string languageName = "LanguageName";
        public static string locationName = "LocationName";
        public static string religionType = "ReligionType";
        public static string smokeDetails = "SmokeDetails";
        public static string salary = "Salary";
        public static string orientationType = "OrientationType";
        public static string proficiency = "Proficiency";

        //UserProfile class methods
        public static string insertUserProfileMethod = "InsertUserProfile";
        public static string getUserProfileMethod = "GetUserProfile";
        public static string updateUserProfileMethod = "UpdateUserProfile";
        //UserProfile class properties
        public static string sUserId = "UserId";
        public static string bodyTypeId = "BodyTypeId";
        public static string drinkId = "DrinkId";
        public static string eduId = "EduId";
        public static string ethnicityId = "EthnicityId";
        public static string drugId = "DrugId";
        public static string haveChildrenId = "HaveChildrenId";
        public static string horoscopeId = "HoroscopeId";
        public static string smokeId = "SmokeId";
        public static string locationId = "LocationId";
        public static string religionId = "ReligionId";
        public static string jobId = "JobId";
        public static string orientationId = "OrientationId";
        public static string userWeight = "UserWeight";
        public static string incomeId = "IncomeId";
        public static string height = "Height";
        public static string horoscopeImportance = "HoroscopeImportance";
        public static string religionImportance = "ReligionImportance";
        public static string bloodGroup = "BloodGroup";
        public static string isVesibleToStraightPeople = "IsVesibleToStraightPeople";


        //UserLanguages class methods
        public static string addUserLanguagesMethod = "AddUserLanguages";
        public static string getUserLanguagesByIdMethod = "GetUserLanguagesById";
        public static string activateOrDeactivateUserLanguagesMethod = "ActivateOrDeactivateUserLanguages";
        public static string updateUserLanguagesMethod = "UpdateUserLanguages";


        //ReportedPhotoOptions Class methods
        public static string addReportedPhotoOptionsMethod = "AddReportedPhotoOptions";
        public static string getReportedPhotoOptionsMethod = "GetReportedPhotoOptions";
        public static string getReportedPhotoOptionsByIdMethod = "GetReportedPhotoOptionsById";
        public static string activateOrDeactivateReportedPhotoOptionsMethod = "ActivateOrDeactivateReportedPhotoOptions";
        public static string updateReportedPhotoOptionsMethod = "UpdateReportedPhotoOptions";        

        //Properties
        public static string optionText = "OptionText";

        //AdminMaster message class methods
        public static string addAdminMasterMessageMethod = "AddAdminMasterMessage";

        //Attachments class methods
        public static string addAttachmentMethod = "AddAttachment";
        public static string getAttachmentByIdMethod = "GetAttachmentById";

        //AdminPhoto class methods
        public static string addAdminPhotoReportMethod = "AddAdminPhotoReport";
        public static string getAdminPhotoReportByIdMethod = "GetAdminPhotoReportById";
        public static string getAdminPhotoReportByAdminIdMethod = "GetAdminPhotoReportByAdminId";
        public static string getAdminPhotoLastApprovalStatusMethod = "GetAdminPhotoLastApprovalStatus";
        

        //AbusiveReport class methods
        public static string addAbusiveReportMethod = "AddAbusiveReport";
        public static string getAbusiveReportMethod = "GetAbusiveReport";
        public static string getAllAbusiveReportsMethod = "GetAllAbusiveReports";

        //AdminMessageHistory class methods
        public static string addAdminMessageHistoryMethod = "AddAdminMessageHistory";
        public static string getAdminMessageHistoryMethod = "GetAdminMessageHistory";
        public static string getAdminMessageHistoryByMessageIdMethod = "GetAdminMessageHistoryByMessageId";
        public static string getAdminRejectedMessagesHistoryMethod = "GetAdminRejectedMessagesHistory";

        //MessageRePlay Class Methods
        public static string addMessageReplyMethod = "AddMessageReply";
        public static string getMessageReplyByIdMethod = "GetMessageReplyById";
        public static string updateMessageReplyMethod = "UpdateMessageReply";
      
        //properties
        public static string IsReply = "IsReply";


        //UserNotification class methods
        public static string getNotificationMethod = "GetNotification";
        public static string AddNotificationMethod = "AddNotification";


        //TextToWords static class methods
        public static string convertTextToWordsMethod = "ConvertTextToWords";

    }
}

