using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SPOTService.DataStorage.Entities;

namespace SPOTService.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {           
            modelBuilder.CreateRoles();
        }

        static void CreateRoles(this ModelBuilder modelBuilder)
        {
            var roleAdmin = new Role() {
                Id = 1,
                Title = "admin" 
            }; 
            modelBuilder.Entity<Role>().HasData(roleAdmin);
        }
    }
}
