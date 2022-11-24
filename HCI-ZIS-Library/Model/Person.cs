using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCI_ZIS_Library.Model
{
    public class Person
    {

        public int ID { get; set; } 
        public string Firstname { get; set; }

        public string LastName { get; set; }    
        public string JMB { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }    
        public string DateOfBirth { get; set; }
        public Person()
        {
        }

        public Person(int iD, string firstname, string lastName, string jMB, string username, string password, string dateOfBirth)
        {
            ID = iD;
            Firstname = firstname;
            LastName = lastName;
            JMB = jMB;
            Username = username;
            Password = password;
            DateOfBirth = dateOfBirth;
        }

        public override bool Equals(object obj)
        {
            return obj is Person person &&
                   ID == person.ID;
        }
    }
}
