using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HCI_ZIS_Library.Model;
using HCI_ZIS_Library.Util;
using MySql.Data.MySqlClient;

namespace HCI_ZIS_Library.DAO
{
    public class PrescriptionDAO
    {
        private DatabaseConnectionManager databaseConnectionManager = new DatabaseConnectionManager();
        private MedicationDAO medicationDAO = new MedicationDAO();

        public int CreatePrescription(Examination examination, Medication medication,string note)
        {
            MySqlConnection connection = databaseConnectionManager.GetConnection();
            String cmd = "INSERT INTO recept (Pregled_idPregled, Lijek_idLijek, Napomena) VALUES (@pregled,@lijek,@nap)";
            MySqlCommand command = new MySqlCommand(cmd, connection);
            command.Parameters.AddWithValue("@pregled", examination.Id);
            command.Parameters.AddWithValue("@lijek", medication.ID);
            command.Parameters.AddWithValue("@nap", note);
            command.Prepare();
            command.ExecuteNonQuery();
            int index = (int)command.LastInsertedId;
            databaseConnectionManager.ReturnConnection(connection);
            return index;
        }
        public List<Prescription> ReadAllPrescriptionsByExamination(int examination)
        {
            MySqlConnection connection = databaseConnectionManager.GetConnection();
            String cmd = "SELECT * FROM recept WHERE Pregled_idPregled=@pregled";
            MySqlCommand command = new MySqlCommand(cmd, connection);
            command.Parameters.AddWithValue("@pregled", examination);
            command.Prepare();
            MySqlDataReader rd = command.ExecuteReader();
            List<Prescription> prescriptions = new List<Prescription>();
            while (rd.Read())
            {
                int medicationID = rd.GetInt32("Lijek_idLijek");
                string note = rd.GetString("Napomena");
                int id = rd.GetInt32("idRecept");
                Medication medication = medicationDAO.ReadMedicationById(medicationID);
                prescriptions.Add(new Prescription(id,examination,medication,note));
            }
            rd.Close();
            databaseConnectionManager.ReturnConnection(connection);
            return prescriptions;
        }
    }
}
