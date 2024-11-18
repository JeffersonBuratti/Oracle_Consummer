var builder = WebApplication.CreateBuilder(args);

// Adiciona os servi�os necess�rios para a aplica��o
builder.Services.AddControllers();

// Adiciona o Swagger para documenta��o e testes interativos da API
builder.Services.AddEndpointsApiExplorer(); // Esta linha permite a explora��o dos endpoints
builder.Services.AddSwaggerGen(); // Esta linha adiciona o Swagger

var app = builder.Build();

// Configura o pipeline de requisi��es HTTP
if (app.Environment.IsDevelopment())
{
    // Ativa o Swagger no ambiente de desenvolvimento
    app.UseSwagger(); // Habilita o Swagger
    app.UseSwaggerUI(); // Habilita a interface gr�fica do Swagger
}


// Mapeia os controllers da API
app.MapControllers();

// Executa a aplica��o
app.Run();
