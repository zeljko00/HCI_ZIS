using HCI_ZIS_Library.Exceptions;

using MySql.Data.MySqlClient;
using Org.BouncyCastle.Utilities.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

using HCI_ZIS_Library.Model;
using HCI_ZIS_Library.Util;

namespace HCI_ZIS_Library.DAO
{
    public class UserDAO
    {
        private HCI_ZIS_Library.Util.DatabaseConnectionManager databaseConnectionmanager;

        public UserDAO()
        {
            databaseConnectionmanager = new HCI_ZIS_Library.Util.DatabaseConnectionManager();
        }
        public int CreateUser(HCI_ZIS_Library.Model.Person user)
        {
            String passwordHash = System.Convert.ToBase64String(new MD5CryptoServiceProvider().ComputeHash(ASCIIEncoding.ASCII.GetBytes(user.Password)));
            MySqlConnection connection = databaseConnectionmanager.GetConnection();
            String cmd = " INSERT INTO osoba (JMB, Ime, Prezime, datumRodjenja, username, password) VALUES (@jmb, @ime, @prezime, @datum, @username, @password) ";
            MySqlCommand command = new MySqlCommand(cmd, connection);
            command.Parameters.AddWithValue("@jmb", user.JMB);
            command.Parameters.AddWithValue("@ime", user.Firstname);
            command.Parameters.AddWithValue("@prezime", user.LastName);
            command.Parameters.AddWithValue("@datum", user.DateOfBirth);
            command.Parameters.AddWithValue("@username", user.Username);
            command.Parameters.AddWithValue("@password", passwordHash);
            command.Prepare();
            try
            {
                command.ExecuteNonQuery();
                int index = (int)command.LastInsertedId;
                databaseConnectionmanager.ReturnConnection(connection);
                return index;
            }
            catch (Exception e)
            {
                databaseConnectionmanager.ReturnConnection(connection);
                throw new AlreadyExistsException("Korisnik je već registovan na sistemu!");
            }
        }
        public HCI_ZIS_Library.Model.Person ReadUser(string username,string password)
        {
            MySqlConnection connection = databaseConnectionmanager.GetConnection();
            String passwordHash = System.Convert.ToBase64String(new MD5CryptoServiceProvider().ComputeHash(ASCIIEncoding.ASCII.GetBytes(password)));
            String cmd = " SELECT * from osoba WHERE username=@username and password=@password";
            MySqlCommand command = new MySqlCommand(cmd, connection);
            command.Parameters.AddWithValue("@username", username);
            command.Parameters.AddWithValue("@password", passwordHash);
            command.Prepare();
            MySqlDataReader rs = command.ExecuteReader();
            if (rs.HasRows)
            {
                rs.Read();
                Person res = new Person(rs.GetInt32("idOsoba"), rs.GetString("Ime"), rs.GetString("Prezime"), rs.GetString("JMB"), username, password, rs.GetString("datumRodjenja"));
                rs.Close();
                databaseConnectionmanager.ReturnConnection(connection);
                return res;
            }
            else
            {
                rs.Close();
                databaseConnectionmanager.ReturnConnection(connection);
                throw new HCI_ZIS_Library.Exceptions.NotFoundException("Korisnik sa datim kredencijalima ne postoji!");
            }
        }
        public Person ReadUser(int id)
        {
            MySqlConnection connection = databaseConnectionmanager.GetConnection();
            String cmd = " SELECT * from osoba WHERE idOsoba=@val";
            MySqlCommand command = new MySqlCommand(cmd, connection);
            command.Parameters.AddWithValue("@val", id);
            command.Prepare();
            MySqlDataReader rs = command.ExecuteReader();
            Person person = null;
            if (rs.Read())
                person = new Person(id, rs.GetString("Ime"), rs.GetString("Prezime"), rs.GetString("JMB"), rs.GetString("username"), rs.GetString("password"), rs.GetString("datumRodjenja"));
            rs.Close();
            databaseConnectionmanager.ReturnConnection(connection);
            return person;
        }
        public void UpdateUser(Person user)
        {
            String passwordHash = System.Convert.ToBase64String(new MD5CryptoServiceProvider().ComputeHash(ASCIIEncoding.ASCII.GetBytes(user.Password)));
            MySqlConnection connection = databaseConnectionmanager.GetConnection();
            String cmd = "UPDATE osoba SET JMB=@jmb, Ime=@ime, Prezime=@prezime, datumRodjenja=@datum, username=@username, password=@password WHERE idOsoba=@id";
            MySqlCommand command = new MySqlCommand(cmd, connection);
            command.Parameters.AddWithValue("@jmb", user.JMB);
            command.Parameters.AddWithValue("@ime", user.Firstname);
            command.Parameters.AddWithValue("@prezime", user.LastName);
            command.Parameters.AddWithValue("@datum", user.DateOfBirth);
            command.Parameters.AddWithValue("@username", user.Username);
            command.Parameters.AddWithValue("@password", passwordHash);
            command.Parameters.AddWithValue("@id", user.ID);
            command.Prepare();
            command.ExecuteNonQuery();
            databaseConnectionmanager.ReturnConnection(connection);
        }

        public void DeleteUser(Person user)
        {
            MySqlConnection connection = databaseConnectionmanager.GetConnection();
            String cmd = "DELETE from osoba WHERE idOsoba=@id";
            MySqlCommand command = new MySqlCommand(cmd, connection);
            command.Parameters.AddWithValue("@id", user.ID);
            command.Prepare();
            command.ExecuteNonQuery();
            databaseConnectionmanager.ReturnConnection(connection);
        }
    }
}
