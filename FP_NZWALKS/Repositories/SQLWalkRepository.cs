using FP_NZWALKS.Data;
using FP_NZWALKS.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace FP_NZWALKS.Repositories
{
    public class SQLWalkRepository : IWalkRepository
    {
        private readonly NZWalksDbContext dbContext;

        public SQLWalkRepository(NZWalksDbContext dbContext) {
            this.dbContext = dbContext;
        }
        public async Task<Walk> CreateAsync(Walk walk)
        {
            await dbContext.Walks.AddAsync(walk);
            await dbContext.SaveChangesAsync();
            return walk;
        }

        public async Task<Walk?> DeleteAsync(Guid id)
        {
            var existingWalk = await dbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);

            if (existingWalk  == null)
            {
                return null;
            }

            dbContext.Walks.Remove(existingWalk);

            await dbContext.SaveChangesAsync();
            return existingWalk; 
        }

        

        public async Task<List<Walk>> GetAllAsync(string? filterOn = null, string? filterQuery = null,
            string? sortBy = null, bool isAscending = true,int pageNumber= 1, int pageSize = 1000)
        {
            var Walks = dbContext.Walks.Include("Difficulty").Include("Region").AsQueryable();

            //filtering
            if (string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery) == false)
            {
                if (filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    Walks = Walks.Where(x => x.Name.Contains(filterQuery));
                }
            }
            //Sorting

            if (string.IsNullOrWhiteSpace(sortBy) == false)
            {
                if (sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    Walks = isAscending ? Walks.OrderBy(x => x.Name) : Walks.OrderByDescending(x => x.Name);
                }
                else if (sortBy.Equals("Length", StringComparison.OrdinalIgnoreCase))
                {
                    Walks = isAscending ? Walks.OrderBy(x => x.LengthKm) : Walks.OrderByDescending(x => x.LengthKm);
                }
            }
            //Pagination 

            var skipResults = (pageNumber - 1)* pageSize;

            return await Walks.Skip(skipResults).Take(pageSize).ToListAsync();

            // return  await  dbContext.Walks.Include("Difficulty").Include("Region").ToListAsync();
        }

        public async Task<Walk?> GetByIdAsync(Guid id)
        {
          return await dbContext.Walks
                .Include("Difficulty")
                .Include("Region")
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Walk?> UpdateAsync(Guid id, Walk walk)
        {
            var exitstingWalk = await dbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);

            if (exitstingWalk == null) {
            return null;
            }
            exitstingWalk.Name = walk.Name;
            exitstingWalk.Description = walk.Description;
            exitstingWalk.LengthKm = walk.LengthKm;
            exitstingWalk.WalkImageUrl  = walk.WalkImageUrl;
            exitstingWalk.DifficultyId = walk.DifficultyId;
            exitstingWalk.RegionId = walk.RegionId;
                
            await dbContext.SaveChangesAsync();

            return exitstingWalk;

        }
    }
}
