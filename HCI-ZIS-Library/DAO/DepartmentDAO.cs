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
    public class DepartmentDAO
    {
        private DatabaseConnectionManager databaseConnectionManager = new DatabaseConnectionManager();
        public int CreateDepartment(Department department)
        {
            MySqlConnection connection = databaseConnectionManager.GetConnection();
            String cmd = "INSERT into odjeljenje (Naziv, OpisDjelatnosti, SmjestajniKapaciteti, idZdravstvenaUstanova) VALUES (@naziv, @opis, @kap, @id)";
            MySqlCommand command = new MySqlCommand(cmd, connection);
            command.Parameters.AddWithValue("@naziv",department.Name);
            command.Parameters.AddWithValue("@opis", department.Desc);
            command.Parameters.AddWithValue("@kap", department.Capacity);
            command.Parameters.AddWithValue("@id", department.HealthCareFacility);
            command.ExecuteNonQuery();
            int index = (int)command.LastInsertedId;
            databaseConnectionManager.ReturnConnection(connection);
            return index;
        }

        public List<Department> ReadAllDepartmentsByHealthCareFacility(HealthCareFacility hcf)
        {
            MySqlConnection connection = databaseConnectionManager.GetConnection();
            String cmd = " SELECT * from odjeljenje WHERE idZdravstvenaUstanova=@val";
            MySqlCommand command = new MySqlCommand(cmd, connection);
            command.Parameters.AddWithValue("@val", hcf.ID);
            command.Prepare();
            MySqlDataReader rs = command.ExecuteReader();
            List<Department> departments = new List<Department>();
            while (rs.Read())
            {
                departments.Add(new Department(rs.GetInt32("idOdjeljenje"), rs.GetString("Naziv"),rs.GetInt32("SmjestajniKapaciteti"), rs.GetString("OpisDjelatnosti"), rs.GetInt32("idZdravstvenaUstanova")));
            }
            rs.Close();
            databaseConnectionManager.ReturnConnection(connection);
            return departments;
        }

        public Department ReadDepartmentById(int id)
        {
            MySqlConnection connection = databaseConnectionManager.GetConnection();
            String cmd = " SELECT * from odjeljenje WHERE idOdjeljenje=@val";
            MySqlCommand command = new MySqlCommand(cmd, connection);
            command.Parameters.AddWithValue("@val", id);
            command.Prepare();
            MySqlDataReader rs = command.ExecuteReader();
            Department department = null;
            if (rs.Read())
            {
                department= new Department(rs.GetInt32("idOdjeljenje"), rs.GetString("Naziv"), rs.GetInt32("SmjestajniKapaciteti"), rs.GetString("OpisDjelatnosti"), rs.GetInt32("idZdravstvenaUstanova"));
            }
            rs.Close();
            return department;
        }
        public void UpdateDepartment(Department department)
        {
            MySqlConnection connection = databaseConnectionManager.GetConnection();
            String cmd = "UPDATE odjeljenje SET Naziv=@naziv, OpisDjelatnosti=@opis, SmjestajniKapaciteti=@kap WHERE idOdjeljenje=@id";
            MySqlCommand command = new MySqlCommand(cmd, connection);
            command.Parameters.AddWithValue("@naziv", department.Name);
            command.Parameters.AddWithValue("@opis", department.Desc);
            command.Parameters.AddWithValue("@kap", department.Capacity);
            command.Parameters.AddWithValue("@id", department.Id);
            command.Prepare();
            command.ExecuteNonQuery();
            databaseConnectionManager.ReturnConnection(connection);
        }

        public void DeleteDepartment(Department department)
        {
            MySqlConnection connection = databaseConnectionManager.GetConnection();
            String cmd = "DELETE from odjeljenje WHERE idOdjeljenje=@id";
            MySqlCommand command = new MySqlCommand(cmd, connection);
            command.Parameters.AddWithValue("@id", department.Id);
            command.Prepare();
            command.ExecuteNonQuery();
            databaseConnectionManager.ReturnConnection(connection);
        }
    }
}
