using ApiConsoleApplication;
using ApiConsoleApplication.ExceptionSignInError;
using MySqlConnector;
using SDKBrasilAPI;


Console.Write("Seu primeiro Nome: ");
string? firstName = Console.ReadLine();
Console.Write("Seu ultimo Nome: ");
string? lastName = Console.ReadLine();
Console.Write("Sua Idade: ");
int idade = Int32.Parse(Console.ReadLine());
Console.Write("Seu Cep: ");
string? cep = Console.ReadLine();
Console.WriteLine();

ClienteCadastro cliente = new ClienteCadastro(firstName, lastName, idade, cep);


//Lista utilizada para receber os dados do cliente
List<string> listaDadosApi = new List<string>();

try
{
    //Verificação do CEP para que a API puxe os dados referente ao cep inserido
    var verificarDados = cliente;
    Console.WriteLine();
    Console.WriteLine(verificarDados.ToString());
    using (BrasilAPI api = new BrasilAPI())
    {
        var nomeCidade = await api.CEP(cliente.Cep);
        var nomeBairro = await api.CEP(cliente.Cep);
        var nomeRua = await api.CEP(cliente.Cep);

        
        //Tratamento de dados da lista
        foreach (var valorCep in nomeCidade.City.Split((char)StringSplitOptions.RemoveEmptyEntries))  
        {
            foreach (var valorBairro in nomeBairro.Neighborhood.Split((char)StringSplitOptions.RemoveEmptyEntries))
            {
                foreach (var valorRua in nomeRua.Street.Split((char)StringSplitOptions.RemoveEmptyEntries))
                {
                    listaDadosApi.Add(valorCep);
                    listaDadosApi.Add(valorBairro);
                    listaDadosApi.Add(valorRua);
                    foreach (var lista in listaDadosApi)
                    {
                        Console.WriteLine($"Sua Cidade: {lista[0]}\nSeu bairro: {lista[1]}\nSua rua: {lista[2]}" );
                    }
                }
            }
        }
    }

    //Utilização da Dependencia MYSQL para inserir dados do cliente na tabela
    using (MySqlConnection connection = new MySqlConnection("Server=localhost;Database=apidados;User Id=root;Password=123456;"))
    {
        connection.Open();

        var valorCidade = listaDadosApi.ElementAt(0);
        var valorBairro = listaDadosApi.ElementAt(1);
        var valorRua = listaDadosApi.ElementAt(2);
        
        //Variavel que determina os valores que serão enviado aos campos da tabela
        string valores =
            "INSERT INTO dadosdouser(firtsname, lastname, idade, cep, cidade, bairro, rua) VALUES ('" +
            cliente.NomeCliente + "', '" + cliente.UltimoNomeCliente + "', '" + cliente.Idade + "', '" + cliente.Cep+"', '"+valorCidade+"', '"+valorBairro+"', '"+valorRua+"')"; 
        
        MySqlCommand command = new MySqlCommand(valores, connection);
        command.ExecuteNonQuery(); 
        
        connection.Close();
        Console.WriteLine("Connection Closed. Press any key to exit...");
        Console.ReadKey();
    }
}
//Tratamento de Exceções
catch (SingInError ex)
{
    Console.WriteLine(ex.Message);
}
catch (BrasilAPIException exception)
{
    Console.WriteLine(exception.Code);
    Console.WriteLine(exception.Message);
}

