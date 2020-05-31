using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace DoctorFIRE.Models
{
    public class CCbyID
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        public int CCbyIDID { get; set; }
        public int ContentID { get; set; }
        public int ContentIDD { get; set; }
        public int ClickCount { get; set; }
        public int IdD { get; set; }

    }
}
