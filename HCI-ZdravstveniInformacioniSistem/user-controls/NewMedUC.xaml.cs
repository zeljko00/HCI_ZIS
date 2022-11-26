using HCI_ZIS_Library.DAO;
using HCI_ZIS_Library.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
namespace HCI_ZdravstveniInformacioniSistem.user_controls
{
    /// <summary>
    /// Interaction logic for NewMedUC.xaml
    /// </summary>
    public partial class NewMedUC : UserControl, INotifyPropertyChanged
    {

        private const string darkTheme = "dark";
        private const string lightTheme = "light";
        private const string defaultTheme = "default";
        private const string srpLang = "SRP";
        private const string engLang = "ENG";

        public event PropertyChangedEventHandler PropertyChanged;

        private MedicationDAO medicationDAO = new MedicationDAO();

        private string langugage;
        public string Lang
        {
            get
            {
                return langugage;
            }
            set
            {
                langugage = value;

                bool changeName = false;
                bool changeContent = false;
                bool changePrice = false;
                bool changeManual = false;
                bool changePurpose = false;

                if (MedNameTxt.Text.Trim().Equals(MedName) || MedNameTxt.Text.Trim().Equals(""))
                    changeName = true;
                if (MedContentTxt.Text.Trim().Equals(MedContent) || MedContentTxt.Text.Trim().Equals(""))
                    changeContent = true;
                if (MedManualTxt.Text.Trim().Equals(MedManual) || MedManualTxt.Text.Trim().Equals(""))
                    changeManual = true;
                if (MedPriceTxt.Text.Trim().Equals(MedPrice) || MedPriceTxt.Text.Trim().Equals(""))
                    changePrice = true;
                if (MedPurposeTxt.Text.Trim().Equals(MedPurpose) || MedPurposeTxt.Text.Trim().Equals(""))
                    changePurpose = true;

                if (srpLang.Equals(value))
                {
                   
                    MedName = "Naziv lijeka";
                    MedContent = "Sastav lijeka";
                    MedManual = "Uputstvo za korištenje";
                    MedPurpose = "Namjena";
                    MedPresc = "Isključivo na recept";
                    MedNoPresc = "Slobodna prodaja";
                    MedPrice = "Cijena";
                    MedAdd = "Sačuvaj";

                }
                else
                {
                    MedName = "Medication name";
                    MedContent = "Medication composition";
                    MedManual = "Manual";
                    MedPurpose = "Purpose";
                    MedPresc = "Prescription only";
                    MedNoPresc = "No prescription needed";
                    MedPrice = "Price";
                    MedAdd = "Save";
                }

                if (changeName)
                {
                    MedNameTxt.Text = MedName;
                    MedNameTxt.Foreground = System.Windows.Media.Brushes.Gray;
                }
                if (changeContent) 
                { 
                    MedContentTxt.Text = MedContent;
                    MedContentTxt.Foreground = System.Windows.Media.Brushes.Gray;
                }
                if (changeManual) { 

                    MedManualTxt.Text = MedManual;
                    MedManualTxt.Foreground = System.Windows.Media.Brushes.Gray;
                }
                if (changePurpose) 
                { 
                    MedPurposeTxt.Text = MedPurpose;
                    MedPurposeTxt.Foreground = System.Windows.Media.Brushes.Gray;
                }
                if (changePrice)
                { 
                    MedPriceTxt.Text = MedPrice;
                    MedPriceTxt.Foreground = System.Windows.Media.Brushes.Gray;
                }
            }
        }
        private string medLbl;
        public string MedLbl
        {
            get
            {
                return medLbl;
            }
            set
            {
                medLbl = value;
                OnPropertyChanged("MedLbl");
            }
        }
        public string MedName { get; set; }
   
        public string MedContent { get; set; }
      
        public string MedManual { get; set; }
       
        public string MedPurpose { get; set; }
       
        private string medPresc;
        public string MedPresc
        {
            get
            {
                return medPresc;
            }
            set
            {
                medPresc = value;
                OnPropertyChanged("MedPresc");
            }
        }
        private string medNoPresc;
        public string MedNoPresc
        {
            get
            {
                return medNoPresc;
            }
            set
            {
                medNoPresc = value;
                OnPropertyChanged("MedNoPresc");
            }
        }
        public string MedPrice { get; set; }

        private string medAdd;
        public string MedAdd
        {
            get
            {
                return medAdd;
            }
            set
            {
                medAdd = value;
                OnPropertyChanged("MedAdd");
            }
        }
        public NewMedUC(string lang)
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
        private void MedName_LostFocus(object sender, RoutedEventArgs e)
        {
            if (MedNameTxt.Text.Trim().Equals(""))
            {
                MedNameTxt.Text = MedName;
                MedNameTxt.Foreground = System.Windows.Media.Brushes.Gray;
            }
        }

