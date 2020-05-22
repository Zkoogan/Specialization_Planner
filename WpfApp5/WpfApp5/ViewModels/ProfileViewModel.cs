using Dapper;
using System;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using Microsoft.Win32;
using System.Text.RegularExpressions;
using System.IO;

namespace WpfApp5
{
    class ProfileViewModel
    {
        public ObservableCollection<Course> YearOne { get; set; }
        public ObservableCollection<Course> YearTwo { get; set; }
        public ObservableCollection<Course> YearThree { get; set; }
        public string Name { get; set; }
        public string idNbr { get; set; }
        public Command LoadProfileCommand { get; set; }

        public ProfileViewModel()
        {
            YearOne = new ObservableCollection<Course>();
            YearTwo = new ObservableCollection<Course>();
            YearThree = new ObservableCollection<Course>();
            FetchCourses(YearOne, 1);
            FetchCourses(YearTwo, 2);
            FetchCourses(YearThree, 3);
            Name = "Namn: ";
            idNbr = "Personnummer: ";
            LoadProfileCommand = new Command(ExecuteLoadProfile, CanExecuteLoadProfile);
        }

        private bool CanExecuteLoadProfile(object arg)
        {
            return true;
        }

        private void FetchCourses(ObservableCollection<Course> courses, int index)
        {
            using (IDbConnection connection = new SQLiteConnection(ConfigurationManager.ConnectionStrings["CourseDB"].ConnectionString))
                {
                string query = "SELECT * from base_course_info WHERE Specialisering LIKE '%Årskurs " + index + "%'";

                    foreach (var v in connection.Query<Course>(query).ToList())
                    {
                        courses.Add(v);
                    }
                }
            
        }

        private void ExecuteLoadProfile(object obj)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Pdf files (*.pdf)|*.pdf|All files (*.*)|*.*",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
            };
            openFileDialog.ShowDialog();
            String path = openFileDialog.FileName;
            String fileContent = ReadPdfContent(path);

            MatchCollection c = Regex.Matches(fileContent, @"[A-Z]{3,4}[0-9]{2,3}\s[A-Öa-ö\s]+");

            //Need a new collection so that I can refill basecoures again after it has been altered, this way we force the listview to update
            ObservableCollection<Course> temp = new ObservableCollection<Course>();
            foreach (Course co in YearOne)
            {
                co.Passed = false;
            }
            foreach (Course co in YearTwo)
            {
                co.Passed = false;
            }
            foreach (Course co in YearThree)
            {
                co.Passed = false;
            }

            foreach (Match match in c)
            {
                Match m = Regex.Match(match.Value, @"([A-Z0-9]+)(\s)([\p{L}\s\p{N}\p{P}\,\-]+)");
                foreach (var course in YearOne)
                {
                    String course_code_temp = course.Kurskod.Trim();
                    String course_name_temp = course.Kursnamn.Trim();
                    String match_code_temp = m.Groups[1].Value.Trim();
                    String match_name_temp = m.Groups[3].Value.Trim();
                    if (course_code_temp .Equals(match_code_temp) || course_name_temp.Equals(match_name_temp))
                    {
                        course.Passed = true;
                    }
                }
                foreach (var course in YearTwo)
                {
                    String course_code_temp = course.Kurskod.Trim();
                    String course_name_temp = course.Kursnamn.Trim();
                    String match_code_temp = m.Groups[1].Value.Trim();
                    String match_name_temp = m.Groups[3].Value.Trim();
                    if (course_code_temp.Equals(match_code_temp) || course_name_temp.Equals(match_name_temp))
                    {
                        course.Passed = true;
                    }
                }
                foreach (var course in YearThree)
                {
                    String course_code_temp = course.Kurskod.Trim();
                    String course_name_temp = course.Kursnamn.Trim();
                    String match_code_temp = m.Groups[1].Value.Trim();
                    String match_name_temp = m.Groups[3].Value.Trim();
                    if (course_code_temp.Equals(match_code_temp) || course_name_temp.Equals(match_name_temp))
                    {
                        course.Passed = true;
                    }
                }
            }
        }


        private String ReadPdfContent(String path)
        {
            StringBuilder text = new StringBuilder();
            if (File.Exists(path))
            {
                using (PdfReader reader = new PdfReader(path))
                {

                    for (int i = 1; i <= reader.NumberOfPages; i++)
                    {
                        text.Append(PdfTextExtractor.GetTextFromPage(reader, i));
                    }
                }
            }
            return text.ToString();
        }

    }
}
