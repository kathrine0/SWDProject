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
        public AbstractExpression LeftExpression { get; set;}
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

        public Expression(string text, Dictionary<int, Expression> dictionary, bool negative = false)
        {
            this.Negation = negative;
            char[] chars = {'˅', '˄', '>'};
            string[] array = text.Split(chars);

            AbstractExpression[] abstractExpressions = ParseHelper.GetExpressions(array, dictionary);
            Operations[] operationArray = ParseHelper.GetOperations(text, chars);

            if (operationArray.Any())
            {
                LeftExpression = abstractExpressions[0];
                Operation = operationArray[0];
                RightExpression = abstractExpressions[1];

                for (int i = 1; i < operationArray.Length; i++)
                {
                    this.AddRight(operationArray[i], abstractExpressions[i + 1]);
                }
            }
            else
            {
                if (abstractExpressions[0] is Expression)
                {
                    var exp = (Expression) abstractExpressions[0];

                    LeftExpression = exp.LeftExpression;
                    Operation = exp.Operation;
                    RightExpression = exp.RightExpression;
                    Negation = exp.Negation;
                }
                else
                {
                    var exp = (FormulaElementary)abstractExpressions[0];
                    LeftExpression = exp;
                }
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

        public override string ToString(bool symbolic = false)
        {
            string result = string.Empty;
            string[] pisemnie = {" oraz ", " lub ", " to "};
            string[] symbolicznie = {" ˄ ", " ˅ ", " > "};
            string[] currentSymbols = symbolic ? symbolicznie : pisemnie;


            if(Negation)
                result += "!";

            result += "(";
            if (Operation == Operations.Implication && !symbolic)
                result += "Jeśli ";

            result += LeftExpression.ToString(symbolic);


            if (RightExpression == null)
            {
                result += ")";
                return result;
            }
            switch (Operation) {
                case Operations.And: result += currentSymbols[0]; break;
                case Operations.Or: result += currentSymbols[1]; break;
                case Operations.Implication: result += currentSymbols[2]; break;
            }
            if (RightExpression != null)
                result += RightExpression.ToString(symbolic) + ")";
 
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
                var negative = false;
                if (text[i] == '(')
                {
                    startPosition = i;
                }
                if (text[i] == ')')
                {
                    if (startPosition > 0 && text[startPosition - 1] == '!')
                        negative = true;
                    var exp = text.Substring(startPosition+1, i - startPosition - 1);
                    expression.Add(j, new Expression(exp, expression, negative));
                    text = negative ? text.Replace("!("+ exp + ")", "e" + j) : text.Replace("(" + exp + ")", "e" + j);
                    i = -1;
                    j++;
                }
            }
            Expression result =  new Expression(text, expression);

            return result;
        }
    }
}
