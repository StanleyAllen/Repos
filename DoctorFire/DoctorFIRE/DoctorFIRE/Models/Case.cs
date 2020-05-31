using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace DoctorFIRE.Models
{
    public class Case
    {
	    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	    [Key]
		public Guid Id { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
