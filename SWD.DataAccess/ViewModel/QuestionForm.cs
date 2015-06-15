using SWD.DataAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWD.DataAccess.ViewModel
{
    public class QuestionForm
    {
        private Repository _repository;

        public QuestionForm()
        {
            _repository = new Repository();
            AvailableInterests = _repository.GetAvailableValuesFor("Zainteresowania");
        }

        [Display(Name = "Co chcesz robić?")]
        public List<string> Interests { get; set; }

        [Display(Name = "Czy chcesz spędzać czas aktywnie?")]
        public bool ActiveHoliday { get; set; }

        [Display(Name = "Czy algorytm z dekompozycją?")]
        public bool Decomposition { get; set; }

        public List<string> AvailableInterests { get; private set; }
    }
}
