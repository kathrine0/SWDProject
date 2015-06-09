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

        public Dictionary<int, bool> ParsePersonal(PersonalForm form)
        {
            Repository repo = new Repository();
            
            var dictionary = new Dictionary<int, bool>();
            
            
            dictionary.Add(repo.GetBoolId("Płeć"), form.Sex == sex.mężczyzna);

            foreach (var id in repo.GetAgePositiveId(form.Age))
            {
                dictionary.Add(id, true);
            }

            foreach (var id in repo.GetAgeNegativeId(form.Age))
            {
                dictionary.Add(id, false);
            }

            return dictionary;
        }

        public string ParseDictionaryToString(Dictionary<int, bool> dictionary)
        {
            string result = "";
            foreach (var keyValue in dictionary)
            {;
                result += keyValue.Value ? keyValue.Key.ToString() : "!" + keyValue.Key;
                result += "^";
            }
            return result.Substring(0, result.Length -1);
        }

        public string ParseQuestion(QuestionForm form)
        {
            var dictionary = new Dictionary<int, bool>();
            var repo = new Repository();
            dictionary.Add(repo.GetBoolId("Szczepienia"), form.Vaccination);
            dictionary.Add(repo.GetBoolId("Forma wypoczynku"), form.ActiveHoliday);
            
            var positiveInterests = repo.GetListPositiveId("Zainteresowania", form.Interests);
            var negativeInterests = repo.GetListNegativeId("Zainteresowania", form.Interests);
            var positivePreferences = repo.GetListPositiveId("Lubi", form.Preferences);
            var negativePreferences = repo.GetListNegativeId("Lubi", form.Preferences);

            foreach (var positiveInterest in positiveInterests)
            {
                dictionary.Add(positiveInterest, true);
            }

            foreach (var negativeInterest in negativeInterests)
            {
                dictionary.Add(negativeInterest, false);
            }

            foreach (var positivePreference in positivePreferences)
            {
                dictionary.Add(positivePreference, true);
            }

            foreach (var negativePreference in negativePreferences)
            {
                dictionary.Add(negativePreference, false);
            }
            return ParseDictionaryToString(dictionary);
        }
    }
}
