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

    public partial class Customer
    {
        public int Customer_ID { get; set; }
        public string Customer_Name { get; set; }
        public string Customer_Gender { get; set; }
        public string Customer_Phone { get; set; }
        public string Customer_Address { get; set; }
        [Required]
        [Display(Name = "User Name")]
        public string Customer_Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Customer_Password { get; set; }
    }
}
