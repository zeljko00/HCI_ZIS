
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace HCI_ZIS_Library.Model
{   
    public class MedicalDoctor: System.ComponentModel.INotifyPropertyChanged
    {
        public Person Person { get; set; }
        public string Specialization { get; set; }
        public string Licence { get; set; }
        public int HealthCareFacility { get; set; }

        public bool Active;

        private int examinationNum;

        public int ExaminationNum
        {
            get
            {
                return examinationNum;
            }
            set
            {
                examinationNum = value;
                OnPropertyChanged("ExaminationNum");
            }
        }
        private int prescriptionNum;
        public int PrescriptionNum
        {
            get
            {
                return prescriptionNum;
            }
            set
            {
                prescriptionNum = value;
                OnPropertyChanged("PrescriptionNum");
            }
        }
        private int refferalNum;
        public int RefferalNum
        {
            get
            {
                return refferalNum;
            }
            set
            {
                refferalNum = value;
                OnPropertyChanged("RefferalNum");
            }
        }
        public bool IsAdmin { get; set; }

        public string Theme { get; set; }
        public string Language { get; set; }

        public MedicalDoctor(Person person, string specialization, string licence, int hcf, int examinationNum, int prescriptionNum, int refferalNum, bool isAdmin, string theme, string language, bool active)
        {
            Person = person;
            Specialization = specialization;
            Licence = licence;
            HealthCareFacility = hcf;
            ExaminationNum = examinationNum;
            PrescriptionNum = prescriptionNum;
            RefferalNum = refferalNum;
            IsAdmin = isAdmin;
            Theme = theme;
            Language = language;
            Active = active;
        }

        public MedicalDoctor()
        {
        }
        private void OnPropertyChanged(string info)
        {
            System.ComponentModel.PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new System.ComponentModel.PropertyChangedEventArgs(info));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public override bool Equals(object obj)
        {
            if(obj is MedicalDoctor)
            {
                MedicalDoctor md = (MedicalDoctor)obj;
                return md.Person.Equals(Person);
            }
            return false;
        }
    }
}
