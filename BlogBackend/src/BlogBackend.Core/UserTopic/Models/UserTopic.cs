namespace BlogBackend.Core.UserTopic.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BlogBackend.Core.User.Models;
using BlogBackend.Core.Topic.Models;

public class UserTopic
{
    [Key]
    public int Id { get; set; }
    [ForeignKey("TopicId"), Required]
    public int TopicId { get; set; }
    public Topic? Topic { get; set; }
    [ForeignKey("UserId"), Required]
    public int UserId { get; set; }
    public User? User { get; set; }
}
