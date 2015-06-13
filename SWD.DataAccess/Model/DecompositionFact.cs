using System.Collections.Generic;
using System.Linq;
using SWD.Model;

namespace SWD.DataAccess.Model
{
    public class DecompositionFact
    {
        public List<FormulaElementary> AP { get; set; }
        public List<Fact> Facts { get; set; }
        public List<FormulaElementary> AN { get; set; }

        public DecompositionFact(List<FormulaElementary> ap, List<Fact> facts, List<FormulaElementary> usedFormulas )
        {
            var repo = new Repository();
            AP = ap;
            Facts = new List<Fact>();
            AN = new List<FormulaElementary>();
            foreach (var fact in facts)
            {
                var resultFormulaElementary = fact.ContainsFormula(ap, usedFormulas);
                if (resultFormulaElementary != null)
                {
                    Facts.Add(fact);
                    AN.AddRange(resultFormulaElementary);
                }
            }
            AN = AN.GroupBy(x => x.Id).Select(x => x.FirstOrDefault()).ToList();
        }

    }
}
