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
    /// <summary>
    /// Interaction logic for ExaminationHistory.xaml
    /// </summary>
    public partial class ExaminationHistory : UserControl, INotifyPropertyChanged
    {
        private List<HCI_ZIS_Library.Model.Examination> exams;

        public event PropertyChangedEventHandler PropertyChanged;

        private List<user_controls.InfoControl> controls = new List<user_controls.InfoControl>();

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
                OnPropertyChanged("");
                foreach (user_controls.InfoControl ic in controls)
                    ic.Data.Lang = lang;
            }

        }
        public ExaminationHistory(List<HCI_ZIS_Library.Model.Examination> exams,string lang)
        {
            this.exams = exams;
            Lang = lang;
            InitializeComponent();
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
            ExaminationsGrid.RowDefinitions.Clear();
            ExaminationsGrid.Children.Clear();
            controls.Clear();
            int counter = 0;
            foreach (HCI_ZIS_Library.Model.Examination d in exams)
            {
                RowDefinition rd = new RowDefinition();
                rd.Height = new System.Windows.GridLength(210);
                ExaminationsGrid.RowDefinitions.Add(rd);
                InfoControl medicationControl = new InfoControl(d,lang);
                controls.Add(medicationControl);
                Grid.SetRow(medicationControl, counter++);
                ExaminationsGrid.Children.Add(medicationControl);
            }
        }
    }
}
