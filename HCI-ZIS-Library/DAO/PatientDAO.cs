using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

using HCI_ZIS_Library.Model;
using HCI_ZIS_Library.Exceptions;
using MySql.Data.MySqlClient;
using HCI_ZIS_Library.Util;

namespace HCI_ZIS_Library.DAO
{
    public class PatientDAO
    {
        private HCI_ZIS_Library.Util.DatabaseConnectionManager databaseConnectionmanager;
        private UserDAO userDAO = new UserDAO();

        public PatientDAO()
        {
            databaseConnectionmanager = new HCI_ZIS_Library.Util.DatabaseConnectionManager();
        }
        public int CreatePatient(HCI_ZIS_Library.Model.Patient patient)
        {
            String passwordHash = System.Convert.ToBase64String(new MD5CryptoServiceProvider().ComputeHash(ASCIIEncoding.ASCII.GetBytes(patient.Person.Password)));
            MySqlConnection connection = databaseConnectionmanager.GetConnection();
            String cmd = " INSERT INTO osoba (JMB, Ime, Prezime, datumRodjenja, username, password) VALUES (@jmb, @ime, @prezime, @datum, @Patientname, @password) ";
            MySqlCommand command = new MySqlCommand(cmd, connection);
            command.Parameters.AddWithValue("@jmb", patient.Person.JMB);
            command.Parameters.AddWithValue("@ime", patient.Person.Firstname);
            command.Parameters.AddWithValue("@prezime", patient.Person.LastName);
            command.Parameters.AddWithValue("@datum", patient.Person.DateOfBirth);
            command.Parameters.AddWithValue("@Patientname", patient.Person.Username);
            command.Parameters.AddWithValue("@password", passwordHash);
            command.Prepare();
            try
            {
                command.ExecuteNonQuery();
                int index = (int)command.LastInsertedId;

                cmd = " INSERT INTO pacijent (BrojZdravstveneKnjizice, idOsoba, Napomena) VALUES (@broj, @id, @napomena) ";
                command = new MySqlCommand(cmd, connection);
                command.Parameters.AddWithValue("@broj", patient.HealthCardID);
                command.Parameters.AddWithValue("@id", index);
                command.Parameters.AddWithValue("@napomena", patient.Note);
                command.Prepare();
                command.ExecuteNonQuery();
                index=(int)command.LastInsertedId;
                databaseConnectionmanager.ReturnConnection(connection);
                return index;
            }
            catch (Exception e)
            {
                databaseConnectionmanager.ReturnConnection(connection);
                throw new AlreadyExistsException("Korisnik je već registovan na sistemu!");
            }
        }
        public HCI_ZIS_Library.Model.Patient ReadPatient(string username, string password)
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
                Person person = new Person(rs.GetInt32("idOsoba"), rs.GetString("Ime"), rs.GetString("Prezime"), rs.GetString("JMB"), username, password, rs.GetString("datumRodjenja"));
                rs.Close();

                cmd = " SELECT * from pacijent WHERE idOsoba=@id";
                command = new MySqlCommand(cmd, connection);
                command.Parameters.AddWithValue("@id", person.ID);
                command.Prepare();
                rs = command.ExecuteReader();

