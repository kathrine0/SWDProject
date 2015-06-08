using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using SWD.Model;

namespace SWD.DataAccess.Algoritm
{
    public class Algoritm
    {
        public static string Run(Fact[] facts, Fact input, Dictionary<int, bool> constansFormulaElementary )
        {
            var list = new List<string>();
            var repo = new Repository();
            var formulaElementaries = repo.GetFormulaElementaries().Where(x => !x.Personal).ToArray();
            var formuliesExit = formulaElementaries.Where(x => x.Type == FormulaElementaryType.Exit);
            for (var i = 0; i < Math.Pow(2, formulaElementaries.Length); i++)
            {
                var dictionary = GenerateValues(formulaElementaries, i, constansFormulaElementary);

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
                res += item + "v";
            }

            if (res.Length > 0)
                return res.Substring(0, res.Length - 1);
            return "";
        }

        private static bool CalculateFact(Dictionary<int, bool> formulaElementariesValue, Fact fact )
        {
            return fact.Expression.Calculate(formulaElementariesValue);
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

        private static Dictionary<int, bool> GenerateValues(FormulaElementary[] formulaElementaries, int value, Dictionary<int, bool> constFormulaElementary)
        {
            var dictionary = new Dictionary<int, bool>();

            for (var i = 1; i <= formulaElementaries.Length; i++)
            {
                dictionary.Add(formulaElementaries[i-1].Id, false);
            }

            while (value != 0)
            {
                var bit = GetMaxBit(value);
                dictionary[formulaElementaries[bit-1].Id] = true;
                value = value - (int)Math.Pow(2, bit - 1);
            }

            foreach (var item in constFormulaElementary)
            {
                dictionary.Add(item.Key, item.Value);
            }

            return dictionary;
        }
    }
}
