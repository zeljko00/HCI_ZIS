using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCI_ZIS_Library.Model
{
    public class Patient
    {
        public Person Person { get; set; }
        public string HealthCardID { get; set; }
        public string Note { get; set; }

        public Patient(Person person, string healthCardID, string note)
        {
            Person = person;
            HealthCardID = healthCardID;
            Note = note;
        }

        public override bool Equals(object obj)
        {
            return obj is Patient patient &&
                   Person == patient.Person;
        }
    }
}
