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
    
    public partial class in_criteriaoptions_trn
    {
        public int trnid { get; set; }
        public Nullable<int> criteriaid { get; set; }
        public string optiontext { get; set; }
        public string status { get; set; }
    
        public virtual in_criteria_mst in_criteria_mst { get; set; }
    }
}
