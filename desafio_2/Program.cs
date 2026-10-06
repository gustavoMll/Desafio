using System.Text.Json;

class Program
{
    static void Main()
    {
        DadosEstoque? dados = LerJson("questao_2.json");

        if (dados == null)
        {
            Console.WriteLine("Não foi possível carregar o estoque.");
            return;
        }

        foreach (Produto produto in dados.Estoque)
        {
            mostrarProduto(produto);
        }

        bool continuar = true;

        while (continuar)
        {
            int codigoProduto = selecionarCodigo();

            Produto? produto = dados.Estoque.Find(p => p.CodigoProduto == codigoProduto);

            if (produto == null)
            {
                Console.WriteLine("Produto não encontrado.");
                continue;
            }

            Console.WriteLine("\nProduto selecionado:");
            mostrarProduto(produto);

            int codigoMovimentacao = selecionarMovimentacao();

            int qtdEstoque = informarQtdEstoque();

            if (codigoMovimentacao == 1)
            {
                entradaEstoque(produto, qtdEstoque);

                Console.WriteLine("\nEntrada realizada com sucesso!");
            }
            else if (codigoMovimentacao == 2)
            {
                while (!validarQtdEstoque(produto, qtdEstoque))
                {
                    Console.WriteLine(
                        $"Quantidade inválida. Estoque atual: {produto.Estoque}"
                    );

                    qtdEstoque = informarQtdEstoque();
                }

                saidaEstoque(produto, qtdEstoque);

                Console.WriteLine("\nSaída realizada com sucesso!");
            }

            Console.WriteLine("\nEstoque atualizado:");
            mostrarProduto(produto);

            Console.Write("\nDeseja realizar outra movimentação? (s/n): ");
            string? resposta = Console.ReadLine();

            continuar = resposta?.ToLower() == "s";
        }
    }

    private static DadosEstoque? LerJson(string caminho)
    {
        string json = File.ReadAllText(caminho);

        JsonSerializerOptions options = new()
        {
            PropertyNameCaseInsensitive = true
        };

        return JsonSerializer.Deserialize<DadosEstoque>(json, options);
    }

    private static void mostrarProduto(Produto produto)
    {
        Console.WriteLine($"Código: {produto.CodigoProduto}");
        Console.WriteLine($"Descrição: {produto.DescricaoProduto}");
        Console.WriteLine($"Estoque: {produto.Estoque}");
        Console.WriteLine("----------------------");
    }

    private static int selecionarCodigo()
    {
        int codigo;

        do
        {
            Console.Write("Digite o código do produto: ");

        } while (!int.TryParse(Console.ReadLine(), out codigo) || codigo <= 0);

        return codigo;
    }

    private static int selecionarMovimentacao()
    {
        int codigo;

        do
        {
            Console.WriteLine("\n1 - Entrada no estoque");
            Console.WriteLine("2 - Saída do estoque");
            Console.Write("Digite o código da movimentação: ");

        } while (
            !int.TryParse(Console.ReadLine(), out codigo) ||
            (codigo != 1 && codigo != 2)
        );

        return codigo;
    }

    private static int informarQtdEstoque()
    {
        int quantidade;

        do
        {
            Console.Write("Digite a quantidade de estoque a ser movimentada: ");

        } while (
            !int.TryParse(Console.ReadLine(), out quantidade) ||
            quantidade <= 0
        );

        return quantidade;
    }

    private static bool validarQtdEstoque(
        Produto produto,
        int qtdEstoque)
    {
        return produto.Estoque >= qtdEstoque;
    }

    private static void entradaEstoque(
        Produto produto,
        int qtdEstoque)
    {
        produto.Estoque += qtdEstoque;
    }

    private static void saidaEstoque(
        Produto produto,
        int qtdEstoque)
    {
        produto.Estoque -= qtdEstoque;
    }
}

class DadosEstoque
{
    public List<Produto> Estoque { get; set; } = new();
}

class Produto
{
    public int CodigoProduto { get; set; }

    public string? DescricaoProduto { get; set; }

    public int Estoque { get; set; }
}