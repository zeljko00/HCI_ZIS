using HCI_ZIS_Library.DAO;
using HCI_ZIS_Library.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace HCI_ZdravstveniInformacioniSistem.user_controls
{
    
    public partial class NewPatientUC : UserControl, INotifyPropertyChanged
    {
        private const string darkTheme = "dark";
        private const string lightTheme = "light";
        private const string defaultTheme = "default";
        private const string srpLang = "SRP";
        private const string engLang = "ENG";

        private PatientDAO patientDAO=new PatientDAO();

        public event PropertyChangedEventHandler PropertyChanged;

        private string saveBtnContent;
        public string SaveBtnContent
        {
            get
            {
                return saveBtnContent;
            }
            set
            {
                saveBtnContent = value;
                OnPropertyChanged("SaveBtnContent");
            }
        }

        private string feedback;
        public string Feedback
        {
            get
            {
                return feedback;
            }
            set
            {
                feedback = value;
                OnPropertyChanged("Feedback");
            }
        }

        private string hideLblContent;
        public string HideLblContent
        {
            get
            {
                return hideLblContent;
            }
            set
            {
                hideLblContent = value;
                OnPropertyChanged("HideLblContent");
            }
        }


        private string nameContent;
        private string descContent;
        private string telContent;


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

                bool changeNameContent = false;
                bool changeDescContent = false;
                bool changeTelContent = false;

                if ("".Equals(Name.Text.Trim()) || nameContent.Equals(Name.Text.Trim()))
                    changeNameContent = true;
                if ("".Equals(Desc.Text.Trim()) || descContent.Equals(Desc.Text.Trim()))
                    changeDescContent = true;
                if ("".Equals(Tel.Text.Trim()) || telContent.Equals(Tel.Text.Trim()))
                    changeTelContent = true;

                if (srpLang.Equals(Lang))
                {
                    nameContent = "Ime";
                    descContent = "Prezime";
                    telContent = "JMB";
                    HideLblContent = "Izaberi datum";
                    SaveBtnContent = "Sačuvaj";
                }
                else
                {
                    nameContent = "First name";
                    descContent = "Last name";
                    telContent = "ID";
                    HideLblContent = "Pick a date";
                    SaveBtnContent = "Save";
                }

                if (changeNameContent)
                {
                    Name.Text = nameContent;
                    Name.Foreground = System.Windows.Media.Brushes.Gray;
                }
                if (changeDescContent)
                {
                    Desc.Text = descContent;
                    Desc.Foreground = System.Windows.Media.Brushes.Gray;
                }
                if (changeTelContent)
                {
                    Tel.Text = telContent;
                    Tel.Foreground = System.Windows.Media.Brushes.Gray;
                }

            }
        }
        public NewPatientUC(string lang)
        {
            InitializeComponent();
            DataContext = this;
            Lang = lang;
        }
        private void OnPropertyChanged(string info)
        {
            System.ComponentModel.PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new System.ComponentModel.PropertyChangedEventArgs(info));
            }

        }
        private void SaveBtn_OnClick(object sender, RoutedEventArgs e)
        {
            if ("".Equals(Name.Text.Trim()) || "".Equals(Desc.Text.Trim()) || nameContent.Equals(Name.Text.Trim()) || descContent.Equals(Desc.Text.Trim()) || telContent.Equals(Tel.Text.Trim()) || "".Equals(Tel.Text.Trim()) || DatePicker.SelectedDate.HasValue==false)
                Feedback = srpLang.Equals(Lang) ? "Popunite sva polja!" : "Fill all fields!";
            else if(Tel.Text.Trim().Length!=13)
                Feedback = srpLang.Equals(Lang) ? "JMB mora imati 13 karaktera!" : "ID must be 13 characters long!";
            else
            {
                try
                {
                    Person person = new Person(-1, Name.Text.Trim(), Desc.Text.Trim(), Tel.Text.Trim(), Tel.Text.Trim(), DatePicker.SelectedDate.Value.ToString("dd.MM.yyyy"), DatePicker.SelectedDate.Value.ToString("dd.MM.yyyy"));
                    Patient patient = new Patient(person,"MR#"+Tel.Text.Trim(), "-");
                    patientDAO.CreatePatient(patient);
                    Feedback = srpLang.Equals(Lang) ? "Sačuvano!" : "Saved!";
                    Name.Text = nameContent;
                    Desc.Text = descContent;
                    Tel.Text = telContent;
                    HideLbl.Opacity = 1.0;
                }
                catch (Exception)
                {
                    Feedback = srpLang.Equals(Lang) ? "Pacijent sa tim JMB-om već postoji!" : "Patient with specified ID already exists!";
                }
            }
        }

        private void Name_LostFocus(object sender, RoutedEventArgs e)
        {
            if (Name.Text.Trim().Equals(""))
            {
                Name.Text = nameContent;
                Name.Foreground = System.Windows.Media.Brushes.Gray;
            }
        }

        private void Name_GotFocus(object sender, RoutedEventArgs e)
        {
            if (Name.Text.Trim().Equals(nameContent))
            {
                Name.Text = "";
                Name.Foreground = System.Windows.Media.Brushes.Black;
            }
        }

        private void Desc_LostFocus(object sender, RoutedEventArgs e)
        {
            if (Desc.Text.Trim().Equals(""))
            {
                Desc.Text = descContent;
                Desc.Foreground = System.Windows.Media.Brushes.Gray;
            }
        }

        private void Desc_GotFocus(object sender, RoutedEventArgs e)
        {
            if (Desc.Text.Trim().Equals(descContent))
            {
                Desc.Text = "";
                Desc.Foreground = System.Windows.Media.Brushes.Black;
            }
        }
        private void Tel_LostFocus(object sender, RoutedEventArgs e)
        {
            if (Tel.Text.Trim().Equals(""))
            {
                Tel.Text = telContent;
                Tel.Foreground = System.Windows.Media.Brushes.Gray;
            }
        }

        private void Tel_GotFocus(object sender, RoutedEventArgs e)
        {
            if (Tel.Text.Trim().Equals(telContent))
            {
                Tel.Text = "";
                Tel.Foreground = System.Windows.Media.Brushes.Black;
            }
        }

        private void DatePicker_OnSelect(object sender, SelectionChangedEventArgs e)
        {
            HideLbl.Opacity = 0.0;
        }
    }
}
