//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Job.Core.Domain
{
    using System;
    using System.Collections.Generic;
    
    public partial class Banner:BaseEntityModel
    {
        public string Title { get; set; }
        public string Image { get; set; }
        public string ImageName { get; set; }
        public Nullable<System.DateTime> DateCreate { get; set; }
        public Nullable<bool> Active { get; set; }
    }
}
