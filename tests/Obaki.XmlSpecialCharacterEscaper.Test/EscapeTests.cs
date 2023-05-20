using Obaki.XmlSpecialCharacterEscaper;
namespace Obaki.XmlSpecialCharacterEscaper.Test;

public class EscapeTests
{
    [Theory]
    [InlineData("&amp;", "&")]
    [InlineData("&apos;", "\'")]
    [InlineData("&lt;", "<")]
    [InlineData("&gt;", ">")]
    [InlineData("&quot;", "\"")]
    [InlineData("&quot; &amp; &apos; &lt; &gt;", "\" & \' < >")]
    [InlineData("&quot;&quot;", "&quot;\"")]
    public void Escape_ValidInput_ShouldEscapeSpecialCharacters(string expected, string input)
    {
        //Arrange
        string test = input;
        //Act
        var result = test.Escape();
        //Assert
        Assert.Equal(expected, result);
        Assert.Equal(expected.Length, result.Length);

    }

    [Theory]
    [InlineData("<tag Value=\" &gt;\"", "<tag Value=\" >\"")]
    public void EscapeWithRegex_ValidInput_ShouldEscapeSpecialCharacters(string expected, string input)
    {
        //Arrange
        string test = input;
        string regexPattern = @"(?<=Value\s*=\s*"")([^""]*)(?="")";

        //Act
        var result = test.Escape(regexPattern);
        //Assert
        Assert.Equal(expected, result);

    }
}