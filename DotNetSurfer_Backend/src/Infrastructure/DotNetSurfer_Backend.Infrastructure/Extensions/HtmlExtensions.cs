using System;
using System.Text.RegularExpressions;

namespace DotNetSurfer_Backend.Infrastructure.Extensions
{
    public static class HtmlExtensions
    {
        public static string ToTrimmedPlainTextFromHtml(this string html, int length)
        {
            if (string.IsNullOrEmpty(html))
            {
                return html;
            }

            if (length < 0)
            {
                throw new ArgumentException($"Negative length is not allowed. Length: {length}");
            }

            string plainText = html.ToPlainTextFromHtml();
            string trimmedText = plainText.SubstringByLength(length);

            return trimmedText;
        }

        private static string ToPlainTextFromHtml(this string html)
        {
            return Regex.Replace(html, "<[^>]*>", "");
        }

        private static string SubstringByLength(this string text, int length)
        {
            int lengthToCut = Math.Min(text.Length, length);

            return text.Substring(0, lengthToCut);
        }
    }
}
