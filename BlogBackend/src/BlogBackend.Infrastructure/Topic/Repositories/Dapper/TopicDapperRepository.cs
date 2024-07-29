using Dapper;

namespace BlogBackend.Infrastructure.Topic.Repositories.Dapper;

using Npgsql;
using BlogBackend.Core.Topic.Repositories.Base;
using BlogBackend.Core.Topic.Models;
using System.Collections.Generic;

public class TopicDapperRepository : ITopicRepository
{
    private readonly string connectionString;
    public TopicDapperRepository(string connectionString)
    {
        this.connectionString = connectionString;
    }

    public async Task<IEnumerable<Topic?>> GetAllAsync()
    {
        using var connection = new NpgsqlConnection(connectionString);
        var topics = await connection.QueryAsync<Topic>(@"select * from ""Topics""");

        return topics;
    }

    public async Task<Topic?> GetByIdAsync(int id)
    {
        using var connection = new NpgsqlConnection(connectionString);
        var topics = await connection.QueryAsync<Topic>(@"select * from ""Topics"" where ""Id"" = @Id", new {
            Id = id,
        });

        return topics.FirstOrDefault();
    }
}
