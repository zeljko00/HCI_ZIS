namespace HCI_ZIS_Library.Model
{
    public class Prescription
    {
        public int Id { get; set; }
        public int Examination { get; set; }
        public Medication Medication { get; set; }

        public string Note { get; set; }
        public Prescription(int id, int examination, Medication medication, string note)
        {
            Id = id;
            Examination = examination;
            Medication = medication;
            Note = note;
        }

        public override bool Equals(object obj)
        {
            return obj is Prescription prescription &&
                   Id == prescription.Id;
        }
    }
}