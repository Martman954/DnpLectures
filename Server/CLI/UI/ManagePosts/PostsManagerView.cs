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
        _ = createPostAsync("MisterFirstPost", "BlaBLALDALDASPdAdapDASMDPASMDSKAFMASSAFMAFA", 1);
        _ = createPostAsync("SecondPpost", "SomeStufffdIdkWhatTO Wtrirjte amz,pre", 3);
        _ = createPostAsync("OmfgTHirdPme", "WordsOWrldsOWrkdsWORkds", 2);
        _ = createPostAsync("ItsSooLateIwannasleep", "!SDSDJIOASJDIASFJOASFJIOAJFOPAFOPA", 4);
    }


    public async Task<Post> createPostAsync(string title, string body, int userId)
    {
        Post post = new Post(title, body, userId);
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