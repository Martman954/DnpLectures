using CLI.UI.ManageComments;
using CLI.UI.ManagePosts;
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
    private PostsManagerView postsManager;
    private CommentsManagerView commentsManager;

    public CliApp(IUserRepository userRepository, ICommentRepository commentRepository, IPostRepository postRepository)
    {
        this.userRepository = userRepository;
        this.commentRepository = commentRepository;
        this.postRepository = postRepository;
        
        usersManager = new UsersManagerView(userRepository);
        postsManager = new PostsManagerView(postRepository);
        commentsManager = new CommentsManagerView(commentRepository);
    }

    public async Task StartAsync()
    {
        
        
        while (true)
        {
            Console.WriteLine("1:Manage Users, 2:Manage Posts, 3:Manage Comments");
            string? input = Console.ReadLine();
        
            if (input != "1" && input != "2" && input != "3")
            {
                Console.Clear();
                continue;
            }
        
            switch (input)
            {
                case "1":
                    await ManageUsersScenario();
                    break;
                case "2":
                    await ManagePostScenario();
                    break;
                case "3":
                    await ManageCommentsScenario();
                    break;
            }
            
        }
    }

    //  In all of these add multiple scenarios, and change them
    private async Task ManageUsersScenario()
    {
        Console.WriteLine("1:Create User, 2:View User, 3:Delete User, 4:List of Users");
        string? input = Console.ReadLine();
        
        if (input != "1" && input != "2" && input != "3" && input != "4")
        {
            Console.Clear();
            return;
        }

        int id;
        switch (input)
        {
            case "1":
                Console.WriteLine("User Creation: name, password, id");
                string? name = Console.ReadLine();
                string? password = Console.ReadLine();
                await usersManager.createUserAsync(name, password);
                Console.WriteLine("User Created: " + name + ", " + password);
                break;
            case "2":
                Console.WriteLine("Reading user: id?");
                id = int.Parse(Console.ReadLine());
                User user = await usersManager.readUserAsync(id);
                Console.WriteLine(user.Username + ", pass: " + user.Password + "(" + user.Id + ")");
                break;
            case "3":
                Console.WriteLine("Deleting user: id?");
                id = int.Parse(Console.ReadLine());
                await usersManager.deleteUserAsync(id);
                break;
            case "4":
                Console.WriteLine("Listing all users...");
                List<User> users = await usersManager.getAllUsers();
                foreach (User x in users)
                {
                    Console.WriteLine(x.Username + ", " + x.Password + "(" + x.Id + ")");
                }
                break;
            default:
                Console.WriteLine("default");
                break;
        }
    }

    private async Task ManagePostScenario()
    {
        Console.WriteLine("1:Create Posts, 2:View Posts, 3:Delete Post, 4:List of Posts");
        string? input = Console.ReadLine();
        
        if (input != "1" && input != "2" && input != "3" && input != "4")
        {
            Console.Clear();
            return;
        }

        int id;
        switch (input)
        {
            case "1":
                Console.WriteLine("Post Creation: title, body, userId");
                string? title = Console.ReadLine();
                string? body = Console.ReadLine();
                int userId = int.Parse(Console.ReadLine()); 
                await postsManager.createPostAsync(title, body, userId);
                Console.WriteLine("Post Created: " + title + " (from: " + usersManager.readUserAsync(userId) + "): \n" + body);
                break;
            case "2":
                Console.WriteLine("Reading post: id?");
                id = int.Parse(Console.ReadLine());
                Post post = await postsManager.readPostAsync(id);
                Console.WriteLine("Post: " + post.Title + " (from: " + usersManager.readUserAsync(post.UserId) + "): \n" + post.Body);
                break;
            case "3":
                Console.WriteLine("Deleting post: id?");
                id = int.Parse(Console.ReadLine());
                await postsManager.deletePostAsync(id);
                break;
            case "4":
                Console.WriteLine("Listing all posts...");
                List<Post> posts = await postsManager.getAllPosts();
                foreach (Post x in posts)
                {
                    Console.WriteLine(x.Title + " (" + x.Id + ")");
                }
                break;
        }
    }

    private async Task ManageCommentsScenario()
    {
        Console.WriteLine("1:Create Comments, 2:View Comment, 3:Delete Comment, 4:List of Comments");
        string? input = Console.ReadLine();
        
        if (input != "1" && input != "2" && input != "3" && input != "4")
        {
            Console.Clear();
            return;
        }

        int id;
        switch (input)
        {
            case "1":
                Console.WriteLine("Comment Creation: body, postId, userId");
                string? body = Console.ReadLine();
                int userId = int.Parse(Console.ReadLine()); 
                int postId = int.Parse(Console.ReadLine()); 
                await commentsManager.createCommentAsync(body, userId, postId);
                Console.WriteLine("Comment Created: " + body + " (from: " + usersManager.readUserAsync(userId) + "), to post " + postsManager.readPostAsync(postId));
                break;
            case "2":
                Console.WriteLine("Reading comment: id?");
                id = int.Parse(Console.ReadLine());
                Comment comment = await commentsManager.readCommentAsync(id);
                Console.WriteLine("Comment from: " + usersManager.readUserAsync(comment.UserId) + "): \n" + comment.Body);
                break;
            case "3":
                Console.WriteLine("Deleting comment: id?");
                id = int.Parse(Console.ReadLine());
                await commentsManager.deleteCommentAsync(id);
                break;
            case "4":
                Console.WriteLine("Listing all comments...");
                List<Comment> comments = await commentsManager.getAllComments();
                foreach (Comment x in comments)
                {
                    Console.WriteLine(usersManager.readUserAsync(x.UserId) + " (" + x.Body + ")");
                }
                break;
        }
    }

}