using HCI_ZIS_Library.Model;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Remoting.Lifetime;
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
    /// Interaction logic for InfoControl.xaml
    /// </summary>
    public partial class InfoControl : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public Data Data;
        public InfoControl(HCI_ZIS_Library.Model.Medication medication,string lang):this(medication,null,null,lang)
        {
            InitializeComponent();
            
            MedName.Content = medication.Name;
            
        }
        public InfoControl(HCI_ZIS_Library.Model.Disease disease,string lang) : this( null,disease, null, lang)
        {
            InitializeComponent();
            MedName.Content = disease.Diagnose;

        }
        public InfoControl(HCI_ZIS_Library.Model.Examination exam,string lang) : this(null, null,exam, lang)
        {
            InitializeComponent();
            MedName.Content = exam.Patient.Person.Firstname + " " + exam.Patient.Person.LastName + "  -  " + exam.Patient.HealthCardID;
            

  
        }
        public InfoControl(Medication medication, Disease disease, Examination examination, string lang) {
            InitializeComponent();
            Data = new Data(medication, disease,examination,lang);
            DataContext = Data;
        }
        private void OnPropertyChanged(string info)
        {
            System.ComponentModel.PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new System.ComponentModel.PropertyChangedEventArgs(info));
            }
        }
            private void Hover(object sender, MouseEventArgs e)
        {
            BorderControl.BorderBrush = System.Windows.Media.Brushes.Green;
        }

        private void HoverEnd(object sender, MouseEventArgs e)
        {
            BorderControl.BorderBrush = (Brush)new BrushConverter().ConvertFrom("#FF5E5858");
        }
    }
    public class Data: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        
        private const string engLang = "ENG";
        private const string srpLang = "SRP";

        private const string typeMed = "Medication";
        private const string typeDis = "Disease";
        private const string typeExam = "Exam";

        private Medication medication;
        private Examination examination;
        private Disease disease;

        private string type;

        private string content2;
        public string Content2
        {
            get
            {
                return content2;
            }
            set
            {
                content2 = value;
                OnPropertyChanged("Content2");
            }
        }
        private string content;
        public string Content
        {
            get
            {
                return content;
            }
            set
            {
                content = value;
                OnPropertyChanged("Content");
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
                Disease d = disease;
                if (disease != null || medication != null || examination != null)
                    switch (type)
                    {
                        case typeDis:
                            Content = (lang.Equals(srpLang) ? "Naziv: " : "Name: ") + disease.Name;
                            Content2 = disease.Treatment;
                            break;
                        case typeMed:
                            Content = (lang.Equals(srpLang) ? "Sastav: " : "Content: ") + medication.Content;
                            Content2 = medication.Description + Environment.NewLine + (medication.OnPrescriptionOnly ? (lang.Equals(srpLang) ? "Izdavanje isključivo na recept!" : "Prescription only!") : (lang.Equals(srpLang) ? "Izdavanje moguće bez ljekarskog recepta!" : "Without prescription!"));
                            break;
                        case typeExam:
                            Content = examination.Date;
                            Content2 = examination.Content + Environment.NewLine + (lang.Equals(srpLang) ? "Dijagnoze: " : "Diagnoses: ") + Environment.NewLine;

                            foreach (Disease dis in examination.Diseases)
                                Content2 += "         " + dis.Diagnose + " - " + dis.Name + Environment.NewLine;

                            Content2 += lang.Equals(srpLang) ? "Terapija: " : "Treatment: " + Environment.NewLine;

                            foreach (Prescription p in examination.Prescriptions)
                                Content2 += "         " + p.Medication.Name + Environment.NewLine;
                            break;
                    }
            }
        }
        public Data(Medication medication,Disease disease, Examination examination, string lang)
        {
            this.medication = medication;
            this.disease = disease;
            this.examination = examination;
            if (disease != null)
                type = typeDis;
            else if (medication != null)
                type = typeMed;
            else
                type = typeExam;
            Lang = lang;
        }
        public Data()
        {

        }
        private void OnPropertyChanged(string info)
        {
            System.ComponentModel.PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new System.ComponentModel.PropertyChangedEventArgs(info));
            }
        }
    }
}
