//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AddressBookApp.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Address
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Kana { get; set; }
        public string ZipCode { get; set; }
        public string Prefecture { get; set; }
        public string StreetAddress { get; set; }
        public string Telephone { get; set; }
        public string Mail { get; set; }
        public Nullable<int> Group_Id { get; set; }
    
        public virtual Group Group { get; set; }
    }
}
