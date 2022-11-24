using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCI_ZIS_Library.Model
{
    public class Disease
    {
        public int ID { get; set; } 
        public string Diagnose { get; set; }
        public string Treatment { get; set; }
        public string Name { get; set; }

        public Disease(int iD, string diagnose, string treatment, string name)
        {
            ID = iD;
            Diagnose = diagnose;
            Treatment = treatment;
            Name = name;
        }

        public Disease()
        {
        }

        public override bool Equals(object obj)
        {
            return obj is Disease disease &&
                   ID == disease.ID;
        }
        public override string ToString()
        {
            return Diagnose+" - "+Name;
        }
        public override int GetHashCode()
        {
            return ID;
        }
    }
}
