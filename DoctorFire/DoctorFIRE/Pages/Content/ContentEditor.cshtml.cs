using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DoctorFIRE.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace DoctorFIRE.Pages.Content
{
    public class ContentEditorModel : PageModel
    {
	    private readonly DoctorFIREContext _context;
	    public List<SelectListItem> Contexts { get; set; }

		public ContentEditorModel(DoctorFIREContext context)
		{
			_context = context;
		}

		public void OnGet()
		{
			Contexts = _context.Contexts.Where(e => e.Hide == false)
				.OrderBy(e => e.Name )
				.Select(e =>
				new SelectListItem
				{
					Value = e.Id.ToString(),
					Text = e.Name
				}).ToList();
		}
    }
}