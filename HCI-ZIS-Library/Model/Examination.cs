using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace HCI_ZIS_Library.Model
{
    public class Examination
    {
        public int Id { get; set; }
        public string Date { get; set; }
        public string Content { get; set; }
        public MedicalDoctor Doctor { get; set; }
        public Patient Patient { get; set; }

        public List<Prescription> Prescriptions { get; set; }
        public List<Disease> Diseases { get; set; }

        public List<Refferal> Refferals { get; set; }

        public Examination(int id, string date, string content, MedicalDoctor doctor, Patient patient, List<Prescription> prescriptions, List<Disease> diseases, List<Refferal> refferals)
        {
            Id = id;
            Date = date;
            Content = content;
            Doctor = doctor;
            Patient = patient;
            Prescriptions = prescriptions;
            Diseases = diseases;
            Refferals = refferals;  
        }
        public Examination(int id, string content, MedicalDoctor doctor, Patient patient, List<Prescription> prescriptions, List<Disease> diseases, List<Refferal> refferals)
        {
            Id = id;
            Date = DateTime.Now.ToString("dd.MM.yyyy HH:mm");
            Content = content;
            Doctor = doctor;
            Patient = patient;
            Prescriptions = prescriptions;
            Diseases = diseases;
            Refferals = refferals;
        }
        public override bool Equals(object obj)
        {
            return obj is Examination examination &&
                   Id == examination.Id;
        }
    }
}
