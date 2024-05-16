using System.Text.RegularExpressions;
using Microsoft.Extensions.Configuration;

namespace DevTask;

public class Tools
{
    private static Tools _tools;//singleton
    public readonly char MainCurrency;
    public readonly IEnumerable<CurrencyBuild> Currencies;
    private Regex _findCurrency;
    

    private Tools(char mainCurrency, IEnumerable<CurrencyBuild> currencies)
    {
        MainCurrency = mainCurrency;
        Currencies = currencies;
        string simbols = string.Join('|', currencies.Select(ch => ch.Symbol.Equals('$') ? @"\$" : $"{ch.Symbol}"));
        _findCurrency = new Regex(@$"(?<num1>\d+(\.\d+)?)(?<currency1>({simbols}))\s*(?<operator>(!=|=|>|>=|<|<=|\+|-|\*|\/))\s*(?<num2>\d+(\.\d+)?)(?<currency2>({simbols}))");
    }

    public Match GetMatch(string input) =>
        _findCurrency.Match(input);

    public bool ContainsCurrency(char currency) =>
        Currencies.Select(s => s.Symbol).Contains(currency);

    /// <summary>
    /// 
    /// </summary>
    /// <returns>Setting of app</returns>
    /// <exception cref="Exception">Check and see appsettings.json from path of start app</exception>
    public static Tools GetTools()
    {
        if (_tools != null) return _tools;
        
        IConfigurationBuilder builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false);
        
        IConfiguration config = builder.Build();
        
        string? mainCur = config[nameof(MainCurrency)];
        
        if (string.IsNullOrWhiteSpace(mainCur))
            throw new Exception("Main currency not set");
        
        Dictionary<string, Dictionary<string, decimal>>? currencies = 
            config.Get<Dictionary<string,Dictionary<string,decimal>>>();

        if (currencies == null || currencies.First().Value.Count == 0)
            throw new Exception("Currencies not specified");
        
        CurrencyBuild[] currenciesArray = currencies.First().Value
            .Select(s =>  CurrencyBuild.Build(s.Value, s.Key.ToCharArray()[0]))
            .ToArray();

        return _tools = new Tools(mainCur.ToCharArray()[0], currenciesArray);
    }
}