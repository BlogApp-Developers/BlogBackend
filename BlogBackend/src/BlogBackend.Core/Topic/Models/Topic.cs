namespace BlogBackend.Core.Topic.Models;

using System.ComponentModel.DataAnnotations;
using BlogBackend.Core.UserTopic.Models;

public class Topic
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string? Name { get; set; }
    public IEnumerable<UserTopic>? Users {set; get;}
}
