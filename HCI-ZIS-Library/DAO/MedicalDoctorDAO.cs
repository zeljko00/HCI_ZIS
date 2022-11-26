using HCI_ZIS_Library.Exceptions;

using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

using HCI_ZIS_Library.Model;
using HCI_ZIS_Library.Util;
using System.Runtime.InteropServices;
using System.Reflection;

namespace HCI_ZIS_Library.DAO
{
    public class MedicalDoctorDAO
    {
        private HCI_ZIS_Library.Util.DatabaseConnectionManager databaseConnectionmanager;

        public MedicalDoctorDAO()
        {
            databaseConnectionmanager = new HCI_ZIS_Library.Util.DatabaseConnectionManager();
        }
        public int CreateMedicalDoctor(HCI_ZIS_Library.Model.MedicalDoctor medicalDoctor)
        {
            String passwordHash = System.Convert.ToBase64String(new MD5CryptoServiceProvider().ComputeHash(ASCIIEncoding.ASCII.GetBytes(medicalDoctor.Person.Password)));
            MySqlConnection connection = databaseConnectionmanager.GetConnection();
            String cmd = " INSERT INTO osoba (JMB, Ime, Prezime, datumRodjenja, username, password) VALUES (@jmb, @ime, @prezime, @datum, @MedicalDoctorname, @password) ";
            MySqlCommand command = new MySqlCommand(cmd, connection);
            command.Parameters.AddWithValue("@jmb", medicalDoctor.Person.JMB);
            command.Parameters.AddWithValue("@ime", medicalDoctor.Person.Firstname);
            command.Parameters.AddWithValue("@prezime", medicalDoctor.Person.LastName);
            command.Parameters.AddWithValue("@datum", medicalDoctor.Person.DateOfBirth);
            command.Parameters.AddWithValue("@MedicalDoctorname", medicalDoctor.Person.Username);
            command.Parameters.AddWithValue("@password", passwordHash);
            command.Prepare();

                int index=-1;
                try
                {
                    command.ExecuteNonQuery();
                    index = (int)command.LastInsertedId;
                }
                catch (Exception)
                {
                    cmd = " SELECT idOsoba from osoba WHERE JMB=@val";
                    command = new MySqlCommand(cmd, connection);
                    command.Parameters.AddWithValue("@val", medicalDoctor.Person.JMB);
                    command.Prepare();
                    MySqlDataReader rs=command.ExecuteReader();
                    if (rs.Read())
                    {
                        index = rs.GetInt32("idOsoba");
                    }
                }
                    databaseConnectionmanager.ReturnConnection(connection);
                    return CreateDoctorOnly(medicalDoctor, index);
                
        }
        private int CreateDoctorOnly(MedicalDoctor medicalDoctor, int index)
        {
                MySqlConnection connection = databaseConnectionmanager.GetConnection();
            try
            {
                string cmd = " INSERT INTO ljekar (BrojLicence, idOsoba, specijalizacija, brojPregleda, brojUputnica, brojRecepata, isAdmin, Tema, Jezik,idZU) VALUES (@licenca, @id, @spec, @pregled, @uputnica, @recept, @admin, @tema, @jezik, @zu) ";
                MySqlCommand command1 = new MySqlCommand(cmd, connection);
                command1.Parameters.AddWithValue("@licenca", medicalDoctor.Licence);
                command1.Parameters.AddWithValue("@id", index);
                command1.Parameters.AddWithValue("@spec", medicalDoctor.Specialization);
                command1.Parameters.AddWithValue("@pregled", medicalDoctor.ExaminationNum);
                command1.Parameters.AddWithValue("@uputnica", medicalDoctor.RefferalNum);
                command1.Parameters.AddWithValue("@recept", medicalDoctor.PrescriptionNum);
                command1.Parameters.AddWithValue("@admin", medicalDoctor.IsAdmin ? 1 : 0);
                command1.Parameters.AddWithValue("@tema", medicalDoctor.Theme);
                command1.Parameters.AddWithValue("@jezik", medicalDoctor.Language);
                command1.Parameters.AddWithValue("@zu", medicalDoctor.HealthCareFacility);
                command1.Prepare();
                command1.ExecuteNonQuery();
                databaseConnectionmanager.ReturnConnection(connection);
                return index;
            }
            catch (Exception e)
            {
                databaseConnectionmanager.ReturnConnection(connection);
                throw new AlreadyExistsException("Korisnik je već registovan na sistemu!");
            }
        }
        public HCI_ZIS_Library.Model.MedicalDoctor ReadMedicalDoctor(string username, string password)
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

                cmd = " SELECT * from ljekar WHERE idOsoba=@id";
                command = new MySqlCommand(cmd, connection);
                command.Parameters.AddWithValue("@id", person.ID);
                command.Prepare();
                rs = command.ExecuteReader();

