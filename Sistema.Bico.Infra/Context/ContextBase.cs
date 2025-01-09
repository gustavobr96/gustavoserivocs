using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Sistema.Bico.Domain.Entities;
using Sistema.Bico.Domain.Generics.Entities;
using Sistema.Bico.Infra.Mappers;
using System.IO;

namespace Sistema.Bico.Infra.Context
{
    public class ContextBase : IdentityDbContext<ApplicationUser>
    {
        public ContextBase(DbContextOptions<ContextBase> options) : base(options)
        { }
        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        public DbSet<Client> Client { get; set; }
        public DbSet<ProfessionalProfile> ProfessionalProfile { get; set; }
        public DbSet<ProfessionalArea> ProfessionalArea { get; set; }
        public DbSet<ProfessionalEspeciality> ProfessionalEspeciality { get; set; }
        public DbSet<TermUse> TermUse { get; set; }
        public DbSet<Address> Address { get; set; }
        public DbSet<Worker> Worker { get; set; }
        public DbSet<WorkerProfessional> WorkerProfessional { get; set; }
        public DbSet<ProfessionalClient> ProfessionalClient { get; set; }
        public DbSet<WorkerDoneProfessional> WorkerDoneProfessional { get; set; }
        public DbSet<WorkerDone> WorkerDone { get; set; }
        public DbSet<ThreeAvaliation> ThreeAvaliation { get; set; }
        public DbSet<ProfessionalPayment> ProfessionalPayment { get; set; }
        public DbSet<Template> Template { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .Build();

                var connectionString = configuration.GetConnectionString("DefaultConnection");

                optionsBuilder.UseNpgsql(connectionString);
            }

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new ClientMap());
            builder.ApplyConfiguration(new ApplicationUserMap());
            builder.ApplyConfiguration(new ProfessionalAreaMap());
            builder.ApplyConfiguration(new ProfessionalProfileMap());
            builder.ApplyConfiguration(new ProfessionalEspecialityMap());
            builder.ApplyConfiguration(new AddressMap());
            builder.ApplyConfiguration(new TermUseMap());
            builder.ApplyConfiguration(new WorkerMap());
            builder.ApplyConfiguration(new WorkerProfessionalMap());
            builder.ApplyConfiguration(new ProfessionalClientMap());
            builder.ApplyConfiguration(new ThreeAvaliationMap());
            builder.ApplyConfiguration(new WorkerDoneProfessionalMap());
            builder.ApplyConfiguration(new WorkerDoneMap());
            builder.ApplyConfiguration(new ProfessionalPaymentMap());
            builder.ApplyConfiguration(new TemplateMap());

            base.OnModelCreating(builder);
        }
    }
}
