using Pregex;

namespace PregexTester;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
        
        var emailRegex = RegexBuilder.Create()
            .StartOfString()
            .Word().OneOrMore()
            .Literal("@")
            .Word().OneOrMore()
            .Literal(".")
            .Set("a-zA-Z").Between(2, 6)
            .EndOfString()
            .Build();
        
        bool isValid = emailRegex.IsMatch("test@example.com");
        
        Console.WriteLine($"Pattern: {emailRegex}");
        Console.WriteLine($"test@example.com: {emailRegex.IsMatch("test@example.com")}");
        Console.WriteLine($"invalid@: {emailRegex.IsMatch("invalid@")}");
        
        Console.WriteLine("\n=== URL Regex with Named Groups ===");
        var urlRegex = RegexBuilder.Create()
            .NamedGroup("protocol", b => b.Literal("http").Literal("s").Optional())
            .Literal("://")
            .NamedGroup("domain", b => b.NotSet(" /").OneOrMore())
            .NamedGroup("path", b => b.Literal("/").AnyChar().ZeroOrMore()).Optional()
            .Build();

        Console.WriteLine($"Pattern: {urlRegex}");
        var match = urlRegex.Match("https://example.com/path");
        if (match.Success)
        {
            Console.WriteLine($"Protocol: {match.Groups["protocol"].Value}");
            Console.WriteLine($"Domain: {match.Groups["domain"].Value}");
            Console.WriteLine($"Path: {match.Groups["path"].Value}");
        }
    }
}