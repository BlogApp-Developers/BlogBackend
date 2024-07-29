namespace BlogBackend.Core.Blog.Repositories.Base;

using BlogBackend.Core.Blog.Models;
using BlogBackend.Core.Base.Methods;

public interface IBlogRepository: ICreateAsync<Blog>, IGetByIdAsync<Blog?>
{   
    public Task<IEnumerable<Blog?>> GetAllByUserIdAsync(int userId);
    public Task<IEnumerable<Blog?>> GetAllByTopicIdAsync(int topicId);
    public Task<IEnumerable<Blog?>> GetAllByNameAsync(string name);
    
}
