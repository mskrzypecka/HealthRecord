using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HealthRecord.Data.Entities
{
    public class Creation
    {
        public string Id { get; set; }
        public string Name { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DisplayName("Date of birth")]
        public DateTime DateOfBirth { get; set; }
        public Sex Sex { get; set; }
        [DisplayName("Member type")]
        public MemberType MemberType { get; set; }
        public double Weight { get; set; }
        [DisplayName("Image link")]
        public string ImageLink { get; set; }
        public int Age
        {
            get
            {
                DateTime now = DateTime.Today;
                int age = now.Year - DateOfBirth.Year;
                return (DateOfBirth > now.AddYears(-age)) ? --age : age;
            }
        }
        public ApplicationUser Account { get; set; }
        public List<Vaccine> Vaccines { get; set; }
        public List<MedicalHistoryRecord> MedicalHistory { get; set; }
    }

    public enum Sex
    {
        Male,
        Female
    }

    public enum MemberType
    {
        Human,
        Pet,
        FarmAnimal
    }
}