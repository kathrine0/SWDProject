using System.Linq;
using SWD.Model;
using System.Collections.Generic;
using SWD.DataAccess.Helpers;
using SWD.DataAccess.ViewModel;
using System;

namespace SWD.DataAccess
{
    public class Repository
    {
        private static Context db = new Context();

        public FormulaElementary GetFormulaElementaryById(int id)
        {
            return db.FormulaElementaries.FirstOrDefault(x => x.Id == id);
        }

        public FormulaElementary[] GetFormulaElementaries()
        {
            return db.FormulaElementaries.ToArray();
        }

        public FormulaElementary[] GetFormulaElementariesExit()
        {
            return db.FormulaElementaries.Where(q => q.Type == FormulaElementaryType.Exit).ToArray();
        }

        public Fact[] GetFacts()
        {
            return db.Facts.ToArray();
        }

        public List<string> GetPersonalEnterFormulasNames()
        {
            return db.FormulaElementaries.Where(q => q.Type == FormulaElementaryType.Enter)
                .Select(q => q.Name)
                .Distinct()
                .ToList();
        }

        public IQueryable<FormulaElementary> GetFormulasByName(string name)
        {
            return db.FormulaElementaries.Where(q => q.Name == name);
        }

        public ResultModel GetResultPlaces(string algoritmOutput)
        {
            return new ResultModel()
            {
                RecommendedPlaces = GetPositivePlaces(algoritmOutput),
                NotRecommendedPlaces = GetNegativePlaces(algoritmOutput)
            };
        }

        public List<string> GetAvailableValuesFor(string name)
        {
            return this.GetFormulasByName(name).Select(q => q.Value).ToList();
        }

        public List<int> GetAgeFormulaId(int age)
        {
            return db.FormulaElementaries.Where(q => q.Name == "Wiek" && Convert.ToInt32(q.Value) > age).Select(w => w.Id).ToList();
        }
        
        public List<int> GetListPositiveId(string name, List<string> values)
        {
            return db.FormulaElementaries.Where(q => q.Name == name && values.Any(w => w == q.Value)).Select(q => q.Id).ToList();
        }

        public List<int> GetListNegativeId(string name, List<string> values)
        {
            return db.FormulaElementaries.Where(q => q.Name == name && values.All(w => w != q.Value)).Select(q => q.Id).ToList();
        }

        //public int GetBoolId(string name, string value)
        //{
        //    var id = db.FormulaElementaries.Where(q => q.Name == name).Select(q => q.Id).ToList();
        //}

        //public int GetBoolNegativeId(string name, string value)
        //{

        //}

        #region private methods

        private List<string> GetPositivePlaces(string algoritmOutput)
        {
            var idList = AlgoritmHelper.GetPlaces(algoritmOutput, true);

            return db.FormulaElementaries.Where(q => q.Name == "Kierunek" && idList.Any(w => w == q.Id))
                .Select(q => q.Value)
                .ToList();
        }

        private List<string> GetNegativePlaces(string algoritmOutput)
        {
            var idList = AlgoritmHelper.GetPlaces(algoritmOutput, false);

            return db.FormulaElementaries.Where(q => q.Name == "Kierunek" && idList.Any(w => w == q.Id))
                .Select(q => q.Value)
                .ToList();
        }

        #endregion
    }
}
