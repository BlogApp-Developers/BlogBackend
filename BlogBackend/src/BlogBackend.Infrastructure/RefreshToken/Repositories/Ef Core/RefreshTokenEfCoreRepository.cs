#pragma warning disable CS1998

namespace BlogBackend.Infrastructure.RefreshToken.Repositories.Ef_Core;

using BlogBackend.Core.RefreshToken.Repositories;
using BlogBackend.Core.RefreshToken.Entity;
using BlogBackend.Infrastructure.Data.DbContext;
using Microsoft.EntityFrameworkCore;

public class RefreshTokenEfCoreRepository : IRefreshTokenRepository
{
    private readonly BlogDbContext dbContext;

    public RefreshTokenEfCoreRepository(BlogDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task CreateAsync(RefreshToken refreshToken)
    {
        await dbContext.RefreshTokens.AddAsync(refreshToken);
        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(RefreshToken refreshToken)
    {
        dbContext.RefreshTokens.Remove(refreshToken);
        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteRangeRefreshTokens(RefreshToken refreshToken)
    {
        var refreshTokenToDelete = dbContext.RefreshTokens.Where(rt => rt.UserId == refreshToken.UserId);
        dbContext.RefreshTokens.RemoveRange(refreshTokenToDelete);
        await dbContext.SaveChangesAsync();
    }

    public async Task<RefreshToken?> GetRefreshTokenAsync(RefreshToken refreshToken)
    {
        return await dbContext.RefreshTokens.Where(refToken => refToken.Token == refreshToken.Token && refToken.UserId == refreshToken.UserId).FirstOrDefaultAsync();
    }
}
