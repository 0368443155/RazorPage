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
    public class DeleteModel : PageModel
    {
        private readonly CS58_Razor09_Entity_ASP.Models.MyBlogContext _context;

        public DeleteModel(CS58_Razor09_Entity_ASP.Models.MyBlogContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Article Article { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return Content("Không tìm thấy bài viết");
            }

            var article = await _context.Articles.FirstOrDefaultAsync(m => m.Id == id);

            if (article is not null)
            {
                Article = article;

                return Page();
            }

            return NotFound();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return Content("Không tìm thấy bài viết");
            }

            var article = await _context.Articles.FindAsync(id); //tìm trong list Articles xem có article nào có id được truyền vào không
            if (article != null)
            {
                Article = article;//nếu có thì gán article tìm được cho Article được post lên 
                _context.Articles.Remove(Article);//xóa Article được post lên khỏi list Article trong dbcontext
                await _context.SaveChangesAsync();//lưu thay đổi
            }

            return RedirectToPage("./Index");
        }
    }
}
