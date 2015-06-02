using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using SWD.DataAccess;

namespace SWD.Model.Helpers
{
    public class ParseHelper
    {
        public static int OperatorCount(string text, char[] chars)
        {
            return 0;
        }

        public static Operations GetOperator(char op)
        {
            switch (op)
            {
                case '˅': return Operations.Or;
                case '˄': return Operations.And;
                case '>': return Operations.Implication;
                default: throw  new Exception("Bad Operator");
            }
        }

        public static Operations[] GetOperations(string text, char[] chars)
        {
            IList<Operations> result = new List<Operations>();
            for (var i = 0; i < text.Length; i++)
            {
                if (chars.Contains(text[i]))
                {
                    result.Add(GetOperator(text[i]));
                }
            }
            return result.ToArray();
        }

        public static AbstractExpression GetExpression(string value, Dictionary<int, Expression> dictionary)
        {
            
            AbstractExpression expression;

            if (!value.Contains('e'))
            {
                var repo = new Repository();
                bool negation = value.Contains('!') ? true : false;
                expression = repo.GetFormulaElementaryById(Int32.Parse(value.Replace("!","")));
                expression.Negation = negation;
            }
            else
            {
                int key = 0;
                if (value.Contains("!e"))
                    key = Int32.Parse(value.Replace("!e", ""));
                else
                    key = Int32.Parse(value.Replace("e", ""));
                expression  = dictionary[key];

            }
            return expression;
        }

        public static AbstractExpression[] GetExpressions(string[] values, Dictionary<int, Expression> dictionary)
        {
            IList<AbstractExpression> result = new List<AbstractExpression>();

            foreach (var value in values)
            {
                result.Add(GetExpression(value, dictionary));
            }
            return result.ToArray();
        }

    }
}
