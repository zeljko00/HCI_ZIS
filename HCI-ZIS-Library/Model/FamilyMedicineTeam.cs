
namespace HCI_ZIS_Library.Model
{
    public class FamilyMedicineTeam
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int PatientCount { get; set; }= 0;
        public MedicalDoctor  Doctor { get; set; }
        public HealthCareFacility HealthCareFacility { get; set; }

        public FamilyMedicineTeam(int id, string name, int patientCount, MedicalDoctor doctor, HealthCareFacility healthCareFacility)
        {
            Id = id;
            Name = name;
            PatientCount = patientCount;
            Doctor = doctor;
            HealthCareFacility = healthCareFacility;
        }

        public override bool Equals(object obj)
        {
            return obj is FamilyMedicineTeam team &&
                   Id == team.Id;
        }
    }
}