using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public Fact(int id, Expression expression)
        {
            ID = id;
            Expression = expression;
        }



    }
}
