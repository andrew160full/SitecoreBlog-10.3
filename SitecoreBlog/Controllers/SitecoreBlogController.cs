using Sitecore.ContentSearch;
using Sitecore.ContentSearch.Linq;
using Sitecore.ContentSearch.Linq.Utilities;
using SitecoreBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace SitecoreBlog.Controllers
{
    public class SitecoreBlogController : Controller
    {
        public ActionResult GetArticleList()
        {
            return View("~/Views/SitecoreBlog/ArticleList.cshtml", GetItems());
        }
        public ActionResult DemoComponent()
        {

            var item = Sitecore.Context.Database.GetItem("{97108A05-9C12-4700-A39A-F6B69B613A98}");
                
            return View("~/Views/SitecoreBlog/ControllerDemo.cshtml", item);
        }
        public ActionResult DoSearch(string searchText)
        {
            //return View("~/Views/SitecoreBlog/ArticleList.cshtml", GetItems().Where(x => x != null).Where(x => x.Fields["Title"].Value.Contains(searchText)
            //|| x.Fields["ShortDescription"].Value.Contains(searchText)));
            return View("~/Views/SitecoreBlog/Index.cshtml", GetItems().Where(x => x != null).Where(x => x.Fields["Title"].Value.Contains(searchText)
            || x.Fields["ShortDescription"].Value.Contains(searchText)));
        }

        public List<Sitecore.Data.Items.Item> GetItems()
        {
            var items = new List<Sitecore.Data.Items.Item>();
            var searchIndex = ContentSearchManager.GetIndex("sitecore_web_index"); // Get the search index
            
            using (var searchContext = searchIndex.CreateSearchContext()) // Get a context of the search index
            {
                var searchResults = searchContext.GetQueryable<SearchBlog>().Where(x => x.TemplateID == "bd77ee380e0942caa0f9d9ce322e4de6");
                var fullResults = searchResults.GetResults();

                foreach (var hit in fullResults.Hits)
                {
                    items.Add(Sitecore.Context.Database.GetItem(hit.Document.BlogId));
                }

                return items.Where(x => x != null).ToList();
            }
        }

        /// <summary>
        /// Search logic
        /// </summary>
        /// <param name="searchText">Search term</param>
        /// <returns>Search predicate object</returns>
        public static Expression<Func<SearchBlog, bool>> GetSearchPredicate(string searchText)
        {
            var predicate = PredicateBuilder.True<SearchBlog>();

            predicate = predicate.Or(x => x.BlogName.Contains(searchText));
            predicate = predicate.Or(x => x.BlogTitle.Contains(searchText));
            predicate = predicate.Or(x => x.ShortDescription.Contains(searchText));
            predicate = predicate.Or(x => x.LongDescription.Contains(searchText)); // .Boost(2.0f);

            //predicate = predicate.Or(x => x.Title.Contains(searchText)); // .Boost(2.0f);
            //Where Author="searchText" OR Description="searchText" OR Title="searchText"
            return predicate;
        }
    }
}