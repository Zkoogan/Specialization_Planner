using Dapper;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Windows.Input;

namespace WpfApp5
{
    public class StatisticsViewModel : INotifyPropertyChanged
    {

        Dictionary<string, Specialization> SpecDict;

        public event PropertyChangedEventHandler PropertyChanged;


        private double _NbrOfPoints;
        private double _NbrOfAdvancedPoints;

        public double NbrOfPoints { get { return _NbrOfPoints; } set { _NbrOfPoints = value; OnPropertyChanged("NbrOfPoints"); } }
        public double NbrOfAdvancedPoints { get { return _NbrOfAdvancedPoints; }  set { _NbrOfAdvancedPoints = value; OnPropertyChanged("NbrOfAdvancedPoints"); } }
        public ICommand RemoveCommand { get; set; }
        public ObservableCollection<Specialization> specializations { get; set; }
        public ObservableCollection<Course> Courses { get; set; }
        public ObservableCollection<double> Läsperioder { get; set; }
        public ObservableCollection<bool> CsnBerättigad { get; set; }

        public StatisticsViewModel(ObservableCollection<Course> courses)
        {
            
            Messenger.Default.Register<ObservableCollection<Course>>(this, (action) => UpdateCourseList(action));
            SpecDict = new Dictionary<string, Specialization>();
            RemoveCommand = new RelayCommand<Course>(this.ExecuteRemove);
            fetchSpecializations();
            specializations = new ObservableCollection<Specialization>();
            foreach (Specialization v in SpecDict.Values)
                specializations.Add(v);
            this.Courses = courses;
            Läsperioder = new ObservableCollection<double>();
            CsnBerättigad = new ObservableCollection<bool>();
            _setup();
        }



        private void OnPropertyChanged(string parameter)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(parameter));
            }
        }


        private void _setup()
        {
            Läsperioder.Add(0.0);
            Läsperioder.Add(0.0);
            Läsperioder.Add(0.0);
            Läsperioder.Add(0.0);
            CsnBerättigad.Add(false);
            CsnBerättigad.Add(false);
            CsnBerättigad.Add(false);
            CsnBerättigad.Add(false);
        }

        private void fetchSpecializations()
        {
            using (IDbConnection connection = new SQLiteConnection(ConfigurationManager.ConnectionStrings["CourseDB"].ConnectionString))
            {
                String query = "Select distinct Specialisering from Specialiseringar;";
                foreach (string v in connection.Query<string>(query).ToList())
                {
                    SpecDict.Add(v, new Specialization(v));
                }
            }
        }

        private void UpdateCourseList(ObservableCollection<Course> action)
        {


            using (IDbConnection connection = new SQLiteConnection(ConfigurationManager.ConnectionStrings["CourseDB"].ConnectionString))
            {
                if (action.Last().Poängtyp == "A")
                    NbrOfAdvancedPoints += Convert.ToDouble(action.Last().Antal_poäng);
                NbrOfPoints += Convert.ToDouble(action.Last().Antal_poäng);

                string[] separator = { "/" };
                string[] strlist = action.Last().Läsperiod.Split(separator, 3, StringSplitOptions.RemoveEmptyEntries);

                foreach(var v in strlist)
                {
                    Läsperioder[Int16.Parse(v) - 1] += Convert.ToDouble(action.Last().Antal_poäng) / strlist.Count();
                }


                String query = "Select Distinct Specialisering from Specialiseringar Where Kurskod LIKE '%" + action.Last().Kurskod.Trim() + "%';";
                foreach (string v in connection.Query<string>(query).ToList())
                {
                    SpecDict[v].Add(action.Last());
                }
            }
            CsnCalculation();
        }

        private void ExecuteRemove(Course obj)
        {
            Courses.Remove(obj);
            using (IDbConnection connection = new SQLiteConnection(ConfigurationManager.ConnectionStrings["CourseDB"].ConnectionString))
            {
                if (obj.Poängtyp == "A")
                    NbrOfAdvancedPoints -= Convert.ToDouble(obj.Antal_poäng);
                NbrOfPoints -= Convert.ToDouble(obj.Antal_poäng);
                String query = "Select Distinct Specialisering from Specialiseringar Where Kurskod LIKE '%" + obj.Kurskod.Trim() + "%';";
                foreach (string v in connection.Query<string>(query).ToList())
                {
                    SpecDict[v].Remove(obj);
                }
                Messenger.Default.Send<Course>(obj);
            }

            string[] separator = { "/" };
            string[] strlist = obj.Läsperiod.Split(separator, 3, StringSplitOptions.RemoveEmptyEntries);

            foreach (var v in strlist)
            {
                Läsperioder[Int16.Parse(v) - 1] -= Convert.ToDouble(obj.Antal_poäng) / strlist.Count();
            }


            CsnCalculation();
        }

        private void CsnCalculation()
        {
            CsnBerättigad[0] = Läsperioder[0] + Läsperioder[1] >= 30;
            CsnBerättigad[1] = Läsperioder[2] + Läsperioder[3] >= 30;
            CsnBerättigad[2] = Läsperioder[0] + Läsperioder[1] >= 60;
            CsnBerättigad[3] = Läsperioder[2] + Läsperioder[3] >= 60;
        }

    }
}
