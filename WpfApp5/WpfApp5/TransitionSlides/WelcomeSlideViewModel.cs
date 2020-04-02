using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp5.TransitionSlides
{
    public class WelcomeSlideViewModel
    {
        public string WelcomeMessage { get; set; }
        
        public WelcomeSlideViewModel()
        {
            WelcomeMessage = "Välkommen till Specialiseringsplaneraren, guiden till din examen.\n\nOm det är första gången ni använder applikationen kan ni trycka på knappen nedan för en snabb introduktion.";
        }
    }
}
