using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace SWD.Model
{
    public class FormulaElementary : AbstractExpression
    {
        public string Name { get; set; }
        public bool Personal { get; set; }
        public FormulaElementaryType Type { get; set; }
        public string Value { get; set; }
        public string Connective { get; set; }

        public FormulaElementary()
        {
        }

        public FormulaElementary(FormulaElementary formula)
        {
            Name = formula.Name;
            Personal = formula.Personal;
            Type = formula.Type;
            Id = formula.Id;
            Value = formula.Value;
            Connective = formula.Value;
        }

        public FormulaElementary(string text)
        {
            if (text.Contains('!'))
            {
                Negation = true;
            }
            Name = text.Replace("!", "");
        }

        public FormulaElementary(int id, string name)
        {
            this.Id = id;
            this.Name = name;
        }

        public FormulaElementary(string name, bool personal, FormulaElementaryType type)
        {
            Name = name;
            Personal = personal;
            Type = type;
        }

        public FormulaElementary(int id, string name, bool personal, FormulaElementaryType type)
        {
            Id = id;
            Name = name;
            Personal = personal;
            Type = type;
        }

        public FormulaElementary(int id, string name, string connective, string value, bool personal, FormulaElementaryType type)
        {
            Id = id;
            Name = name;
            Personal = personal;
            Type = type;
            Connective = connective;
            Value = value;
        }

        public override string ToString(bool symbolic = false)
        {
            string result = Negation ? "!" : "";
            return result + Id;
        }

        public override bool Calculate(Dictionary<int, bool> formulaElementariesValue)
        {
            var result = formulaElementariesValue[Id];
            if (Negation)
                result = !result;
            return result;
        }
    }
}
