namespace BlogBackend.Core.RefreshToken.Repositories;

using BlogBackend.Core.Base.Methods;
using BlogBackend.Core.RefreshToken.Entity;

    public interface IRefreshTokenRepository : IDeleteAsync<RefreshToken>, ICreateAsync<RefreshToken>
    {
        public Task<RefreshToken?> GetRefreshTokenAsync(RefreshToken refreshToken);
    }
