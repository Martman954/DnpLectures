using Entities;

namespace RepositoryContracts;

public interface IUserRepository
{
    Task<User> Add(User user);
    Task Update(User user);
    Task Delete(int id);
    Task<User> GetSingle(int id);
    IQueryable<User> GetMany();
}