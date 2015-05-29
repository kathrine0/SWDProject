using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWD.Model
{
    public class FormulaElementary : AbstractExpression
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public FormulaElementary(string name)
        {
            if (name.Contains('!'))
            {
                Negation = true;
            }
            Name = name.Replace("!","");
        }

        public override string ToString(bool symbolic = false)
        {
            string result = Negation ? "!" : ""; 
            return  result + Name;
        }
    }
}
