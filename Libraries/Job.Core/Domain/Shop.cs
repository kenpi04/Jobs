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
    
    public partial class Shop :BaseEntityModel
    {
        public string ShopAddress { get; set; }
        public string PhoneNumber { get; set; }
        public int LocationId { get; set; }
        public virtual  District  District { get; set; }
        public string Name { get; set; }
    }
}
