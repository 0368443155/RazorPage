using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CS58_Razor09_Entity_ASP.Pages.Shared
{
    public class _PagingHTLModel : PageModel
    {
        public int currentPage { get; set; }
        public int countPages { get; set; }
        public Func<int?, string> generateUrl { get; set; }
        public void OnGet()
        {
		}
    }
}
