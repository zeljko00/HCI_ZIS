using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCI_ZIS_Library.Model
{
    public class HealthCareFacility
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }

        public HealthCareFacility() {
        }
        public HealthCareFacility(int id,string Name,string Address,string PhoneNumber)
        {
            this.ID = id;
            this.Name = Name;
            this.Address = Address;
            this.PhoneNumber = PhoneNumber;
        }

        override public bool Equals(Object obj)
        {
            if(obj.GetType() == typeof(HealthCareFacility))
            {
                HealthCareFacility hcf = (HealthCareFacility)obj;
                return (hcf.ID == ID);
            }
            return false;

        }
    }
}
