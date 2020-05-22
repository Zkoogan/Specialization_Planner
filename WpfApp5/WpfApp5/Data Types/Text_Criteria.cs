using System;

namespace WpfApp5
{

    public class Text_Criteria
    {

        public Boolean isChecked { get; set; }
        public String representation { get; set; }
        public String input { get; set; }

        public Text_Criteria(String name)
        {
            this.representation = name;
            this.input = "";
            this.isChecked = true;
        }

        public override string ToString()
        {
            return representation;
        }
    }
}


