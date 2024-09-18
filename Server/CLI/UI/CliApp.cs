using CLI.UI.ManageUsers;
using Entities;
using RepositoryContracts;

namespace CLI.UI;

public class CliApp
{
    private IUserRepository userRepository;
    private ICommentRepository commentRepository;
    private IPostRepository postRepository;
    
    private UsersManagerView usersManager;

    public CliApp(IUserRepository userRepository, ICommentRepository commentRepository, IPostRepository postRepository)
    {
        this.userRepository = userRepository;
        this.commentRepository = commentRepository;
        this.postRepository = postRepository;
        
        usersManager = new UsersManagerView(userRepository);
        
    }

    public async Task StartAsync()
    {
        await usersManager.createUserAsync("Samo","Susa");
        await usersManager.createUserAsync("Matej","Palas");

        
        while (true)
        {
            Console.WriteLine("1:Create, 2:View, 3:Delete, 4:List ");
            string? input = Console.ReadLine();
        
            if (input != "1" && input != "2" && input != "3" && input != "4")
            {
                Console.Clear();
                continue;
            }
        
            switch (input)
            {
                case "1":
                    await UserCreateScenario();
                    break;
                case "2":
                    await UserViewScenario();
                    break;
                case "3":
                    await UserDeleteScenario();
                    break;
                case "4":
                    await UserViewListScenario();
                    break;
                default:
                    Console.WriteLine("default");
                    break;
            }
            
        }
    }

    private async Task UserViewListScenario()
    {
        Console.WriteLine("Listing all users...");
        List<User> users = await usersManager.getAllUsers();
        foreach (User x in users)
        {
            Console.WriteLine(x.Username + ", " + x.Password + "(" + x.Id + ")");
        }
    }

    private async Task UserDeleteScenario()
    {
        int id;
        Console.WriteLine("Deleting user: id?");
        id = int.Parse(Console.ReadLine());
        await usersManager.deleteUserAsync(id);
    }

    private async Task UserViewScenario()
    {
        Console.WriteLine("Reading user: id?");
        int id = int.Parse(Console.ReadLine());
        User user = await usersManager.readUserAsync(id);
        Console.WriteLine(user.Username + ", pass: " + user.Password + "(" + user.Id + ")");
    }

    private async Task UserCreateScenario()
    {
        Console.WriteLine("User Creation: name, password, id");
        string? name = Console.ReadLine();
        string? password = Console.ReadLine();
        await usersManager.createUserAsync(name, password);
        Console.WriteLine("User Created: " + name + ", " + password);
    }
}