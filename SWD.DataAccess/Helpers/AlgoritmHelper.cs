using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public static List<string> GetPositiveCity(string algoritmOutput)
        {
            var splitted = algoritmOutput.Replace("(", "").Replace(")", "").Split('^');
            var result = new List<string>();
            foreach (var split in splitted)
            {
                if (!split.Contains('!'))
                {
                    //todo Pobrać nazwy miejscowości - terazjest numer FormulaElementary
                    result.Add(split);
                }
            }
            return result;

        }
    }
}
