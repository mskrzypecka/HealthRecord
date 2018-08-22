using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HealthRecord.Data.Entities
{
    public class MedicalHistoryRecord
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DisplayName("Date of event")]
        public DateTime DateOfEvent { get; set; }
        [DisplayName("Record type")]
        public RecordType RecordType { get; set; }
    }

    public enum RecordType
    {
        Disease,
        [Display(Name = "Doctors Appointment")]
        DoctorsAppointment,
        [Display(Name = "Laboratory Examination")]
        LaboratoryExamination
    }
}