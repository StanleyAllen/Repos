using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DoctorFIRE.Models;

namespace DoctorFIRE.Pages.Context
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
            return Page();
        }

        [BindProperty]
        public Models.Context Context { get; set; }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Contexts.Add(Context);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
