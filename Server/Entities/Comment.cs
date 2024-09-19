namespace Entities;

public class Comment
{
    public int Id { get; set; }
    public string Body { get; set; }
    public int PostId { get; private set; }
    public int UserId { get; private set; }

    public Comment(string body, int postId, int userId)
    {
        this.Body = body;
        this.PostId = postId;
        this.UserId = userId;
    }
}