                if (rs.HasRows)
                {
                    rs.Read();
                    Patient patient = new Patient(person, rs.GetString("BrojZdravstveneKnjizice"), rs.GetString("Napomena")); //ne citamo tim porodicne medicine
                    rs.Close();
                    databaseConnectionmanager.ReturnConnection(connection);
                    return patient;
                }
                else
                {
                    databaseConnectionmanager.ReturnConnection(connection);
                    throw new HCI_ZIS_Library.Exceptions.NotFoundException("Pacijent sa datim kredencijalima ne postoji!");
                }
            }
            else
            {
                rs.Close();
                databaseConnectionmanager.ReturnConnection(connection);
                throw new HCI_ZIS_Library.Exceptions.NotFoundException("Korisnik sa datim kredencijalima ne postoji!");
            }
        }

        public HCI_ZIS_Library.Model.Patient ReadPatient(int id)
        {
            MySqlConnection connection = databaseConnectionmanager.GetConnection();
            String cmd = " SELECT * from osoba WHERE idOsoba=@val";
            MySqlCommand command = new MySqlCommand(cmd, connection);
            command.Parameters.AddWithValue("@val", id);
            command.Prepare();
            MySqlDataReader rs = command.ExecuteReader();
            if (rs.HasRows)
            {
                rs.Read();
                Person person = new Person(rs.GetInt32("idOsoba"), rs.GetString("Ime"), rs.GetString("Prezime"), rs.GetString("JMB"), rs.GetString("username"), rs.GetString("password"), rs.GetString("datumRodjenja"));
                rs.Close();

                cmd = " SELECT * from pacijent WHERE idOsoba=@id";
                command = new MySqlCommand(cmd, connection);
                command.Parameters.AddWithValue("@id", person.ID);
                command.Prepare();
                rs = command.ExecuteReader();

                if (rs.HasRows)
                {
                    rs.Read();
                    Patient patient = new Patient(person, rs.GetString("BrojZdravstveneKnjizice"), rs.GetString("Napomena")); //ne citamo tim porodicne medicine
                    rs.Close();
                    databaseConnectionmanager.ReturnConnection(connection);
                    return patient;
                }
                else
                {
                    rs.Close();
                    databaseConnectionmanager.ReturnConnection(connection);
                    return null;
                }
            }
            else
            {
                rs.Close();
                databaseConnectionmanager.ReturnConnection(connection);
                return null;
            }
        }

        public List<HCI_ZIS_Library.Model.Patient> ReadAllPatients()
        {
            List<Patient> patients = new List<Patient>();

            MySqlConnection connection = databaseConnectionmanager.GetConnection();
            String cmd = "SELECT * from pacijent";
            MySqlCommand command = new MySqlCommand(cmd, connection);
            command.Prepare();
            MySqlDataReader rs = command.ExecuteReader();

            while (rs.Read())
            {
                Patient patient = new Patient(null, rs.GetString("BrojZdravstveneKnjizice"), rs.GetString("Napomena"));
                int id = rs.GetInt32("idOsoba");

                Person person = userDAO.ReadUser(id);
                patient.Person = person;
                patients.Add(patient);
            }
             rs.Close();
             databaseConnectionmanager.ReturnConnection(connection);
             return patients;
        }
        public void UpdatePatient(Patient medicalDoctor)
        {
            String passwordHash = System.Convert.ToBase64String(new MD5CryptoServiceProvider().ComputeHash(ASCIIEncoding.ASCII.GetBytes(medicalDoctor.Person.Password)));
            MySqlConnection connection = databaseConnectionmanager.GetConnection();
            String cmd = "UPDATE osoba SET JMB=@jmb, Ime=@ime, Prezime=@prezime, datumRodjenja=@datum, username=@username, password=@password WHERE idOsoba=@id";
            MySqlCommand command = new MySqlCommand(cmd, connection);
            command.Parameters.AddWithValue("@jmb", medicalDoctor.Person.JMB);
            command.Parameters.AddWithValue("@ime", medicalDoctor.Person.Firstname);
            command.Parameters.AddWithValue("@prezime", medicalDoctor.Person.LastName);
            command.Parameters.AddWithValue("@datum", medicalDoctor.Person.DateOfBirth);
            command.Parameters.AddWithValue("@username", medicalDoctor.Person.Username);
            command.Parameters.AddWithValue("@password", passwordHash);
            command.Parameters.AddWithValue("@id", medicalDoctor.Person.ID);
            command.Prepare();
            command.ExecuteNonQuery();

            cmd = "UPDATE pacijent SET BrojZdravstveneKnjizice=@broj, Napomena=@napomena";
            command = new MySqlCommand(cmd, connection);
            command.Parameters.AddWithValue("@broj", medicalDoctor.HealthCardID);
            command.Parameters.AddWithValue("@napomena", medicalDoctor.Note);
            command.Prepare();
            command.ExecuteNonQuery();
            databaseConnectionmanager.ReturnConnection(connection);
        }

        public void DeletePatient(Patient patient)
        {
            MySqlConnection connection = databaseConnectionmanager.GetConnection();
            String cmd = "DELETE from pacijent WHERE idOsoba=@id";
            MySqlCommand command = new MySqlCommand(cmd, connection);
            command.Parameters.AddWithValue("@id", patient.Person.ID);
            command.Prepare();
            command.ExecuteNonQuery();
            databaseConnectionmanager.ReturnConnection(connection);
        }
    }
}

