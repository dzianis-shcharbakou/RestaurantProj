namespace Mango.Services.Identity.DbContexts
{
	public interface IApplicationDbContextSeed
	{
		Task SeedAsync();
	}
}