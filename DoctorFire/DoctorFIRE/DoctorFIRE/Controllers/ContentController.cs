using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using DoctorFIRE.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Controller = Microsoft.AspNetCore.Mvc.Controller;
using JsonResult = Microsoft.AspNetCore.Mvc.JsonResult;
using SelectListItem = Microsoft.AspNetCore.Mvc.Rendering.SelectListItem;

namespace DoctorFIRE.Controllers
{
	[ApiController]
	[Microsoft.AspNetCore.Mvc.Route("doctorfire/api")]

	public class ContentController : Controller
	{
		private readonly DoctorFIREContext _context;

		public ContentController(DoctorFIREContext context)
		{
			_context = context;
		}

		[Microsoft.AspNetCore.Mvc.HttpPost("savecontent")]
		public async Task<IActionResult> SaveContent([FromBody] ContentBody content)
		{
			var model = new Content()
			{
				Text = content.Text,
				Soap = content.Soap,
				ContextId = content.ContextId,
				Reference = content.Reference
			};



			_context.Contents.Add(model);
			await _context.SaveChangesAsync();
			return new OkResult();
		}

		[Microsoft.AspNetCore.Mvc.HttpGet("contentsbycontextId")]
		public JsonResult OnGetContents(Guid contextId)
		{
			var contents = _context.Contents.Where(e => e.Hide == false && e.ContextId == contextId)
				.OrderBy(e => e.Text)
				.Select(e =>
					new SelectListItem
					{
						Value = e.Id.ToString(),
						Text = e.Text
					}).ToList();
			return new JsonResult(contents);
		}

		[Microsoft.AspNetCore.Mvc.HttpGet("getnewcase")]
		public async Task<JsonResult> GetNewCase()
		{
			var caseInstance = new Case()
			{
				CreatedOn = new DateTime()
			};

			_context.Cases.Add(caseInstance);
			await _context.SaveChangesAsync();

			var json = new { caseId = caseInstance.Id };

			return new JsonResult(json);
		}

		[Microsoft.AspNetCore.Mvc.HttpGet("selectcontent")]
		public async Task<JsonResult> SelectContent(Guid contentId, Guid contextId, Guid caseId)
		{
			var content = _context.Contents.FirstOrDefault(e => e.Id == contentId);

			var caseRecord = new CaseRecord()
			{
				CaseId = caseId,
				ContextId = contextId,
				ContentId = contentId
			};

			_context.CaseRecords.Add(caseRecord);
			await _context.SaveChangesAsync();

			return new JsonResult(new {text = content.Text});
		}
	}

	public class ContentBody
	{
		[JsonProperty("text")]
		public string Text { get; set; }

		[JsonProperty("contextId")]
		public Guid ContextId { get; set; }
		[JsonProperty("reference")]
		public string Reference { get; set; }
		[JsonProperty("heat")]
		public string Soap { get; set; }
	}


}
