using FP_NZWALKS.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace FP_NZWALKS.Data
{
    public class NZWalksDbContext : DbContext
    {
        public NZWalksDbContext(DbContextOptions<NZWalksDbContext> dbContextOptions) : base(dbContextOptions)
        {

        }
        public DbSet<Difficulty> Difficulties { get; set; }

        public DbSet<Region> Regions { get; set; }

        public DbSet<Walk> Walks { get; set; }

        public DbSet<Image> Images { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //seed the data for difficulties
            //easy medium and hard 
            var difficulties = new List<Difficulty>()
            {
                new Difficulty
                {
                    Id = Guid.Parse("f536df82-2678-465d-9d66-d0b5a4818488"),
                    Name = "Easy"
                },
                new Difficulty
                {
                    Id = Guid.Parse("a7c20756-17ae-4837-b798-77db79d43f0f"),
                    Name = "Medium"
                },
                new Difficulty
                {
                    Id = Guid.Parse("4abb4eea-404f-4138-9fb9-d0f1fb18aa33"),
                    Name = "Hardcore"
                }
            };
            //seed dificulties to the database 
            modelBuilder.Entity<Difficulty>().HasData(difficulties);

            //seed data for regions
            var regions = new List<Region>()
            {
                new Region
                {
                    Id= Guid.Parse("2f73ecc4-0559-4ae2-b022-96b95df21d6f"),
                    Name = "Lviv",
                    Code = "LV",
                    RegionImageUrl = "https://wallpapercave.com/wp/wp8910550.jpg"

                }, 
                  new Region
                {
                    Id= Guid.Parse("9ca7b68f-5a8a-4e5e-84f4-c1d3e7c784b9"),
                    Name = "Kyiv",
                    Code = "KV",
                    RegionImageUrl = null

                },
                  new Region
                {
                    Id= Guid.Parse("46bd6124-c108-421a-8440-8a8b4ad04873"),
                    Name = "Odessa",
                    Code = "OD",
                    RegionImageUrl = "https://vie-en-ukraine.fr/wp-content/uploads/2019/11/odessa.jpeg"

                },
                   new Region
                {
                    Id= Guid.Parse("3320a8db-bd57-4d9c-8549-1aa907943682"),
                    Name = "Bremen",
                    Code = "BR",
                    RegionImageUrl = "https://assets-global.website-files.com/5bd82da7b7abc53f312e765d/5c7565f1d885aa313b6db38e_iStock-680198122%20-%20Bremen%20Cathedral.jpg"

                },
                   new Region
                {
                    Id= Guid.Parse("02f72dcc-adeb-4e21-aca9-795eba41a42c"),
                    Name = "Berlin",
                    Code = "BR",
                    RegionImageUrl = null

                },
                    new Region
                {
                    Id= Guid.Parse("cb52cea1-d6cd-4ccd-a19e-65ebc67e447c"),
                    Name = "Hamburg",
                    Code = "HB  ",
                    RegionImageUrl = "https://www.marvest.de/wp-content/uploads/2019/09/travel-3136679_1920.jpg"
                },
            };
            
            modelBuilder.Entity<Region>().HasData(regions);
        }
    }
}

