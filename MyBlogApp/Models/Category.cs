using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyBlogApp.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        [DisplayName("カテゴリー")]
        public string CategoryName { get; set; }

        [DisplayName("件数")]
        public int Count { get; set; }

        public virtual ICollection<Article> Articles { get; set; }
    }
}