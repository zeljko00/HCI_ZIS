using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCI_ZIS_Library.Model
{
    public class HealthService
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }

        public HealthService(int iD, string name, string desc)
        {
            ID = iD;
            Name = name;
            Desc = desc;
        }

        public override bool Equals(object obj)
        {
            return obj is HealthService service &&
                   ID == service.ID;
        }

        public override string ToString()
        {
            return Name;
        }
        public override int GetHashCode()
        {
            return ID;
        }
    }
}
