using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp5
{
    public class Specialization : INotifyPropertyChanged
    {
        public ObservableCollection<Course> Courses { get; set; }
        public string Name { get; set; }
        public double NbrOfAdvancedPoints { get { return _hiddenAPoints; } set { _hiddenAPoints = value; OnPropertyChanged("NbrOfAdvancedPoints"); } }
        public double NbrOfPoints { get {  return _hiddenPoints; } set { _hiddenPoints = value; OnPropertyChanged("NbrOfPoints"); } }
        public bool ValidForGraduation { get; set; }

        private double _hiddenAPoints { get; set; }
        private double _hiddenPoints { get; set; }

        public Specialization(string name)
        {
            Name = name;
            Courses = new ObservableCollection<Course>();
        }

        public event PropertyChangedEventHandler PropertyChanged;


        public void Add(Course course)
        {
            Courses.Add(course);
            if (course.Poängtyp == "A")
                NbrOfAdvancedPoints += Convert.ToDouble(course.Antal_poäng);
            NbrOfPoints += Convert.ToDouble(course.Antal_poäng);
            if (NbrOfAdvancedPoints >= 30 && NbrOfPoints >= 45 && !Name.Contains("Valfri")) 
                ValidForGraduation = true;
        }

        internal void Remove(Course course)
        {
            Courses.Remove(course);
            if (course.Poängtyp == "A")
                NbrOfAdvancedPoints -= Convert.ToDouble(course.Antal_poäng);
            NbrOfPoints -= Convert.ToDouble(course.Antal_poäng);
            if (NbrOfAdvancedPoints < 30 && NbrOfPoints < 45 && !Name.Contains("Valfri"))
                ValidForGraduation = false;
        }

        private void OnPropertyChanged(string parameter)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(parameter));
            }
        }
    }
}
