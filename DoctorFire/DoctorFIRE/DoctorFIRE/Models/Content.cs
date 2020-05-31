using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DoctorFIRE.Models
{



    public class Content
    {
	    public Content()
	    {
			
	    }


        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }

        public string Text { get; set; }

        public string Soap { get; set; } 

        public string Reference { get; set; }

        public Guid ContextId { get; set; }	

		public Context Context { get; set; }

		public bool Hide { get; set; }
    }
}
