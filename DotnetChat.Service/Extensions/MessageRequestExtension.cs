using System.Text.RegularExpressions;

namespace DotnetChat.Service.Extensions
{
    public static class MessageRequestExtension
    {
        public static bool IsTextComand(this string text)
        {
            var stockRegex = @"\/stock=[a-z _ A-Z 0-9]+";
            Regex res = new Regex(stockRegex);
            if (res.IsMatch(text))
                return true;
            else
                return false;
        }
    }
}
