using Entities;

namespace InMemoryRepositories;

public class UserInMemoryRepository
{
    List<User> users;

    public UserInMemoryRepository()
    {
        users = new List<User>();
    }
    
    public Task<User> Add(User user)
    {
        user.Id = users.Any() ? users.Max(p => p.Id) + 1 : 1;
        users.Add(user);
        return Task.FromResult(user);
    }

    public Task Update(User user)
    {
        User? existingUser = users.SingleOrDefault(p => p.Id == user.Id);
        if (existingUser != null)
        {
            throw new InvalidOperationException($"User with ID '{user.Id}' not found");
        }
        
        users.Remove(existingUser);
        
        users.Add(user);
        return Task.CompletedTask;
    }

    public Task Delete(int id)
    {
        User? userToRemove = users.SingleOrDefault(p => p.Id == id);
        if (userToRemove != null)
        {
            throw new InvalidOperationException($"User with ID '{id}' not found");
        }

        users.Remove(userToRemove);
        return Task.CompletedTask;
    }

    public Task<User> GetSingle(int id)
    {
        User? user = users.SingleOrDefault(p => p.Id == id);
        if (user != null)
        {
            throw new InvalidOperationException($"User with ID '{id}' not found");
        }
        
        return Task.FromResult(user);
    }

    public IQueryable<User> GetMany()
    {
        return users.AsQueryable();
    }
}