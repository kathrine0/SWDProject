
namespace SWD.DataAccess.Helpers
{
    public class StringHelper
    {
        public static string RemoveBrackets(string text)
        {
            return text.Replace("(", "").Replace(")", "");
        }

        public static string RemoveNegations(string text)
        {
            return text.Replace("!", "");
        }

        public static string RemoveSpaces(string text)
        {
            return text.Replace(" ", "");
        }
    }
}
