using HCI_ZIS_Library.Model;
using HCI_ZIS_Library.Util;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCI_ZIS_Library.DAO
{
    public class ExaminationDAO
    {
        private DatabaseConnectionManager databaseConnectionManager = new DatabaseConnectionManager();
        private PatientDAO patientDAO = new PatientDAO();
        private MedicalDoctorDAO medicalDoctorDAO=new MedicalDoctorDAO();
        private PrescriptionDAO prescriptionDAO = new PrescriptionDAO();
        private DiagnosisDAO diagnosisDAO = new DiagnosisDAO();
        public int CreateExamination(Examination examination)
        {
            MySqlConnection connection = databaseConnectionManager.GetConnection();
            String cmd = "INSERT into pregled (Datum, Opis, idOsoba, Ljekar_idOsoba) VALUES (@datum, @opis, @osoba, @ljekar)";
            MySqlCommand command = new MySqlCommand(cmd, connection);
            command.Parameters.AddWithValue("@datum", examination.Date);
            command.Parameters.AddWithValue("@opis", examination.Content);
            command.Parameters.AddWithValue("@osoba", examination.Patient.Person.ID);
            command.Parameters.AddWithValue("@ljekar", examination.Doctor.Person.ID);
            command.ExecuteNonQuery();
            int index = (int)command.LastInsertedId;
            databaseConnectionManager.ReturnConnection(connection);
            return index;
        }

        public List<Examination> ReadAllExaminationsByPatient(Patient patient)
        {
            MySqlConnection connection = databaseConnectionManager.GetConnection();
            String cmd = " SELECT * from pregled WHERE idOsoba=@val";
            MySqlCommand command = new MySqlCommand(cmd, connection);
            command.Parameters.AddWithValue("@val", patient.Person.ID);
            command.Prepare();
            MySqlDataReader rs = command.ExecuteReader();
            List<Examination> examinations = new List<Examination>();
            while (rs.Read())
            {
                MedicalDoctor doctor = medicalDoctorDAO.ReadMedicalDoctor(rs.GetInt32("Ljekar_idOsoba"));
                //procitati uputnice
                List<Prescription> prescriptions = prescriptionDAO.ReadAllPrescriptionsByExamination(rs.GetInt32("idPregled"));
                List<Disease> diagnosis = diagnosisDAO.ReadAllDiagnosisByExamination(rs.GetInt32("idPregled"));
                examinations.Add(new Examination(rs.GetInt32("idPregled"), rs.GetString("Datum"), rs.GetString("Opis"), doctor,patient, prescriptions,diagnosis,null));
            }
            rs.Close();
            databaseConnectionManager.ReturnConnection(connection);
            return examinations;
        }
        public List<Examination> ReadAllExaminationsByDoctor(MedicalDoctor doctor)
        {
            MySqlConnection connection = databaseConnectionManager.GetConnection();
            String cmd = " SELECT * from pregled WHERE Ljekar_idOsoba=@val";
            MySqlCommand command = new MySqlCommand(cmd, connection);
            command.Parameters.AddWithValue("@val", doctor.Person.ID);
            command.Prepare();
            MySqlDataReader rs = command.ExecuteReader();
            List<Examination> examinations = new List<Examination>();
            while (rs.Read())
            {
                Patient patient = patientDAO.ReadPatient(rs.GetInt32("idOsoba"));
                //procitati uputnice
                List<Prescription> prescriptions = prescriptionDAO.ReadAllPrescriptionsByExamination(rs.GetInt32("idPregled"));
                List<Disease> diagnosis = diagnosisDAO.ReadAllDiagnosisByExamination(rs.GetInt32("idPregled"));
                examinations.Add(new Examination(rs.GetInt32("idPregled"), rs.GetString("Datum"), rs.GetString("Opis"), doctor, patient, prescriptions, diagnosis, null));
            }
            rs.Close();
            databaseConnectionManager.ReturnConnection(connection);
            return examinations;
        }

        public void DeleteExamination(Examination examination)
        {
            MySqlConnection connection = databaseConnectionManager.GetConnection();
            String cmd = "DELETE from pregled WHERE idPregled=@id";
            MySqlCommand command = new MySqlCommand(cmd, connection);
            command.Parameters.AddWithValue("@id", examination.Id);
            command.Prepare();
            command.ExecuteNonQuery();
            databaseConnectionManager.ReturnConnection(connection);
        }
    }
}
