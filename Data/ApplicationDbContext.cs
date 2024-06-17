using HelpDeskSystem.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HelpDeskSystem.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Ticket> Tickets { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<AuditTrail> AuditTrails { get; set; }

        public DbSet<TicketCategory> TicketCategories { get; set; }

        public DbSet<TicketSubCategory> TicketSubCategories { get; set; }


        public DbSet<SystemCode> SystemCodes { get; set; }

        public DbSet<SystemCodeDetail> SystemCodeDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Comment>()
                .HasOne(c => c.CreatedBy)
                .WithMany()
                .HasForeignKey(c => c.CreatedById)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Comment>()
               .HasOne(c => c.Ticket)
               .WithMany()
               .HasForeignKey(c => c.TicketId)
               .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<TicketCategory>()
            .HasOne(c => c.ModifiedBy)
            .WithMany()
            .HasForeignKey(c => c.ModifiedById)
            .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<TicketCategory>()
          .HasOne(c => c.CreatedBy)
          .WithMany()
          .HasForeignKey(c => c.CreatedById)
          .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Ticket>()
                .HasOne(c => c.CreatedBy)
                .WithMany()
                .HasForeignKey(c => c.CreatedById)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<SystemCodeDetail>()
         .HasOne(c => c.SystemCode)
         .WithMany()
         .HasForeignKey(c => c.SystemCodeId)
         .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Ticket>()
        .HasOne(c => c.Priority)
        .WithMany()
        .HasForeignKey(c => c.PriorityId)
        .OnDelete(DeleteBehavior.Restrict);
      }
    }
}
