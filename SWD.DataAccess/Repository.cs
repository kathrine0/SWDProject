using System.Linq;
using SWD.Model;

namespace SWD.DataAccess
{
    public class Repository
    {
        private Context db = new Context();

        public FormulaElementary GetFormulaElementaryById(int id)
        {
            return db.FormulaElementaries.FirstOrDefault(x => x.Id == id);
        }
    }
}
