namespace BlogBackend.Infrastructure.Blog.Handlers;

using System.Threading;
using System.Threading.Tasks;
using BlogBackend.Core.Blog.Repositories.Base;
using BlogBackend.Core.Blog.Models;
using BlogBackend.Infrastructure.Blog.Queries;
using MediatR;

public class GetAllByNameHandler : IRequestHandler<GetAllByNameQuery, IEnumerable<Blog?>>
{
    private readonly IBlogRepository repository;
    public GetAllByNameHandler(IBlogRepository repository)
    {
        this.repository = repository;
    }

    public async Task<IEnumerable<Blog?>> Handle(GetAllByNameQuery request, CancellationToken cancellationToken)
    {
        if(string.IsNullOrEmpty(request.Name) || string.IsNullOrWhiteSpace(request.Name)) {
            throw new ArgumentException("incorrect param of search");
        }

        return await repository.GetAllByNameAsync(request.Name);
    }
}