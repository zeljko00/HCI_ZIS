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

using HCI_ZIS_Library.DAO;
using HCI_ZIS_Library.Model;

namespace HCI_ZdravstveniInformacioniSistem.user_controls
{
    public partial class DoctorsUC : UserControl,INotifyPropertyChanged
    {
        private const string darkTheme = "dark";
        private const string lightTheme = "light";
        private const string defaultTheme = "default";
        private const string srpLang = "SRP";
        private const string engLang = "ENG";

        private MedicalDoctorDAO medicalDoctorDAO = new MedicalDoctorDAO();

        public event PropertyChangedEventHandler PropertyChanged;

        private List<MedicalDoctor> medicalDoctors;
        private List<MedicalDoctor> searchedDoctors;

        private string search;

        private string update;
        public string Update
        {
            get
            {
                return update;
            }
            set
            {
                update = value; 
                OnPropertyChanged("Update");
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
                bool changeSearch = false;

                if ("".Equals(SearchBar.Text.Trim()) || search.Equals(SearchBar.Text.Trim()))
                    changeSearch = true;

                lang = value;
                if (srpLang.Equals(lang))
                {
                    Update = "Ažuriraj";
                    search = "Pretraga";
                }
                else
                {
                    Update = "Update";
                    search = "Search";
                }

                if (changeSearch)
                {
                    SearchBar.Text = search;
                    SearchBar.Foreground = System.Windows.Media.Brushes.Gray;
                }
            }
        }
        public DoctorsUC(string lang)
        {
            medicalDoctors = medicalDoctorDAO.ReadAllMedicalDoctors();
            searchedDoctors = medicalDoctors;

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
            DoctorsGrid.Children.Clear();
            DoctorsGrid.RowDefinitions.Clear();
            int counter = 0;
            foreach(MedicalDoctor md in searchedDoctors)
            {
                RowDefinition rd = new RowDefinition();
                rd.Height = new System.Windows.GridLength(30);
                 
                DoctorsGrid.RowDefinitions.Add(rd);

                Border border = new Border();
                Grid.SetColumnSpan(border, 2);
                Grid.SetColumn(border, 0);
                Grid.SetRow(border, counter);
                border.HorizontalAlignment = HorizontalAlignment.Stretch;
                border.VerticalAlignment = VerticalAlignment.Stretch;
                if (counter % 2 == 0)
                    border.Background = (Brush)new BrushConverter().ConvertFrom("#FFB5AFAF");
                else
                    border.Background = (Brush)new BrushConverter().ConvertFrom("#FF767272");
                border.Margin=new System.Windows.Thickness(1,1,1,1);
                DoctorsGrid.Children.Add(border);

                Label lbl = new Label();
                lbl.Margin = new System.Windows.Thickness(2,2,2,2);
                Grid.SetRow(lbl, counter);
                Grid.SetColumn(lbl, 0);
                lbl.Content = md.Person.Firstname + " " + md.Person.LastName + " | " + md.Specialization + " | " + md.Licence+" |  "+(md.Active?"+":"-");

                Button xBtn = new Button();
                xBtn.Margin = new System.Windows.Thickness(2, 2, 2, 2);
                xBtn.Content = "+/-";
                Grid.SetColumn(xBtn, 1);
                Grid.SetRow(xBtn, counter);
                xBtn.Click += (object sender, RoutedEventArgs e) =>
                {
                    if(md.Active)
                        lbl.Content=lbl.Content.ToString().Replace("+","-");
                    else
                        lbl.Content =lbl.Content.ToString().Replace("-", "+");
                    md.Active = !md.Active;
                    medicalDoctorDAO.UpdateMedicalDoctor(md);
                };

                //Button upBtn = new Button();
                //upBtn.Margin = new System.Windows.Thickness(2, 2, 2, 2);
                //Grid.SetColumn(upBtn, 1);
                //Grid.SetRow(upBtn, counter);
                //Binding binding2 = new Binding("Update");
                //binding2.Source = this;
                //upBtn.SetBinding(ContentProperty, binding2);
                //upBtn.Click += (object sender, RoutedEventArgs e) =>
                //{
                //    //update ljekara
                //};
                //DoctorsGrid.Children.Add(upBtn);

                DoctorsGrid.Children.Add(lbl);
                DoctorsGrid.Children.Add(xBtn);

                counter++;
            }
        }

        private void SearchBar_GotFocus(object sender, RoutedEventArgs e)
        {
            if (SearchBar.Text.Trim().Equals(search))
            {
                SearchBar.Text = "";
                SearchBar.Foreground = System.Windows.Media.Brushes.Black;
            }
        }

        private void SearchBar_LostFocus(object sender, RoutedEventArgs e)
        {
            if (SearchBar.Text.Trim().Equals(""))
            {
                SearchBar.Text = search;
                SearchBar.Foreground = System.Windows.Media.Brushes.Gray;
            }
        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (SearchBar.Text.Trim().Equals(search) == false)
            {

                searchedDoctors = medicalDoctors.FindAll((MedicalDoctor m) =>
                {
                    return m.Person.Firstname.ToLower().Contains(SearchBar.Text.Trim().ToLower()) || m.Person.LastName.ToLower().Contains(SearchBar.Text.ToLower().Trim());
                });
                RenderContent();
            }
        }
    }
}
