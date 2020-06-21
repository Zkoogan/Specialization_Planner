using Dapper;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using WpfApp5.ViewModels;

namespace WpfApp5
{
    public class SearchViewModel : BaseViewModel
    {
        public ICommand SearchCommand { get; set; }
        public ObservableCollection<Text_Criteria> text_criteria { get; set; }
        public ObservableCollection<List_Criteria> list_criteria { get; set; }
        public ObservableCollection<IntervalCriteria> Interval_criteria { get; set; }
        public ObservableCollection<Course> CourseList { get; set; }
        public ICommand SearchButtonCommand { get; set; }
        public ICommand ChooseStudyPeriodCommand { get; set; }
        private Course hidden;
        public ObservableCollection<Course> Chosen { get; set; }
        public ICommand AddButtonCommand { get; set; }
        public SnackbarMessageQueue MessageQueue { get; set; }
        public ObservableCollection<string> StudyPeriods { get; set; }
        public bool OpenDialog { get { return _OpenDialog; } set { _OpenDialog = value; OnPropertyChanged("OpenDialog"); } }
        public bool _OpenDialog { get; set; }

        public Course _selected
        {
            get
            {
                return hidden;
            }
            set
            {
                hidden = value;
                OnPropertyChanged("_selected");
            }
        }

        public SearchViewModel(ObservableCollection<Course> courses)
        {
            text_criteria = new ObservableCollection<Text_Criteria>();
            list_criteria = new ObservableCollection<List_Criteria>();
            Interval_criteria = new ObservableCollection<IntervalCriteria>();
            FetchCriteria();
            SearchCommand = new Command(ExecuteSearch, CanExecuteSearch);
            CourseList = new ObservableCollection<Course>();
            Chosen = courses;
            SearchButtonCommand = new Command(ExecuteSearch, CanExecuteSearch);
            _FetchSpecCourse();
            AddButtonCommand = new Command(ExecuteAdd, CanExecuteAdd);
            MessageQueue = new SnackbarMessageQueue();
            StudyPeriods = new ObservableCollection<string>();
            ChooseStudyPeriodCommand = new RelayCommand<string>(this.ChooseStudyPeriod);
        }

        private void ChooseStudyPeriod(string obj)
        {
            Chosen.Add(_selected.returnNewCourse(obj));
            Messenger.Default.Send<ObservableCollection<Course>>(Chosen);
            OpenDialog = false;
            MessageQueue.Enqueue(_selected.ToString() + " tillagd i specialiseringen");
            StudyPeriods.Clear();
        }

        private void _FetchSpecCourse()
        {
            using (IDbConnection connection = new SQLiteConnection(ConfigurationManager.ConnectionStrings["CourseDB"].ConnectionString))
            {
                String query = "SELECT * FROM spec_course_info;";
                foreach (var v in connection.Query<Course>(query, new DynamicParameters()).ToList())
                {
                    CourseList.Add(v);
                }
            }
        }

        private void FetchCriteria()
        {
            using (IDbConnection connection = new SQLiteConnection(ConfigurationManager.ConnectionStrings["CourseDB"].ConnectionString))
            {
                String query = "SELECT * FROM search_criteria";

                foreach (var v in connection.Query(query).ToList())
                {
                    if (v.type == 0)
                        text_criteria.Add(new Text_Criteria(v.name));
                    else if (v.type == 1)
                    {
                        List_Criteria temp = new List_Criteria(v.name);
                        String list_query = "SELECT value FROM search_criteria_options WHERE name == '" + v.name + "'";
                        foreach (var t in connection.Query(list_query).ToList())
                        {
                            temp.Values.Add(t.value);
                        }
                        list_criteria.Add(temp);
                    }
                    else
                        Interval_criteria.Add(new IntervalCriteria(v.name));
                }
            }
        }

        private void ExecuteSearch(object parameter)
        {
            String QueryBaseAddition = "";
            using (IDbConnection connection = new SQLiteConnection(ConfigurationManager.ConnectionStrings["CourseDB"].ConnectionString))
            {

                String dynamic_query_part = "";

                foreach (var v in text_criteria)
                {
                    if (v.isChecked)
                    {
                        if (dynamic_query_part == "")
                        {
                            dynamic_query_part = dynamic_query_part + v.representation + " LIKE '%" + v.input + "%'";
                        }
                        else
                        {
                            dynamic_query_part = dynamic_query_part + " AND " + v.representation + " LIKE '%" + v.input + "%'";
                        }
                    }
                }

                foreach (var v in list_criteria)
                {
                    if (v.isChecked)
                    {
                        bool used = false;
                        if(v.representation == "Läsperiod" && v.Selected != "") {
                            QueryBaseAddition += "JOIN Läsperioder USING (Kurskod) ";
                            used = true;
                        }
                        if (v.representation == "Specialisering" && v.Selected != "")
                        {
                            QueryBaseAddition += "JOIN Specialiseringar USING (Kurskod) ";
                            used = true;
                        }
                        if (dynamic_query_part == "" && used)
                        {
                            dynamic_query_part = dynamic_query_part + v.representation + " LIKE '%" + v.Selected + "%'";
                        }
                        else if (used || v.representation == "Poängtyp")
                        {
                            dynamic_query_part = dynamic_query_part + " AND " + v.representation + " LIKE '%" + v.Selected + "%'";
                        }
                    }
                }

                foreach (var v in Interval_criteria)
                {
                    if (v.isChecked)
                    {
                        String lower = v.LowerBounds.ToString().Replace(',', '.');
                        String upper = v.UpperBounds.ToString().Replace(',', '.');

                        if (dynamic_query_part == "")
                        {
                            dynamic_query_part = dynamic_query_part + v.representation + " >= " + lower + " AND " + v.representation + " <= " + upper;
                        }
                        else
                        {
                            dynamic_query_part = dynamic_query_part + " AND " + v.representation + " >= " + lower + " AND " + v.representation + " <= " + upper;
                        }
                    }
                }
                CourseList.Clear();
                String query = "SELECT DISTINCT Kurskod, Antal_poäng, Poängtyp, Kursnamn, Beskrivning,  Representant_email FROM spec_course_info " + QueryBaseAddition + " WHERE " + dynamic_query_part + ";";
                if (dynamic_query_part != "")
                {
                    foreach (var v in connection.Query<Course>(query).ToList())
                     {
                        CourseList.Add(v);
                    }
                }
            }

        }
        private Boolean CanExecuteSearch(object parameter)
        {
            return true;
        }

        private bool CanExecuteAdd(object obj)
        {
            return true;
        }

        private void ExecuteAdd(object obj)
        {
            if (_selected != null)
            {
                if (!Chosen.Contains(_selected))
                {
                    using (IDbConnection connection = new SQLiteConnection(ConfigurationManager.ConnectionStrings["CourseDB"].ConnectionString))
                    {
                        String query = "Select distinct Läsperiod from Läsperioder Where Kurskod LIKE '%" + _selected.Kurskod.Trim() + "%';";
                        foreach (string v in connection.Query<string>(query).ToList())
                        {
                            StudyPeriods.Add(v);
                        }
                    }

                    if(StudyPeriods.Count == 1) {
                        Chosen.Add(_selected.returnNewCourse(StudyPeriods.First()));
                        Messenger.Default.Send<ObservableCollection<Course>>(Chosen);
                        MessageQueue.Enqueue(_selected.ToString() + " tillagd i specialiseringen");
                        StudyPeriods.Clear();
                    }
                    else
                    {
                        OpenDialog = true;
                    }
                }
                else
                {
                    MessageBox.Show("The course has already been chosen for your future specialization");
                }
            }
        }
    }
}
