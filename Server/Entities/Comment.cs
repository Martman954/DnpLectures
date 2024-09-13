namespace Entities;

public class Comment
{
    public int Id { get; set; }
    public string Body { get; set; }
    public int PostId { get; private set; }
    public int UserId { get; private set; }

    public Comment(string body, int id, int postId, int userId)
    {
        this.Body = body;
        id = Id;
        this.PostId = postId;
        this.UserId = userId;
    }
}