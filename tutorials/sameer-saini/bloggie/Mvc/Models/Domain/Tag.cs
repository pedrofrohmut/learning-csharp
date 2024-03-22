namespace Bloggie.Mvc.Models.Domain;

public class Tag
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string DisplayName { get; set; }

    // N to N ref
    public ICollection<BlogPost> BlogPosts { get; set; }
}
