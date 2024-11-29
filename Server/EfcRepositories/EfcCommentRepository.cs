using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using RepositoryContracts;

namespace EfcRepositories;

public class EfcCommentRepository : ICommentRepository
{
    private readonly AppContext ctx;

    public EfcCommentRepository(AppContext ctx)
    {
        this.ctx = ctx;
    }
    public async Task<Comment> AddAsync(Comment comment)
    {
        EntityEntry<Comment> entityEntry = await ctx.Comments.AddAsync(comment);
        await ctx.SaveChangesAsync();
        return entityEntry.Entity;
    }

    public async Task<Comment> UpdateAsync(Comment comment)
    {
        if (!(await ctx.Comments.AnyAsync(c => c.Id == comment.Id)))
        {
            throw new DirectoryNotFoundException("Comment with id {post.Id} not found");
        }

        ctx.Comments.Update(comment);
        await ctx.SaveChangesAsync();
        return comment;
    }

    public async Task<Comment> DeleteAsync(int id)
    {
        Comment? existing = await ctx.Comments.SingleOrDefaultAsync(c => c.Id == id);
        if (existing == null)
        {
            throw new DirectoryNotFoundException($"Comment with id {id} not found");
        }

        ctx.Comments.Remove(existing);
        await ctx.SaveChangesAsync();
        return existing;
    }

    public async Task<Comment> GetSingleAsync(int id)
    {
        Comment? comment = await ctx.Comments.SingleOrDefaultAsync(c => c.Id == id);
        if (comment == null)
        {
            throw new DirectoryNotFoundException($"Comment with id {id} not found");
        }
        return comment;
    }

    public IQueryable<Comment> GetManyAsync()
    {
        return ctx.Comments.AsQueryable();
    }
}