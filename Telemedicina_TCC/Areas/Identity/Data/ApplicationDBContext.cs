using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Telemedicina_TCC.Areas.Identity.Data;
using Telemedicina_TCC.Models;

namespace Telemedicina_TCC.Data;

public class ApplicationDBContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options)
        : base(options)
    {
    }

    public DbSet<Atendimentos> Atendimentos { get; set; }
    public DbSet<Triagens> Triagens { get; set; }
    public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<ApplicationUser>().ToTable(nameof(ApplicationUsers)).HasKey(t => t.Id);
    }
}
