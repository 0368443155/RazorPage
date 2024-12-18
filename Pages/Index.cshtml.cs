using CS58_Razor09_Entity_ASP.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CS58_Razor09_Entity_ASP.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        public readonly MyBlogContext _myBlogContext;

        public IndexModel(ILogger<IndexModel> logger, MyBlogContext myBlogContext)
        {
            _logger = logger;
            _myBlogContext = myBlogContext;
        }

        public void OnGet()
        {
            var posts = (from a in _myBlogContext.Articles
                        orderby a.Created descending
                        select a).ToList();
            ViewData["posts"] = posts;
        }
    }
}
