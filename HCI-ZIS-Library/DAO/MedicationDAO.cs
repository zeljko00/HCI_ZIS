using HCI_ZIS_Library.Exceptions;
using HCI_ZIS_Library.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

using HCI_ZIS_Library.Model;
using HCI_ZIS_Library.Util;

namespace HCI_ZIS_Library.DAO
{
    public class MedicationDAO
    {
        private HCI_ZIS_Library.Util.DatabaseConnectionManager databaseConnectionmanager;

        public MedicationDAO()
        {
            databaseConnectionmanager = new HCI_ZIS_Library.Util.DatabaseConnectionManager();
        }
        public int CreateMedication(HCI_ZIS_Library.Model.Medication medication)
        {
            MySqlConnection connection = databaseConnectionmanager.GetConnection();
            String cmd = " INSERT INTO lijek (Naziv, Sastav, UputstvoZaUpotrebu, Namjena, IzdavanjeNaReceptIskljucivo, CijenaFond) VALUES (@naziv, @sastav, @uputstvo, @namjena, @izdavanje, @cijena) ";
            MySqlCommand command = new MySqlCommand(cmd, connection);
            command.Parameters.AddWithValue("@naziv", medication.Name);
            command.Parameters.AddWithValue("@sastav", medication.Content);
            command.Parameters.AddWithValue("@uputstvo", medication.Manual);
            command.Parameters.AddWithValue("@namjena", medication.Description);
            command.Parameters.AddWithValue("@izdavanje", medication.OnPrescriptionOnly);
            command.Parameters.AddWithValue("@cijena", medication.Price);
            command.Prepare();
            command.ExecuteNonQuery();
            int index = (int)command.LastInsertedId;
            databaseConnectionmanager.ReturnConnection(connection);
            return index;
        }
        public List<Medication> ReadAllMedications()
        {
            MySqlConnection connection = databaseConnectionmanager.GetConnection();
            String cmd = " SELECT * from lijek";
            MySqlCommand command = new MySqlCommand(cmd, connection);
            command.Prepare();
            MySqlDataReader rs = command.ExecuteReader();
            List<Medication> medications = new List<Medication>();
            while (rs.Read())
            {
               
                medications.Add(new Medication(rs.GetInt32("idLijek"), rs.GetString("Naziv"), rs.GetString("Namjena"), rs.GetString("Sastav"), rs.GetBoolean("IzdavanjeNaReceptIskljucivo"), rs.GetDouble("CijenaFOnd"), rs.GetString("UputstvoZaUpotrebu")));
            }
            rs.Close();
            databaseConnectionmanager.ReturnConnection(connection);
            return medications;
        }
        public Medication ReadMedicationById(int id)
        {
            MySqlConnection connection = databaseConnectionmanager.GetConnection();
            String cmd = " SELECT * from lijek WHERE idLijek=@val";
            MySqlCommand command = new MySqlCommand(cmd, connection);
            command.Parameters.AddWithValue("@val", id);
            command.Prepare();
            MySqlDataReader rs = command.ExecuteReader();
            Medication medication = null;
            if (rs.Read())
            {
                medication=new Medication(rs.GetInt32("idLijek"), rs.GetString("Naziv"), rs.GetString("Namjena"), rs.GetString("Sastav"), rs.GetBoolean("IzdavanjeNaReceptIskljucivo"), rs.GetDouble("CijenaFOnd"), rs.GetString("UputstvoZaUpotrebu"));
            }
            rs.Close();
            databaseConnectionmanager.ReturnConnection(connection);
            return medication;
        }
        public void UpdateMedication(Medication medication)
        {
            MySqlConnection connection = databaseConnectionmanager.GetConnection();
            String cmd = "UPDATE lijek SET Naziv=@naziv, Sastav=@sastav, Namjena=@namjena, UputstvoZaUpotrebu=@uputstvo, IzdavanjeNaReceptIskljucivo=@izdavanje, CijenaFond=@cijena WHERE idLijek=@id";
            MySqlCommand command = new MySqlCommand(cmd, connection);
            command.Parameters.AddWithValue("@naziv", medication.Name);
            command.Parameters.AddWithValue("@sastav", medication.Content);
            command.Parameters.AddWithValue("@uputstvo", medication.Manual);
            command.Parameters.AddWithValue("@izdavanje", medication.OnPrescriptionOnly);
            command.Parameters.AddWithValue("@cijena", medication.Price);
            command.Parameters.AddWithValue("@namjena", medication.Description);
            command.Parameters.AddWithValue("@id", medication.ID);
            command.Prepare();
            command.ExecuteNonQuery();
            databaseConnectionmanager.ReturnConnection(connection);
        }

        public void DeleteMedication(Medication medication)
        {
            MySqlConnection connection = databaseConnectionmanager.GetConnection();
            String cmd = "DELETE from lijek WHERE idLijek=@id";
            MySqlCommand command = new MySqlCommand(cmd, connection);
            command.Parameters.AddWithValue("@id", medication.ID);
            command.Prepare();
            command.ExecuteNonQuery();
            databaseConnectionmanager.ReturnConnection(connection);
        }
    }
}
