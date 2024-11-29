using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using RepositoryContracts;

namespace EfcRepositories;

public class EfcPostRepository : IPostRepository
{
    private readonly AppContext ctx;

    public EfcPostRepository(AppContext ctx)
    {
        this.ctx = ctx;
    }

    public async Task<Post> AddAsync(Post post)
    {
        EntityEntry<Post> entityEntry = await ctx.Posts.AddAsync(post);
        await ctx.SaveChangesAsync();
        return entityEntry.Entity;
    }

    public async Task<Post> UpdateAsync(Post post)
    {
        if (!(await ctx.Posts.AnyAsync(p => p.Id == post.Id)))
        {
            throw new DirectoryNotFoundException("Post with id {post.Id} not found");
        }

        ctx.Posts.Update(post);
        await ctx.SaveChangesAsync();
        return post;
    }

    public async Task<Post> DeleteAsync(int id)
    {
        Post? existing = await ctx.Posts.SingleOrDefaultAsync(p => p.Id == id);
        if (existing == null)
        {
            throw new DirectoryNotFoundException($"Post with id {id} not found");
        }

        ctx.Posts.Remove(existing);
        await ctx.SaveChangesAsync();
        return existing;
    }

    public async Task<Post> GetSingleAsync(int id)
    {
        Post? post = ctx.Posts.SingleOrDefault(p => p.Id == id);
        if (post == null)
        {
            throw new DirectoryNotFoundException($"Post with id {id} not found");
        }

        return post;
    }

    public IQueryable<Post> GetManyAsync()
    {
        foreach (var post in ctx.Posts.ToList()) // Executes the query
        {
            Console.WriteLine($"Post ID: {post.Id}, Name: {post.Title}, UserId: {post.UserId}");
        }
        return ctx.Posts.AsQueryable();
    }
}