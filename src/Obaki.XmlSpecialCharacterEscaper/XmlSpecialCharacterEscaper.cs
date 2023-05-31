using System.Text;
using System.Text.RegularExpressions;
namespace Obaki.XmlSpecialCharacterEscaper;
internal static class XmlSpecialCharacterEscaper
{
    internal static string Escape(string xmlString)
    {
        if (string.IsNullOrWhiteSpace(xmlString))
        {
            throw new ArgumentNullException($"{nameof(xmlString)} should not be empty");
        }

        StringBuilder sb = new StringBuilder(xmlString.Length);

        for (int i = 0; i < xmlString.Length; i++)
        {
            char c = xmlString[i];
            switch (c)
            {
                case '&':
                    //Ignore if ampersand is already part of the escaped character.
                    if (i + 3 < xmlString.Length && xmlString[i + 1] == 'l' && xmlString[i + 2] == 't' && xmlString[i + 3] == ';')
                    {
                        sb.Append("&");
                        break;
                    }
                    if (i + 3 < xmlString.Length && xmlString[i + 1] == 'g' && xmlString[i + 2] == 't' && xmlString[i + 3] == ';')
                    {
                        sb.Append("&");
                        break;
                    }
                    if (i + 5 < xmlString.Length && xmlString[i + 1] == 'q' && xmlString[i + 2] == 'u' && xmlString[i + 3] == 'o' && xmlString[i + 4] == 't' && xmlString[i + 5] == ';')
                    {
                        sb.Append("&");
                        break;
                    }

                    if (i + 5 < xmlString.Length && xmlString[i + 1] == 'a' && xmlString[i + 2] == 'p' && xmlString[i + 3] == 'o' && xmlString[i + 4] == 's' && xmlString[i + 5] == ';')
                    {
                        sb.Append("&");
                        break;
                    }

                    if (i + 4 < xmlString.Length && xmlString[i + 1] == 'a' && xmlString[i + 2] == 'm' && xmlString[i + 3] == 'p' && xmlString[i + 4] == ';')
                    {
                        sb.Append("&");
                        break;
                    }

                    sb.Append("&amp;");
                    break;

                case '\'':
                    sb.Append("&apos;");
                    break;

                case '\"':
                    sb.Append("&quot;");
                    break;

                case '<':
                    sb.Append("&lt;");
                    break;

                case '>':
                    sb.Append("&gt;");
                    break;

                default:
                    sb.Append(c);
                    break;
            }
        }
        return sb.ToString();
    }

    internal static string Escape(string xmlString, string regexPattern)
    {
        if (string.IsNullOrWhiteSpace(regexPattern))
        {
            throw new ArgumentNullException($"{nameof(regexPattern)} should not be empty");
        }

        if (string.IsNullOrWhiteSpace(xmlString))
        {
            throw new ArgumentNullException($"{nameof(xmlString)} should not be empty");
        }

        try
        {
            return Regex.Replace(xmlString, regexPattern, match =>
            {
                if (!string.IsNullOrEmpty(match.Value))
                {
                    return Escape(match.Value);
                }
                else
                {
                    return string.Empty;
                }
            });

        }
        catch (ArgumentException ex)
        {
            throw new ArgumentException(ex.Message);
        }
    }

    internal static async Task<string> EscapeAsync(string xmlString)
    {
        return await Task.Run(() => Escape(xmlString));
    }

    //TODO: Deep dive on how to implement the async version correctly or is it even necessary to use it.
    internal static async Task<string> EscapeAsync(string xmlString, string regexPattern)
    {
        if (string.IsNullOrEmpty(regexPattern))
        {
            throw new ArgumentNullException($"{nameof(regexPattern)} should not be empty");
        }

        if (string.IsNullOrEmpty(xmlString))
        {
            throw new ArgumentNullException($"{nameof(xmlString)} should not be empty");
        }

        return await Task.Run(() => Escape(xmlString, regexPattern));
    }
}
