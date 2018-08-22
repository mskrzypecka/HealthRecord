using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace HealthRecord.Data.Entities
{
    public class Human : Creation
    {
        public virtual long PESEL { get; set; }
        [DisplayName("First name")]
        public virtual string FirstName { get; set; }
        [DisplayName("Last name")]
        public virtual string LastName { get; set; }
        public virtual string Name { get { return FirstName + " " + LastName; } }
        [DisplayName("Phone number")]
        public virtual string PhoneNumber { get; set; }
        public virtual string Address { get; set; }
        public virtual int Region { get; set; }
    }
}