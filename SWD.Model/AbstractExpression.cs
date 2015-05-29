
namespace SWD.Model
{
    public abstract class AbstractExpression
    {

        public bool Negation { get; set; }

        protected AbstractExpression()
        { 
        
        }

        public abstract string ToString(bool symbolic = false);
        
    }
}
