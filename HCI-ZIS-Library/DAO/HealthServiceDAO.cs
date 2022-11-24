using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

using HCI_ZIS_Library.Model;
using HCI_ZIS_Library.Util;
using MySql.Data.MySqlClient;

namespace HCI_ZIS_Library.DAO
{
    public class HealthServiceDAO
    {
        private DatabaseConnectionManager databaseConnectionManager = new DatabaseConnectionManager();
        public int CreateHealthService(HealthService healthService)
        {
            MySqlConnection connection = databaseConnectionManager.GetConnection();
            String cmd = "INSERT into zdravstvenausluga (Naziv, KratakOpis) VALUES (@naziv, @opis)";
            MySqlCommand command = new MySqlCommand(cmd,connection);
            command.Parameters.AddWithValue("@naziv",healthService.Name);
            command.Parameters.AddWithValue("@opis", healthService.Desc);
            command.ExecuteNonQuery();
            int index = (int)command.LastInsertedId;
            databaseConnectionManager.ReturnConnection(connection);
            return index;
        }

        public List<HealthService> ReadAllHealthServices()
        {
            MySqlConnection connection = databaseConnectionManager.GetConnection();
            String cmd = " SELECT * from zdravstvenausluga";
            MySqlCommand command = new MySqlCommand(cmd, connection);
            command.Prepare();
            MySqlDataReader rs = command.ExecuteReader();
            List<HealthService> services = new List<HealthService>();
            while (rs.Read())
            {
                services.Add(new HealthService(rs.GetInt32("idZdravstvenaUsluga"), rs.GetString("Naziv"), rs.GetString("KratakOpis")));
            }
            rs.Close();
            databaseConnectionManager.ReturnConnection(connection);
            return services;
        }
        public HealthService ReadHealthServiceById(int id)
        {
            MySqlConnection connection = databaseConnectionManager.GetConnection();
            String cmd = " SELECT * from zdravstvenausluga WHERE idZdravstvenaUsluga=@val";
            MySqlCommand command = new MySqlCommand(cmd, connection);
            command.Parameters.AddWithValue("@val",id);
            command.Prepare();
            MySqlDataReader rs = command.ExecuteReader();
            HealthService service = null;
            while (rs.Read())
            {
                service=new HealthService(rs.GetInt32("idZdravstvenaUsluga"), rs.GetString("Naziv"), rs.GetString("KratakOpis"));
            }
            rs.Close();
            databaseConnectionManager.ReturnConnection(connection);
            return service;
        }
        public void UpdateHealthService(HealthService service)
        {
            MySqlConnection connection = databaseConnectionManager.GetConnection();
            String cmd = "UPDATE zdravstvenausluga SET Naziv=@naziv, KratakOpis=@opis WHERE idZdravstvenaUsluga=@id";
            MySqlCommand command = new MySqlCommand(cmd, connection);
            command.Parameters.AddWithValue("@naziv", service.Name);
            command.Parameters.AddWithValue("@opis", service.Desc);
            command.Parameters.AddWithValue("@id", service.ID);
            command.Prepare();
            command.ExecuteNonQuery();
            databaseConnectionManager.ReturnConnection(connection);
        }

        public void DeleteHealthService(HealthService service)
        {
            MySqlConnection connection = databaseConnectionManager.GetConnection();
            String cmd = "DELETE from zdravstvenausluga WHERE idZdravstvenaUsluga=@id";
            MySqlCommand command = new MySqlCommand(cmd, connection);
            command.Parameters.AddWithValue("@id", service.ID);
            command.Prepare();
            command.ExecuteNonQuery();
            databaseConnectionManager.ReturnConnection(connection);
        }
    }
}
