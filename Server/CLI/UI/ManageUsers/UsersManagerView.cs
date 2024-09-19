using Entities;
using RepositoryContracts;

namespace CLI.UI.ManageUsers;

public class UsersManagerView
{
    private IUserRepository userRepository;

    public UsersManagerView(IUserRepository userRepository)
    {
        this.userRepository = userRepository;

        DummyData();
    }

    private void DummyData()
    {
        _ = createUserAsync("Marty", "Party");
        _ = createUserAsync("Torpedo", "Fifty");
        _ = createUserAsync("Samo", "Susic");
        _ = createUserAsync("Gengster", "Prankster");
    }

    public async Task<User> createUserAsync(string name, string password)
    {
        User user = new User(name,password);
        return await userRepository.AddAsync(user);
    }

    public async Task<User> readUserAsync(int id)
    {
        return await userRepository.GetSingleAsync(id);
    }
    
    public async Task deleteUserAsync(int id)
    {
        await userRepository.DeleteAsync(id);
    }

    public Task<List<User>> getAllUsers()
    {
        return Task.FromResult(userRepository.GetManyAsync().ToList());
    }
    
}