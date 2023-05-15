using System;
using System.Text;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
namespace Obaki.XmlSpecialCharacterEscaper.Benchmark;

class Program
{
    static void Main(string[] args)
    {
        var results = BenchmarkRunner.Run<EscapeBenchmarks>();
        
        //dotnet commands
        //dotnet run --framework net7.0 net6.0 --configuration Release --no-debug
        //dotnet run --configuration Release --no-debug
    }

}

[SimpleJob(RuntimeMoniker.Net60,baseline:true)]
[SimpleJob(RuntimeMoniker.Net70)]
[MemoryDiagnoser]
public class EscapeBenchmarks
{
    private const string xmlInput = "<node id=\"1\">&amp;&lt;&gt;&quot;&apos;< ><><> <> &&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&<</node>";

    [Benchmark]
    public string EscapeUsingSubstring() => Escape(xmlInput);

    [Benchmark]
    public string EscapeUsingIndexManipulation() => Escape2(xmlInput);

    [Benchmark]
    public string EscapeUsingDictionary() => Escape3(xmlInput);

    private static string Escape(string xmlInput)
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

    private static string Escape2(string xmlInput)
    {
        StringBuilder sb = new StringBuilder(xmlInput.Length);
           
            for (int i = 0; i < xmlInput.Length; i++)
            {
                char c = xmlInput[i];
                switch (c)
                {
                    case '&':
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

    private static readonly Dictionary<string, string> EscapeSequences = new Dictionary<string, string>
    {
        {"lt;", "&lt;"},
        {"gt;", "&gt;"},
        {"quot;", "&quot;"},
        {"apos;", "&apos;"},
        {"amp;", "&amp;"}
    };

    private static string Escape3(string xmlInput)
    {
        StringBuilder sb = new StringBuilder(xmlInput.Length);

        for (int i = 0; i < xmlInput.Length; i++)
        {
            char c = xmlInput[i];
            switch (c)
            {
                case '&':
                    bool matched = false;
                    foreach (var escapeSequence in EscapeSequences)
                    {
                        if (i + escapeSequence.Key.Length < xmlInput.Length &&
                            xmlInput.Substring(i + 1, escapeSequence.Key.Length) == escapeSequence.Key)
                        {
                            sb.Append("&");
                            matched = true;
                            break;
                        }
                    }
                    if (!matched)
                    {
                        sb.Append("&amp;");
                    }
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

}