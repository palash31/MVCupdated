//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace The_New_Paradise.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class AdminTable
    {
        public int Admin_ID { get; set; }
        public string Admin_Name { get; set; }
        public string Admin_Phone { get; set; }
        public string Admin_Address { get; set; }
        [Display(Name = "User Name")]
        [Required]
        public string Admin_Email { get; set; }
        [Display(Name = "Password")]
        [Required]
        [DataType(DataType.Password)]
        public string Admin_Password { get; set; }
    }
}