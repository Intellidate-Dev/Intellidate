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
    
    public partial class in_bodytype_mst
    {
        public in_bodytype_mst()
        {
            this.in_userprofile_trn = new HashSet<in_userprofile_trn>();
        }
    
        public int BodyTypeId { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
    
        public virtual ICollection<in_userprofile_trn> in_userprofile_trn { get; set; }
    }
}
