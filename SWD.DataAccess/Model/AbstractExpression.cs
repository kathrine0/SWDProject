
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SWD.Model
{
    public abstract class AbstractExpression
    {
        public int Id { get; set; }

        [NotMapped]
        public bool Negation { get; set; }

        protected AbstractExpression()
        { 
        
        }

        public abstract string ToString(bool symbolic = false);
        public abstract bool Calculate(Dictionary<int, bool> formulaElementariesValue);

    }
}
