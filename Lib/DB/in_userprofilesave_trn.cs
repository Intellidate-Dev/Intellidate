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
    
    public partial class in_userprofilesave_trn
    {
        public int trnID { get; set; }
        public Nullable<int> UserID { get; set; }
        public Nullable<int> SaveUserID { get; set; }
        public System.DateTime SavedTime { get; set; }
        public string Status { get; set; }
    
        public virtual in_user_mst in_user_mst { get; set; }
        public virtual in_user_mst in_user_mst1 { get; set; }
    }
}
