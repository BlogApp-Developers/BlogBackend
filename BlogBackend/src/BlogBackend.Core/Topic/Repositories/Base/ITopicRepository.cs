namespace BlogBackend.Core.Topic.Repositories.Base;

using BlogBackend.Core.Topic.Models;
using BlogBackend.Core.Base.Methods;

public interface ITopicRepository : IGetByIdAsync<Topic?>, IGetAllAsync<Topic?>
{
    
}
