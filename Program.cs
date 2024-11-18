var builder = WebApplication.CreateBuilder(args);

// Adiciona os serviços necessários para a aplicação
builder.Services.AddControllers();

// Adiciona o Swagger para documentação e testes interativos da API
builder.Services.AddEndpointsApiExplorer(); // Esta linha permite a exploração dos endpoints
builder.Services.AddSwaggerGen(); // Esta linha adiciona o Swagger

var app = builder.Build();

// Configura o pipeline de requisições HTTP
if (app.Environment.IsDevelopment())
{
    // Ativa o Swagger no ambiente de desenvolvimento
    app.UseSwagger(); // Habilita o Swagger
    app.UseSwaggerUI(); // Habilita a interface gráfica do Swagger
}


// Mapeia os controllers da API
app.MapControllers();

// Executa a aplicação
app.Run();
