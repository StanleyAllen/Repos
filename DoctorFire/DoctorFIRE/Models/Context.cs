using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DoctorFIRE.Models
{
    public class Context
    {

	    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Key]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public int Rank { get; set; }

        public ICollection<Content> Content { get; set; }

		public bool Hide { get; set; }
        
    }
}
