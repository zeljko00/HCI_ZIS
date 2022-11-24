using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using HCI_ZIS_Library.DAO;
using HCI_ZIS_Library.Model;

namespace HCI_ZdravstveniInformacioniSistem.user_controls
{
    /// <summary>
    /// Interaction logic for AccountPicker.xaml
    /// </summary>
    public partial class AccountPicker : Window
    {
        private MedicalDoctor medicalDoctor;
        public AccountPicker(MedicalDoctor doctor,string lang)
        {
            medicalDoctor = doctor;
            InitializeComponent();
            if ("SRP".Equals(lang))
            {
                Doc.Content = "Ljekar";
            }
            else
            {
                Doc.Content = "M.D.";
            }
        }

        private void Doc_Click(object sender, RoutedEventArgs e)
        {
            DoctorScreen doctorScreen = new DoctorScreen(medicalDoctor);
            doctorScreen.Show();
            this.Close();
        }

        private void Admin_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
