using System.Linq;
using SWD.Model.Userform;

namespace SWD.Model
{
    public class FormulaElementary : AbstractExpression
    {
        public string Name { get; set; }
        public bool Personal { get; set; }
        public FormulaElementaryType Type { get; set; }

        public FormulaElementary()
        {
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

        public override string ToString(bool symbolic = false)
        {
            string result = Negation ? "!" : "";
            return result + Id;
        }
    }
}
