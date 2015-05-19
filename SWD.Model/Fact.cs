using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWD.Model
{
    public class Fact
    {
        public Expression Expression { get; set; }

        public Fact(Expression expression)
        {
            Expression = expression;
        }

        public void Test()
        { 

        }

    }
}
