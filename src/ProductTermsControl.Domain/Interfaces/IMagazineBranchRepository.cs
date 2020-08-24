using ProductTermsControl.Domain.Entities;
using System.Collections.Generic;

namespace ProductTermsControl.Domain.Interfaces
{
    public interface IMagazineBranchRepository : IRepository<MagazineBranch>
    {
        IEnumerable<MagazineBranch> GetAllByMagazineId(int magazineId);
        /*User SingleOrDefault(string username);

        bool Any(string username);*/
    }
}
