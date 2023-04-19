using MyBlogApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyBlogApp.Controllers
{
    [AttributeUsage(AttributeTargets.Class)]
    public class CategoryFilterAttribute : FilterAttribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            using (var db = new BlogContext())
            {
                var categories = db.Categories.OrderByDescending(item => item.Count).ToList();

                filterContext.Controller.ViewBag.Categories = categories;
            }
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            
        }
    }
}