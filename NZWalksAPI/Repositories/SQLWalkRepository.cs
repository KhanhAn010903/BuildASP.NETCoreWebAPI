using Microsoft.EntityFrameworkCore;
using NZWalksAPI.Data;
using NZWalksAPI.Models.Domain;

namespace NZWalksAPI.Repositories;

public class SQLWalkRepository(NZWalksDbContext dbContext) : IWalkRepository
{
    public async Task<Walk> CreateAsync(Walk walk)
    {
        await dbContext.Walks.AddAsync(walk);
        await dbContext.SaveChangesAsync();
        return walk;
    }

    public async Task<Walk?> DeleteAcync(Guid id)
    {
        var existingWalk = await dbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);
        if (existingWalk == null) return null;
        dbContext.Walks.Remove(existingWalk);
        await dbContext.SaveChangesAsync();
        return existingWalk;
    }

    public async Task<List<Walk>> GetAllAsync()
    {
        return await dbContext.Walks.Include("Difficulty").Include("Region").ToListAsync();
    }

    public async Task<Walk?> GetByIdAsync(Guid id)
    {
        return await dbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Walk> UpdateAsync(Guid id, Walk walk)
    {
        var walkExisting = await dbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);
        if (walkExisting == null) return null;
        walkExisting.Name = walk.Name;
        walkExisting.Description = walk.Description;
        walkExisting.LengthInKm = walk.LengthInKm;
        walkExisting.WalkImageUrl = walk.WalkImageUrl;
        walkExisting.DifficultyId = walk.DifficultyId;
        walkExisting.RegionId = walk.RegionId;
        await dbContext.SaveChangesAsync();
        return walkExisting;
    }
}