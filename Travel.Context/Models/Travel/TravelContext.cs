﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travel.Context.Models.Travel
{
    public class TravelContext : DbContext
    {
        public TravelContext()
        {
        }

        public TravelContext(DbContextOptions<TravelContext> options)
            : base(options)
        {
        }
        public DbSet<Tourbooking> Tourbookings { get; set; }
        public DbSet<TourbookingDetails> tourbookingDetails { get; set; }
        public DbSet<Payment> Payment { get; set; }
        public DbSet<Province> Provinces { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<Ward> Wards { get; set; }
        public DbSet<CostTour> CostTours { get; set; }
        public DbSet<TourDetail> TourDetails { get; set; }
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Place> Places { get; set; }
        public DbSet<Voucher> Vouchers { get; set; }
        public DbSet<Banner> Banners { get; set; }
        public DbSet<Contract> Contracts { get; set; }
        //
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Role> roles { get; set; }
        public DbSet<Car> cars { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Promotion> Promotions { get; set; }
        public DbSet<Tour> Tour { get; set; }
        public DbSet<File> files { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Timeline> timelines { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Seed();

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.HasOne(e => e.Tourbooking)
                .WithMany(d => d.Payment)
                .HasForeignKey(e => e.IdTourBooking);
            });

            // tourbooking
            modelBuilder.Entity<Tourbooking>(entity =>
            {
                entity.HasOne(e => e.TourbookingDetails)
                .WithOne(e => e.Tourbooking)                             
                .HasForeignKey<TourbookingDetails>(e => e.IdTourBooking);

                entity.Property(e => e.Email).HasDefaultValue("");
                entity.Property(e => e.Address).HasMaxLength(100);
                entity.Property(e => e.Phone1).HasMaxLength(14);
                //entity.Property(e => e.TourbookingDetails).IsRequired(true);
            });

            modelBuilder.Entity<District>(entity =>
            {
                entity.HasOne(e => e.Province)
                .WithMany(d => d.Districts)
                .HasForeignKey(e => e.IdProvice);
                entity.Property(e => e.Name).HasMaxLength(30);
            });

            modelBuilder.Entity<Ward>(entity =>
            {
                entity.HasOne(e => e.District)
                .WithMany(e => e.Ward)
                .HasForeignKey(e => e.IdDistrict);
                entity.Property(e => e.Name).HasMaxLength(30);
            });

            modelBuilder.Entity<TourDetail>(entity =>
            {
                entity.HasOne<CostTour>(e => e.CostTour)
                  .WithOne(e => e.TourDetails)
                  .HasForeignKey<CostTour>(e => e.IdTourDetail);

                entity.Property(e => e.Description).HasMaxLength(300);
            });

            modelBuilder.Entity<CostTour>(entity =>
            {
                entity.HasOne<Place>(e => e.Place)
                  .WithMany(e => e.CostTours)
                  .HasForeignKey(e => e.IdPlace);

                entity.HasOne<Hotel>(e => e.Hotel)
                  .WithMany(e => e.CostTours)
                  .HasForeignKey(e => e.IdHotel);

                entity.HasOne<Restaurant>(e => e.Restaurant)
                  .WithMany(e => e.CostTours)
                  .HasForeignKey(e => e.IdRestaurant);
            });


            modelBuilder.Entity<Place>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(100);
                entity.Property(e => e.Address).HasMaxLength(100);
                entity.Property(e => e.Phone).HasMaxLength(15);
            });

            modelBuilder.Entity<Restaurant>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(100);
                entity.Property(e => e.Address).HasMaxLength(100);
                entity.Property(e => e.Phone).HasMaxLength(15);
            });

            modelBuilder.Entity<Hotel>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(100);
                entity.Property(e => e.Address).HasMaxLength(100);
                entity.Property(e => e.Phone).HasMaxLength(15);
            });

            modelBuilder.Entity<Voucher>(entity =>
            {
                entity.Property(e => e.Point).HasDefaultValue(0);
                entity.Property(e => e.IsDelete).IsRequired().HasDefaultValue(0);
                entity.Property(e => e.Description).HasMaxLength(100);
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(s => s.Id);

                entity.HasOne(e => e.Car)
                .WithOne(d => d.Employee)
                .HasForeignKey<Car>(e => e.IdEmployee);
                entity.Property(e => e.AccessToken).HasMaxLength(30);
                entity.Property(e => e.Email).HasDefaultValue(0);
                entity.Property(e => e.Email).IsRequired(true);
            });
     
            modelBuilder.Entity<Role>()
             .HasKey(s => s.Id);

            modelBuilder.Entity<Car>()
               .HasKey(s => s.Id);
            modelBuilder.Entity<Schedule>(entity =>
            {
                entity.HasKey(s => s.Id);

                entity.HasOne<Car>(e => e.Cars)
                 .WithMany(d => d.Schedules)
                 .HasForeignKey(e => e.IdCar);

                entity.HasOne(e => e.Employee)
               .WithMany(d => d.Schedules)
               .HasForeignKey(e => e.IdEmployee);

                entity.HasOne<Tour>(e => e.Tour)
               .WithMany(d => d.Schedules)
               .HasForeignKey(e => e.IdTour);
            });
            modelBuilder.Entity<Promotion>(entity =>
            {
                entity.HasKey(s => s.Id);

                entity.HasOne<Schedule>(e => e.Schedules)
               .WithMany(d => d.Promotions)
               .HasForeignKey(e => e.IdSchedule);
            });
            modelBuilder.Entity<Timeline>(entity =>
            {
                entity.HasOne<Schedule>(e => e.Schedule)
               .WithMany(d => d.Timelines)
               .HasForeignKey(e => e.IdSchedule);
            });
        }
        
    }
}
