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

    }

    public async Task<Comment> createCommentAsync(string body,int postId, int userId)
    {
        Comment comment = null;
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