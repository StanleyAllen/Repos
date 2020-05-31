using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using DoctorFIRE.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.Language.Intermediate;
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

		[Microsoft.AspNetCore.Mvc.HttpGet("makeprediction")]
		public async Task<JsonResult> GetRecommendedContents(Guid contextId, Guid caseId, Guid contentId)
		{

			try
			{


				// Get the last content Id for the case being created.
				var lastContentId = _context.CaseRecords.Where(e => e.CaseId == caseId)
					.OrderByDescending(e => e.Id).Select(e => e.ContentId).FirstOrDefault();

				// Get all case content grouped by case
				var selectedCases = _context.CaseRecords.Where( e => e.ContextId == contextId).OrderBy(e => e.Id);
					


				var nextContents = new List<string>();
				var getNext = false;
				foreach( var selectedCase in selectedCases )
				{
					if( getNext )
					{
						nextContents.Add(selectedCase.ContentId.ToString());
						getNext = false;
					}


					if( selectedCase.ContentId == contentId )
					{
						getNext = true;
					}
				}




				string projectedContentId = nextContents.GroupBy(s => s)
					.OrderByDescending(s => s.Count())
					.First().Key;

				//foreach( var group in groupedCases)
				//{
				//	var orderedContents = group.OrderBy(e => e.Id);

				//	var idOfContentInGroup = orderedContents.Where(e => e.ContentId == lastContentId).Select(e => e.Id)
				//		.First();

				//	var remainingContentsInCase = orderedContents.Where(e => e.Id > idOfContentInGroup);

				//	if( remainingContentsInCase.Any() )
				//		projectedContent.Add(remainingContentsInCase.Select(e => e.ContentId).FirstOrDefault());
				//}

				

				return new JsonResult(new {contentId = projectedContentId.ToUpper() });
			}
			catch( Exception ex )
			{
				var t = ex;
			}

			return null;
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
