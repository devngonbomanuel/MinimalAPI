namespace Minimal.Dominio.ModelViews;

//Modelo de Visualização para Home da API
public struct Home
{
    public string Mensagem { get => "Bem-vindo à API de vendas!";}
    
    public string Documentacao {get => "/swagger";}
}