                if (rs.Read())
                {
                    MedicalDoctor doctor = new MedicalDoctor(person, rs.GetString("specijalizacija"), rs.GetString("BrojLicence"), rs.GetInt32("idZU"),rs.GetInt32("brojPregleda"), rs.GetInt32("brojRecepata"), rs.GetInt32("brojUputnica"),rs.GetInt16("isAdmin")>0?true:false,rs.GetString("Tema"),rs.GetString("Jezik"),rs.GetInt16("aktivan")>0?true:false);
                    rs.Close();
                    databaseConnectionmanager.ReturnConnection(connection);
                    return doctor;
                }
                else
                {
                    rs.Close();
                    databaseConnectionmanager.ReturnConnection(connection);
                    throw new HCI_ZIS_Library.Exceptions.NotFoundException("Ljekar sa datim kredencijalima ne postoji!");
                }
            }
            else 
            {
                rs.Close();
                databaseConnectionmanager.ReturnConnection(connection);
                throw new HCI_ZIS_Library.Exceptions.NotFoundException("Korisnik sa datim kredencijalima ne postoji!"); 
            }
        }


        public HCI_ZIS_Library.Model.MedicalDoctor ReadMedicalDoctor(int id)
        {
            MySqlConnection connection = databaseConnectionmanager.GetConnection();
            String cmd = " SELECT * from osoba WHERE idOsoba=@val";
            MySqlCommand command = new MySqlCommand(cmd, connection);
            command.Parameters.AddWithValue("@val", id);
            command.Prepare();
            MySqlDataReader rs = command.ExecuteReader();
            if (rs.Read())
            {
                Person person = new Person(rs.GetInt32("idOsoba"), rs.GetString("Ime"), rs.GetString("Prezime"), rs.GetString("JMB"), rs.GetString("username"), rs.GetString("password"), rs.GetString("datumRodjenja"));
                rs.Close();

                cmd = " SELECT * from ljekar WHERE idOsoba=@id";
                command = new MySqlCommand(cmd, connection);
                command.Parameters.AddWithValue("@id", person.ID);
                command.Prepare();
                rs = command.ExecuteReader();

                if (rs.Read())
                {
                    MedicalDoctor doctor = new MedicalDoctor(person, rs.GetString("specijalizacija"), rs.GetString("BrojLicence"), rs.GetInt32("idZU"), rs.GetInt32("brojPregleda"), rs.GetInt32("brojRecepata"), rs.GetInt32("brojUputnica"), rs.GetInt16("isAdmin") > 0 ? true : false, rs.GetString("Tema"), rs.GetString("Jezik"),rs.GetInt16("aktivan") > 0 ? true : false);
                    rs.Close();
                    databaseConnectionmanager.ReturnConnection(connection);
                    return doctor;
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

        public List<HCI_ZIS_Library.Model.MedicalDoctor> ReadAllMedicalDoctors()
        {
            MySqlConnection connection = databaseConnectionmanager.GetConnection();
            String cmd = " SELECT * from ljekar";
            MySqlCommand command = new MySqlCommand(cmd, connection);
            command.Prepare();
            MySqlDataReader rs = command.ExecuteReader();
            List<MedicalDoctor> result = new List<MedicalDoctor>();
                while (rs.Read())
                {
                    MedicalDoctor doctor = ReadMedicalDoctor(rs.GetInt32("idOsoba"));
                    result.Add(doctor);
                }
            return result;

        }
        public void UpdateMedicalDoctor(MedicalDoctor medicalDoctor)
        {
            String passwordHash = System.Convert.ToBase64String(new MD5CryptoServiceProvider().ComputeHash(ASCIIEncoding.ASCII.GetBytes(medicalDoctor.Person.Password)));
            MySqlConnection connection = databaseConnectionmanager.GetConnection();
            String cmd = "UPDATE osoba SET Ime=@ime, Prezime=@prezime, datumRodjenja=@datum, password=@password WHERE idOsoba=@id";
            MySqlCommand command = new MySqlCommand(cmd, connection);
            command.Parameters.AddWithValue("@ime", medicalDoctor.Person.Firstname);
            command.Parameters.AddWithValue("@prezime", medicalDoctor.Person.LastName);
            command.Parameters.AddWithValue("@datum", medicalDoctor.Person.DateOfBirth);
            command.Parameters.AddWithValue("@password", passwordHash);
            command.Parameters.AddWithValue("@id", medicalDoctor.Person.ID);

            command.Prepare();
            command.ExecuteNonQuery();

            cmd = "UPDATE ljekar SET BrojLicence=@licenca, specijalizacija=@spec, idZU=@zu, brojPregleda=@pregled, brojUputnica=@uputnica, brojRecepata=@recept, isAdmin=@admin, Tema=@tema, Jezik=@jezik,aktivan=@val WHERE idOsoba=@id";
            command = new MySqlCommand(cmd, connection);
            command.Parameters.AddWithValue("@licenca", medicalDoctor.Licence);
            command.Parameters.AddWithValue("@spec", medicalDoctor.Specialization);
            command.Parameters.AddWithValue("@zu", medicalDoctor.HealthCareFacility);
            command.Parameters.AddWithValue("@pregled", medicalDoctor.ExaminationNum);
            command.Parameters.AddWithValue("@uputnica", medicalDoctor.RefferalNum);
            command.Parameters.AddWithValue("@recept", medicalDoctor.PrescriptionNum);
            command.Parameters.AddWithValue("@admin", medicalDoctor.IsAdmin == true ? 1 : 0);
            command.Parameters.AddWithValue("@tema", medicalDoctor.Theme);
            command.Parameters.AddWithValue("@jezik", medicalDoctor.Language);
            command.Parameters.AddWithValue("@val", medicalDoctor.Active?1:0);
            command.Parameters.AddWithValue("@id", medicalDoctor.Person.ID);
            command.Prepare();
            command.ExecuteNonQuery();
            databaseConnectionmanager.ReturnConnection(connection);

        }

            public void DeleteMedicalDoctor(MedicalDoctor doctor)
        {
            MySqlConnection connection = databaseConnectionmanager.GetConnection();
            String cmd = "DELETE from ljekar WHERE idOsoba=@id";
            MySqlCommand command = new MySqlCommand(cmd, connection);
            command.Parameters.AddWithValue("@id", doctor.Person.ID);
            command.Prepare();
            command.ExecuteNonQuery();
            databaseConnectionmanager.ReturnConnection(connection);
        }
    }
}
