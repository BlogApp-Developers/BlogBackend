namespace BlogBackend.Core.UserTopic.Repositories.Base;

using BlogBackend.Core.UserTopic.Models;
using BlogBackend.Core.Topic.Models;
using BlogBackend.Core.Base.Methods;

public interface IUserTopicRepository : ICreateAsync<UserTopic>, IDeleteAsync<UserTopic>
{
    public Task<IEnumerable<Topic?>> GetAllTopicsByUserIdAsync(int userId);
    public Task CreateListAsync(IEnumerable<UserTopic> userTopics);
}
