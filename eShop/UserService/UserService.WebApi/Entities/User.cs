namespace UserService.WebApi.Entities
{
  using System.ComponentModel.DataAnnotations;

  public class User
  {
    [Key]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public string Mail { get; set; }
  }
}
