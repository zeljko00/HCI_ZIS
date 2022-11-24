using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Lifetime;
using System.Text;
using System.Threading.Tasks;

using HCI_ZIS_Library.Model;
using HCI_ZIS_Library.Util;
using MySql.Data.MySqlClient;

namespace HCI_ZIS_Library.DAO
{
    public class DiagnosisDAO
    {
        private DatabaseConnectionManager databaseConnectionManager=new DatabaseConnectionManager();
        private DiseaseDAO diseaseDAO = new DiseaseDAO();

        public int CreateDiagnosis(Examination examination, Disease disease)
        {
            MySqlConnection connection = databaseConnectionManager.GetConnection();
            String cmd = "INSERT INTO dijagnostifikovanabolest (Pregled_idPregled, idBolest) VALUES (@pregled,@bolest)";
            MySqlCommand command=new MySqlCommand(cmd, connection);
            command.Parameters.AddWithValue("@pregled", examination.Id);
            command.Parameters.AddWithValue("@bolest", disease.ID);
            command.Prepare();
            command.ExecuteNonQuery();
            int index = (int)command.LastInsertedId;
            databaseConnectionManager.ReturnConnection(connection);
            return index;
        }
        public List<Disease> ReadAllDiagnosisByExamination(int examination)
        {
            MySqlConnection connection = databaseConnectionManager.GetConnection();
            String cmd = "SELECT * FROM dijagnostifikovanabolest WHERE Pregled_idPregled=@pregled";
            MySqlCommand command = new MySqlCommand(cmd, connection);
            command.Parameters.AddWithValue("@pregled", examination);
            command.Prepare();
            MySqlDataReader rd = command.ExecuteReader();
            List<Disease> diseases = new List<Disease>();
            while (rd.Read())
            {
                int diseaseID = rd.GetInt32("idBolest");
                Disease disease = diseaseDAO.ReadDisease(diseaseID);
                diseases.Add(disease);
            }
            rd.Close();
            databaseConnectionManager.ReturnConnection(connection);
            return diseases;
        }
    }
}
