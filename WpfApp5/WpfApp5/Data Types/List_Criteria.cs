using System;
using System.Collections.ObjectModel;

namespace WpfApp5
{

    public class List_Criteria
    {

        public Boolean isChecked { get; set; }
        public String representation { get; set; }
        public String Selected { get; set; }
        public ObservableCollection<String> Values { get; set; }
        public String search_representation { get; set; }

        public List_Criteria(String name)
        {
            this.representation = name;
            this.search_representation = representation + ":";
            this.isChecked = true;
            this.Values = new ObservableCollection<string>();
            Selected = "";
            Values.Add("");
        }

        public override string ToString()
        {
            return representation;
        }
    }
}
