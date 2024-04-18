using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Sistema.Bico.Domain.Entities;
using Sistema.Bico.Domain.Generics.Entities;
using Sistema.Bico.Infra.Mappers;

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

                optionsBuilder.UseNpgsql(GetStringConectionConfig());
                base.OnConfiguring(optionsBuilder);
            }
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


        private string GetStringConectionConfig()
        {
            //string strCon = "Server=localhost;Port=5432;User Id=postgres;Password=@leteelias23;Database=db_workfree;";
            //string strCon = "Host=localhost;Port=5433;Database=db_workfree;Username=postgres;Password=@Abc123;Pooling=true";
            string strCon = "Host=localhost;Port=5433;Database=db_workfree;Username=postgres;Password=@Abc123;Pooling=true";
            // string strCon = "Data Source = SQL5107.site4now.net; Initial Catalog = db_a93e2a_bico; User Id = db_a93e2a_bico_admin; Password = wshKNfSxEVup7L5j";

            return strCon;
        }

    }
}
