namespace Obaki.XmlSpecialCharacterEscaper;

public static class XmlSpecialCharacterEscaperExtensions
{
    public static string Escape(this string xmlString)
        => XmlSpecialCharacterEscaper.Escape(xmlString);
    
    public static string Escape(this string xmlString, string regexPattern)
        => XmlSpecialCharacterEscaper.EscapeWithRegex(xmlString, regexPattern);

    public static async Task<string> EscapeAsync(this string xmlString)
       => await XmlSpecialCharacterEscaper.EscapeAsync(xmlString);

    public static async Task<string> EscapeAsync(this string xmlString, string regexPattern)
        => await XmlSpecialCharacterEscaper.EscapeWithRegexAsync(xmlString, regexPattern);


}