using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SWD.DataAccess.Model;
using SWD.DataAccess.ViewModel;
using SWD.Model;

namespace SWD.DataAccess.Helpers
{
    public class AlgoritmHelper
    {
        Repository repo = new Repository();

        public static string GetTheBest(string algoritmOutput)
        {
            var results = algoritmOutput.Split('v');

            return GetTheBest(results);
        }

        public static string GetTheBest(string[] algoritmOutput)
        {
            int min = 99999;
            var result = "";
            foreach (var item in algoritmOutput)
            {
                int count = item.Length - item.Replace("!", "").Length;
                if (count < min)
                {
                    min = count;
                    result = item;
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
                if (!string.IsNullOrEmpty(split) && (isPositive && !split.Contains('!')) || (!isPositive && split.Contains('!')))
                {
                    var split2 = split.Replace("!", "");
                    result.Add(Convert.ToInt32(split2));
                }
            }
            return result;
        }


        /// <summary>
        /// THE MOST IMPORTANT FUNCTION
        /// </summary>
        public ResultModel GetResult(PersonalForm form1, QuestionForm form2)
        {
            var res1 = ParsePersonal(form1);
            var res2 = ParseQuestion(form2);

            var algoritmOutput = Algoritm.Algoritm.RunWithDecomposition(repo.GetFacts(), new Fact(res2), res1);
            
            return new ResultModel()
            {
                RecommendedPlaces = repo.GetPositivePlaces(algoritmOutput),
                NotRecommendedPlaces = repo.GetNegativePlaces(algoritmOutput)
            };
        }

        public List<string> CountriesForTrip(FormulaElementary exitFormulaElementary)
        {
            var repo = new Repository();
            var input = new List<FormulaElementary> {exitFormulaElementary};
            var decompositeFact = new DecompositionFact(input, repo.GetFacts().ToList(), new List<FormulaElementary>());
            return decompositeFact.AN.Select(x => x.Value).ToList();
        }

        private Dictionary<int, bool> ParsePersonal(PersonalForm form)
        {
           

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


        public static string ParseDictionaryToString(Dictionary<int, bool> dictionary)
        {
            string result = "";
            foreach (var keyValue in dictionary)
            {
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
