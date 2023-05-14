namespace Obaki.XmlSpecialCharacterEscaper;

public static class XmlSpecialCharacterEscaperExtensions
{
    public static string EscapeXmlSpecialCharacters(this string xmlString)
    {
        return XmlSpecialCharacterEscaper.Escape(xmlString);
    }
}