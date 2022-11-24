using HCI_ZIS_Library.Model;
using HCI_ZIS_Library.Util;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HCI_ZIS_Library.Model;

namespace HCI_ZIS_Library.DAO
{
    public class HealthCareFacilityDAO

    {
        private HCI_ZIS_Library.Util.DatabaseConnectionManager databaseConnectionManager = new DatabaseConnectionManager();

        public int CreateHealthCareFacility(HealthCareFacility hcf)
        {
            MySqlConnection connection = databaseConnectionManager.GetConnection();
            String cmd = " INSERT INTO zdravstvenaustanova (Naziv, Adresa, BrojTelefona) VALUES (@naziv, @adresa, @broj)";
            MySqlCommand command = new MySqlCommand(cmd, connection);
            command.Parameters.AddWithValue("@naziv", hcf.Name);
            command.Parameters.AddWithValue("@adresa", hcf.Address);
            command.Parameters.AddWithValue("@broj", hcf.PhoneNumber);
            command.Prepare();
            command.ExecuteNonQuery();
            int index = (int)command.LastInsertedId;
            databaseConnectionManager.ReturnConnection(connection);
            return index;
        }
        public List<HealthCareFacility> ReadAllHealthCareFacilities()
        {
            MySqlConnection connection = databaseConnectionManager.GetConnection();
            String cmd = " SELECT * from zdravstvenaustanova";
            MySqlCommand command = new MySqlCommand(cmd, connection);
            command.Prepare();
            MySqlDataReader rs = command.ExecuteReader();
            List<HealthCareFacility> facilities = new List<HealthCareFacility>();
            while (rs.Read())
            {
                facilities.Add(new HealthCareFacility(rs.GetInt32("idZdravstvenaUstanova"), rs.GetString("Naziv"), rs.GetString("Adresa"), rs.GetString("BrojTelefona")));
            }
            rs.Close();
            databaseConnectionManager.ReturnConnection(connection);
            return facilities;
        }
        public HealthCareFacility ReadHealthCareFacility(int id)
        {
            MySqlConnection connection = databaseConnectionManager.GetConnection();
            String cmd = " SELECT * from zdravstvenaustanova WHERE idZdravstvenaUstanova=@val";
            MySqlCommand command = new MySqlCommand(cmd, connection);
            command.Parameters.AddWithValue("@val", id);
            command.Prepare();
            MySqlDataReader rs = command.ExecuteReader();
            HealthCareFacility facility = null;
            if (rs.Read())
            {
                facility= new HealthCareFacility(rs.GetInt32("idZdravstvenaUstanova"), rs.GetString("Naziv"), rs.GetString("Adresa"), rs.GetString("BrojTelefona"));
            }
            rs.Close();
            databaseConnectionManager.ReturnConnection(connection);
            return facility;
        }
        public void UpdateHealthCareFacility(HealthCareFacility hcf)
        {
            MySqlConnection connection = databaseConnectionManager.GetConnection();
            String cmd = "UPDATE zdravstvenaustanova SET Naziv=@naziv, Adresa=@adresa, BrojTelefona=@broj WHERE idZdravstvenaUStanova=@id";
            MySqlCommand command = new MySqlCommand(cmd, connection);
            command.Parameters.AddWithValue("@naziv", hcf.Name);
            command.Parameters.AddWithValue("@adresa", hcf.Address);
            command.Parameters.AddWithValue("@broj", hcf.PhoneNumber);
            command.Parameters.AddWithValue("@id", hcf.ID);
            command.Prepare();
            command.ExecuteNonQuery();
            databaseConnectionManager.ReturnConnection(connection);
        }

        public void DeleteHealthCareFacility(HealthCareFacility hcf)
        {
            MySqlConnection connection = databaseConnectionManager.GetConnection();
            String cmd = "DELETE from zdravstvenaustanova WHERE idZdravstvenaUstanova=@id";
            MySqlCommand command = new MySqlCommand(cmd, connection);
            command.Parameters.AddWithValue("@id", hcf.ID);
            command.Prepare();
            command.ExecuteNonQuery();
            databaseConnectionManager.ReturnConnection(connection);
        }
    }
}
