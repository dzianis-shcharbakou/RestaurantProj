using Mango.Service.ProductAPI;
using Mango.Service.ProductAPI.DbContexts;
using Mango.Service.ProductAPI.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using Azure.Identity;

var builder = WebApplication.CreateBuilder(args);

var dbConnectionString = string.Empty;

builder.Configuration.AddUserSecrets(Assembly.GetExecutingAssembly(), true);
string appSettingConnectionString = builder.Configuration["ConnectionStrings:AppConfig"];
builder.Configuration.AddAzureAppConfiguration(options =>
{
    options.Connect(appSettingConnectionString)
            .ConfigureKeyVault(kv =>
            {
                kv.SetCredential(new DefaultAzureCredential());
            });
});
var productBlobStorageName = builder.Configuration["ProductBlobStorageName"];

if (builder.Environment.EnvironmentName == "Development")
{
    dbConnectionString = builder.Configuration["ConnectionStrings:Dev:ProductDbConnectionDev"];
}
else
{
    dbConnectionString = builder.Configuration["ConnectionStrings:Dev:ProductDbConnectionDev"];
}

ApplicationContext.ProductBlobStorageName = productBlobStorageName;

// Add services to the container.
builder.Services.AddDbContext<ApplicationContext>(options =>
{
	options.UseSqlServer(dbConnectionString);
});

//builder.Services.BuildServiceProvider().GetService<ApplicationContext>().Database.Migrate();

var mapper = MappingConfig.GetMapperConfiguration();
builder.Services.AddSingleton(mapper);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddTransient<IProductRepository, ProductRepository>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo 
	{ 
		Title = $"Mango.Services.ProductAPI {builder.Environment.EnvironmentName}", 
		Version = "v1"
    });
	options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
	{
		Description = @"Enter 'Bearer' [space] and your token",
		Name = "Authorization",
		In = ParameterLocation.Header,
		Type = SecuritySchemeType.ApiKey,
		Scheme = "Bearer"
	});
	options.AddSecurityRequirement(new OpenApiSecurityRequirement
	{
		{
			new OpenApiSecurityScheme
			{
				Reference = new OpenApiReference
				{
					Type = ReferenceType.SecurityScheme,
					Id = "Bearer"
				},
				Scheme = "oauth2",
				Name = "Bearer",
				In = ParameterLocation.Header
			},
			new List<string>()
		},
	});
});

builder.Services.AddAuthentication("Bearer")
	.AddJwtBearer("Bearer", options =>
	{
		options.Authority = "https://localhost:7077";
		options.TokenValidationParameters = new TokenValidationParameters
		{
			ValidateAudience = false,
		};
	});

builder.Services.AddAuthorization(options =>
{
	options.AddPolicy("ApiScope", policy =>
	{
		policy.RequireAuthenticatedUser();
		policy.RequireClaim("scope", "mango");
	});
});

var app = builder.Build();

app.UseSwagger();

app.UseSwaggerUI(c =>
{
	c.SwaggerEndpoint("/swagger/v1/swagger.json", "Product API V1");
});

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
