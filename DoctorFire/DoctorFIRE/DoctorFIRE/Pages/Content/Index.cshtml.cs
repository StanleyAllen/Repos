using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DoctorFIRE.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DoctorFIRE.Pages.Content
{
    public class IndexModel : PageModel
    {
        private readonly DoctorFIRE.Models.DoctorFIREContext _context;

        public IndexModel(DoctorFIRE.Models.DoctorFIREContext context)
        {
            _context = context;
        }

        public List<SelectListItem> Contexts { get; set; }
		public List<SelectListItem> Soap { get; set; }
		public async Task OnGetAsync()
        {
				Contexts = _context.Contexts.Where(e => e.Hide == false)
					.OrderBy(e => e.Name)
					.Select(e =>
						new SelectListItem
						{
							Value = e.Id.ToString(),
							Text = e.Name
						}).ToList();

				Soap = new List<SelectListItem>()
				{
					new SelectListItem{ Text = "Subjective", Value="S"},
					new SelectListItem{ Text = "Objective", Value = "O"},
					new SelectListItem{Text = "Authority", Value = "A"},
					new SelectListItem{ Text = "Propose", Value = "P"}
				};
        }

		public JsonResult OnGetContents(Guid contextId)
		{
			var contents = _context.Contents.Where(e => e.Hide == false)
				.OrderBy(e => e.Text)
				.Select(e =>
					new SelectListItem
					{
						Value = e.Id.ToString(),
						Text = e.Text
					});

			return new JsonResult(contents);
		}
	}
}
