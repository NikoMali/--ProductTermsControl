using ProductTermsControl.Domain.Entities;
using ProductTermsControl.Domain.HelperModel;
using System.Linq;

namespace ProductTermsControl.Domain.Interfaces
{
    public interface ICommonRepository
    {
        IQueryable<ProductWithTerm> GetAllProductByBranchId(int branchId);
        ProductToBranch GetProductViewTermByBranchId(int branchId, int productId);
        IQueryable<ProductWithTerm> GetAllProductByBranchIdAndResponsibleId(int branchId, int userId);
        /*User SingleOrDefault(string username);

        bool Any(string username);*/
    }
}
