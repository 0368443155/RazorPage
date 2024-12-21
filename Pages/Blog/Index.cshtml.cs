using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CS58_Razor09_Entity_ASP.Models;

namespace CS58_Razor09_Entity_ASP.Pages_Blog
{
    public class IndexModel : PageModel
    {
        private readonly CS58_Razor09_Entity_ASP.Models.MyBlogContext _context;
        public IndexModel(CS58_Razor09_Entity_ASP.Models.MyBlogContext context)
        {
            _context = context;
        }

        public IList<Article> Article { get;set; } = default!;
        public const int ItemsPerPage = 5;

        [BindProperty(SupportsGet = true, Name = "p")]//binding dữ liệu từ query về cho giá trị currentPage
        public int currentPage { get; set; }
        public int countPages {  get; set; }

        public async Task OnGetAsync(string SearchString)
        {
            //Article = await _context.Articles.ToListAsync();

            int totalArticle = await _context.Articles.CountAsync();
            countPages = (int)Math.Ceiling((double)totalArticle / ItemsPerPage);
            if (currentPage < 1) currentPage = 1;
            if (currentPage > countPages) currentPage = countPages;

            var qr = (from a in _context.Articles
                      orderby a.Created descending
                      select a)
                     .Skip((currentPage - 1) * ItemsPerPage) //bỏ đi những phần tử phía đầu
                     .Take(ItemsPerPage); //lấy ra số lượng phần tử
            if (!string.IsNullOrEmpty(SearchString))
            {
                Article = qr.Where(a => a.Title.Contains(SearchString)).ToList();
            }
            else
            {
				Article = await qr.ToListAsync();
            }
        }
    }
}
