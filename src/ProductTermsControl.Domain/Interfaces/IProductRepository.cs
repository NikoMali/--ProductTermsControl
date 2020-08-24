using ProductTermsControl.Domain.Entities;


namespace ProductTermsControl.Domain.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        /*User SingleOrDefault(string username);

        bool Any(string username);*/
    }
}
