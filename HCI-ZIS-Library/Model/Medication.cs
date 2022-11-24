using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCI_ZIS_Library.Model
{
    public class Medication
    {
        public int ID {  get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public bool OnPrescriptionOnly { get; set; }
        public double Price { get; set; }
        public string Manual { get; set; }

        public Medication(int iD, string name, string description, string content, bool onPrescriptionOnly, double price, string manual)
        {
            ID = iD;
            Name = name;
            Description = description;
            Content = content;
            OnPrescriptionOnly = onPrescriptionOnly;
            Price = price;
            Manual = manual;
        }

        public override bool Equals(object obj)
        {
            return obj is Medication medication &&
                   ID == medication.ID;
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
