using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Context.Extensions;

namespace Travel.Context.Models.Notification
{
    public class NotificationContext : DbContext
    {
        public NotificationContext()
        {
        }

        public NotificationContext(DbContextOptions<NotificationContext> options)
            : base(options)
        {
        }
        public DbSet<Comment> Comment { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.HasKey(e => e.IdComment);
                entity.Property(e => e.NameCustomer).HasMaxLength(50);
                entity.Property(e => e.CommentText).HasMaxLength(1000);
                entity.Property(e => e.IdTour).HasMaxLength(50);

            });
        }

    }
}