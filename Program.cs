using Microsoft.EntityFrameworkCore;
using Minimal.Infraestrutura.Data;
using Minimal.Dominio.DTOs;
using Minimal.Dominio.Entities;
using Minimal.Dominio.Interface;
using Minimal.Dominio.Services;
using Microsoft.AspNetCore.Mvc;
using Minimal.Dominio.ModelViews;

/**TODO**/
/*Nuget 
*    Microsoft.EntityFrameworkCore.Design Version=8.0.0
*   Microsoft.EntityFrameworkCore.SqlServer Version=8.0.0
*    Microsoft.EntityFrameworkCore.Tools Version=8.0.0
*    Microsoft.EntityFrameworkCore Version=8.0.0
*   System.ComponentModel.Annotations Version=5.0.0
*    Microsoft.AspNetCore.Mvc Version=2.2.0
*/
// Using o pacote Swashbuckle.AspNetCore
//Fazer migration --appdbcontext
//ToDo-BuscaTodos --ICalcadoServico

#region Builder

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<IAdminServico, AdminServico>();
builder.Services.AddScoped<ICalcadoServico, CalcadoServico>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var connectionString = builder.Configuration.GetConnectionString("Conexao");
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));
var app = builder.Build();

#endregion

//Documentacãoo da rota
#region Home

app.MapGet("/", () => Results.Json(new Home())).withTags("Home");

#endregion

#region Administradores
app.MapPost("/administradores/login", ( [FromBody] LoginDTO loginDTO, IAdminServico adminServico) => {
    if(adminServico.Login(loginDTO) != null)
        return Results.Ok("Login com sucesso!");
    else
        return Results.Unauthorized();
}).withTags("Administradores");

//==================Retornar Todos
app.MapGet("/administradores", ( [FromBody], IAdminServico adminServico) => {
 var admns = new List<AdminModelView>();
 var admins = adminServico.Todos();
 foreach (var adm in admins)
 {
    admns.Add(new AdminModelView{
        Id  = adm.Id,
        Email = adm.Email,
        Perfil = adm.Perfil

    });
    
 }
    return Results.Ok(admins);
}).withTags("Administradores");

//=================Retornar por Id
app.MapGet("/administradores", ( [FromRoute]int id, IAdminServico adminServico) => {
 var admins = adminServico.BuscaPorId();
 if(admins == null) return Results.NotFound();
    return Results.Ok(new AdminModelView{
        Id  = admins.Id,
        Email = admins.Email,
        Perfil = admins.Perfil

    });
}).withTags("Administradores");

//=================Criar
app.MapPost("/administradores", ( [FromBody] AdminDTO adminDTO, IAdminServico adminServico) => {
    var validacao = new ErroValidacao(){
        Mensgens = List<string>();
    };

    if(string.IsNullOrEmpty(AdminDTO.Nome))
    validacao.Mensgens.Add("Campo nome não pode estar vazio!");
    if(string.IsNullOrEmpty(AdminDTO.Senha))
    validacao.Mensgens.Add("Campo nome não pode estar vazio!");
    if(AdminDTO.Perfil == null)
    validacao.Mensgens.Add("Campo nome não pode estar vazio!");

    if(validacao.Mensgens.Count > 0)
    return Results.BadRequest(validacao);

    var admin = new Admin{
    Email = adminDTO.Email,
    Senha = adminDTO.Senha,
    Perfil = adminDTO.Perfil.ToString() ?? Perfil.Gerente.ToString();
    };

    adminServico.Criar(admin);
    return Results.Created($"/admin/{admin.Id}", new AdminModelView{
        Id  = admin.Id,
        Email = admin.Email,
        Perfil = admin.Perfil
    }); 
}).withTags("Administradores");

#endregion


#region Calcados
//=================Validação
ErroValidacao validaDTO(CalcadoDTO calcadoDTO)
{
    var msg = new ErroValidacao
    {
        Mensgens = new List<string>()
    };

    if(!string.IsNullOrEmpty(calcadoDTO.Marca))
        msg.Mensgens.Add("O campo marca é obrigatório!");

    if(!string.IsNullOrEmpty(calcadoDTO.Modelo))
        msg.Mensgens.Add("O campo modelo é obrigatório!");

    if(calcadoDTO.Preco <= 0)
        msg.Mensgens.Add("O campo não pode estar vazio e o preco não pode ser negativo/igual a zero!");

    if(calcadoDTO.Tamanho < 10)
        msg.Mensgens.Add("O tamanho não pode ser abaixo de 10!");
}

//=================Retornar todos
app.MapGet("/calcados", ([FromBody], ICalcadoServico calcadoServico) => {
 var calcados = calcadoServico.Todos();
    return Results.Ok(calcados);
}).withTags("Calcados")#endregion

//=================Retornar por id
app.MapGet("/calcados/{id}", ([FromRoute] int id, ICalcadoServico calcadoServico) => {
 var calcado = calcadoServico.BuscaPorId(id);
    if(calcado == null) return Results.NotFound();
    return Results.Ok(calcado);
}).withTags("Calcados");

//=================Criar
app.MapPost("/calcados", ([FromBody] CalcadoDTO calcadoDTO, ICalcadoServico calcadoServico) => {
    
    var msg = validaDTO(calcadoDTO);
    if(msg.Mensgens.Count > 0)
    return BadRequest(msg);

    var calcado = new Calcado{
    Marca = calcadoDTO.Marca,
    Modelo = calcadoDTO.Modelo,
    Preco = calcadoDTO.Preco,
    Tamanho = calcadoDTO.Tamanho
    };
    calcadoServico.Adicionar(calcado);
    return Results.Created($"/veiculo/{calcado.Id}", calcado);
}).withTags("Calcados");

app.MapPut("/calcados/{id}", ([FromRoute] int id, ICalcadoServico calcadoServico) => {
    
    var calcado = calcadoServico.BuscaPorId(id);
    if(calcado == null) return Results.NotFound();

    var msg = validaDTO(calcadoDTO);
    if(msg.Mensgens.Count > 0)
    return BadRequest(msg);

    Marca = calcadoDTO.Marca;
    Modelo = calcadoDTO.Modelo;
    Preco = calcadoDTO.Preco;
    Tamanho = calcadoDTO.Tamanho;

    calcadoServico.Atualizar(calcado);

    return Results.Ok(calcado);
}).withTags("Calcados");


app.MapDelete("/calcados/{id}", ([FromRoute] int id, ICalcadoServico calcadoServico) => {
 var calcado = calcadoServico.BuscaPorId(id);
    if(calcado == null) return Results.NotFound();

    calcadoServico.Apagar(calcado);

    return Results.NoContent();
}).withTags("Calcados");

#endregion


#region App

//Usando SwaggerUI configurado
app.UseSwagger();
app.UseSwaggerUI();
app.Run();

#endregion