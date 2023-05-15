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
    public void Escape_ValidInput_ShouldEscapeSpecialCharacters(string expected, string input)
    {
        //Arrange
        string test = input;
        //Act
        var result  = test.EscapeXmlSpecialCharacters();
        //Assert
        Assert.Equal(expected, result);
        Assert.Equal(expected.Length, result.Length);

    }
}