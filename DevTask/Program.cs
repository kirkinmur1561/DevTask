using DevTask;

internal class Program
{
    public async static Task<int> Main()
    {
        string[] texts =
        {
            "10€ / 35¥"
        };

        foreach (string text in texts)
        {
            var match = Tools.GetTools().GetMatch(text);
            string num1 = match.Groups[nameof(num1)].Value,
                num2 = match.Groups[nameof(num2)].Value,
                currency1 = match.Groups[nameof(currency1)].Value,
                currency2 = match.Groups[nameof(currency2)].Value;

            if (!decimal.TryParse(num1, out decimal n1) ||
                !decimal.TryParse(num2, out decimal n2)) continue;
            try
            {
                CurrencyBuild cb1 = CurrencyBuild.Build(n1, currency1.ToCharArray()[0]);
                CurrencyBuild cb2 = CurrencyBuild.Build(n2, currency2.ToCharArray()[0]);
                
                switch (match.Groups["operator"].Value)
                {
                    case "!=":
                        Console.WriteLine($"{cb1} != {cb2} = {cb1 != cb2}");
                        break;
                    case "=":
                        Console.WriteLine($"{cb1} == {cb2} = {cb1 == cb2}");
                        break;
                    case ">":
                        Console.WriteLine($"{cb1} > {cb2} = {cb1 > cb2}");
                        break;
                    case ">=":
                        Console.WriteLine($"{cb1} >= {cb2} = {cb1 >= cb2}");
                        break;
                    case "<=":
                        Console.WriteLine($"{cb1} <= {cb2} = {cb1 <= cb2}");
                        break;
                    case "<":
                        Console.WriteLine($"{cb1} < {cb2} = {cb1 < cb2}");
                        break;
                    case "+":
                        Console.WriteLine($"{cb1} + {cb2} = {cb1 + cb2}");
                        break;
                    case "-":
                        Console.WriteLine($"{cb1} - {cb2} = {cb1 - cb2}");
                        break;
                    case "*":
                        Console.WriteLine($"{cb1} * {cb2} = {cb1 * cb2}");
                        break;
                    case "/":
                        Console.WriteLine($"{cb1} / {cb2} = {cb1 / cb2}");
                        break;
                    default:
                        Console.WriteLine("operator not defined");
                        break;
                }
            }
            catch (ArgumentOutOfRangeException er)
            {
                Console.WriteLine($"{nameof(ArgumentOutOfRangeException)}{Environment.NewLine}{er}");
            }
            catch (Exception er)
            {
                Console.WriteLine($"{nameof(Exception)}{Environment.NewLine}{er}");
            }
        }
        
        return 0;
    }
}