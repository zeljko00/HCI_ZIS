using HCI_ZIS_Library.Model;
using System.Collections.Generic;

namespace HCI_ZIS_Library.Model
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }    
        public int Capacity { get; set; }
        public string Desc { get; set; }
        public int HealthCareFacility { get; set; }
        public Department(int id, string name, int capacity, string desc, int healthCareFacility)
        {
            Id = id;
            Name = name;
            Capacity = capacity;
            Desc = desc;
            HealthCareFacility = healthCareFacility;
        }

        public override bool Equals(object obj)
        {
            return obj is Department department &&
                   Id == department.Id;
        }
    }
}