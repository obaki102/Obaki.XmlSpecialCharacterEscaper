namespace Obaki.XmlSpecialCharacterEscaper.Test;
public class EscapeAsyncTests
{
    [Theory]
    [InlineData("&amp;", "&")]
    [InlineData("&apos;", "\'")]
    [InlineData("&lt;", "<")]
    [InlineData("&gt;", ">")]
    [InlineData("&quot;", "\"")]
    [InlineData("&quot; &amp; &apos; &lt; &gt;", "\" & \' < >")]
    [InlineData("&quot;&quot;", "&quot;\"")]
    public async Task EscapeAsync_ValidInput_ShouldEscapeAsyncSpecialCharacters(string expected, string input)
    {
        //Arrange
        string test = input;

        //Act
        var result = await test.EscapeAsync();

        //Assert
        Assert.Equal(expected, result);
        Assert.Equal(expected.Length, result.Length);

    }

    [Theory]
    [InlineData("<tag Value=\" &gt;\"", "<tag Value=\" >\"")]
    public async Task EscapeAsyncWithRegex_ValidInput_ShouldEscapeAsyncSpecialCharacters(string expected, string input)
    {
        //Arrange
        string test = input;
        string regexPattern = @"(?<=Value\s*=\s*"")([^""]*)(?="")";

        //Act
        var result = await test.EscapeAsync(regexPattern);

        //Assert
        Assert.Equal(expected, result);

    }

    [Theory]
    [InlineData("  ")]
    [InlineData(null)]
    [InlineData("")]
    public void EscapeAsync_InValidInput_ShouldThrowNullArgumentExceptionError(string input)
    {
        //Act
        var function = new Func<Task>(async ()  => await input.EscapeAsync());

        //Assert
        Assert.ThrowsAsync<ArgumentNullException>(function);
    }

    [Theory]
    [InlineData("  ")]
    [InlineData(null)]
    [InlineData("")]
    public void EscapeAsyncWithRegex_InValidInput_ValidRegexPattern_ShouldThrowNullArgumentExceptionError(string input)
    {
        //Arrange
        string regexPattern = @"((?="")";

        //Act
        var function = new Func<Task>(async ()  => await input.EscapeAsync(regexPattern));

        //Assert
        Assert.ThrowsAsync<ArgumentNullException>(function);
    }

    [Theory]
    [InlineData("??<=Value\\s*=\\s*\")([^\"]*)")]
    public void EscapeAsyncWithRegex_InValidInput_ValidRegexPattern_ShouldThrowArgumentExceptionError(string regexPattern)
    {
        //Arrange
        string test = "<root test=\"test1\"></root>";

        //Act
        var function = new Func<Task>(async () => await test.EscapeAsync(regexPattern));

        //Assert
        Assert.ThrowsAsync<ArgumentException>(function);
    }

    [Theory]
    [InlineData("  ")]
    [InlineData(null)]
    [InlineData("")]
    public void EscapeAsyncWithRegex_ValidInput_EmptyRegexPattern_ShouldThrowNullArgumentExceptionError(string regexPattern)
    {
        //Arrange
        string test = "<root test=\"test1\"></root>";

        //Act
        var function = new Func<Task>(async () => await test.EscapeAsync(regexPattern));

        //Assert
        Assert.ThrowsAsync<ArgumentException>(function);
    }
}
