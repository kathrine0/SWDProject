using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWD.Model
{
    public class Fact
    {
        public int ID { get; set; }
        public Expression Expression { get; set; }

        public Fact()
        {
        }

        public Fact(Expression expression)
        {
            Expression = expression;
        }

        public Fact(int id, Expression expression)
        {
            ID = id;
            Expression = expression;
        }



    }
}
