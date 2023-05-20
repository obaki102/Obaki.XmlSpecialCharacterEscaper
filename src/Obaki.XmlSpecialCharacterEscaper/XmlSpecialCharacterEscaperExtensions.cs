namespace Obaki.XmlSpecialCharacterEscaper;

public static class XmlSpecialCharacterEscaperExtensions
{
    public static string Escape(this string xmlString)
        => XmlSpecialCharacterEscaper.Escape(xmlString);
    
    public static string Escape(this string xmlString, string regexPattern)
        => XmlSpecialCharacterEscaper.Escape(xmlString, regexPattern);
    
}