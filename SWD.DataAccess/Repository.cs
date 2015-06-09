using System.Linq;
using SWD.Model;
using System.Collections.Generic;
using SWD.DataAccess.Helpers;

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

        public List<string> GetPositivePlaces(string algoritmOutput)
        {
            var idList = AlgoritmHelper.GetPlaces(algoritmOutput, true);

            return db.FormulaElementaries.Where(q => q.Name == "Kierunek" && idList.Any(w => w == q.Id))
                .Select(q => q.Value)
                .ToList();
        }

        public List<string> GetNegativePlaces(string algoritmOutput)
        {
            var idList = AlgoritmHelper.GetPlaces(algoritmOutput, false);

            return db.FormulaElementaries.Where(q => q.Name == "Kierunek" && idList.Any(w => w == q.Id))
                .Select(q => q.Value)
                .ToList();
        }

        public List<string> GetAvailableValuesFor(string name)
        {
            return this.GetFormulasByName(name).Select(q => q.Value).ToList();
        }
    }
}
