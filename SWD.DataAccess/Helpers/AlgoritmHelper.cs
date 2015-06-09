using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SWD.DataAccess.ViewModel;

namespace SWD.DataAccess.Helpers
{
    public class AlgoritmHelper
    {
        public static string GetTheBest(string algoritmOutput)
        {
            var result = "";
            var results = algoritmOutput.Split('v');

            int min = 99999;

            foreach (var item in results)
            {
                int count = item.Length - item.Replace("!", "").Length;
                if (count < min)
                {
                    min = count;
                    result= item;
                }
            }
            return result;
        }

        public static List<int> GetPlaces(string algoritmOutput, bool isPositive)
        {
            var splitted = algoritmOutput.Replace("(", "").Replace(")", "").Split('^');
            var result = new List<int>();
            foreach (var split in splitted)
            {
                if ((isPositive && !split.Contains('!')) || (!isPositive && split.Contains('!')))
                {
                    result.Add(Convert.ToInt32(split));
                }
            }
            return result;

        }

        //public Dictionary<int, bool> ParsePersonal(PersonalForm form)
        //{
 
            //var dictionary = new Dictionary<int, bool>();
            //dictionary.Add(1, form.Sex == sex.Mezczyzna);
            //dictionary.Add(6, form.Age > 10);
            //dictionary.Add(7, form.Age > 20);
            //dictionary.Add(8, form.Age > 30);
            //dictionary.Add(9, form.Age > 40);
            //dictionary.Add(10, form.Age > 50);
            //dictionary.Add(11, form.Age > 60);
            //return dictionary;
        //}

        public Dictionary<int, bool> ParseQuestion(QuestionForm form)
        {
            var dictionary = new Dictionary<int, bool>();
            dictionary.Add(2, form.Vaccination);
            dictionary.Add(2, form.Vaccination);
            return dictionary;
        }
    }
}
