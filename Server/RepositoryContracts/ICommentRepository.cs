using Entities;

namespace RepositoryContracts;

public interface ICommentRepository
{
    Task<Comment> Add(Comment comment);
    Task Update(Comment comment);
    Task Delete(int id);
    Task<Comment> GetSingle(int id);
    IQueryable<Comment> GetMany();
}