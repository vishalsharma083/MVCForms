using System;
using System.Text.RegularExpressions;

namespace MVCForms.Helpers
{
    public static class TextHelpers
    {
        public static string KillHtml(this string text)
        {
            return string.IsNullOrEmpty(text) ? null : Regex.Replace(text, @"<(.|\n)*?>\n", string.Empty);
        }

        public static string PreserveBreaks(this string text)
        {
            return string.IsNullOrEmpty(text) ? null : text.Replace(Environment.NewLine, @"\\br\\");
        }

        public static string RestoreBreaks(this string text)
        {
            return string.IsNullOrEmpty(text) ? null : text.Replace(@"\\br\\", Environment.NewLine);
        }
    }
}