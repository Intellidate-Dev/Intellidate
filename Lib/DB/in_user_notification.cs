//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class in_user_notification
    {
        public int NotificationId { get; set; }
        public Nullable<int> UserId { get; set; }
        public Nullable<int> OtherUserId { get; set; }
        public string NotificationText { get; set; }
        public string NotificationType { get; set; }
        public Nullable<System.DateTime> TimeSpan { get; set; }
        public Nullable<bool> IsViewed { get; set; }
    }
}