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
    
    public partial class in_orientation_mst
    {
        public in_orientation_mst()
        {
            this.in_userprofile_trn = new HashSet<in_userprofile_trn>();
        }
    
        public int OrientationId { get; set; }
        public string OrientationType { get; set; }
        public string Status { get; set; }
    
        public virtual ICollection<in_userprofile_trn> in_userprofile_trn { get; set; }
    }
}
