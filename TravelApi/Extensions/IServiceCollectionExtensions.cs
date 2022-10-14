﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Travel.Context.Models.Notification;
using Travel.Context.Models.Travel;
using Travel.Data.Interfaces;
using Travel.Data.Repositories;
using Travel.Data.Responsives;

namespace TravelApi.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            // Configure DbContext with Scoped lifetime
            services.AddDbContext<NotificationContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("notifyTravelEntities"));
            }
            );

            services.AddDbContext<TravelContext>(options =>
            {
                options
                .UseLazyLoadingProxies()
                .UseSqlServer(configuration.GetConnectionString("travelRoverEntities"));
            });
            return services;
        }
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services
              .AddScoped<IRole, RoleRes>();
            services
              .AddScoped<IEmployee, EmployeeRes>();
            services
               .AddScoped<ILocation, LocationRes>();
            services
                .AddScoped<INews, NewsRes>();
            services
                .AddScoped<IAuthentication, AuthenticationRes>();
            services
          .AddScoped<ITour, TourRes>();
            services
           .AddScoped<ICustomer, CustomerRes>();
            services
         .AddScoped<ISchedule, ScheduleRes>();
            services
            .AddScoped<ICars, CarRes>();
            services
            .AddScoped<ITimeLine, TimeLineRes>();
            services
          .AddScoped<ICostTour, CostTourRes>();
            return services;
        

        }

    }
}
