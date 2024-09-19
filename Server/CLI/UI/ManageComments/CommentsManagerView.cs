using Entities;
using RepositoryContracts;

namespace CLI.UI.ManageComments;

public class CommentsManagerView
{
    private ICommentRepository commentRepository;

    public CommentsManagerView(ICommentRepository commentRepository)
    {
        this.commentRepository = commentRepository;

        DummyData();
    }

    private void DummyData()
    {
        _ = createCommentAsync("This Poste is shit", 2, 1);
        _ = createCommentAsync("Quite Good!", 1, 3);
        _ = createCommentAsync("@$@$%#^%@$", 3, 4);
        _ = createCommentAsync("OOH NOOO", 4, 2);
    }

    public async Task<Comment> createCommentAsync(string body,int postId, int userId)
    {
        Comment comment = new Comment(body, postId, userId);
        return await commentRepository.AddAsync(comment);
    }

    public async Task<Comment> readCommentAsync(int id)
    {
        return await commentRepository.GetSingleAsync(id);
    }
    
    public async Task deleteCommentAsync(int id)
    {
        await commentRepository.DeleteAsync(id);
    }

    public Task<List<Comment>> getAllComments()
    {
        return Task.FromResult(commentRepository.GetManyAsync().ToList());
    }
}