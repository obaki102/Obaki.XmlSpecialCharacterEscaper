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
    [InlineData("<tag Value=\" &gt;\"/>", "<tag Value=\" >\"/>")]
    [InlineData("<tag Value=\" &lt;\"/>", "<tag Value=\" <\"/>")]
    [InlineData("<root value=\"&quot; &amp; &amp; &amp; &amp; &amp; \"/>", "<root value=\"\" & & & & & \"/>")]
    [InlineData("<root value=\"&quot; &amp;      &amp; &amp; &amp;      &amp; \"/>", "<root value=\"\" &      & & &      & \"/>")]
    [InlineData("<tag>&quot;&apos; &amp; &amp; &amp; &apos; &lt; &gt; &lt;&gt; </tag>", "<tag>\"' & & & ' < > <> </tag>")]
    [InlineData("<AMS Value  = \"Glaser&quot;-Focused HD\" Verb=\"\" Asset_ID = \"test  &quot;  \" Asset_Name=\"test_&quot;Glaser&quot;-FocusedT\" Creation_Date=\"2022-02-10\" Description=\"&quot;Glaser&quot;-Focused--title--\" Product=\"MOD\" Provider=\"HD\" Provider_ID=\".com\" Name=\"Title_Brief\" Value=\"&quot;Glaser&quot;-Focused HD\"  />", "<AMS Value  = \"Glaser\"-Focused HD\" Verb=\"\" Asset_ID = \"test  \"  \" Asset_Name=\"test_\"Glaser\"-FocusedT\" Creation_Date=\"2022-02-10\" Description=\"\"Glaser\"-Focused--title--\" Product=\"MOD\" Provider=\"HD\" Provider_ID=\".com\" Name=\"Title_Brief\" Value=\"\"Glaser\"-Focused HD\"  />")]
    public async Task EscapeAsyncWithRegex_ValidInput_ShouldEscapeAsyncSpecialCharacters(string expected, string input)
    {
        //Arrange
        string test = input;
        string regexPattern = @"(?<=(<(\w+)>))(?<value>.*?)(?=(</(\w+)>))|(?<=(\s*=\s*['""]))(?<value>.*?)(?=(?:['""]\s*/>|['""]\s*\w+\s*=\s*))";

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
