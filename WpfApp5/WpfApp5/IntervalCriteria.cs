using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp5
{
    public class IntervalCriteria
    {

        public Boolean isChecked { get; set; }
        public String representation { get; set; }
        public double LowerBounds { get; set; }
        public double UpperBounds { get; set; }

        public IntervalCriteria(String name)
        {
            this.representation = name;
            this.isChecked = true;
            LowerBounds = 0;
            UpperBounds = 15;
        }

        public override string ToString()
        {
            return representation;
        }
    }
}
