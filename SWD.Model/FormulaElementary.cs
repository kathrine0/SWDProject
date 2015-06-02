using System.Linq;
using SWD.Model.Userform;

namespace SWD.Model
{
    public class FormulaElementary : AbstractExpression
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public bool Personal { get; set; }
        public FormulaElementaryType Type { get; set; }

        public FormulaElementary(string text)
        {
            if (text.Contains('!'))
            {
                Negation = true;
            }
            Name = text.Replace("!", "");
        }

        public FormulaElementary(int Id, string name)
        {
            ID = Id;
            Name = name;
        }

        public FormulaElementary(int Id, string name, bool personal, FormulaElementaryType type)
        {
            ID = Id;
            Name = name;
            Personal = personal;
            Type = type;
        }

        public override string ToString(bool symbolic = false)
        {
            string result = Negation ? "!" : "";
            return result + Name;
        }
    }
}
