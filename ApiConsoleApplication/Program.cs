using System.Globalization;
using ApiConsoleApplication;
using Microsoft.VisualBasic;
using SDKBrasilAPI;

ClienteCadastro cliente = new ClienteCadastro();

Console.Write("Seu primeiro Nome: ");
cliente.NomeCliente = Console.ReadLine();
Console.Write("Seu ultimo Nome: ");
cliente.UltimoNomeCliente = Console.ReadLine();
Console.Write("Sua Idade: ");
cliente.Idade = Int32.Parse(Console.ReadLine());
Console.Write("Seu Cep: ");
cliente.Cep = Console.ReadLine();
Console.WriteLine();

using (var brasilApi = new BrasilAPI())
{
    try
    {
        var cepCidade = await brasilApi.CEP(cliente.Cep);
        var cepBairro = await brasilApi.CEP(cliente.Cep);
        var cepRua = await brasilApi.CEP(cliente.Cep);
        
        foreach (var valorCep in cepCidade.City.Split((char)StringSplitOptions.RemoveEmptyEntries))
        {
            Console.WriteLine($"Obrigado pelo cadastro Sr(a).{cliente.NomeCliente} {cliente.UltimoNomeCliente}");
            foreach (var valorBairro in cepBairro.Neighborhood.Split((char)StringSplitOptions.RemoveEmptyEntries))
            {
                foreach (var valorRua in cepRua.Street.Split((char)StringSplitOptions.RemoveEmptyEntries))
                {
                    Console.WriteLine($"Reside na Cidade: {valorCep}\nBairro: {valorBairro}\nRua: {valorRua}");
                }
            }
        }
    }
    catch(BrasilAPIException exception)
    {
        Console.WriteLine(exception.Code);
    }
}

