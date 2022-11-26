using System;
using System.Collections.Generic;
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

using HCI_ZIS_Library.Model;
using HCI_ZIS_Library.DAO;
using System.ComponentModel;
using System.Collections;
using System.Diagnostics;
using System.CodeDom;

namespace HCI_ZdravstveniInformacioniSistem.user_controls
{
    public partial class NewExamination : UserControl, System.ComponentModel.INotifyPropertyChanged
    {
        private const string srpLang = "SRP";
        private const string engLang = "ENG";
        private const string searchSrp = "Pretraga pacijenata";
        private const string searchEng = "Search patient";
        private const string infoSrp = "Informacije o pregledu";
        private const string infoEng = "Examination info";

        private Queue emptyDiagRows=new Queue();
        private Queue emptyReffRows = new Queue();
        private Queue emptyMedRows = new Queue();

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
                if (examinationHistory != null)
                    examinationHistory.Lang = lang;
                if (lang.Equals(srpLang))
                {
                    TitleContent = "Pregled pacijenta" + (CurrentPatient==null?"":" - "+CurrentPatient.Person.Firstname+" "+CurrentPatient.Person.LastName);
                    DiagnoseLbl = "Diagnoze";
                    RefferalLbl = "Uputnice";
                    TreatmentBtn = "Dodaj terapiju";
                    TreatmentLbl = "Terapija";
                    DoctorSignature = "Dr " + medicalDoctor.Person.Firstname + " " + medicalDoctor.Person.LastName + " - " + medicalDoctor.Specialization;
                    Search = searchSrp;
                    if (SearchBar.Text.Equals(searchEng) || SearchBar.Text.Equals(""))
                        SearchBar.Text = searchSrp;
                    if (ExamContent.Text.Equals(infoEng) || ExamContent.Text.Equals(""))
                        ExamContent.Text = infoSrp;
                    ExamInfo = infoSrp;
                    SaveBtn = "Sačuvaj";
                }
                else
                {
                    TitleContent = "Examination of" + (CurrentPatient == null ? "" : " " + CurrentPatient.Person.Firstname + " " + CurrentPatient.Person.LastName);
                    DiagnoseLbl = "Diagnoses";
                    RefferalLbl = "Refferals";
                    TreatmentBtn = "Add treatment";
                    TreatmentLbl = "Treatment";
                    DoctorSignature = "M.D. " + medicalDoctor.Person.Firstname + " " + medicalDoctor.Person.LastName + " - " + medicalDoctor.Specialization;
                    Search = searchEng;
                    ExamInfo = infoEng;
                    SaveBtn = "Save";
                    if (SearchBar.Text.Equals(searchSrp) || SearchBar.Text.Equals(""))
                        SearchBar.Text = searchEng;
                    if (ExamContent.Text.Equals(infoSrp) || ExamContent.Text.Equals(""))
                        ExamContent.Text = infoEng;
                }
            }

        }

        private List<Patient> patients;
        private List<Patient> searchedPatients;
        private List<Examination> exams;
        private List<Medication> meds;
        private List<HealthService> services;
        private List<Disease> diseases;
        private MedicalDoctor medicalDoctor;

        private bool submited = false;

        private List<Disease> diagnosedDiseases = new List<Disease>();
        private Hashtable prescriptions = new Hashtable();
        private List<HealthService> refferalls = new List<HealthService>();

        private ExaminationDAO examinationDAO=new ExaminationDAO();
        private MedicationDAO medicationDAO = new MedicationDAO();
        private HealthServiceDAO healthServiceDAO = new HealthServiceDAO();
        private DiseaseDAO diseaseDAO = new DiseaseDAO();
        private DiagnosisDAO diagnosisDAO = new DiagnosisDAO();
        private RefferalDAO refferalDAO = new RefferalDAO();
        private PrescriptionDAO prescriptionDAO = new PrescriptionDAO();
        private MedicalDoctorDAO medicalDoctorDAO = new MedicalDoctorDAO();

        private user_controls.ExaminationHistory examinationHistory = null;

        public event PropertyChangedEventHandler PropertyChanged;

        public Patient CurrentPatient { get; set; }
        public string ExamDate { get; set; }

        private string doctorSignature;
        public string DoctorSignature {
            get
            {
                return doctorSignature;
            }
            set 
            {
                doctorSignature = value;// notify
                OnPropertyChanged("DoctorSignature");
            }
        }
        
        private string titleContent;
        public string  TitleContent
        {
            get { return titleContent; }
            set
            {
                titleContent = value;
                OnPropertyChanged("TitleContent");
            }
        }

        private string saveBtn;
        public string SaveBtn
        {
            get { return saveBtn; }
            set
            {
                saveBtn = value;
                OnPropertyChanged("SaveBtn");
            }
        }
        private string search;
        public string Search
        {
            get { return search; }
            set
            {
                search = value;
                OnPropertyChanged("Search");
            }
        }
        private string diagnoseLbl;
        public string DiagnoseLbl
        {
            get { return diagnoseLbl; }
            set
            {
                diagnoseLbl = value;
                OnPropertyChanged("DiagnoseLbl");
            }
        }

        private string treatmentLbl;
        public string TreatmentLbl
        {
            get { return treatmentLbl; }
            set
            {
                treatmentLbl = value;
                OnPropertyChanged("TreatmentLbl");
            }
        }

        private string refferalLbl;
        public string RefferalLbl
        {
            get { return refferalLbl; }
            set
            {
                refferalLbl = value;
                OnPropertyChanged("RefferalLbl");
            }
        }

        private string refferalBtn;
        public string RefferalBtn
        {
            get { return refferalBtn; }
            set
            {
                refferalBtn = value;
                OnPropertyChanged("RefferalBtn");
            }
        }

        private string treatmentBtn;
        public string TreatmentBtn
        {
            get { return treatmentBtn; }
            set
            {
                treatmentBtn = value;
                OnPropertyChanged("TreatmentBtn");
            }
        }
        public string ExamInfo { get; set; }


        public NewExamination(List<Patient> patients, MedicalDoctor medicalDoctor, string lang)
        {
            InitializeComponent();
            this.medicalDoctor = medicalDoctor;
            Lang=lang;
            ExamDate = DateTime.Now.ToString("dd.MM.yyyy");
            this.patients = patients;
            searchedPatients = patients;
            meds = medicationDAO.ReadAllMedications();
            services = healthServiceDAO.ReadAllHealthServices();
            diseases = diseaseDAO.ReadAllDiseases();
            meds.Sort((Medication m1, Medication m2) => { return m1.Name.CompareTo(m2.Name); });
            diseases.Sort((Disease d1, Disease d2) => { return d1.Name.CompareTo(d2.Name); });
            services.Sort((HealthService h1, HealthService h2) => { return h1.Name.CompareTo(h2.Name); });

            foreach (Medication m in meds)
                TreatmentPicker.Items.Add(m);
            foreach (Disease d in diseases)
                DiagnosePicker.Items.Add(d);
            foreach (HealthService s in services)
                ReferalPicker.Items.Add(s);

            DataContext = this;

            RenderContent();
        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (SearchBar.Text.Trim().Equals(Search) == false)
            {
                searchedPatients = patients.FindAll(pat => (pat.Person.Firstname.ToLower().Contains(SearchBar.Text.Trim().ToLower()) || pat.Person.LastName.ToLower().Contains(SearchBar.Text.Trim().ToLower())));
                RenderContent();
            }
        }
        private void RenderContent()
        {
            PatientsGrid.RowDefinitions.Clear();
            PatientsGrid.Children.Clear();
            int counter = 0;
            foreach (HCI_ZIS_Library.Model.Patient p in searchedPatients)
            {
                RowDefinition rd = new RowDefinition();
                rd.Height = new System.Windows.GridLength(40);
                PatientsGrid.RowDefinitions.Add(rd);
                Button patientLabel = new Button();
                patientLabel.MinHeight = 28;
                patientLabel.HorizontalContentAlignment = HorizontalAlignment.Left;
                patientLabel.FontSize = 16;
                patientLabel.HorizontalAlignment = HorizontalAlignment.Left;
                patientLabel.MaxWidth = 400;
                patientLabel.Padding = new System.Windows.Thickness(3);
                patientLabel.MinWidth = 300;
                patientLabel.VerticalAlignment=VerticalAlignment.Center;
                patientLabel.Background = System.Windows.Media.Brushes.White;
                patientLabel.Content =(counter+1)+") "+ p.Person.Firstname + " " + p.Person.LastName + "  -  " + p.HealthCardID;
                patientLabel.Click += (object sender, RoutedEventArgs e) =>
                {
                    CurrentPatient = p;
                    Lang = lang;
                    exams = examinationDAO.ReadAllExaminationsByPatient(p);
                    PatientsGrid.Children.Clear();
                    MainGrid.Children.Remove(PatientsScroll);
                    MainGrid.Children.Remove(SearchBar);
                    examinationHistory = new ExaminationHistory(exams,lang);
                    examinationHistory.Margin=new System.Windows.Thickness(30,10,30,10);
                    Grid.SetRow(examinationHistory, 2);
                    MainGrid.Children.Add(examinationHistory);

                };
                Grid.SetRow(patientLabel, counter++);
                PatientsGrid.Children.Add(patientLabel);
            }
        }
        private void OnPropertyChanged(string info)
        {
            System.ComponentModel.PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new System.ComponentModel.PropertyChangedEventArgs(info));
            }
        }

        private void PatientLabel_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void SearchBar_TxtBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (SearchBar.Text.Equals(searchSrp)  || SearchBar.Text.Equals(searchEng))
            {
                SearchBar.Foreground = System.Windows.Media.Brushes.Black;
                SearchBar.Text = "";
            }
        }

        private void SearchBar_LostFocus(object sender, RoutedEventArgs e)
        {
            if (SearchBar.Text.Trim().Equals(""))
            {
                SearchBar.Text = Search;
                SearchBar.Foreground = (Brush)new BrushConverter().ConvertFrom("#FF646161");
            }
        }

        private void ExamContent_GotFocus(object sender, RoutedEventArgs e)
        {
            if (ExamContent.Text.Trim().Equals(ExamInfo))
            {
                ExamContent.Text = "";
                ExamContent.Foreground = System.Windows.Media.Brushes.Black;
            }
        }

        private void ExamContent_LostFocus(object sender, RoutedEventArgs e)
        {
            if (ExamContent.Text.Trim().Equals("") == true) {
                ExamContent.Text = ExamInfo;
                ExamContent.Foreground = (Brush)new BrushConverter().ConvertFrom("#FF646161");
            }
        }

        private void DiagnosePicker_SelectionChanged(object sender, SelectionChangedEventArgs e)
               
        {
            Disease d = (Disease)DiagnosePicker.SelectedItem;

            if (diagnosedDiseases.Contains(d) == false)
            {
                diagnosedDiseases.Add(d);

                RowDefinition rd1 = new RowDefinition();
                rd1.Height = new System.Windows.GridLength(30);

                RowDefinition rd2 = new RowDefinition();
                rd2.Height = new System.Windows.GridLength(30);

                Label lbl = new Label();
                lbl.Content = d.Name;
                lbl.FontSize = 16;
                Button button = new Button();
                button.Content = "X";
                button.MaxWidth = 25;
                int pos;

                if (emptyDiagRows.Count > 0)
                    pos = (int)emptyDiagRows.Dequeue();
                else
                {
                    pos = diagnosedDiseases.Count - 1;
                    DiagNameGrid.RowDefinitions.Add(rd1);
                    XBtnGrid.RowDefinitions.Add(rd2);
                }

                Grid.SetRow(lbl, pos);
                Grid.SetRow(button, pos);

                button.Click += (object obj, RoutedEventArgs rea) =>
                {
                    diagnosedDiseases.Remove(d);
                    DiagNameGrid.Children.Remove(lbl);
                    XBtnGrid.Children.Remove(button);
                    emptyDiagRows.Enqueue(pos);

                };

                DiagNameGrid.Children.Add(lbl);
                XBtnGrid.Children.Add(button);
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Medication med = (Medication)TreatmentPicker.SelectedItem;
            string manual = Manual.Text;
            string str = med.ToString() + " - " + manual;

            if (prescriptions.ContainsKey(med)==false)
            {
                prescriptions.Add(med, manual);

                RowDefinition rd1 = new RowDefinition();
                rd1.Height = new System.Windows.GridLength(30);

                RowDefinition rd2 = new RowDefinition();
                rd2.Height = new System.Windows.GridLength(30);

                Label lbl = new Label();
                lbl.Content = str;
                lbl.FontSize = 16;
                lbl.MaxHeight = 30;
                Button button = new Button();
                button.Content = "X";
                button.MaxWidth = 25;
                int pos;

                if (emptyMedRows.Count > 0)
                    pos = (int)emptyMedRows.Dequeue();
                else
                {
                    pos = prescriptions.Count - 1;
                    TreatmentNameGrid.RowDefinitions.Add(rd1);
                    TreatmentBtnGrid.RowDefinitions.Add(rd2);
                }

                Grid.SetRow(lbl, pos);
                Grid.SetRow(button, pos);

                button.Click += (object obj, RoutedEventArgs rea) =>
                {
                    prescriptions.Remove(med);
                    TreatmentNameGrid.Children.Remove(lbl);
                    TreatmentBtnGrid.Children.Remove(button);
                    emptyMedRows.Enqueue(pos);

                };

                TreatmentNameGrid.Children.Add(lbl);
                TreatmentBtnGrid.Children.Add(button);
            }
        }

        private void ReferalPicker_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            HealthService healthService = (HealthService)ReferalPicker.SelectedItem;
           
            if (refferalls.Contains(healthService)==false)
            {
                refferalls.Add(healthService);

                RowDefinition rd1 = new RowDefinition();
                rd1.Height = new System.Windows.GridLength(30);

                RowDefinition rd2 = new RowDefinition();
                rd2.Height = new System.Windows.GridLength(30);

                Label lbl = new Label();
                lbl.Content = healthService.Name;
                lbl.FontSize = 16;
                Button button = new Button();
                button.Content = "X";
                button.MaxWidth = 25;
                int pos;

                if (emptyReffRows.Count > 0)

                    pos = (int)emptyReffRows.Dequeue();
                else
                {
                    pos = refferalls.Count - 1;
                    RefferalNameGrid.RowDefinitions.Add(rd1);
                    RefferalBtnGrid.RowDefinitions.Add(rd2);
                }

                Grid.SetRow(lbl, pos);
                Grid.SetRow(button, pos);

                button.Click += (object obj, RoutedEventArgs rea) =>
                {
                    refferalls.Remove(healthService);
                    RefferalNameGrid.Children.Remove(lbl);
                    RefferalBtnGrid.Children.Remove(button);
                    emptyReffRows.Enqueue(pos);

                };

                RefferalNameGrid.Children.Add(lbl);
                RefferalBtnGrid.Children.Add(button);
            }
        }

        private void SubmitBtn_Click(object sender, RoutedEventArgs e)
        {
            if (submited)
                Feedback.Content = Lang.Equals(srpLang) ? "Već se dodali ovaj pregled!":"Examination already saved!";
            else if (CurrentPatient == null)
                Feedback.Content = Lang.Equals(srpLang) ? "Morate odabrati pacijenta!":"Pick a petient!";
            else
            {

                Examination examination = new Examination(-1, ExamContent.Text,medicalDoctor, CurrentPatient,null, null, null);
                int id=examinationDAO.CreateExamination(examination);
                examination.Id = id;
                medicalDoctor.ExaminationNum++;
                foreach (Disease d in diagnosedDiseases)
                    diagnosisDAO.CreateDiagnosis(examination, d);
                foreach(HealthService hs in refferalls)
                {
                    Refferal refferal = new Refferal(-1, id, hs);
                    refferalDAO.CreateRefferal(refferal);
                    medicalDoctor.RefferalNum++;
                }
                foreach(DictionaryEntry entry in prescriptions)
                { 
                    prescriptionDAO.CreatePrescription(examination, (Medication)entry.Key, (string)entry.Value);
                    medicalDoctor.PrescriptionNum++;
                }
                submited = true;
                medicalDoctorDAO.UpdateMedicalDoctor(medicalDoctor);
                Feedback.Content = Lang.Equals(srpLang)?"Pregled uspješno završen!":"Examination completed!";
            }
        }
    }
}
