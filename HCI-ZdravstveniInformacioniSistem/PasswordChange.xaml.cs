using HCI_ZIS_Library.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace HCI_ZdravstveniInformacioniSistem
{

    public partial class PasswordChange : Window
    {
        public bool Feedback { get; set; } = false;
        private MedicalDoctor Doctor;

        private string saveBtnContent;
        private string oldPasswordContent;
        private string newPasswordContent;
        public PasswordChange()
        {
            InitializeComponent();
        }
        public PasswordChange(MedicalDoctor doctor,bool b)
        {
            InitializeComponent();
            Doctor = doctor;
            Feedback = b;
            if (doctor.Language.Equals("ENG"))
            {
                saveBtnContent = "Save";
                newPasswordContent= "New password";
                oldPasswordContent = "Old password";
                PasswordChangeWin.Title = "Change password";
            }
            else
            {
                saveBtnContent = "Sačuvaj";
                newPasswordContent = "Nova lozinka";
                oldPasswordContent = "Stara lozinka";
                PasswordChangeWin.Title = "Izmjena lozinke";
            }
            switch (Doctor.Theme)
            {
                case "default":
                    Background = (Brush)new BrushConverter().ConvertFrom("#FF79B8AF");
                    FontFamily = new System.Windows.Media.FontFamily("Arial");
                    break;
                case "light":
                    Background = (Brush)new BrushConverter().ConvertFrom("#FFB1C0BE");
                    FontFamily = new System.Windows.Media.FontFamily("Calibri");
                    break;
                default:
                    Background = (Brush)new BrushConverter().ConvertFrom("#1B2430");
                    FontFamily = new System.Windows.Media.FontFamily("Monolisa");
                    break;
            }
            ChangePassword_btn.Content = saveBtnContent;
            NewPasswordPlaceholder_txtBox.Text = newPasswordContent;
            OldPasswordPlaceholder_txtBox.Text = oldPasswordContent;
        }
        private void ChangePassword_btn_Click(object sender, RoutedEventArgs e)
        {
            if (Doctor.Person.Password.Equals(OldPassword_txtBox.Password))
            {
                Doctor.Person.Password = NewPassword_txtBox.Password;
                PasswordChangeWin.Close();
            }
            else {
                Error_lbl.Foreground = System.Windows.Media.Brushes.Red;
                Error_lbl.Content = Doctor.Language.Equals("ENG")?"Wrong password...":"Pogrešna lozinka...";
            }
        }

        private void OldPasswordPlaceholder_txtBox_GotFocus(object sender, RoutedEventArgs e)
        {
            OldPasswordPlaceholder_txtBox.Focusable = false;
            OldPasswordPlaceholder_txtBox.Visibility = System.Windows.Visibility.Collapsed;
            OldPassword_txtBox.Focus();

        }

        private void OldPassword_TxtBox_LostFocus(object sender, RoutedEventArgs e)
        {
            OldPassword_txtBox.Opacity = 0.8;
            if (OldPassword_txtBox.Password.Equals("") == true)
            {
                OldPasswordPlaceholder_txtBox.Focusable = true;
                OldPasswordPlaceholder_txtBox.Visibility = System.Windows.Visibility.Visible;
                OldPasswordPlaceholder_txtBox.Opacity = 0.8;
                OldPassword_txtBox.Opacity = 0;
                Error_lbl.Content = "";
                Error_lbl.Foreground = (Brush)new BrushConverter().ConvertFrom("#FFF10404");
            }
        }

        private void OldPassword_txtBox_GotFocus(object sender, RoutedEventArgs e)
        {
            OldPassword_txtBox.Password = "";
            OldPassword_txtBox.Opacity = 1.0;
        }



        private void NewPasswordPlaceholder_txtBox_GotFocus(object sender, RoutedEventArgs e)
        {
            NewPasswordPlaceholder_txtBox.Focusable = false;
            NewPasswordPlaceholder_txtBox.Visibility = System.Windows.Visibility.Collapsed;
            NewPassword_txtBox.Focus();
        }

        private void NewPassword_TxtBox_LostFocus(object sender, RoutedEventArgs e)
        {
            NewPassword_txtBox.Opacity = 0.8;
            if (NewPassword_txtBox.Password.Equals("") == true)
            {
                NewPasswordPlaceholder_txtBox.Focusable = true;
                NewPasswordPlaceholder_txtBox.Visibility = System.Windows.Visibility.Visible;
                NewPasswordPlaceholder_txtBox.Opacity = 0.8;
                NewPassword_txtBox.Opacity = 0;
                Error_lbl.Content = "";
                Error_lbl.Foreground = (Brush)new BrushConverter().ConvertFrom("#FFF10404");
            }
        }

        private void NewPassword_TxtBox_GotFocus(object sender, RoutedEventArgs e)
        {
            NewPassword_txtBox.Password = "";
            NewPassword_txtBox.Opacity = 1.0;
        }

        private void NewPassword_TxtBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            int len = NewPassword_txtBox.Password.Length;
            if (Feedback)
            {

                if (len < 6 && len > 0)
                {
                    Error_lbl.Content = Doctor.Language.Equals("ENG") ? "Weak password":"Slaba lozinka";
                    Error_lbl.Foreground = System.Windows.Media.Brushes.Red;
                }
                else if (len > 5 && len < 11)
                {
                    Error_lbl.Content = Doctor.Language.Equals("ENG") ? "Good password":"Srednje jaka lozinka";
                    Error_lbl.Foreground = System.Windows.Media.Brushes.Blue;
                }
                else if (len > 10)
                {
                    Error_lbl.Content = Doctor.Language.Equals("ENG") ? "Strong password":"Veoma jaka lozinka";
                    Error_lbl.Foreground = System.Windows.Media.Brushes.Green;
                }
            }

        }

        private void Change_OnEnter(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                ChangePassword_btn_Click(null,null);
        }
    }
}