using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HealthRecord.Data.Entities
{
    public class Vaccine
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public long Serie { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DisplayName("Date of vaccination")]
        public DateTime DateOfVaccination { get; set; }

        // TODO: Date of next portion? function
    }
}