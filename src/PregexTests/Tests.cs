using Pregex;

namespace PregexTests;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void TestEmailMatch()
    {
        var emailRegex = RegexBuilder.Create()
            .StartOfString()
            .Word().OneOrMore()
            .Literal("@")
            .Word().OneOrMore()
            .Literal(".")
            .Set("a-zA-Z").Between(2, 6)
            .EndOfString()
            .Build();
        
        var match = emailRegex.IsMatch("test@example.com");
        
        Assert.IsTrue(match);
    }

    [Test]
    public void TestEmailNotMatch()
    {
        var emailRegex = RegexBuilder.Create()
            .StartOfString()
            .Word().OneOrMore()
            .Literal("@")
            .Word().OneOrMore()
            .Literal(".")
            .Set("a-zA-Z").Between(2, 6)
            .EndOfString()
            .Build();
        
        Assert.IsFalse(emailRegex.IsMatch("invalid@"));
    }

    [Test]
    public void TestPhoneMatch()
    {
        var phoneRegex = RegexBuilder.Create()
            .StartOfString()
            .Literal("+").Optional()
            .Group(b => b.Digit().Exactly(3).Literal("-")).Optional()
            .Digit().Exactly(3)
            .Literal("-").Optional()
            .Digit().Exactly(4)
            .EndOfString()
            .Build();
        var match = phoneRegex.IsMatch("123-456-7890");
        Assert.IsTrue(match);
    }

    [Test]
    public void TestPhoneNotMatch()
    {
        var phoneRegex = RegexBuilder.Create()
            .StartOfString()
            .Literal("+").Optional()
            .Group(b => b.Digit().Exactly(3).Literal("-")).Optional()
            .Digit().Exactly(3)
            .Literal("-").Optional()
            .Digit().Exactly(4)
            .EndOfString()
            .Build();
        
        Assert.IsFalse(phoneRegex.IsMatch("+1-123-456"));
    }
}