﻿//------------------------------------------------------------------------------
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
    using System.ComponentModel.DataAnnotations;
    
    public partial class User :BaseEntityModel
    {
        [Required(ErrorMessage="Nhập tên tài khoản")]
        public string UserName { get; set; }
        [Required(ErrorMessage="Nhập mật khẩu")]        
        public string Password { get; set; }
        [Required(ErrorMessage="Nhập tên")]
        public string Name { get; set; }
        [Required(ErrorMessage="Nhập lại mật khẩu")]
        [Compare("Password",ErrorMessage="Nhập lại mật khẩu không khớp")]
        public string RePassword { get; set; }
    }
}
