using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using CupCakeShop.Models;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;
using CupCakeShop.Helpers;

/// <summary>
/// Oligert Crroj
/// Creted on :7/1/2021 10:24 Am
/// Last changes made: 7/2/2021 2:02 pm
/// </summary>

namespace CupCakeShop.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        private readonly ICurrentUserService currentUserService;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options,
            ICurrentUserService currentUserService)
            : base(options)
        {
            this.currentUserService = currentUserService ?? throw new ArgumentNullException(nameof(currentUserService));
        }
        public DbSet<CupCakeShop.Models.Cake> CupCake { get; set; }




        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            ProcessSave();
            return base.SaveChangesAsync(cancellationToken);
        }

        #region ProcessSave -function to automatically updat details
        //Process Save Function
        //Used to automatically update the details page for the datas
        //I used the entites to get the name of the used and also the time edited/created
        private void ProcessSave()
        {
            
            var currentTime = DateTimeOffset.UtcNow;

            foreach (var item in ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added && e.Entity is Entity))
            {
                var entity = item.Entity as Entity;
                entity.CreatedDate = currentTime;
                entity.CreatedByUser = currentUserService.GetCurrentUserName();
                entity.ModifiedDate = currentTime;
                entity.ModifiedByUser = currentUserService.GetCurrentUserName(); 
            }

            foreach (var item in ChangeTracker.Entries()
               .Where(e => e.State == EntityState.Modified && e.Entity is Entity))
            {
                var entity = item.Entity as Entity;
                entity.ModifiedDate = currentTime;
                entity.ModifiedByUser = currentUserService.GetCurrentUserName(); 
                item.Property(nameof(entity.CreatedDate)).IsModified = false;
                item.Property(nameof(entity.CreatedByUser)).IsModified = false;
            }

        }
    }
       #endregion
}
