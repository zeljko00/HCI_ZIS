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
    public partial class Dieases : UserControl, INotifyPropertyChanged
    {
        private List<HCI_ZIS_Library.Model.Disease> diseases;
        private List<HCI_ZIS_Library.Model.Disease> searchedDiseases;
        public event PropertyChangedEventHandler PropertyChanged;

        private List<user_controls.InfoControl> controls = new List<user_controls.InfoControl>();

        private const string engLang = "ENG";
        private const string srpLang = "SRP";

        private string search;
        public string Search
        {
            get
            {
                return search;
            }
            set
            {
                search = value;
                OnPropertyChanged("Search");
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
                Search = lang.Equals(srpLang) ? "Pretraga" : "Search";
                foreach (user_controls.InfoControl ic in controls)
                    ic.Data.Lang = lang;
            }
        }
        public Dieases(List<HCI_ZIS_Library.Model.Disease> diseases,string lang)
        {
            this.diseases = diseases;
            searchedDiseases = diseases;
            InitializeComponent();

            DataContext = this;
            Lang = lang;
            RenderContent();
        }

        private void OnPropertyChanged(string info)
        {
            System.ComponentModel.PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new System.ComponentModel.PropertyChangedEventArgs(info));
            }
        }
            private void RenderContent()
        {
            DiseasesGrid.RowDefinitions.Clear();
            DiseasesGrid.Children.Clear();
            controls.Clear();
            int counter = 0;
            foreach (HCI_ZIS_Library.Model.Disease d in searchedDiseases)
            {
                RowDefinition rd = new RowDefinition();
                rd.Height = new System.Windows.GridLength(150);
                DiseasesGrid.RowDefinitions.Add(rd);
                InfoControl medicationControl = new InfoControl(d,lang);
                controls.Add(medicationControl);
                Grid.SetRow(medicationControl, counter++);
                DiseasesGrid.Children.Add(medicationControl);
            }
        }

        private void SearchBox_TxtBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (SearchBar.Text.Equals(Search))
            {
                SearchBar.Foreground = System.Windows.Media.Brushes.Black;
                SearchBar.Text = "";
            }
        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (SearchBar.Text.Trim().Equals(Search) == false)
            {
                searchedDiseases = diseases.FindAll(dis => (dis.Name.ToLower().Contains(SearchBar.Text.Trim().ToLower()) || dis.Diagnose.ToLower().Contains(SearchBar.Text.Trim().ToLower())));
                RenderContent();
            }
            //kada tekst za pretragu bude "" ponovo ce prikazati sve lijekove
        }

        private void SearchBar_LostFocus(object sender, RoutedEventArgs e)
        {
            if (SearchBar.Text.Trim().Equals(""))
            {
                SearchBar.Text = Search;
                SearchBar.Foreground = (Brush)new BrushConverter().ConvertFrom("#FF646161");

            }
        }
    }
}
