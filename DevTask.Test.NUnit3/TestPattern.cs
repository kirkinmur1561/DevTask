using System.Text.RegularExpressions;

namespace DevTask.Test.NUnit3;

public class TestPattern
{
    private Regex finder;
    [SetUp]
    public void Setup()
    {
        finder = new(@"(?<num1>(\d+(\.\d+)?))(?<currency1>(\$|€|₽|₸|¥|£))\s*(?<operator>(!=|=|>|>=|<|<=|\+|-|\*|\\))\s*(?<num2>\d+(\.\d+)?)(?<currency2>(\$|€|₽|₸|¥|£))");
    }

    [Test,
    TestCase("10$+10$"),
    TestCase("10$ + 10$"),
    TestCase("10.01$ + 0.10$")]
    public void SuccessFinder(string value)
    {
        Assert.IsTrue(finder.Match(value).Success, "finder.Match(value).Success",value);
    }

    [Test,
     TestCase("10+ 10$"),
     TestCase("10+10")]
    public void NotSuccessFinder(string value)
    {
        Assert.IsFalse(finder.Match(value).Success, "finder.Match(value).Success", value);
    }
}