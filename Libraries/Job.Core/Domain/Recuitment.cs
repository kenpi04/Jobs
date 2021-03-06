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
    
    public partial class Recuitment:BaseEntityModel
    {
        public int CateId { get; set; }
        public string WorkPlace { get; set; }
        public string FullName { get; set; }
        public System.DateTime BirthDay { get; set; }
        public string IdNo { get; set; }
        public string Address { get; set; }
        public int StateProvinceId { get; set; }
        public string Email { get; set; }
        public string Height { get; set; }
        public string Weight { get; set; }
        public bool Gender { get; set; }
        public int BirthPlace { get; set; }
        public bool MarriedStatus { get; set; }
        public int DistrictId { get; set; }
        public string Mobile { get; set; }
        public string DesiredIncome { get; set; }
        public string PersonalInformation { get; set; }
        public string School { get; set; }
        public string MajorContent { get; set; }
        public int QualificationType { get; set; }
        public string Certificate { get; set; }
        public string StudyScheduleMorning { get; set; }
        public string StudyScheduleAfternoon { get; set; }
        public string StudyScheduleEvening { get; set; }
        public string Image { get; set; }
        public string CvFileName { get; set; }
        public System.DateTime DateCreate { get; set; }
        public int Status { get; set; }
        public virtual  CareerNewCate Cate { get; set; }
        public virtual District DistrictModel { get; set; }
        public virtual StateProvice BirthPlaceModel { get; set; }
        public virtual ICollection<WorkedCompany> WokedCompany { get; set; }
    }
}
