using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DoctorFIRE.Models;

namespace DoctorFIRE.Pages.Context
{
    public class EditModel : PageModel
    {
        private readonly DoctorFIRE.Models.DoctorFIREContext _context;

        public EditModel(DoctorFIRE.Models.DoctorFIREContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Models.Context Context { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Context = await _context.Contexts.FirstOrDefaultAsync(m => m.Id == id);

            if (Context == null)
            {
                return NotFound();
            }
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

            _context.Attach(Context).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContextExists(Context.Id))
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

        private bool ContextExists(Guid id)
        {
            return _context.Contexts.Any(e => e.Id == id);
        }
    }
}
