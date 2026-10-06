using System.Text.Json;

class Program
{
    static void Main()
    {
        DadosVendas? dados = LerJson("questao_1.json");

        if (dados != null)
        {
            Dictionary<string, decimal> comissoes = new();

            foreach (Venda venda in dados.Vendas)
            {
                decimal comissao = CalcularComissao(venda.Valor);

                if (comissoes.ContainsKey(venda.Vendedor!))
                {
                    comissoes[venda.Vendedor!] += comissao;
                }
                else
                {
                    comissoes[venda.Vendedor!] = comissao;
                }
            }

            foreach (var item in comissoes)
            {
                Console.WriteLine($"{item.Key}: R$ {item.Value:F2}");
            }
        }
    }

    private static decimal CalcularComissao(decimal valor)
    {
        if (valor < 100) return 0;

        if (valor < 500) return valor * 0.01m;

        return valor * 0.05m;
    }

    private static DadosVendas? LerJson(string caminho)
    {
        string json = File.ReadAllText(caminho);

        JsonSerializerOptions options = new() {PropertyNameCaseInsensitive = true};

        return JsonSerializer.Deserialize<DadosVendas>(json, options);
    }
}

class DadosVendas
{
    public List<Venda> Vendas { get; set; } = new();
}

class Venda
{
    public string? Vendedor { get; set; }
    public decimal Valor { get; set; }
}