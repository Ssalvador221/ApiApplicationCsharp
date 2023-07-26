using ApiConsoleApplication.ExceptionSignInError;
using SDKBrasilAPI;

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
    

    public override string ToString()
    {
        return "Cadastro feito com Sucesso!\n" +
               $"Bem-Vindo: {NomeCliente} {UltimoNomeCliente}\nSua idade: {Idade}\n";
    }

    public void IfNameIsNull() 
    {
        if (NomeCliente != null)
        {
            throw new SingInError("Opss... Firstname field is empty, please set a valid name!");
        }        
        if (UltimoNomeCliente != null)
        {
            throw new SingInError("Opss... Lastname field is empty, please set a valid name!");
        }        
        if (Idade != null || Idade <= 18)
        {
            throw new SingInError("Opss... Age field is empty or set a age greater than 18!");
        }
    }

   
}
