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
    }
}
