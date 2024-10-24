using CLI.UI.ManageUsers;
using Entities;
using RepositoryContracts;

namespace CLI.UI.ManagePosts;

public class PostsManagerView
{
    private IPostRepository postRepository;

    public PostsManagerView(IPostRepository postRepository)
    {
        this.postRepository = postRepository;

        DummyData();
    }

    private void DummyData()
    {

    }


    public async Task<Post> createPostAsync(string title, string body, int userId)
    {
        Post post = null;
        return await postRepository.AddAsync(post);
    }

    public async Task<Post> readPostAsync(int id)
    {
        return await postRepository.GetSingleAsync(id);
    }
    
    public async Task deletePostAsync(int id)
    {
        await postRepository.DeleteAsync(id);
    }

    public Task<List<Post>> getAllPosts()
    {
        return Task.FromResult(postRepository.GetManyAsync().ToList());
    }
}