        private void MedName_GotFocus(object sender, RoutedEventArgs e)
        {
            if (MedNameTxt.Text.Trim().Equals(MedName))
            {
                MedNameTxt.Text = "";
                MedNameTxt.Foreground = System.Windows.Media.Brushes.Black;
            }
        }

        private void MedContent_LostFocus(object sender, RoutedEventArgs e)
        {
            if (MedContentTxt.Text.Trim().Equals(""))
            {
                MedContentTxt.Text = MedContent;
                MedContentTxt.Foreground = System.Windows.Media.Brushes.Gray;
            }
        }

        private void MedContent_GotFocus(object sender, RoutedEventArgs e)
        {
            if (MedContentTxt.Text.Trim().Equals(MedContent))
            {
                MedContentTxt.Text = "";
                MedContentTxt.Foreground = System.Windows.Media.Brushes.Black;
            }
        }

        private void MedManual_GotFocus(object sender, RoutedEventArgs e)
        {
            if (MedManualTxt.Text.Trim().Equals(MedManual))
            {
                MedManualTxt.Text = "";
                MedManualTxt.Foreground = System.Windows.Media.Brushes.Black;
            }
        }

        private void MedManual_LostFocus(object sender, RoutedEventArgs e)
        {
            if (MedManualTxt.Text.Trim().Equals(""))
            {
                MedManualTxt.Text = MedManual;
                MedManualTxt.Foreground = System.Windows.Media.Brushes.Gray;
            }
        }

        private void MedPurpose_GotFocus(object sender, RoutedEventArgs e)
        {
            if (MedPurposeTxt.Text.Trim().Equals(MedPurpose))
            {
                MedPurposeTxt.Text = "";
                MedPurposeTxt.Foreground = System.Windows.Media.Brushes.Black;
            }
        }

        private void MedPurpose_LostFocus(object sender, RoutedEventArgs e)
        {
            if (MedPurposeTxt.Text.Trim().Equals(""))
            {
                MedPurposeTxt.Text = MedPurpose;
                MedPurposeTxt.Foreground = System.Windows.Media.Brushes.Gray;
            }
        }

        private void MedPrice_GotFocus(object sender, RoutedEventArgs e)
        {
            if (MedPriceTxt.Text.Trim().Equals(MedPrice))
            {
                MedPriceTxt.Text = "";
                MedPriceTxt.Foreground = System.Windows.Media.Brushes.Black;
            }
        }

        private void MedPrice_LostFocus(object sender, RoutedEventArgs e)
        {
            if (MedPriceTxt.Text.Trim().Equals(""))
            {
                MedPriceTxt.Text = MedPrice;
                MedPriceTxt.Foreground = System.Windows.Media.Brushes.Gray;
            }
        }

        private void MedAdd_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                double price = Double.Parse(MedPriceTxt.Text);
                if(MedNameTxt.Text.Trim().Equals(MedName) || MedContentTxt.Text.Trim().Equals(MedContent) || MedManualTxt.Text.Trim().Equals(MedManual) || MedPurposeTxt.Text.Trim().Equals(MedPurpose) || MedNameTxt.Text.Trim().Equals("") || MedContentTxt.Text.Trim().Equals("") || MedManualTxt.Text.Trim().Equals("") || MedPurposeTxt.Text.Trim().Equals(""))
                    MedLabel.Content = Lang.Equals(srpLang) ? "Niste popunili sva polja!" : "You didn't fill all fields!";
                else
                {
                    Medication med = new Medication(-1, MedNameTxt.Text, MedPurposeTxt.Text, MedContentTxt.Text, MedPrescRadio.IsChecked == true, price, MedManualTxt.Text);
                    int id = medicationDAO.CreateMedication(med);
                        MedLabel.Content = Lang.Equals(srpLang) ? "Sačuvano!" : "Saved!";
                        MedNameTxt.Text = MedName;
                        MedContentTxt.Text =MedContent;
                        MedPriceTxt.Text = MedPrice;
                        MedManualTxt.Text =MedManual;
                        MedPurposeTxt.Text = MedPurpose;
                }
            }
            catch (MySqlException)
            {
                MedLabel.Content = Lang.Equals(srpLang) ? "Lijek sa tim nazivom već postoji!" : "Medication with specified name already exists!";
            }
            catch (Exception){
                MedLabel.Content = Lang.Equals(srpLang) ? "Cijena lijeka mora biti broj!" : "Medication price must be a number!";
            }
        }

    }

}
