using Microsoft.EntityFrameworkCore;
using ProductTermsControl.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProductTermsControl.Application.ApplicationDbContext
{
    public interface IApplicationDbContext
    {
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
        public DbSet<UserActivity> UserActivities { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }

        Task<int> SaveChangesAsync();
    }
}
