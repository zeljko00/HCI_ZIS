using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using HCI_ZIS_Library.DAO;
using HCI_ZIS_Library.Model;

namespace HCI_ZdravstveniInformacioniSistem.user_controls
{
    public partial class LoginForm : UserControl, INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        private string passwordHolder;
        public string PasswordHolder
        {
            get
            {
                return passwordHolder;
            }
            set
            {
                passwordHolder = value;
                OnPropertyChanged("PasswordHolder");
            }
        }
        private string usernameHolder;
        public string UsernameHolder
        {
            get
            {
                return usernameHolder;
            }
            set
            {
                if (Username_txtBox.Text.Equals(UsernameHolder) || Username_txtBox.Text.Equals(""))
                    Username_txtBox.Text = value;
                usernameHolder = value;
            }
        }
        public bool Feedback { get; set; } = false;
        public LoginForm() {

            InitializeComponent();
            DataContext = this;
        }
        public LoginForm(bool b)
        {
            InitializeComponent();
            Feedback = b;
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
        private void Username_TxtBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if(Username_txtBox.Text.Equals(UsernameHolder))
                Username_txtBox.Text = "";
            Username_txtBox.Foreground = System.Windows.Media.Brushes.Black ;
            Username_txtBox.FontStyle= System.Windows.FontStyles.Normal;
            Username_txtBox.Opacity = 1.0;
        }

        private void PasswordPlaceholder_txtBox_GotFocus(object sender, RoutedEventArgs e)
        {
            PasswordPlaceholder_txtBox.Focusable = false;
            PasswordPlaceholder_txtBox.Visibility = System.Windows.Visibility.Collapsed;
            Password_txtBox.Focus();
            Password_txtBox.Opacity = 1.0;
        }

        private void Username_TxtBox_LostFocus(object sender, RoutedEventArgs e)
        {
            Username_txtBox.Opacity = 0.8;
            if (Username_txtBox.Text.Equals("") == true)
            {
                Username_txtBox.Text = UsernameHolder;
                Username_txtBox.Foreground = (Brush) new BrushConverter().ConvertFrom("#FF8A8585");
                Username_txtBox.FontStyle = System.Windows.FontStyles.Italic;
            }

        }

        private void Password_TxtBox_LostFocus(object sender, RoutedEventArgs e)
        {
            Password_txtBox.Opacity = 0.8;
            if (Password_txtBox.Password.Equals("") == true)
            {
                PasswordPlaceholder_txtBox.Focusable = true;
                PasswordPlaceholder_txtBox.Visibility = System.Windows.Visibility.Visible;
                PasswordPlaceholder_txtBox.Opacity = 0.8;
                Password_txtBox.Opacity = 0;
                Error_lbl.Content = "";
                Error_lbl.Foreground = (Brush)new BrushConverter().ConvertFrom("#FFF10404");
            }
        }

        private void Password_txtBox_GotFocus(object sender, RoutedEventArgs e)
        {
            Password_txtBox.Password = "";
        }


    }
}
