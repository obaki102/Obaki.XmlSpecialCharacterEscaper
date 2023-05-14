using System.Text;
namespace Obaki.XmlSpecialCharacterEscaper;
internal static class XmlSpecialCharacterEscaper
{
    internal static string Escape(string xmlInput)
    {
        StringBuilder sb = new StringBuilder(xmlInput.Length);
           
            for (int i = 0; i < xmlInput.Length; i++)
            {
                char c = xmlInput[i];
                switch (c)
                {
                    case '&':
                        if (i + 2 < xmlInput.Length && xmlInput.Substring(i + 1, 2) == "lt")
                        {
                            sb.Append("&");
                            break;
                        }
                        if (i + 2 < xmlInput.Length && xmlInput.Substring(i + 1, 2) == "gt")
                        {
                            sb.Append("&");
                            break;
                        }
                        if (i + 4 < xmlInput.Length && xmlInput.Substring(i + 1, 4) == "quot")
                        {
                            sb.Append("&");
                            break;
                        }

                        if (i + 4 < xmlInput.Length && xmlInput.Substring(i + 1, 4) == "apos")
                        {
                            sb.Append("&");
                            break;
                        }

                        if (i + 3 < xmlInput.Length && xmlInput.Substring(i + 1, 3) == "amp")
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
    internal static string Escape(string regexPattern, string xmlxmlInput)
    {
        throw new NotImplementedException();
    }

}
