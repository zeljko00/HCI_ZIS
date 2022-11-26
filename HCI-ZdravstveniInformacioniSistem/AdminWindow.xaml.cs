using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Emit;
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
using HCI_ZdravstveniInformacioniSistem.user_controls;
using HCI_ZIS_Library.DAO;
using HCI_ZIS_Library.Model;

namespace HCI_ZdravstveniInformacioniSistem
{
    public partial class AdminWindow : Window, INotifyPropertyChanged
    {
        private MedicalDoctor medicalDoctor;

        private MedicalDoctorDAO medicalDoctorDAO = new MedicalDoctorDAO();

        public event PropertyChangedEventHandler PropertyChanged;

        private const string darkTheme = "dark";
        private const string lightTheme = "light";
        private const string defaultTheme = "default";
        private const string srpLang = "SRP";
        private const string engLang = "ENG";

        private NewMedUC newMedUC;
        private NewHealthServiceUC newHealthServiceUC;
        private NewHCF newHCF;
        private NewPatientUC newPatientUC;
        private DoctorsUC doctorsUC;


        private string menuDoctor;
        public string MenuDoctor
        {
            get
            {
                return menuDoctor;
            }
            set
            {
                menuDoctor = value;
                OnPropertyChanged("MenuDoctor");
            }
        }

        private string newDoctor;
        public string NewDoctor
        {
            get
            {
                return newDoctor;
            }
            set
            {
                newDoctor = value;
                OnPropertyChanged("NewDoctor");
            }
        }
        private string doctors;
        public string Doctors
        {
            get
            {
                return doctors;
            }
            set
            {
                doctors = value;
                OnPropertyChanged("Doctors");
            }
        }

        private string menuMeds;
        public string MenuMeds
        {
            get
            {
                return menuMeds;
            }
            set
            {
                menuMeds = value;
                OnPropertyChanged("MenuMeds");
            }
        }

        private string menuServices;
        public string MenuServices
        {
            get
            {
                return menuServices;
            }
            set
            {
                menuServices = value;
                OnPropertyChanged("MenuServices");
            }
        }

        private string menuPatient;
        public string MenuPatient
        {
            get
            {
                return menuPatient;
            }
            set
            {
                menuPatient = value;
                OnPropertyChanged("MenuPatient");
            }
        }

        private string menuHCFacilities;
        public string MenuHCFacilities
        {
            get
            {
                return menuHCFacilities;
            }
            set
            {
                menuHCFacilities = value;
                OnPropertyChanged("MenuHCFacilities");
            }
        }
        private string menuSett;
        public string MenuSett
        {
            get
            {
                return menuSett;
            }
            set
            {
                menuSett = value;
                OnPropertyChanged("MenuSett");
            }
        }
        private string lang;
        public string Lang
        {
            get
            {
                return lang;
            }
            set
            {
                lang = value;
                OnPropertyChanged("Lang");
            }
        }
        private string lang1;
        public string Lang1
        {
            get
            {
                return lang1;
            }
            set
            {
                lang1 = value;
                OnPropertyChanged("Lang1");
            }
        }
        private string lang2;
        public string Lang2
        {
            get
            {
                return lang2;
            }
            set
            {
                lang2 = value;
                OnPropertyChanged("Lang2");
            }
        }
        private string theme;
        public string Theme
        {
            get
            {
                return theme;
            }
            set
            {
                theme = value;
                OnPropertyChanged("Theme");
            }
        }
        private string themeDef;
        public string ThemeDef
        {
            get
            {
                return themeDef;
            }
            set
            {
                themeDef = value;
                OnPropertyChanged("ThemeDef");
            }
        }
        private string themeD;
        public string ThemeD
        {
            get
            {
                return themeD;
            }
            set
            {
                themeD = value;
                OnPropertyChanged("ThemeD");
            }
        }
        private string themeL;
        public string ThemeL
        {
            get
            {
                return themeL;
            }
            set
            {
                themeL = value;
                OnPropertyChanged("ThemeL");
            }
        }

        private string logout;
        public string Logout
        {
            get
            {
                return logout;
            }
            set
            {
                logout = value;
                OnPropertyChanged("Logout");
            }
        }

        public AdminWindow(MedicalDoctor medicalDoctor)
        {
            this.medicalDoctor = medicalDoctor;
            newMedUC = new NewMedUC(medicalDoctor.Language);
            Grid.SetRow(newMedUC, 1);
            newHealthServiceUC = new NewHealthServiceUC(medicalDoctor.Language);
            Grid.SetRow(newHealthServiceUC, 1);
            newHCF = new NewHCF(medicalDoctor.Language);
            Grid.SetRow(newHCF, 1);
            newPatientUC = new NewPatientUC(medicalDoctor.Language);
            Grid.SetRow(newPatientUC, 1);
            doctorsUC=new DoctorsUC(medicalDoctor.Language);
            Grid.SetRow(doctorsUC, 1);

            InitializeComponent();
            ChangeLangugage(medicalDoctor.Language);
            ChangeTheme(medicalDoctor.Theme);
            DataContext = this;
        }

