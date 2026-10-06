using System.Text.Json;

class Program
{
    static void Main()
    {
        DateOnly hoje = DateOnly.FromDateTime(DateTime.Today);
        decimal  valor = informarValor();
        DateOnly dataVencimento = informarDataVencimento();
        
        int diasAtraso = hoje.DayNumber - dataVencimento.DayNumber;

        if (diasAtraso <= 0)
        {
            Console.WriteLine("O pagamento não está atrasado.");
            return;
        }

        decimal juros = valor * 0.025m * diasAtraso;
        decimal valorFinal = valor + juros;

        Console.WriteLine($"Valor original: R$ {valor:F2}");
        Console.WriteLine($"Dias em atraso: {diasAtraso}");
        Console.WriteLine($"Juros: R$ {juros:F2}");
        Console.WriteLine($"Valor final: R$ {valorFinal:F2}");
    }

    private static decimal  informarValor()
    {
        decimal  valor;

        do
        {
            Console.Write("Digite o valor: R$ ");

        } while (!Decimal.TryParse(Console.ReadLine(), out valor));

        return valor;
    }

    private static DateOnly informarDataVencimento()
    {
        DateOnly data;

        do
        {
            Console.Write("Digite a data limite (dd/MM/yyyy): ");

        } while (!DateOnly.TryParse(Console.ReadLine(), out data));

        return data;

    }
}
