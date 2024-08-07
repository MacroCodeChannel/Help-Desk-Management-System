using HelpDeskSystem.AuditsManager;
using HelpDeskSystem.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

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

        public DbSet<Department> Departments { get; set; }

        public DbSet<TicketResolution> TicketResolutions { get; set; }


        public DbSet<SystemTask> SystemTasks { get; set; }

        public DbSet<SystemSetting> SystemSettings { get; set; }

        public DbSet<UserRoleProfile> UserRoleProfiles { get; set; }


        public DbSet<Country> Countries { get; set; }

        public DbSet<City> Cities { get; set; }


        public virtual async Task<int> SaveChangesAsync(string userid = null)
        {
            OnBeforeSaveChanges(userid);
            var result = await base.SaveChangesAsync(); 
            return result;
        }

        private void OnBeforeSaveChanges(string userId)
        {
            ChangeTracker.DetectChanges();
            var aurditEntries = new List<AuditEntry>();

            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.Entity is AuditTrail || entry.State == EntityState.Detached || entry.State == EntityState.Unchanged)
                    continue;

                var auditEntry = new AuditEntry(entry);
                auditEntry.TableName = entry.Entity.GetType().Name;
                auditEntry.Module = entry.Entity.GetType().Name;
                auditEntry.UserId = userId;
                aurditEntries.Add(auditEntry);

                foreach(var property in entry.Properties)
                {
                    string propertyname = property.Metadata.Name;
                    if (property.Metadata.IsPrimaryKey())
                    {
                        auditEntry.KeyValues[propertyname] = property.CurrentValue;
                        continue;
                    }

                    switch (entry.State)
                    {
                         case EntityState.Added:
                            auditEntry.AuditType = AuditType.Create;
                            auditEntry.NewValues[propertyname] = property.CurrentValue;
                            break;

                        case EntityState.Deleted:
                            auditEntry.AuditType = AuditType.Delete;
                            auditEntry.OldValues[propertyname] = property.OriginalValue;
                            break;

                        case EntityState.Modified:
                            if (property.IsModified)
                            {

                                auditEntry.ChangedColumns.Add(propertyname);
                                auditEntry.AuditType = AuditType.Update;
                                auditEntry.OldValues[propertyname] = property.OriginalValue;
                                auditEntry.NewValues[propertyname] = property.CurrentValue;
                            }
                            break;
                    }
                }

            }
            foreach(var auditEntry in aurditEntries)
            {
                AuditTrails.Add(auditEntry.ToAudit());
            }
        }




        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            foreach (var relationship in builder.Model.GetEntityTypes()
                .SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }


            builder.Entity<Comment>()
            .HasOne(c => c.CreatedBy)
            .WithMany()
            .HasForeignKey(c => c.CreatedById)
            .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<TicketResolution>()
            .HasOne(c => c.Status)
            .WithMany()
            .HasForeignKey(c => c.StatusId)
            .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<TicketResolution>()
           .HasOne(c => c.Ticket)
           .WithMany()
           .HasForeignKey(c => c.TicketId)
           .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Comment>()
            .HasOne(c => c.Ticket)
            .WithMany(c=>c.TicketComments)
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

        builder.Entity<ApplicationUser>()
        .HasOne(c => c.Role)
        .WithMany()
        .HasForeignKey(c => c.RoleId)
        .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<ApplicationUser>()
        .HasOne(c => c.Gender)
        .WithMany()
        .HasForeignKey(c => c.GenderId)
        .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
