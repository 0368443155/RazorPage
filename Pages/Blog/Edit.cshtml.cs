using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CS58_Razor09_Entity_ASP.Models;

namespace CS58_Razor09_Entity_ASP.Pages_Blog
{
    public class EditModel : PageModel
    {
        private readonly CS58_Razor09_Entity_ASP.Models.MyBlogContext _context;

        public EditModel(CS58_Razor09_Entity_ASP.Models.MyBlogContext context)
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

            var article =  await _context.Articles.FirstOrDefaultAsync(m => m.Id == id);
            if (article == null)
            {
				return Content("Không tìm thấy bài viết");
			}
            Article = article;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            //Article đang độc lập với dbcontext, ta cần gắn nó vào dbcontext và gắn trạng thái là tự do để có thể chỉnh sửa thoải mái
            _context.Attach(Article).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArticleExists(Article.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ArticleExists(int id)
        {
            return _context.Articles.Any(e => e.Id == id);//kiểm tra trên db có dòng nào có giá trị đúng như thế không
        }
    }
}
