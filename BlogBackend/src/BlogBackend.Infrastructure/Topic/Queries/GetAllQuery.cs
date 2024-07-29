namespace BlogBackend.Infrastructure.Topic.Queries;

using MediatR;
using BlogBackend.Core.Topic.Models;

public class GetAllQuery : IRequest<IEnumerable<Topic?>>
{
    
}