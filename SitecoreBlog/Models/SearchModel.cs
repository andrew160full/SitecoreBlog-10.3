using Sitecore.ContentSearch;
using Sitecore.ContentSearch.SearchTypes;
using System.Collections.Generic;

namespace SitecoreBlog.Models
{
    public class SearchBlog: SearchResultItem
    {
        [IndexField("_name")]
        public virtual string BlogName { get; set; }

        [IndexField("shortdescription_t")]
        public virtual string ShortDescription { get; set; }

        [IndexField("longdescription_t")]
        public virtual string LongDescription { get; set; }

        [IndexField("title_t")]
        public virtual string BlogTitle { get; set; }
        [IndexField("blogid_t")]
        public virtual string BlogId { get; set; }
        [IndexField("_template")]
        public virtual string TemplateID { get; set; }
    }

    public class BlogSearchResult
    {
        public string BlogId { get; set; }
    }

    /// <summary>
    /// Custom search result model for binding to front end
    /// </summary>
    public class BlogSearchResults
    {
        public List<BlogSearchResult> Results { get; set; }
    }
}