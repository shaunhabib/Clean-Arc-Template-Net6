using Core.Domain.Persistence.Common;
using Core.Domain.Persistence.Entities;
using Core.Domain.Persistence.Enums;
using Core.Domain.Shared.Contacts;
using Infrastructure.Persistence.Extensions;
using Infrastructure.Persistence.Helpers;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Context
{
    public class AppDbContext : DbContext
    {
        private readonly IDateTimeService _dateTime;
        private readonly IAuthenticatedUser _authenticatedUser;
        public AppDbContext(DbContextOptions<AppDbContext> options, IDateTimeService dateTime, IAuthenticatedUser authenticatedUser)
            : base(options)
        {
            _dateTime = dateTime;
            _authenticatedUser = authenticatedUser;
        }

        public DbSet<Employee> Employees { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            #region Soft delete setup
            foreach (var type in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(BaseEntity).IsAssignableFrom(type.ClrType))
                    modelBuilder.SetSoftDeleteFilter(type.ClrType);
            }
            #endregion

            base.OnModelCreating(modelBuilder);
        }

        
        public async Task<int> SaveChangesAsync()
        {
            foreach (var entry in ChangeTracker.Entries<BaseEntity>().ToList())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreationDate = _dateTime.Now;
                        entry.Entity.CreatedBy = _authenticatedUser.UserId;
                        break;

                    case EntityState.Modified:
                        entry.Entity.LastUpdatedDate = _dateTime.Now;
                        entry.Entity.LastUpdatedBy = _authenticatedUser.UserId;
                        break;
                }
            }

            return await base.SaveChangesAsync();
        }
    }
}
