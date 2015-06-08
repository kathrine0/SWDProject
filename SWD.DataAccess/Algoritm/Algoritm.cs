using System;
using System.Collections.Generic;
using System.Linq;
using SWD.Model;

namespace SWD.DataAccess.Algoritm
{
    public class Algoritm
    {
        public static string Run(Fact[] facts, Fact input)
        {
            var list = new List<string>();
            var repo = new Repository();
            var formuliesExit = repo.GetFormulaElementariesExit();
            for (var i = 0; i < Math.Pow(2, GetFormulaCount()); i++)
            {
                var dictionary = GenerateValues(i);

                var factResult = true;
                foreach (var fact in facts)
                {
                    factResult = factResult && CalculateFact(dictionary, fact);
                }

                var inputResult = CalculateFact(dictionary, input);
                var result = factResult && inputResult;
                if (result)
                {
                    string resultString = "";
                    foreach (var formulaExit in formuliesExit)
                    {
                        if (dictionary[formulaExit.Id])
                            resultString += formulaExit.Id.ToString();
                        else
                            resultString += "!" + formulaExit.Id;
                        resultString += "^";
                    }

                    list.Add("("+resultString.Substring(0, resultString.Length -1)+")");
                }
            }
            string res = "";
            list = list.Distinct().ToList();
            foreach (var item in list)
            {
                res += item + "˅";
            }

            return res.Substring(0, res.Length - 1);
        }

        private static bool CalculateFact(Dictionary<int, bool> formulaElementariesValue, Fact fact )
        {
            return fact.Expression.Calculate(formulaElementariesValue);
        }

        private static int GetFormulaCount()
        {
            return 5;
        }

        private static int GetMaxBit(int number)
        {
            for (var i = 1; i < 255; i++)
            {
                if (Math.Pow(2, i) > number)
                    return i;
            }
            return 1;
        }

        private static Dictionary<int, bool> GenerateValues(int value)
        {
            var dictionary = new Dictionary<int, bool>();

            for (var i = 1; i <= GetFormulaCount(); i++)
            {
                dictionary.Add(i, false);
            }

            while (value != 0)
            {
                var bit = GetMaxBit(value);
                dictionary[bit] = true;
                value = value - (int)Math.Pow(2, bit - 1);
            } 


            return dictionary;
        }
    }
}
