using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DoctorFIRE.Models;

namespace DoctorFIRE.Pages.Content
{
    public class EditModel : PageModel
    {
        private readonly DoctorFIRE.Models.DoctorFIREContext _context;

        public EditModel(DoctorFIRE.Models.DoctorFIREContext context)
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
           ViewData["ContextId"] = new SelectList(_context.Contexts, "Id", "Id");
            return Page();
        }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Content).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContentExists(Content.Id))
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

        private bool ContentExists(Guid id)
        {
            return _context.Contents.Any(e => e.Id == id);
        }
    }
}
