namespace Obaki.XmlSpecialCharacterEscaper;

public static class XmlSpecialCharacterEscaperExtensions
{
    public static string Escape(this string xmlString)
    {
        return XmlSpecialCharacterEscaper.Escape(xmlString);
    }

    public static string Escape(this string xmlString, string regexPattern)
    {
        return XmlSpecialCharacterEscaper.Escape(xmlString, regexPattern);
    }
}