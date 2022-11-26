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
   
    public partial class NewHCF : UserControl, INotifyPropertyChanged
    {
        private const string darkTheme = "dark";
        private const string lightTheme = "light";
        private const string defaultTheme = "default";
        private const string srpLang = "SRP";
        private const string engLang = "ENG";

        private HealthCareFacilityDAO healthCareFacilityDAO = new HealthCareFacilityDAO();

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
                    nameContent = "Naziv zdravstvene usluge";
                    descContent = "Adresa";
                    telContent = "Telefon";
                    SaveBtnContent = "Sačuvaj";
                }
                else
                {
                    nameContent = "Name of health service";
                    descContent = "Address";
                    telContent = "Phone";
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
        public NewHCF(string lang)
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
            if ("".Equals(Name.Text.Trim()) || "".Equals(Desc.Text.Trim()) || nameContent.Equals(Name.Text.Trim()) || descContent.Equals(Desc.Text.Trim()) || telContent.Equals(Tel.Text.Trim()) || "".Equals(Tel.Text.Trim()))
                Feedback = srpLang.Equals(Lang) ? "Popunite sva polja!" : "Fill all fields!";
            else
            {
                try
                {
                    HealthCareFacility healthCareFacility = new HealthCareFacility(-1, Name.Text.Trim(), Desc.Text.Trim(), Tel.Text.Trim());
                    healthCareFacilityDAO.CreateHealthCareFacility(healthCareFacility);
                    Feedback = srpLang.Equals(Lang) ? "Sačuvano!" : "Saved!";
                    Name.Text = nameContent;
                    Desc.Text = descContent;
                    Tel.Text = telContent;
                }
                catch (Exception)
                {
                    Feedback = srpLang.Equals(Lang) ? "Ustanova sa tim imenom već postoji!" : "Health care facility with specified name already exists!";
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
    }
}
