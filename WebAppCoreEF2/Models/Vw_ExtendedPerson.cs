using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppCoreEF2.Models
{
    public class Vw_ExtendedPerson
    {
        public bool? Gender { get; set; }
        public int PersonId { get; set; }
        public int? Race { get; set; }
        public bool? Married { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

    }
}
