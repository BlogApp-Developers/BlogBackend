using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogBackend.Core.Follow.Models;

using BlogBackend.Core.User.Models;

public class Follow
{
    [Key]
    public int Id { get; set; }
    [ForeignKey(name: "FollowerId"), Required]
    public required int FollowerId { get; set; }
    public User? Follower { get; set; }

    [ForeignKey(name: "FollowingId"), Required]
    public required int FollowingId { get; set; }
    public User? Following { get; set; }
}
