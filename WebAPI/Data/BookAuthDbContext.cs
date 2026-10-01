using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Data
{
    public class BookAuthDbContext : IdentityDbContext
    {
        public BookAuthDbContext(DbContextOptions<BookAuthDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            var readerRoleId = "004c7e80-7dfc-44be-8952-2c7130898655";
            var writeRoleId = "71e282d3-76ca-485e-b094-eff019287fa5";
            var giamDocRoleId = "8f3a1c20-1234-4a5b-9abc-def012345678";
            base.OnModelCreating(builder);

            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id = readerRoleId,
                    ConcurrencyStamp = readerRoleId,
                    Name = "Read",
                    NormalizedName = "Read".ToUpper()
                },
                new IdentityRole
                {
                    Id = writeRoleId,
                    ConcurrencyStamp = writeRoleId,
                    Name = "Write",
                    NormalizedName = "Write".ToUpper()
                },
                new IdentityRole
                {
            Id = giamDocRoleId,
            ConcurrencyStamp = giamDocRoleId,
            Name = "GIÁM ĐỐC",
            NormalizedName = "GIÁM ĐỐC".ToUpper()
        }
            };
            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}