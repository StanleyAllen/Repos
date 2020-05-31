using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DoctorFIRE.Models;

namespace DoctorFIRE.Pages.Content
{
    public class CreateModel : PageModel
    {
        private readonly DoctorFIRE.Models.DoctorFIREContext _context;

        public CreateModel(DoctorFIRE.Models.DoctorFIREContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["ContextId"] = new SelectList(_context.Contexts, "Id", "Id");
            return Page();
        }

        [BindProperty]
        public Models.Content Content { get; set; }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Contents.Add(Content);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
