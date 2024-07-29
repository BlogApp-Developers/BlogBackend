namespace BlogBackend.Core.Base.Methods;

public interface ICreateAsync<TEntity>
{
    public Task CreateAsync(TEntity entity);
}
