using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorFIRE.Models
{
    public class CaseRecord
    {
		[Key]
		public int Id{ get; set; }

		public Guid CaseId { get; set; }

		public Guid ContextId { get; set; }

		public Guid ContentId { get; set; }


	}
}
