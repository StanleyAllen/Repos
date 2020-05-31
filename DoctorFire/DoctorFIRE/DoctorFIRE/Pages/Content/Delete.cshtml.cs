using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DoctorFIRE.Models;

namespace DoctorFIRE.Pages.Content
{
    public class DeleteModel : PageModel
    {
        private readonly DoctorFIRE.Models.DoctorFIREContext _context;

        public DeleteModel(DoctorFIRE.Models.DoctorFIREContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Models.Content Content { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Content = await _context.Contents
                .Include(c => c.Context).FirstOrDefaultAsync(m => m.Id == id);

            if (Content == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Content = await _context.Contents.FindAsync(id);

            if (Content != null)
            {
                _context.Contents.Remove(Content);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
