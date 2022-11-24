using HCI_ZIS_Library.DAO;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HCI_ZIS_Library.Model;
using HCI_ZIS_Library.DAO;
using System.Data.OleDb;

namespace Test
{
    internal class Program
    {
        static void Main(string[] args)
        {
            HCI_ZIS_Library.Model.Person p = new HCI_ZIS_Library.Model.Person(-1, "Zeljko", "Tripic", "1006000120484", "user", "password", "10.06.2020.");
            UserDAO userDAO = new UserDAO();

            //CREATE
            //userDAO.CreateUser(p);

            //READ
            //Console.WriteLine(userDAO.ReadUser("user", "password1").ID);

            //UPDATE
            //userDAO.UpdateUser(p);

            //DELETE
            //userDAO.DeleteUser(p);


            MedicalDoctorDAO doctorDAO = new MedicalDoctorDAO();

            HCI_ZIS_Library.Model.Person p1 = new HCI_ZIS_Library.Model.Person(15, "User", "User", "2106000120474", "user3", "password", "10.06.2020.");
            HCI_ZIS_Library.Model.MedicalDoctor d = new HCI_ZIS_Library.Model.MedicalDoctor(p1,"internista","#11",-1);
            //CREATE
            //doctorDAO.CreateMedicalDoctor(d);

            //READ
            //Console.WriteLine(doctorDAO.ReadMedicalDoctor("user2","password").Licence);

            //UPDATE
            //doctorDAO.UpdateMedicalDoctor(d);

            //DELETE
            //doctorDAO.DeleteMedicalDoctor(d);

            MedicationDAO medicationDAO = new MedicationDAO();
            Medication medication1 = new Medication(1, "Panklav1", "opis", "amoksicilin+klavulanska", true, 12.3, "uputstvo");
            Medication medication2 = new Medication(2, "Panklav2", "opis", "amoksicilin", true, 12.3, "uputstvo");

            //CREATE
            //medicationDAO.CreateMedication(medication1);
            //medicationDAO.CreateMedication(medication2);

            //READ
            //List<Medication> medications = medicationDAO.ReadAllMedications();

            //UPDATE
            //medicationDAO.UpdateMedication(medication1);

            DiseaseDAO diseaseDAO = new DiseaseDAO();
            Disease disease1 = new Disease(1, "m522", "brufen","herpes");
            Disease disease2 = new Disease(2, "m53", "brufen+panklav", "osip");

            //CREATE
            //diseaseDAO.CreateDisease(disease1);
            //diseaseDAO.CreateDisease(disease2);

            //READ
            //List<Disease> diseases = diseaseDAO.ReadAllDiseases();

            //UPDATE
            //diseaseDAO.UpdateDisease(disease1);

            PatientDAO patientDAO = new PatientDAO();
            Person person = new Person(25,"Novoime","prezime","1367891231","korisnik11111","pass","12.12.2000");
            Patient patient = new Patient(person, "#Z222", "napomena", 1);

            //CREATE
            //patientDAO.CreatePatient(patient);

            //READ
            //patientDAO.ReadPatient("korisnik11111", "pass");

            //UPDATE
            //patientDAO.UpdatePatient(patient);

            HealthCareFacility hcf1 = new HealthCareFacility(1, "bolnica111", "adresa1", "broj1");
            HealthCareFacility hcf2 = new HealthCareFacility(-1, "bolnica2", "adresa2", "broj2");
            HealthCareFacilityDAO healthCareFacilityDAO = new HealthCareFacilityDAO();

            //CREATE
            //healthCareFacilityDAO.CreateHealthCareFacility(hcf1);
            //healthCareFacilityDAO.CreateHealthCareFacility(hcf2);

            //READ
            //List<HealthCareFacility> hcfs = healthCareFacilityDAO.ReadAllHealthCareFacilities();

            //UDATE
            //healthCareFacilityDAO.UpdateHealthCareFacility(hcf1);

            HealthService hs1 = new HealthService(1, "ime111", "opis");
            HealthService hs2 = new HealthService(2, "ime2", "opis");
            HealthServiceDAO healthServiceDAO = new HealthServiceDAO();

            //CREATE
            //healthServiceDAO.CreateHealthService(hs1);
            //healthServiceDAO.CreateHealthService(hs2);

            //READ
            //List<HealthService> services = healthServiceDAO.ReadAllHealthServices();

            //UPDATE
            //healthServiceDAO.UpdateHealthService(hs1);

            Department department1 = new Department(1, "psihijatrija", 100, "blblba", 1);
            Department department2 = new Department(-1, "ortopedija", 100, "blblba", 1);
            DepartmentDAO departmentDAO = new DepartmentDAO();

            //CREATE
            //departmentDAO.CreateDepartment(department1);
            //departmentDAO.CreateDepartment(department2);

            //READ
            //departmentDAO.ReadAllDepartmentsByHealthCareFacility(hcf1);

            //UPDATE
            //departmentDAO.UpdateDepartment(department1);

            Examination exam = new Examination(2, "sadrzaj", d, patient, null, null, null);
            ExaminationDAO examinationDAO = new ExaminationDAO();

            //CREATE
            //examinationDAO.CreateExamination(exam);

            //READ
            //examinationDAO.ReadAllExaminationsByPatient(patient);
            //examinationDAO.ReadAllExaminationsByDoctor(d);

            DiagnosisDAO diagnosisDAO = new DiagnosisDAO();
            //CREATE
            //diagnosisDAO.CreateDiagnosis(exam, disease1);
            //diagnosisDAO.CreateDiagnosis(exam, disease2);

            //READ
            //List<Disease> diseases = diagnosisDAO.ReadAllDiagnosisByExamination(exam);

            Prescription prescription1 = new Prescription(-1, 2, medication1, "2x1");
            Prescription prescription2 = new Prescription(-1, 2, medication2, "3x1");

            PrescriptionDAO prescriptionDAO = new PrescriptionDAO();

            //CREATE
            //prescriptionDAO.CreatePrescription(exam, medication1, "2x1");
            //prescriptionDAO.CreatePrescription(exam, medication2, "3x1");

            //READ
            //List<Prescription> prescriptions = prescriptionDAO.ReadAllPrescriptionsByExamination(exam);

            Refferal refferal1 = new Refferal(-1, "upucuje se na...", 2, 1, hs1);
            Refferal refferal2 = new Refferal(-1, "upucuje se na...", 2, 11, hs2);
            RefferalDAO refferalDAO = new RefferalDAO();

            //CREATE
            //refferalDAO.CreateRefferal(refferal1);
            //refferalDAO.CreateRefferal(refferal2);

            //READ
            //List<Refferal> refferals = refferalDAO.ReadAllRefferalsByExamination(2);
        }
    }
}
