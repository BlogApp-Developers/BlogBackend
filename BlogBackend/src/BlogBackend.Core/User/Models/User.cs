namespace BlogBackend.Core.User.Models;

using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using BlogBackend.Core.UserTopic.Models;

public class User : IdentityUser<int>
{
    [Required]
    public string? Name { get; set; }
    public string? AvatarUrl { get; set; }
    public IEnumerable<UserTopic>? Topics {set; get;}
}
