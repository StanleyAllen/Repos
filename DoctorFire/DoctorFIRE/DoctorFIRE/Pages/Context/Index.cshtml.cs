using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DoctorFIRE.Models;

namespace DoctorFIRE.Pages.Context
{
    public class IndexModel : PageModel
    {
        private readonly DoctorFIRE.Models.DoctorFIREContext _context;

        public IndexModel(DoctorFIRE.Models.DoctorFIREContext context)
        {
            _context = context;
        }

        public IList<Models.Context> Context { get;set; }

        public async Task OnGetAsync()
        {
            Context = await _context.Contexts.ToListAsync();
        }
    }
}
