using System;
using System.Data;
using Dapper;
using System.Configuration;
using System.Linq;
using System.Collections.ObjectModel;

namespace WpfApp5
{
    public class SearchQuery
    {
        String queryBase;
        String _criteria;
        String _input;

        public SearchQuery(String criteria, String input)
        {
            _criteria = criteria;
            _input = input;
            queryBase = "select * from course_info";
        }

        public void execute(ObservableCollection<Course> courses)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings["CourseDB"].ConnectionString))
            {
                String query = queryBase + " where " + _criteria + " like '%" + _input + "%'";

                foreach (var v in connection.Query<Course>(query).ToList())
                {
                    courses.Add(v);
                }
            }
        }
    }
}