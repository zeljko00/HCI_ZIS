using HCI_ZIS_Library.DAO;
using HCI_ZIS_Library.Model;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Security.RightsManagement;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;


namespace HCI_ZdravstveniInformacioniSistem
{

    public partial class DoctorScreen : Window, INotifyPropertyChanged
    {
        private const string darkTheme = "dark";
        private const string lightTheme = "light";
        private const string defaultTheme = "default";
        private const string srpLang = "SRP";
        private const string engLang = "ENG";

        private string info;
        public string Info {
            get
            {
                return info;
            }
            set
            {
                info = value;
                OnPropertyChanged("Info");
            }
        }

        private string licenceInfo;
        public string LicenceInfo { 
            get
            {
                return licenceInfo;
            }
            set
            {
                licenceInfo = value;
                OnPropertyChanged("LicenceInfo");
            }
        }
        private Brush appForeground;
        public Brush AppForeground
        {
            get
            {
                return appForeground;
            }
            set
            {
                appForeground = value;
                OnPropertyChanged("AppForeground");
            }
        }

        private string label2;
        public string Label2
        {
            get
            {
                return label2;
            }
            set
            {
                label2 = value;
                OnPropertyChanged("Label2");
            }
        }
        private string label3;
        public string Label3
        {
            get
            {
                return label3;
            }
            set
            {
                label3 = value;
                OnPropertyChanged("Label3");
            }
        }
        private string label1;
        public string Label1
        {
            get
            {
                return label1;
            }
            set
            {
                label1 = value;
                OnPropertyChanged("Label1");
            }
        }

        private string menuExam;
        public string MenuExam
        {
            get
            {
                return menuExam;
            }
            set
            {
                menuExam = value;
                OnPropertyChanged("MenuExam");
            }
        }

        private string menuExams;
        public string MenuExams
        {
            get
            {
                return menuExams;
            }
            set
            {
                menuExams = value;
                OnPropertyChanged("MenuExams");
            }
        }

