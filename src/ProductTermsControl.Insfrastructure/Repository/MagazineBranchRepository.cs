using ProductTermsControl.Domain.Entities;
using ProductTermsControl.Domain.Interfaces;
using ProductTermsControl.Insfrastructure.Helpers;
using System.Collections.Generic;
using System.Linq;

namespace ProductTermsControl.Insfrastructure.Repository
{
    public class MagazineBranchRepository : Repository<MagazineBranch>, IMagazineBranchRepository
    {
        public MagazineBranchRepository(DataContext context)
            : base(context)
        {

        }
        public IEnumerable<MagazineBranch> GetAllByMagazineId(int magazineId)
        {
            return DbSet.Where(m => m.MagazineId == magazineId);
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
