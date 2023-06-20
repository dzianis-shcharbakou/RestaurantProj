using Duende.IdentityServer.Services;
using Mango.Services.Identity;
using Mango.Services.Identity.DbContexts;
using Mango.Services.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

var dbConnectionString = string.Empty;
var clientSecret = string.Empty;
if (builder.Environment.IsDevelopment())
{
    dbConnectionString = builder.Configuration["DefaultDbConnection"];
    clientSecret = builder.Configuration["ClientSecret"];
}
else
{
    throw new NotImplementedException();
}

builder.Services.AddDbContext<ApplicationDbContext>(options => {
	options.UseSqlServer(dbConnectionString,
        sqlServerOptions => sqlServerOptions.EnableRetryOnFailure());
});

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
	.AddEntityFrameworkStores<ApplicationDbContext>()
	.AddDefaultTokenProviders();

builder.Services.AddIdentityServer(options =>
{
	options.Events.RaiseErrorEvents = true;
	options.Events.RaiseInformationEvents = true;
	options.Events.RaiseFailureEvents = true;
	options.Events.RaiseSuccessEvents = true;
	options.EmitStaticAudienceClaim = true;
}).AddInMemoryIdentityResources(StaticDetails.Resources)
.AddInMemoryApiScopes(StaticDetails.Scopes)
.AddInMemoryClients(StaticDetails.GetClients(clientSecret))
.AddAspNetIdentity<ApplicationUser>()
.AddDeveloperSigningCredential();

builder.Services.AddScoped<IApplicationDbContextSeed, ApplicationDbContextSeed>();
builder.Services.AddScoped<IProfileService, ProfileService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseIdentityServer();
app.UseAuthorization();

app.MapRazorPages();

using (var scope = app.Services.CreateScope())
{
	var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
	await db.Database.MigrateAsync();

	var applicationDbSeedService = scope.ServiceProvider.GetRequiredService<IApplicationDbContextSeed>();
	await applicationDbSeedService.SeedAsync();
}

app.Run();
