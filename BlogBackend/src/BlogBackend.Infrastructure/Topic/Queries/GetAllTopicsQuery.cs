namespace BlogBackend.Infrastructure.Topic.Queries;

using MediatR;
using BlogBackend.Core.Topic.Models;

public class GetAllTopicsQuery : IRequest<IEnumerable<Topic?>>
{
    
}