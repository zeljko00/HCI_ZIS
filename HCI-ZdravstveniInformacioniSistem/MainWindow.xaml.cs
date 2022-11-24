using HCI_ZIS_Library.DAO;
using HCI_ZIS_Library.Model;

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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HCI_ZdravstveniInformacioniSistem
{

    public partial class MainWindow : Window
    {
        private MedicalDoctorDAO medicalDoctorDAO = new MedicalDoctorDAO();
        private string error;
        public MainWindow()
        {
            InitializeComponent();
            SRP_Checked(null,null);
        }

        private void SubmitWithEnter(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                DoctorLogIn_Btn_Click(null,null);
        }
        private void DoctorLogIn_Btn_Click(object sender, RoutedEventArgs e)
        {
            CredentialsInputForm.Error_lbl.Foreground = (Brush)new BrushConverter().ConvertFrom("#FFF10404");


            try
            {
                //MedicalDoctor doctor = medicalDoctorDAO.ReadMedicalDoctor(Username_txtBox.Text.Trim(), Password_txtBox.Password.Trim());
                //Error_lbl.Content = doctor.Person.Firstname;

                //zbog testiranja
                MedicalDoctor doctor = medicalDoctorDAO.ReadMedicalDoctor("user11", "password");

                user_controls.AccountPicker accountPicker = new user_controls.AccountPicker(doctor, doctor.Language);
                accountPicker.ShowDialog();
                LoginWindow.Close();
            }
            catch (Exception)
            {
                CredentialsInputForm.Error_lbl.Content = error;
            }
        }


        private void ENG_Checked(object sender, RoutedEventArgs e)
        {
            CredentialsInputForm.PasswordHolder = "Password";
            CredentialsInputForm.UsernameHolder = "Username";
            DoctorSignIn_btn.Content = "Log in";
            error = "Invalid credentials!";
        }

        private void SRP_Checked(object sender, RoutedEventArgs e)
        {
            CredentialsInputForm.PasswordHolder = "Lozinka";
            CredentialsInputForm.UsernameHolder = "Korisničko ime";
            DoctorSignIn_btn.Content = "Prijava";
            error = "Nevalidni kredencijali!";
        }
    }
}
