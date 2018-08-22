using HealthRecord.Data.Entities;
using HealthRecord.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HealthRecord.Helpers
{
    class MemberFactory
    {
        public static Creation CreateMember(MemberType memberType)
        {
            switch (memberType)
            {
                case MemberType.Human:
                    return new Human();
                case MemberType.FarmAnimal:
                    return new FarmAnimal();
                case MemberType.Pet:
                    return new PetAnimal();
                default:
                    throw new ArgumentException();
            }
        }
    }
}