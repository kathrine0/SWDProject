using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using SWD.Model.Helpers;

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

        public Expression(string text, Dictionary<int, Expression> dictionary)
        {
            //TODO
            // zajmijmy się przykładem 5^e1^4
            // musimy otrzymać pojedyńcze obiekt wyrażenia 
            //e1 jest brane jako gotowy expression ze slownika ei - i numer ze słownika
            //Obecnie stworzy obiekt 5^e1

            char[] chars = {'˅', '˄', '>'};
            string[] array = text.Split(chars);

            AbstractExpression[] abstractExpressions = ParseHelper.GetExpressions(array, dictionary);
            Operations[] operationArray = ParseHelper.GetOperations(text, chars);


            //Expression expression = new Expression(abstractExpressions[0], operationArray[0], abstractExpressions[1]);

            LeftExpression = abstractExpressions[0];
            Operation = operationArray[0];
            RightExpression = abstractExpressions[1];

            //Teraz trzeba dodać po prawej następne wyrazenia, ale nie możemy użyć AddRight (przynajmniej tak napisanego) bo jesteśmy w konstruktorze 


            for (int i = 1; i < operationArray.Length; i++)
            {
                this.AddRight(operationArray[i], abstractExpressions[i+1]);
            }

        }

        public void AddRight(Operations operation, AbstractExpression addExpression)
        {
            Expression newExpression = new Expression();
            newExpression.LeftExpression = this.LeftExpression;
            newExpression.Operation = this.Operation;
            newExpression.RightExpression = this.RightExpression;
            this.LeftExpression = newExpression;
            this.Operation = operation;
            this.RightExpression = addExpression; 
        }

        public void AddLeft(Operations operation, AbstractExpression addExpression)
        {
            Expression newExpression = new Expression();
            newExpression.LeftExpression = this.LeftExpression;
            newExpression.Operation = this.Operation;
            newExpression.RightExpression = this.RightExpression;
            this.LeftExpression = addExpression;
            this.Operation = operation;
            this.RightExpression = newExpression;
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

        public static Expression Parse(string text)
        {
            //var expression = new Dictionary<int, string>();
            var expression = new Dictionary<int, Expression>();

            var leftCount = text.Length - text.Replace("(", "").Length;
            var rightCount = text.Length - text.Replace(")", "").Length;

            if(leftCount != rightCount)
                throw new Exception("Różna ilość nawiasów");

            var startPosition = 0;
            var j = 0;
            for (var i = 0; i < text.Length; i++)
            {
                if (text[i] == '(')
                {
                    startPosition = i;
                }
                if (text[i] == ')')
                {
                    var exp = text.Substring(startPosition+1, i - startPosition - 1);
                    expression.Add(j, new Expression(exp, expression));
                    text = text.Replace("("+ exp + ")", "e" + j);
                    i = -1;
                    j++;
                }
            }
            Expression result =  new Expression(text, expression);

            return result;
        }
    }
}
