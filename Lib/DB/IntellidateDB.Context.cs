﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace IntellidateLib.DB
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class intellidatev2Entities : DbContext
    {
        public intellidatev2Entities()
            : base("name=intellidatev2Entities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<in_abusivereport_mst> in_abusivereport_mst { get; set; }
        public DbSet<in_admin_message_mst> in_admin_message_mst { get; set; }
        public DbSet<in_admin_message_trn> in_admin_message_trn { get; set; }
        public DbSet<in_admin_photo> in_admin_photo { get; set; }
        public DbSet<in_adminmessagehistory_mst> in_adminmessagehistory_mst { get; set; }
        public DbSet<in_adminprivileges_mst> in_adminprivileges_mst { get; set; }
        public DbSet<in_adminuser_mst> in_adminuser_mst { get; set; }
        public DbSet<in_advertisement_mst> in_advertisement_mst { get; set; }
        public DbSet<in_album_mst> in_album_mst { get; set; }
        public DbSet<in_attachment_mst> in_attachment_mst { get; set; }
        public DbSet<in_bodytype_mst> in_bodytype_mst { get; set; }
        public DbSet<in_comments_trn> in_comments_trn { get; set; }
        public DbSet<in_criteria_mst> in_criteria_mst { get; set; }
        public DbSet<in_criteriaoptions_trn> in_criteriaoptions_trn { get; set; }
        public DbSet<in_drink_mst> in_drink_mst { get; set; }
        public DbSet<in_drug_mst> in_drug_mst { get; set; }
        public DbSet<in_education_mst> in_education_mst { get; set; }
        public DbSet<in_ethnicity_mst> in_ethnicity_mst { get; set; }
        public DbSet<in_forumpost_trn> in_forumpost_trn { get; set; }
        public DbSet<in_forumprivileges_trn> in_forumprivileges_trn { get; set; }
        public DbSet<in_forumscategory_mst> in_forumscategory_mst { get; set; }
        public DbSet<in_havechildren_mst> in_havechildren_mst { get; set; }
        public DbSet<in_horoscope_mst> in_horoscope_mst { get; set; }
        public DbSet<in_income_mst> in_income_mst { get; set; }
        public DbSet<in_jobtype_mst> in_jobtype_mst { get; set; }
        public DbSet<in_language_mst> in_language_mst { get; set; }
        public DbSet<in_location_mst> in_location_mst { get; set; }
        public DbSet<in_message_trn> in_message_trn { get; set; }
        public DbSet<in_options_trn> in_options_trn { get; set; }
        public DbSet<in_orientation_mst> in_orientation_mst { get; set; }
        public DbSet<in_pages_mst> in_pages_mst { get; set; }
        public DbSet<in_photo_mst> in_photo_mst { get; set; }
        public DbSet<in_question_mst> in_question_mst { get; set; }
        public DbSet<in_questionrating_trn> in_questionrating_trn { get; set; }
        public DbSet<in_religion_mst> in_religion_mst { get; set; }
        public DbSet<in_reportedphotooptions_mst> in_reportedphotooptions_mst { get; set; }
        public DbSet<in_smoke_mst> in_smoke_mst { get; set; }
        public DbSet<in_user_mst> in_user_mst { get; set; }
        public DbSet<in_userlanguage_trn> in_userlanguage_trn { get; set; }
        public DbSet<in_userlogin_trn> in_userlogin_trn { get; set; }
        public DbSet<in_usermessagereport_mst> in_usermessagereport_mst { get; set; }
        public DbSet<in_userprofile_trn> in_userprofile_trn { get; set; }
        public DbSet<in_userprofilesave_trn> in_userprofilesave_trn { get; set; }
        public DbSet<in_userprofileview_trn> in_userprofileview_trn { get; set; }
        public DbSet<in_userquestions_trn> in_userquestions_trn { get; set; }
        public DbSet<in_video_mst> in_video_mst { get; set; }
        public DbSet<in_message_reply> in_message_reply { get; set; }
        public DbSet<in_user_notification> in_user_notification { get; set; }
    }
}
