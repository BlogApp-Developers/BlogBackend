namespace BlogBackend.Core.Base.Methods;

public interface IGetByIdAsync<TEntity>
{
    public Task<TEntity> GetByIdAsync(int id);
}
