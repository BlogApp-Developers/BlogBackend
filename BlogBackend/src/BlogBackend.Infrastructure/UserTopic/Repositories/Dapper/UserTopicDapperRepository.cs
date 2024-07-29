using Dapper;

namespace BlogBackend.Infrastructure.UserTopic.Repositories.Dapper;

using Npgsql;
using BlogBackend.Core.UserTopic.Repositories.Base;
using BlogBackend.Core.UserTopic.Models;
using BlogBackend.Core.Topic.Models;

public class UserTopicDapperRepository : IUserTopicRepository
{
    private readonly string connectionString;
    public UserTopicDapperRepository(string connectionString)
    {
        this.connectionString = connectionString;
    }

    public async Task CreateAsync(UserTopic userTopic)
    {
        using var connection = new NpgsqlConnection(connectionString);
        await connection.ExecuteAsync(@"insert into ""UserTopics""
                                    (""UserId"", ""TopicId"") values (@UserId, @TopicId)", userTopic);
    }

    public Task CreateListAsync(IEnumerable<UserTopic> userTopics)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteAsync(UserTopic userTopic)
    {
        using var connection = new NpgsqlConnection(connectionString);
        await connection.ExecuteAsync(@"delete from ""UserTopics""
                                    where ""UserId"" = @UserId, ""TopicId"" = @TopicId)", userTopic);
    }

    public async Task<IEnumerable<Topic?>> GetAllTopicsByUserIdAsync(int userId)
    {
        using var connection = new NpgsqlConnection(connectionString);
        var topics = await connection.QueryAsync<Topic>(@"select T.""Id"", T.""Name"" from ""Topics"" T join ""UserTopics"" U on U.""TopicId"" = T.""Id"" where ""UserId"" = @UserId", new {
            UserId = userId,
        });

        return topics;
    }
}
