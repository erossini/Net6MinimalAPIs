namespace MinimalApis.Data;

public class ClientContext : DbContext
{
    private readonly IConfiguration _config;

    public ClientContext(DbContextOptions options, IConfiguration config)
        : base(options)
    {
        _config = config;
    }

    public DbSet<Client> Clients => Set<Client>();
    public DbSet<Address> Addresses => Set<Address>();
    public DbSet<Case> Cases => Set<Case>();

    protected override void OnModelCreating(ModelBuilder bldr)
    {
        base.OnModelCreating(bldr);

        bldr.Entity<Client>()
          .HasOne(c => c.Address);
    }
}