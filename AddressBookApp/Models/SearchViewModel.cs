using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AddressBookApp.Models
{
    public class SearchViewModel
    {
        [DisplayName("カナ")]
        public string Kana { get; set; }

        public List<Address> Addresses { get; set; }
    }
}