using System.Text;
using System.Text.RegularExpressions;
namespace Obaki.XmlSpecialCharacterEscaper;
internal static class XmlSpecialCharacterEscaper
{
    internal static string Escape(string xmlInput)
    {
        if(string.IsNullOrEmpty(xmlInput))
        {
            throw new ArgumentNullException(nameof(xmlInput));
        }

        StringBuilder sb = new StringBuilder(xmlInput.Length);

        for (int i = 0; i < xmlInput.Length; i++)
        {
            char c = xmlInput[i];
            switch (c)
            {
                case '&':
                    //Ignore if ampersand is already part of the escaped character.
                    if (i + 3 < xmlInput.Length && xmlInput[i + 1] == 'l' && xmlInput[i + 2] == 't' && xmlInput[i + 3] == ';')
                    {
                        sb.Append("&");
                        break;
                    }
                    if (i + 3 < xmlInput.Length && xmlInput[i + 1] == 'g' && xmlInput[i + 2] == 't' && xmlInput[i + 3] == ';')
                    {
                        sb.Append("&");
                        break;
                    }
                    if (i + 5 < xmlInput.Length && xmlInput[i + 1] == 'q' && xmlInput[i + 2] == 'u' && xmlInput[i + 3] == 'o' && xmlInput[i + 4] == 't' && xmlInput[i + 5] == ';')
                    {
                        sb.Append("&");
                        break;
                    }

                    if (i + 5 < xmlInput.Length && xmlInput[i + 1] == 'a' && xmlInput[i + 2] == 'p' && xmlInput[i + 3] == 'o' && xmlInput[i + 4] == 's' && xmlInput[i + 5] == ';')
                    {
                        sb.Append("&");
                        break;
                    }

                    if (i + 4 < xmlInput.Length && xmlInput[i + 1] == 'a' && xmlInput[i + 2] == 'm' && xmlInput[i + 3] == 'p' && xmlInput[i + 4] == ';')
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

   
    internal static string Escape( string xmlString,string regexPattern)
    {
         if(string.IsNullOrEmpty(regexPattern))
        {
            throw new ArgumentNullException(nameof(regexPattern));
        }
        
         if(string.IsNullOrEmpty(xmlString))
        {
            throw new ArgumentNullException(nameof(xmlString));
        }

        return Regex.Replace(xmlString, regexPattern, match => Escape(match.Value));
    }

}
