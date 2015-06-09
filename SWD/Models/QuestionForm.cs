using SWD.DataAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWD.Models
{
    public class QuestionForm
    {
        private Repository _repository;

        public QuestionForm()
        {
            _repository = new Repository();

            AvailableInterests = _repository.GetAvailableValuesFor("Zainteresowania");
            AvailablePreferences = _repository.GetAvailableValuesFor("Lubi");
        }

        [Display(Name = "Czy jesteś zaszczepiony?")]
        public Boolean Vaccination { get; set; }

        [Display(Name = "Preferencje")]
        public List<string> Preferences { get; set; }
        
        [Display(Name = "Zainteresowania")]
        public List<string> Interests { get; set; }
        
        [Display(Name="Czy lubisz spędzać czas aktywnie?")]
        public bool ActiveHoliday { get; set; }

        public List<string> AvailableInterests { get; private set; }
        public List<string> AvailablePreferences { get; private set; }
    }
}
