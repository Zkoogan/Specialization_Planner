using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace WpfApp5
{
    public class Course : INotifyPropertyChanged
    {
        public string Kursnamn { get; set; }
        public string Antal_poäng { get; set; }
        public string Poängtyp { get; set; }
        public string Kurskod { get; set; }
        public string Beskrivning { get; set; }
        public string Representant_email { get; set; }
        public string Läsperiod { get; set; }
        public string Specialisering { get; set; }
        private bool _Passed;
        public bool Passed { get { return _Passed; } set { _Passed = value; OnPropertyChanged("Passed"); } }

        public Course returnNewCourse(String s)
        {
            Course v = new Course();
            v.Kurskod = this.Kurskod;
            v.Antal_poäng = this.Antal_poäng;
            v.Poängtyp = this.Poängtyp;
            v.Kursnamn = this.Kursnamn;
            v.Beskrivning = this.Beskrivning;
            v.Representant_email = this.Representant_email;
            v.Läsperiod = s;
            v.Specialisering = this.Specialisering;
            return v;
        }

        public string FullInfo
        {
            get{
                return $"{Kurskod} {Antal_poäng} {Poängtyp} {Kursnamn}";
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public override string ToString()
        {
            return Kurskod + " " + Antal_poäng +  " " + Poängtyp + " " + Kursnamn;
        }

        public override bool Equals(object obj)
        {
            
            var v = obj as Course;
            if(v != null)
                return v.ToString().Equals(this.ToString());
            return false;
        }
        private void OnPropertyChanged(string parameter)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(parameter));
            }
        }

        public override int GetHashCode()
        {
            var hashCode = -745740247;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Kursnamn);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Antal_poäng);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Poängtyp);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Kurskod);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Beskrivning);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Representant_email);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Läsperiod);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Specialisering);
            hashCode = hashCode * -1521134295 + _Passed.GetHashCode();
            hashCode = hashCode * -1521134295 + Passed.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(FullInfo);
            return hashCode;
        }
    }
}