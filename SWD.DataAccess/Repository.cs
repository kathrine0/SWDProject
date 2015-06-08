using System.Linq;
using SWD.Model;

namespace SWD.DataAccess
{
    public class Repository
    {
        private static Context db = new Context();

        public FormulaElementary GetFormulaElementaryById(int id)
        {
            return db.FormulaElementaries.FirstOrDefault(x => x.Id == id);
        }

        public FormulaElementary[] GetFormulaElementariesExit()
        {
            return db.FormulaElementaries.Where(q => q.Type == FormulaElementaryType.Exit).ToArray();
        }

        public Fact[] GetFacts()
        {
            return db.Facts.ToArray();
        }
    }
}
