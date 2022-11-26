using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HCI_ZIS_Library.Util;
using MySql.Data.MySqlClient;

namespace HCI_ZIS_Library.DAO
{
    public class SpecializationDAO
    {
        private DatabaseConnectionManager databaseConnectionManager=new DatabaseConnectionManager();
        public List<string> ReadAllSpecializations()
        {
            MySqlConnection connection = databaseConnectionManager.GetConnection();
            String cmd = " SELECT * from specijalizacija";
            MySqlCommand command = new MySqlCommand(cmd, connection);
            command.Prepare();
            MySqlDataReader rs = command.ExecuteReader();
            List<string> result = new List<string>();
            while (rs.Read())
            {
                result.Add(rs.GetString("naziv"));
            }
            rs.Close();
            databaseConnectionManager.ReturnConnection(connection);
            return result;
        }

    }
}
