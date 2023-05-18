var builder = WebApplication.CreateBuilder(args);

var dbConnectionString = string.Empty;
if (builder.Environment.IsDevelopment())
{
    dbConnectionString = builder.Configuration["ShoppingCartDbConnection"];
}
else
{
    throw new NotImplementedException();
}

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
