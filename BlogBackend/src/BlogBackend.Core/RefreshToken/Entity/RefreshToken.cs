using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogBackend.Core.RefreshToken.Entity;

using BlogBackend.Core.User.Models;

public class RefreshToken
{
    [Required]
    public Guid Token { get; set; }
    [ForeignKey("UserId"), Required]
    public int UserId { get; set; }
    public User? User { get; set; }
}