        private string menuNewExam;
        public string MenuNewExam
        {
            get
            {
                return menuNewExam;
            }
            set
            {
                menuNewExam = value;
                OnPropertyChanged("MenuNewExam");
            }
        }
        private string menuMed;
        public string MenuMed
        {
            get
            {
                return menuMed;
            }
            set
            {
                menuMed = value;
                OnPropertyChanged("MenuMed");
            }
        }
        private string menuDiag;
        public string MenuDiag
        {
            get
            {
                return menuDiag;
            }
            set
            {
                menuDiag = value;
                OnPropertyChanged("MenuDiag");
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
        private string password;
        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                password = value;
                OnPropertyChanged("Password");
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

        public MedicalDoctor Doctor { get; set; }
        public HealthCareFacility hcf { get; set; }

        private HealthCareFacilityDAO healthCareFacilityDAO = new HealthCareFacilityDAO();
        private MedicalDoctorDAO medicalDoctorDAO = new MedicalDoctorDAO();
        private MedicationDAO medicationDAO = new MedicationDAO();
        private DiseaseDAO diseaseDAO = new DiseaseDAO();
        private ExaminationDAO examinationDAO = new ExaminationDAO();
        private PatientDAO patientDAO = new PatientDAO();

        private List<Medication> medications;
        private List<Disease> diseases;
        private List<Examination> examinations;
        private List<Patient> patients;

        private user_controls.Dieases diseasesControl = null;
        private user_controls.Medications medicationsControl=null;
        private user_controls.ExaminationHistory examinationHistory = null;
        private user_controls.NewExamination newExamination = null;

        public event PropertyChangedEventHandler PropertyChanged;

        public DoctorScreen(MedicalDoctor doctor)
        {

            InitializeComponent();

            Doctor = doctor;

            hcf = healthCareFacilityDAO.ReadHealthCareFacility(Doctor.HealthCareFacility);

           
            ChangeLangugage(Doctor.Language);
            ChangeTheme(Doctor.Theme);

            this.DataContext = this;

            medications = medicationDAO.ReadAllMedications();
            medications.Sort((Medication m1, Medication m2) =>
            {
                return m1.Name.CompareTo(m2.Name);
            });
            diseases = diseaseDAO.ReadAllDiseases();
            diseases.Sort((Disease d1, Disease d2) => {
                return d1.Name.CompareTo(d2.Name);
            });
            
            patients = patientDAO.ReadAllPatients();
            patients.Sort((Patient p1, Patient p2) =>
            {
                return p1.Person.LastName.CompareTo(p2.Person.LastName);
            });
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

        private void AutoSave_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            medicalDoctorDAO.UpdateMedicalDoctor(Doctor);
        }

        private void ChangePassword_Click(object sender, RoutedEventArgs e)
        {
            Window changePasswordWindow = new PasswordChange(Doctor,true);
            changePasswordWindow.Height = 220;
            changePasswordWindow.Width = 300;
            changePasswordWindow.ShowDialog();
        }

        private void Medications_Click(object sender, RoutedEventArgs e)
        {
            ClearContent();

            if (medicationsControl == null)
            {
                medicationsControl = new user_controls.Medications(medications,Doctor.Language);
                Grid.SetRow(medicationsControl, 1);
            }
            RightPart.Children.Add(medicationsControl);
        }

        private void Diagnosis_Click(object sender, RoutedEventArgs e)
        {
            ClearContent();

            if (diseasesControl == null)
            {
                diseasesControl = new user_controls.Dieases(diseases,Doctor.Language);
                Grid.SetRow(diseasesControl, 1);
            }
            RightPart.Children.Add(diseasesControl);
        }

        private void Exams_Click(object sender, RoutedEventArgs e)
        {
            ClearContent();

            examinations = examinationDAO.ReadAllExaminationsByDoctor(Doctor);
            examinations.Sort((Examination e1, Examination e2) =>
            {
                DateTime dt1 = DateTime.ParseExact(e1.Date, "dd.MM.yyyy HH:mm",
                                  CultureInfo.InvariantCulture);
                DateTime dt2 = DateTime.ParseExact(e2.Date, "dd.MM.yyyy HH:mm",
                                 CultureInfo.InvariantCulture);
                return dt2.CompareTo(dt1);
            });
            examinationHistory = new user_controls.ExaminationHistory(examinations,Doctor.Language);
                Grid.SetRow(examinationHistory, 1);

            RightPart.Children.Add(examinationHistory);
        }

        private void NewExam_Click(object sender, RoutedEventArgs e)
        {
            ClearContent();

            newExamination = new user_controls.NewExamination(patients,Doctor,Doctor.Language);
            Grid.SetRow(newExamination, 1);
            RightPart.Children.Add(newExamination);
        }
        private void ClearContent()
        {
            if (medicationsControl != null)
                RightPart.Children.Remove(medicationsControl);
            if (diseasesControl != null)
                RightPart.Children.Remove(diseasesControl);
            if (examinationHistory != null)
                RightPart.Children.Remove(examinationHistory);
            if (newExamination != null)
                RightPart.Children.Remove(newExamination);
        }
        private void ChangeLangugage(string lang)
        {
            Doctor.Language = lang;
            if(medicationsControl != null)
                medicationsControl.Lang = lang;
            if (diseasesControl != null)
                diseasesControl.Lang = lang;
            if (newExamination != null)
                newExamination.Lang = lang;
            if (examinationHistory != null)
                examinationHistory.Lang = lang;

            switch (lang)
            {
                case srpLang:
                    Label1 = "Broj obavljenih pregleda: ";
                    Label2 = "Broj izdatih recepata: ";
                    Label3 = "Broj izdatih uputnica: ";

                    LicenceInfo = "Broj licence: " + Doctor.Licence;
                    Info = "Dr " + Doctor.Person.Firstname + " " + Doctor.Person.LastName;

                    MenuExam = "Pregledi";
                    MenuExams = "Istorija pregleda";
                    MenuNewExam = "Novi pregled";

                    MenuDiag = "Diagnoze";
                    MenuMed = "Lijekovi";
                    MenuSett = "Podešavanja";
                    Lang = "Jezik";
                    Lang1 = "Srpski jezik";
                    Lang2 = "Engleski jezik";
                    Theme = "Tema";
                    ThemeDef = "Podrazumijevano";
                    ThemeD = "Tamno";
                    ThemeL = "Svijetlo";
                    Password = "Izmjena lozinke";
                    Logout = "Odjava";
                    SRP.IsChecked = true;
                    ENG.IsChecked = false;
                    break;
                default:
                    Label1 = "Performed examinations: ";
                    Label2 = "Prescriptions number: ";
                    Label3 = "Refferals number: ";

                    LicenceInfo = "Licence ID: " + Doctor.Licence;
                    Info = "M.D " + Doctor.Person.Firstname + " " + Doctor.Person.LastName;

                    MenuExam = "Examinations";
                    MenuExams = "History";
                    MenuNewExam = "New examination";

                    MenuDiag = "Diagnoses";
                    MenuMed = "Medications";
                    MenuSett = "Settings";
                    Lang = "Language";
                    Lang1 = "Serbian";
                    Lang2 = "English";
                    Theme = "Theme";
                    ThemeDef = "Default";
                    ThemeD = "Dark";
                    ThemeL = "Light";
                    Logout = "Logout";


                    SRP.IsChecked =false;
                    ENG.IsChecked = true ;
                    break;
            }
        }
        private void ChangeTheme(string theme)
        {
            Doctor.Theme = theme;

            string icon = "";

            switch (theme)
            {
                case defaultTheme:
                    DoctorWin.Background = (Brush)new BrushConverter().ConvertFrom("#FF79B8AF");
                    DoctorWin.FontFamily = new System.Windows.Media.FontFamily("Arial");
                    Menu.Foreground = (Brush)new BrushConverter().ConvertFrom("#FF212121");
                    DoctorWin.FontSize = 15;
                    DocInfo.FontSize = 17;
                    AppForeground = System.Windows.Media.Brushes.Black;

                    Default.IsChecked = true;
                    Dark.IsChecked = false;
                    Light.IsChecked = false;

                    
                    if (Doctor.Person.Firstname.EndsWith("a"))
                        icon = "dr-female.png";
                    else
                        icon = "dr-male.png";
                   

                    break;
                case lightTheme:
                    DoctorWin.Background = (Brush)new BrushConverter().ConvertFrom("#FFB1C0BE");
                    DoctorWin.FontFamily = new System.Windows.Media.FontFamily("Calibri");
                    Menu.Foreground = (Brush)new BrushConverter().ConvertFrom("#FF212121");
                    DoctorWin.FontSize = 17;
                    DocInfo.FontSize = 19;
                    AppForeground = System.Windows.Media.Brushes.Black;

                    Default.IsChecked = false;
                    Dark.IsChecked = false;
                    Light.IsChecked = true;

                    if (Doctor.Person.Firstname.EndsWith("a"))
                        icon = "dr-female-black.png";
                    else
                        icon = "dr-male-black.png";
                    break;
                default:
                    DoctorWin.Background = (Brush)new BrushConverter().ConvertFrom("#1B2430");
                    DoctorWin.FontFamily = new System.Windows.Media.FontFamily("MonoLisa");
                    Menu.Foreground = System.Windows.Media.Brushes.LightGray;
                    DoctorWin.FontSize = 13;
                    DocInfo.FontSize = 15;
                    AppForeground = System.Windows.Media.Brushes.LightGray;

                    Default.IsChecked = false;
                    Dark.IsChecked = true;
                    Light.IsChecked = false;

                    if (Doctor.Person.Firstname.EndsWith("a"))
                        icon = "dr-female-light.png";
                    else
                        icon = "dr-male-light.png";
                    break;
            }
            AccountImg.Source = new ImageSourceConverter().ConvertFrom(new Uri("pack://application:,,,/HCI-ZdravstveniInformacioniSistem;component/resources/" + icon)) as ImageSource;

        }

        private void Logout_OnClick(object sender, RoutedEventArgs e)
        {
            AutoSave_Closing(null,null);
            DoctorWin.Close();
        }
    }
}
