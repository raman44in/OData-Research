using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebAppCoreEF2.Models
{
    public partial class PersonExtended
    {
        [Key]
        public int Personid { get; set; }
        public bool? Gender { get; set; }
        public int? Race { get; set; }
        public bool? Married { get; set; }
    }
}
