using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWD.Model
{
    public class FormulaElementary : AbstractExpression
    {
        public string Name { get; set; }

        public FormulaElementary(string name)
        {
            Name = name;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
