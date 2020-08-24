using ProductTermsControl.Domain.Entities;
using ProductTermsControl.Domain.HelperModel;
using ProductTermsControl.Domain.Interfaces;
using ProductTermsControl.Insfrastructure.Helpers;
using System.Linq;

namespace ProductTermsControl.Insfrastructure.Repository
{
    public class CommonRepository :  ICommonRepository
    {
        protected readonly DataContext Db;

        public CommonRepository(DataContext context)
        {
            Db = context;
        }
        public IQueryable<ProductWithTerm> GetAllProductByBranchId(int branchId)
        {
            var GetProducts = Db.ProductToBranches.Join(
                                                            Db.Products,
                                                            PB => PB.ProductId,
                                                            P => P.Id,
                                                            (PB,P) => new ProductWithTerm { ProductToBranch = PB, Product = P}
                                                        )
                                                        .Where(pb=>pb.ProductToBranch.MagazineBranchId == branchId);
            return GetProducts;
        }
        public ProductToBranch GetProductViewTermByBranchId(int branchId, int productId)
        {
            var GetProducts = Db.ProductToBranches.Join(
                                                            Db.Products,
                                                            PB => PB.ProductId,
                                                            P => P.Id,
                                                            (PB, P) => new { ProductToBranch = PB, Product = P }
                                                        )
                                                        .Where(pb => pb.ProductToBranch.MagazineBranchId == branchId && pb.ProductToBranch.ProductId== productId)
                                                        .FirstOrDefault().ProductToBranch;
            return GetProducts;
        }


        public IQueryable<ProductWithTerm> GetAllProductByBranchIdAndResponsibleId(int branchId, int userId)
        {
            var GetProducts = from PB in Db.ProductToBranches
                              join P in Db.Products on PB.ProductId equals P.Id
                              join RPG in Db.ResponsiblePersonsGroups on PB.ResponsiblePersonsGroupId equals RPG.Id
                              join RPP in Db.ResponsiblePersonsForProducts on RPG.Id equals RPP.ResponsiblePersonsGroupId
                              where PB.MagazineBranchId == branchId && RPP.UserId == userId
                              select new ProductWithTerm { Product = P, ProductToBranch = PB };
            return GetProducts;
        }
        
       
    }
}
