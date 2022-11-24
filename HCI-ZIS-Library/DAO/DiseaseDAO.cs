using HCI_ZIS_Library.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HCI_ZIS_Library.Model;
using System.Runtime.CompilerServices;
using HCI_ZIS_Library.Util;

namespace HCI_ZIS_Library.DAO
{
    public class DiseaseDAO
    {
        private HCI_ZIS_Library.Util.DatabaseConnectionManager databaseConnectionmanager;

        public DiseaseDAO()
        {
            databaseConnectionmanager = new HCI_ZIS_Library.Util.DatabaseConnectionManager();
        }
        public int CreateDisease(HCI_ZIS_Library.Model.Disease disease)
        {
            MySqlConnection connection = databaseConnectionmanager.GetConnection();
            String cmd = " INSERT INTO bolest (Dijagnoza, Opis, NacinLijecenja) VALUES (@dijagnoza, @opis, @lijecenje)";
            MySqlCommand command = new MySqlCommand(cmd, connection);
            command.Parameters.AddWithValue("@dijagnoza", disease.Diagnose);
            command.Parameters.AddWithValue("@opis", disease.Name);
            command.Parameters.AddWithValue("@lijecenje", disease.Treatment);
            command.Prepare();
            command.ExecuteNonQuery();
            int index = (int)command.LastInsertedId;
            databaseConnectionmanager.ReturnConnection(connection);
            return index;
        }
        public List<Disease> ReadAllDiseases()
        {
            MySqlConnection connection = databaseConnectionmanager.GetConnection();
            String cmd = " SELECT * from bolest";
            MySqlCommand command = new MySqlCommand(cmd, connection);
            command.Prepare();
            MySqlDataReader rs = command.ExecuteReader();
            List<Disease> diseases = new List<Disease>();
            while (rs.Read())
            {
                diseases.Add(new Disease(rs.GetInt32("idBolest"), rs.GetString("Dijagnoza"), rs.GetString("NacinLijecenja"), rs.GetString("Opis")));
            }
            rs.Close();
            databaseConnectionmanager.ReturnConnection(connection);
            return diseases;
        }
        public Disease ReadDisease(int id)
        {
            MySqlConnection connection = databaseConnectionmanager.GetConnection();
            String cmd = " SELECT * from bolest WHERE idBolest=@val";
            MySqlCommand command = new MySqlCommand(cmd, connection);
            command.Parameters.AddWithValue("@val", id);
            command.Prepare();
            MySqlDataReader rs = command.ExecuteReader();
            Disease disease = null;
            if (rs.Read())
                disease= new Disease(rs.GetInt32("idBolest"), rs.GetString("Dijagnoza"), rs.GetString("NacinLijecenja"), rs.GetString("Opis"));
            rs.Close();
            databaseConnectionmanager.ReturnConnection(connection);
            return disease;
        }
        public void UpdateDisease(Disease disease)
        {
            MySqlConnection connection = databaseConnectionmanager.GetConnection();
            String cmd = "UPDATE bolest SET Dijagnoza=@dijagnoza, Opis=@opis, NacinLijecenja=@nacin WHERE idBolest=@id";
            MySqlCommand command = new MySqlCommand(cmd, connection);
            command.Parameters.AddWithValue("@dijagnoza", disease.Diagnose);
            command.Parameters.AddWithValue("@nacin",  disease.Treatment);
            command.Parameters.AddWithValue("@opis", disease.Name);
            command.Parameters.AddWithValue("@id", disease.ID);
            command.Prepare();
            command.ExecuteNonQuery();
            databaseConnectionmanager.ReturnConnection(connection);
        }

        public void DeleteDisease(Disease disease)
        {
            MySqlConnection connection = databaseConnectionmanager.GetConnection();
            String cmd = "DELETE from bolest WHERE idBolest=@id";
            MySqlCommand command = new MySqlCommand(cmd, connection);
            command.Parameters.AddWithValue("@id", disease.ID);
            command.Prepare();
            command.ExecuteNonQuery();
            databaseConnectionmanager.ReturnConnection(connection);
        }
    }
}
