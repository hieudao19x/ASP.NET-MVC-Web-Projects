using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MyBlogApp.Models
{
    public class Article
    {
        public int Id { get; set; }

        [Required]
        [DisplayName("タイトル")]
        public string Title { get; set; }

        [Required]
        [DisplayName("本文")]
        public string Body { get; set; }

        [DisplayName("投稿日")]
        public DateTime CreatedOn { get; set; }

        [DisplayName("更新日")]
        public DateTime Modified { get; set; }

        public virtual Category Category { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        [NotMapped]
        [DisplayName("カテゴリー")]
        public string CategoryName { get; set; }
    }
}