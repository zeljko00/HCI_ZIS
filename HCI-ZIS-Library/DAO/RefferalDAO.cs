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
    public class RefferalDAO
    {
        private DatabaseConnectionManager databaseConnectionManager = new DatabaseConnectionManager();
        private HealthServiceDAO healthServiceDAO = new HealthServiceDAO();

        public int CreateRefferal(Refferal refferal)
        {
            MySqlConnection connection = databaseConnectionManager.GetConnection();
            String cmd = "INSERT INTO uputnica (Pregled_idPregled, idZdravstvenaUsluga) VALUES (@pregled,@usluga)";
            MySqlCommand command = new MySqlCommand(cmd, connection);
            command.Parameters.AddWithValue("@pregled", refferal.Examination);
            command.Parameters.AddWithValue("@usluga", refferal.HealthService.ID);
            command.Prepare();
            command.ExecuteNonQuery();
            int index = (int)command.LastInsertedId;
            databaseConnectionManager.ReturnConnection(connection);
            return index;
        }
        public List<Refferal> ReadAllRefferalsByExamination(int examination)
        {
            MySqlConnection connection = databaseConnectionManager.GetConnection();
            String cmd = "SELECT * FROM uputnica WHERE Pregled_idPregled=@pregled";
            MySqlCommand command = new MySqlCommand(cmd, connection);
            command.Parameters.AddWithValue("@pregled", examination);
            command.Prepare();
            MySqlDataReader rd = command.ExecuteReader();
            List<Refferal> refferals = new List<Refferal>();
            while (rd.Read())
            {
                int serviceID = rd.GetInt32("idZdravstvenaUsluga");
                HealthService healthService = healthServiceDAO.ReadHealthServiceById(serviceID);
                refferals.Add(new Refferal(rd.GetInt32("idUputnica"),examination,healthService));
            }
            rd.Close();
            databaseConnectionManager.ReturnConnection(connection);
            return refferals;
        }
    }
}
