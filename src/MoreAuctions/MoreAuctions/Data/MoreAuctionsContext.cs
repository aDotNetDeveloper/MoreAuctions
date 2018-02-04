using Microsoft.EntityFrameworkCore;
using MoreAuctions.Models;

namespace MoreAuctions.Models
{
    public class MoreAuctionsContext : DbContext
    {
        public MoreAuctionsContext (DbContextOptions<MoreAuctionsContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Auction>()
                 .HasIndex(a => a.Description)
                 .IsUnique();
            builder.Entity<AuctionItem>()
                 .HasIndex(a => new { a.AuctionID, a.Title })
                 .IsUnique();
        }

        public DbSet<Auction> Auction { get; set; }

        public DbSet<AuctionItem> AuctionItem { get; set; }
    }
}
