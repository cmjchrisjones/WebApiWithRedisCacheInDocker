using System.Text.RegularExpressions;

namespace BookProcessor.Extensions
{
    public static class StringExtensions
    {
        public static string RemoveHyphensFromISBN(this string s)
        {
            return s.Replace("-", string.Empty);
        }

        private static string ReplaceCommasWithHyphens(this string s)
        {
            var newString =  s.Replace(", ", " ").Replace(","," ");
            newString.Trim();
            newString.Replace("  ", " ");
            return RemoveMultipleSuperflouesSpaces(newString).Trim();
        }

        private static string RemoveMultipleSuperflouesSpaces(string s)
        {
            RegexOptions options = RegexOptions.None;
            Regex regex = new Regex("[ ]{2,}", options);
            return regex.Replace(s, " ");
        }

        public static string FormatForOutput(this string s)
        {
            if (string.IsNullOrWhiteSpace(s))
            {
                return string.Empty;
            }
            return s.ReplaceCommasWithHyphens();
        }
    }
}