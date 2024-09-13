namespace Entities;

public class Post
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Body { get; set; }

    public Post(string title, string body, int id)
    {
        this.Title = title;
        this.Body = body;
        this.Id = id;
    }
}