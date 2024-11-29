using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using RepositoryContracts;

namespace EfcRepositories;

public class EfcUserRepository : IUserRepository
{
    private readonly AppContext ctx;

    public EfcUserRepository(AppContext ctx)
    {
        this.ctx = ctx;
    }
    public async Task<User> AddAsync(User user)
    {
        EntityEntry<User> entityEntry = await ctx.Users.AddAsync(user);
        await ctx.SaveChangesAsync();
        return entityEntry.Entity;
    }

    public async Task<User> UpdateAsync(User user)
    {
        if (!(await ctx.Users.AnyAsync(u => u.Id == user.Id)))
        {
            throw new DirectoryNotFoundException("User with id {post.Id} not found");
        }

        ctx.Users.Update(user);
        await ctx.SaveChangesAsync();
        return user;
    }

    public async Task<User> DeleteAsync(int id)
    {
        User? existing = await ctx.Users.SingleOrDefaultAsync(u => u.Id == id);
        if (existing == null)
        {
            throw new DirectoryNotFoundException($"User with id {id} not found");
        }

        ctx.Users.Remove(existing);
        await ctx.SaveChangesAsync();
        return existing;
    }

    public async Task<User> GetSingleAsync(int id)
    {
        User? user = await ctx.Users.SingleOrDefaultAsync(u => u.Id == id);
        if (user == null)
        {
            throw new DirectoryNotFoundException($"User with id {id} not found");
        }

        return user;
    }

    public IQueryable<User> GetManyAsync()
    {
        foreach (var user in ctx.Users.ToList()) // Executes the query
        {
            Console.WriteLine($"User ID: {user.Id}, Name: {user.Username}");
        }
        return ctx.Users.AsQueryable();
    }
}