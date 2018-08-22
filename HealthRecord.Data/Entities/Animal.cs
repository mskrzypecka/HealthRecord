using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace HealthRecord.Data.Entities
{
    public class Animal : Creation
    {
        [DisplayName("Passport number")]
        public string PassportNumber { get; set; }
        public Human Owner { get; set; }
        [DisplayName("Is sterile?")]
        public bool IsSterile { get; set; }
        public List<Chip> Chips { get; set; }
    }
}