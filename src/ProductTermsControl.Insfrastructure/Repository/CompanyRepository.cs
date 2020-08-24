using ProductTermsControl.Domain.Entities;
using ProductTermsControl.Domain.Interfaces;
using ProductTermsControl.Insfrastructure.Helpers;
using System.Linq;

namespace ProductTermsControl.Insfrastructure.Repository
{
    public class CompanyRepository : Repository<Company>, ICompanyRepository
    {
        public CompanyRepository(DataContext context)
            : base(context)
        {

        }

        /*public User SingleOrDefault(string username)
        {
            return DbSet.SingleOrDefault(x => x.Username == username);
        }

        public bool Any(string username)
        {
            return DbSet.Any(x => x.Username == username);
        }*/
    }
}
