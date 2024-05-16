namespace DevTask;

public class CurrencyBuild
{
    
    /// <summary>
    /// symbol of currency
    /// </summary>
    public readonly char Symbol;
    
    /// <summary>
    /// ratio of one major currency to another currency
    /// </summary>
    public readonly decimal Num;
    

    
    private CurrencyBuild(decimal num, char symbol,int round = 2)
    {
        Symbol = symbol;
        Num = Math.Round(num, round);
    }

    public override string ToString() =>
        string.Join(string.Empty, Math.Round(Num, 2), Symbol);


    /// <summary>
    /// 
    /// </summary>
    /// <param name="num">num of currency</param>
    /// <param name="symbol">symbol of currency.See list of currency in appsettings.json</param>
    /// <returns></returns>
    /// <exception cref="Exception">see appsettings.json. Take name of currency from the Сurrencies field</exception>
    /// <exception cref="ArgumentOutOfRangeException">The num must be greater than 0</exception>
    public static CurrencyBuild Build(decimal num, char symbol)
    {
        if (!Tools.GetTools().ContainsCurrency(symbol))
            throw new Exception("Not found symbol");

        if (num < 0m)
            throw new ArgumentOutOfRangeException(message: "The num must be greater than 0", null);

        return new CurrencyBuild(num, symbol);
    }

    private static decimal ToMainCurrency(CurrencyBuild y) =>
        y.Num /
        Tools.GetTools().Currencies
            .First(f => f.Symbol.Equals(y.Symbol))
            .Num;

    private static decimal FromMainCurrency(CurrencyBuild x, CurrencyBuild y) =>
        ToMainCurrency(y) *
        Tools.GetTools().Currencies
            .First(f => f.Symbol.Equals(x.Symbol))
            .Num;

    public static CurrencyBuild operator +(CurrencyBuild x, CurrencyBuild y) =>
        x.Symbol.Equals(y.Symbol)
            ? new CurrencyBuild(x.Num + y.Num, x.Symbol)
            : new CurrencyBuild(y.Symbol.Equals(Tools.GetTools().MainCurrency)
                    ? x.Num + ToMainCurrency(y)
                    : x.Num + ToMainCurrency(y) * FromMainCurrency(x, y)
                , x.Symbol);

    public static CurrencyBuild operator -(CurrencyBuild x, CurrencyBuild y) =>
        x.Symbol.Equals(y.Symbol)
            ? new CurrencyBuild(x.Num - y.Num, x.Symbol)
            : new CurrencyBuild(y.Symbol.Equals(Tools.GetTools().MainCurrency)
                    ? x.Num - ToMainCurrency(y)
                    : x.Num - ToMainCurrency(y) * FromMainCurrency(x, y)
                , x.Symbol);
    
    public static CurrencyBuild operator *(CurrencyBuild x, CurrencyBuild y) =>
        x.Symbol.Equals(y.Symbol)
            ? new CurrencyBuild(x.Num * y.Num, x.Symbol)
            : new CurrencyBuild(y.Symbol.Equals(Tools.GetTools().MainCurrency)
                    ? x.Num * ToMainCurrency(y)
                    : x.Num * ToMainCurrency(y) * FromMainCurrency(x, y)
                , x.Symbol);
    
    public static CurrencyBuild operator /(CurrencyBuild x, CurrencyBuild y) =>
        x.Symbol.Equals(y.Symbol)
            ? new CurrencyBuild(x.Num / y.Num, x.Symbol)
            : new CurrencyBuild(y.Symbol.Equals(Tools.GetTools().MainCurrency)
                    ? x.Num / ToMainCurrency(y)
                    : x.Num /  ToMainCurrency(y) * FromMainCurrency(x, y)
                , x.Symbol);

    public static bool operator ==(CurrencyBuild x, CurrencyBuild y) =>
        x.Symbol.Equals(y.Symbol) && x.Num == y.Num
        || x.Num == ToMainCurrency(y) * FromMainCurrency(x, y);

    public static bool operator !=(CurrencyBuild x, CurrencyBuild y) => 
        !(x == y);

    public static bool operator >(CurrencyBuild x, CurrencyBuild y) =>
        x.Symbol.Equals(y.Symbol) && x.Num > y.Num
        || x.Num > ToMainCurrency(y) * FromMainCurrency(x, y);

    public static bool operator >=(CurrencyBuild x, CurrencyBuild y) =>
        x.Symbol.Equals(y.Symbol) && x.Num > y.Num
        || x.Num >= ToMainCurrency(y) * FromMainCurrency(x, y);

    public static bool operator <(CurrencyBuild x, CurrencyBuild y) =>
        x.Symbol.Equals(y.Symbol) && x.Num > y.Num
        || x.Num < ToMainCurrency(y) * FromMainCurrency(x, y);

    public static bool operator <=(CurrencyBuild x, CurrencyBuild y) =>
        x.Symbol.Equals(y.Symbol) && x.Num > y.Num
        || x.Num <= ToMainCurrency(y) * FromMainCurrency(x, y);
}