        private void OnPropertyChanged(string info)
        {
            System.ComponentModel.PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new System.ComponentModel.PropertyChangedEventArgs(info));
            }
        }

        private void SRP_Checked(object sender, RoutedEventArgs e)
        {
            ChangeLangugage(srpLang);
        }
        private void ENG_Checked(object sender, RoutedEventArgs e)
        {
            ChangeLangugage(engLang);
        }

        private void Default_Checked(object sender, RoutedEventArgs e)
        {
            ChangeTheme(defaultTheme);
        }
        private void Dark_Checked(object sender, RoutedEventArgs e)
        {
            ChangeTheme(darkTheme);
        }
        private void Light_Checked(object sender, RoutedEventArgs e)
        {
            ChangeTheme(lightTheme);
        }
        private void RemoveContent()
        {
            ContentGrid.Children.Remove(newMedUC);
            ContentGrid.Children.Remove(newHealthServiceUC);
            ContentGrid.Children.Remove(newHCF);
            ContentGrid.Children.Remove(newPatientUC);
            ContentGrid.Children.Remove(doctorsUC);
        }

        private void ChangeLangugage(string lang)
        {
            medicalDoctor.Language = lang;
            newMedUC.Lang = lang;
            newHealthServiceUC.Lang = lang;
            newHCF.Lang = lang;
            newPatientUC.Lang = lang;
            doctorsUC.Lang = lang;

            switch (lang)
            {
                case srpLang:
                    MenuDoctor = "Ljekari";
                    Doctors = "Nalozi ljekara";
                    NewDoctor = "Novi ljekar";
                    MenuPatient = "Novi pacijent";
                    MenuMeds = "Novi lijek";
                    MenuServices = "Nova usluga";
                    MenuHCFacilities = "Nova ZU";

                    MenuSett = "Podešavanja";
                    Lang = "Jezik";
                    Lang1 = "Srpski jezik";
                    Lang2 = "Engleski jezik";
                    Theme = "Tema";
                    ThemeDef = "Podrazumijevano";
                    ThemeD = "Tamno";
                    ThemeL = "Svijetlo";
                    Logout = "Odjava";
                    SRP.IsChecked = true;
                    ENG.IsChecked = false;
                    break;
                default:
                    MenuDoctor = "Medical doctors";
                    Doctors = "Doctors' accounts";
                    NewDoctor = "New doctor";
                    MenuPatient = "New patient";
                    MenuMeds = "New medication";
                    MenuServices = "New service";
                    MenuHCFacilities = "New HCF";

                    MenuSett = "Settings";
                    Lang = "Language";
                    Lang1 = "Serbian";
                    Lang2 = "English";
                    Theme = "Theme";
                    ThemeDef = "Default";
                    ThemeD = "Dark";
                    ThemeL = "Light";
                    Logout = "Logout";


                    SRP.IsChecked = false;
                    ENG.IsChecked = true;
                    break;
            }
        }
        private void ChangeTheme(string theme)
        {
            medicalDoctor.Theme = theme;

            switch (theme)
            {
                case defaultTheme:
                    AdminWin.Background = (Brush)new BrushConverter().ConvertFrom("#FF79B8AF");
                    AdminWin.FontFamily = new System.Windows.Media.FontFamily("Arial");
                    Menu.Foreground = (Brush)new BrushConverter().ConvertFrom("#FF212121");
                    AdminWin.FontSize = 15;

                    Default.IsChecked = true;
                    Dark.IsChecked = false;
                    Light.IsChecked = false;
                    break;
                case lightTheme:
                    AdminWin.Background = (Brush)new BrushConverter().ConvertFrom("#FFB1C0BE");
                    AdminWin.FontFamily = new System.Windows.Media.FontFamily("Calibri");
                    Menu.Foreground = (Brush)new BrushConverter().ConvertFrom("#FF212121");
                    AdminWin.FontSize = 17;

                    Default.IsChecked = false;
                    Dark.IsChecked = false;
                    Light.IsChecked = true;

                    break;
                default:
                    AdminWin.Background = (Brush)new BrushConverter().ConvertFrom("#1B2430");
                    AdminWin.FontFamily = new System.Windows.Media.FontFamily("MonoLisa");
                    Menu.Foreground = System.Windows.Media.Brushes.LightGray;
                    AdminWin.FontSize = 13;

                    Default.IsChecked = false;
                    Dark.IsChecked = true;
                    Light.IsChecked = false;
                    break;
            }

        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            AdminWin.Close();
        }

        private void Patient_Click(object sender, RoutedEventArgs e)
        {
            RemoveContent();
            newPatientUC.Lang=medicalDoctor.Language;
            ContentGrid.Children.Add(newPatientUC);

        }
        private void Doctors_Click(object sender, RoutedEventArgs e)
        {
            RemoveContent();
            doctorsUC.Lang = medicalDoctor.Language;
            ContentGrid.Children.Add(doctorsUC);
        }
        private void NewDoctor_Click(object sender, RoutedEventArgs e)
        {
            RemoveContent();
        }
        private void Services_Click(object sender, RoutedEventArgs e)
        {
            RemoveContent();
            newHealthServiceUC.Lang = medicalDoctor.Language;
            ContentGrid.Children.Add(newHealthServiceUC);
        }
        private void Meds_Click(object sender, RoutedEventArgs e)
        {
            RemoveContent();
            newMedUC.Lang = medicalDoctor.Language;
            ContentGrid.Children.Add(newMedUC);
        }
        private void HCFacility_Click(object sender, RoutedEventArgs e)
        {
            RemoveContent();
            newHCF.Lang = medicalDoctor.Language;
            ContentGrid.Children.Add(newHCF);
        }

        private void AutoSave_Closing(object sender, EventArgs e)
        {
            medicalDoctorDAO.UpdateMedicalDoctor(medicalDoctor);
        }
    }
}
