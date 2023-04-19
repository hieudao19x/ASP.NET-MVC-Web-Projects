using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AddressBookApp.Models
{
    [MetadataType(typeof(GroupMetadata))]
    public partial class Group
    {

    }
    public class GroupMetadata
    {
        [DisplayName("グループ名")]
        [Required]
        public string Name { get; set; }
    }
}