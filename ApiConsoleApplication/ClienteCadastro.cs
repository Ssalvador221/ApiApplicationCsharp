namespace ApiConsoleApplication;

public class ClienteCadastro
{
    public string? NomeCliente { get; set; }
    public string? UltimoNomeCliente { get; set; }
    public int  Idade { get; set; }
    public string? Cep { get; set; }

    public ClienteCadastro()
    {
        
    }
    
    public ClienteCadastro(string nomeCliente, string ultimoNomeCliente, int idade, string cep)
    {
        NomeCliente = nomeCliente;
        UltimoNomeCliente = ultimoNomeCliente;
        Idade = idade;
        Cep = cep;
    }
    
    
}
