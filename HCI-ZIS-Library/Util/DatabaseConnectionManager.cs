using MySql.Data.MySqlClient;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HCI_ZIS_Library.Util
{
    public class DatabaseConnectionManager
    {
        //procitati iz konf fajla
        private static readonly int n = 5;
        private static string connectionString;
        private static Queue<MySqlConnection> connections = null;
        private static Semaphore semaphore; 

        public DatabaseConnectionManager()
        {
            //var v = ConfigurationManager.AppSettings;
            connectionString = ConfigurationManager.AppSettings["mysql_connection_string"];
            Console.WriteLine(connectionString);
            if (connections == null)
            {
                connections=new Queue<MySqlConnection> ();
                for (int i=0; i<n; i++)
                    connections.Enqueue (new MySqlConnection (connectionString));
                semaphore = new Semaphore(n, n);
            }
        }

        public MySqlConnection GetConnection()
        {
            semaphore.WaitOne();
            MySqlConnection connection = connections.Dequeue();
            if (connection.State != ConnectionState.Open)
                connection.Open();
            return connection;
        }
        public void ReturnConnection(MySqlConnection connection)
        {
            connections.Enqueue(connection);
            semaphore.Release();
        }
        public void CloseConnections()
        {
            foreach(MySqlConnection connection in connections)
                connection.Close();
        }
    }
}
