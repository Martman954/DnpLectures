using Entities;

namespace RepositoryContracts;

public interface IPostRepository
{
    Task<Post> Add(Post post);
    Task Update(Post post);
    Task Delete(int id);
    Task<Post> GetSingle(int id);
    IQueryable<Post> GetMany();
}