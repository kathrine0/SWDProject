using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWD.Model
{
    public class Expression : AbstractExpression
    {
        public AbstractExpression LeftExpression { get; set; }
        public AbstractExpression RightExpression { get; set; }
        public Operations Operation { get; set; }

        protected Expression()
        {
            
        }

        public Expression(AbstractExpression expression)
        {
            LeftExpression = expression;
        }

        public Expression(AbstractExpression leftExpression, Operations operation, AbstractExpression rightExpression)
        {
            LeftExpression = leftExpression;
            Operation = operation;
            RightExpression = rightExpression;
        }

        public Expression AddRight(Operations operation, AbstractExpression addExpression)
        {
            Expression newExpression = new Expression();
            newExpression.LeftExpression = this;
            newExpression.Operation = operation;
            newExpression.RightExpression = addExpression;
            return newExpression;
        }

        public Expression AddLeft(Operations operation, AbstractExpression addExpression)
        {
            Expression newExpression = new Expression();
            newExpression.LeftExpression = addExpression;
            newExpression.Operation = operation;
            newExpression.RightExpression = this;
            return newExpression;
        }

        public override string ToString()
        {
            string result = string.Empty;
            if (Operation == Operations.Implication)
                result += "Jeśli ";
            result += "("+LeftExpression.ToString();

            if (RightExpression == null)
                return result;
            switch (Operation) {
                case Operations.And: result += " oraz "; break;
                case Operations.Or: result += " lub "; break;
                case Operations.Implication: result += " to "; break;
            }
            if (RightExpression != null)
                result += RightExpression.ToString() + ")";

            return result;
        }
    }
}
