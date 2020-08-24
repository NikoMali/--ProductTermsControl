using ProductTermsControl.Domain.Entities;


namespace ProductTermsControl.Domain.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        User SingleOrDefault(string username);

        bool Any(string username);
    }
}
