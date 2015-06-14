using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography.X509Certificates;
using SWD.DataAccess.Helpers;
using SWD.DataAccess.Model;
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
                return AlgoritmHelper.GetTheBest(res.Substring(0, res.Length - 1));
            return "";
        }

        public static string RunWithDecomposition(Fact[] facts, Fact input, Dictionary<int, bool> constansFormulaElementary)
        {
            var repository = new Repository();
            var decompositionsFact = new Dictionary<int,DecompositionFact>();
            var usedFormulas = new List<FormulaElementary>();
            var allFacts = new List<Fact>(facts);
            allFacts.Add(input);

            var allFormulas = repository.GetFormulaElementaries();
            var formulaExit = repository.GetFormulaElementariesExit();
            var lastformulas = formulaExit;
            usedFormulas.AddRange(lastformulas);

            var finish = false;
            var i = 1;
            while (!finish)
            {
                var decompositionFact = new DecompositionFact(lastformulas, allFacts, usedFormulas);
                lastformulas = decompositionFact.AN;
                usedFormulas.AddRange(lastformulas);
                allFacts.RemoveAll(x => decompositionFact.Facts.Any(q => q.ID == x.ID));
                decompositionsFact.Add(i, decompositionFact);
                if (allFacts.Count == 0 || usedFormulas.Count == allFormulas.Count())
                    finish = true;
                i++;
            }


            var listDictionaries = new List<Dictionary<int, bool>>();
            listDictionaries.Add(new Dictionary<int, bool>());
            for (i = decompositionsFact.Count; i > 0; i--)
            {
                var currentListDicrionaries = listDictionaries;
                listDictionaries = new List<Dictionary<int, bool>>();
                var decompositionFact = decompositionsFact[i];
                var tempList = new List<FormulaElementary>(decompositionFact.AP);
                tempList.AddRange(decompositionFact.AN);

                foreach(var listDictionary in currentListDicrionaries)
                {
                    constansFormulaElementary = listDictionary;

                    for (var j = 0; j < Math.Pow(2, tempList.Count-constansFormulaElementary.Count); j++)
                    {
                        var dictionary = GenerateValues2(tempList.ToArray(), j, constansFormulaElementary);

                        var factResult = true;
                        foreach (var fact in decompositionFact.Facts)
                        {
                            factResult = factResult && CalculateFact(dictionary, fact);
                        }
                        if (factResult)
                        {
                            var dict = new Dictionary<int, bool>();

                            foreach (var ap in decompositionFact.AP)
                            {
                                dict.Add(ap.Id, dictionary[ap.Id]);
                            }
                            if(!listDictionaries.Exists(x=> x.SequenceEqual(dict)))
                                listDictionaries.Add(dict);
                        }
                    }
                }
            }

            if (listDictionaries.Any())
            {
                var results = new string[listDictionaries.Count];
                for (i = 0; i < listDictionaries.Count; i++)
                {
                    results[i] = AlgoritmHelper.ParseDictionaryToString(listDictionaries[i]);
                }

                return AlgoritmHelper.GetTheBest(results);
            }
            else
            {
                return string.Empty;
            }
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

        private static Dictionary<int, bool> GenerateValues2(FormulaElementary[] formulaElementaries, int value, Dictionary<int, bool> constFormulaElementary)
        {
            var onlyFormulaElementaries = formulaElementaries.Where(x => !constFormulaElementary.ContainsKey(x.Id)).ToArray();

            var dictionary = new Dictionary<int, bool>();

            for (var i = 1; i <= onlyFormulaElementaries.Length; i++)
            {
                dictionary.Add(onlyFormulaElementaries[i - 1].Id, false);
            }

            while (value != 0)
            {
                var bit = GetMaxBit(value);
                dictionary[onlyFormulaElementaries[bit - 1].Id] = true;
                value = value - (int)Math.Pow(2, bit - 1);
            }

            foreach (var item in constFormulaElementary)
            {
                dictionary.Add(item.Key, item.Value);
            }

            return dictionary;
        }

        private static Dictionary<int, bool> GenerateValues(FormulaElementary[] formulaElementaries, int value, Dictionary<int, bool> constFormulaElementary)
        {
            var dictionary = new Dictionary<int, bool>();

            for (var i = 1; i <= formulaElementaries.Length;  i++)
            {
                dictionary.Add(formulaElementaries[i - 1].Id, false);
            }

            while (value != 0)
            {
                var bit = GetMaxBit(value);
                dictionary[formulaElementaries[bit - 1].Id] = true;
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
