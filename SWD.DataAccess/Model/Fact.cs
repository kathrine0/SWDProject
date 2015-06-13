using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SWD.DataAccess;
using SWD.DataAccess.Helpers;

namespace SWD.Model
{
    public class Fact
    {
        public int ID { get; set; }

        [NotMapped]
        public Expression Expression { get; set; }

        public string ExpressionString {
            get { return Expression.ToString(true); }
            set { Expression = Model.Expression.Parse(value); }
        }

        public Fact()
        {
        }

        public Fact(Expression expression)
        {
            Expression = expression;
        }

        public Fact(string expression)
        {
            ExpressionString = expression;
        }

        public Fact(int id, string expression)
        {
            ID = id;
            ExpressionString = expression;
        }

        public Fact(int id, Expression expression)
        {
            ID = id;
            Expression = expression;
        }

        public List<FormulaElementary> ContainsFormula(List<FormulaElementary> formula, List<FormulaElementary> usedFormulas)
        {
            var repo = new Repository();
            var text = StringHelper.RemoveBrackets(ExpressionString);
            text = StringHelper.RemoveNegations(text);
            text = StringHelper.RemoveSpaces(text);
            var splitted = text.Split(new char[] {'^', 'v', '>'});

            if (!splitted.Any(split => formula.Any(x => x.Id == Int32.Parse(split))))
            {
                return null;
            }

            var resultFormulas = repo.GetFormulaElementariesByStringList(splitted);

            return resultFormulas.Where(x => usedFormulas.All(q => q.Id != x.Id)).ToList();
        }


    }
}
