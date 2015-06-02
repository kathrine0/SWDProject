﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SWD.Model.Userform
{
    public enum sex { Female, Male }
    
    public class PersonalForm
    {
        [Display(Name = "Podaj swoje imię")]
        public string Name { get; set; }

        [Display(Name = "Podaj swój wiek")]
        public int Age { get; set; }

        [Display(Name = "Obywatelstwa")]
        public string Nationality { get; set; }

        [Display(Name = "Zainteresowania")]
        public string Interests { get; set; }

        [Display(Name="Podaj swoją płeć")]
        public sex Sex { get; set; }    
            
    }
}