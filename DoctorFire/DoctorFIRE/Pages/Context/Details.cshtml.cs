﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DoctorFIRE.Models;

namespace DoctorFIRE.Pages.Context
{
    public class DetailsModel : PageModel
    {
        private readonly DoctorFIRE.Models.DoctorFIREContext _context;

        public DetailsModel(DoctorFIRE.Models.DoctorFIREContext context)
        {
            _context = context;
        }

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
    }
}
