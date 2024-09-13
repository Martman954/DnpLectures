namespace Entities;

public class Comment
{
    public int Id { get; set; }
    public string Body { get; set; }

    public Comment(string body, int id)
    {
        this.Body = body;
        id = Id;
    }
}