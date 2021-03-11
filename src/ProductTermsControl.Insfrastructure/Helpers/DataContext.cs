using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ProductTermsControl.Application.ApplicationDbContext;
using ProductTermsControl.Domain;
using ProductTermsControl.Domain.Entities;
using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace ProductTermsControl.Insfrastructure.Helpers
{
    public class DataContext : DbContext, IApplicationDbContext
    {
        protected readonly IConfiguration Configuration;

        public DataContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to sql server database
            options.UseMySQL(Configuration.GetConnectionString("WebApiDatabase"));
        }
        //Main Migration and Invoke PM Code: Add-Migration <Name > -Context DataContext -Project ProductTermsControl.Insfrastructure
        public DbSet<User> Users { get; set; }
        public DbSet<Company> Companys { get; set; }
        public DbSet<Magazine> Magazines { get; set; }
        public DbSet<MagazineBranch> MagazineBranches { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductToBranch> ProductToBranches { get; set; }
        public DbSet<ResponsiblePersonsForProduct> ResponsiblePersonsForProducts { get; set; }
        public DbSet<ResponsiblePersonsGroup> ResponsiblePersonsGroups { get; set; }
        public DbSet<UserReference> UserReferences { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<BranchProductStock> BranchProductStocks { get; set; }
        public DbSet<ReasonForOutOfStock> ReasonForOutOfStocks { get; set; }


        public async Task<int> SaveChangesAsync()
        {
            var entries = ChangeTracker.Entries();
            var Now = DateTime.Now;

            foreach (var entry in entries)
            {
                
                if (entry.Entity is BaseEntity trackable)
                {
                    switch (entry.State)
                    {
                        case EntityState.Modified:
                           
                            trackable.UpdateDate = Now;
                            
                            entry.Property("CreateDate").IsModified = false;
                            trackable.CreateDate = DateTime.Parse(entry.GetDatabaseValues().GetValue<object>("CreateDate").ToString());

                            /*foreach (var property in entry.Properties)
                            {
                                if (property.IsModified)
                                {
                                    var original = entry.GetDatabaseValues().GetValue<object>(property.Metadata.Name);
                                    var current = property.CurrentValue;
                                }
                                
                            }*/
                            break;

                        case EntityState.Added:
                           
                            trackable.CreateDate = Now;
                            trackable.UpdateDate = Now;
                            break;
                    }
                }
            }
        


            return await base.SaveChangesAsync();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }

       
    }
}