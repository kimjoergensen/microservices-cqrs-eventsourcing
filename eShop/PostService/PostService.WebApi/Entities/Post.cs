namespace PostService.WebApi.Entities
{
  using System.ComponentModel.DataAnnotations;

  public class Post
  {
    [Key]
    public int Id { get; set; }
    [Required]
    public string Title { get; set; }
    public string Content { get; set; }

    public int UserId { get; set; }
    public User User { get; set; }
  }
}
