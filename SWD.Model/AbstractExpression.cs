
namespace SWD.Model
{
    public abstract class AbstractExpression
    {
        public int Id { get; set; }
        public bool Negation { get; set; }

        protected AbstractExpression()
        { 
        
        }

        public abstract string ToString(bool symbolic = false);
        
    }
}
