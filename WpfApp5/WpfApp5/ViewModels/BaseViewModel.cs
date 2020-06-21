using Dapper;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Linq;

namespace WpfApp5.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string parameter)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(parameter));
            }
        }

        protected List<Course> PerformDatabaseCourseAccess(string query) 
        {
            List<Course> samples; 

            using (IDbConnection connection = new SQLiteConnection(ConfigurationManager.ConnectionStrings["CourseDB"].ConnectionString))
            {
                samples = connection.Query<Course>(query).ToList();
            }
            return samples;
        }

        protected List<string> PerformDatabaseStringAccess(string query) {
            List<string> samples;
            using (IDbConnection connection = new SQLiteConnection(ConfigurationManager.ConnectionStrings["CourseDB"].ConnectionString))
            {
                 samples = connection.Query<string>(query).ToList();
            }
            return samples;
        }
    }
}
