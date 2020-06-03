using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TimeApp.Model;
using TimeApp.Model.DbModels;

namespace TimeApp.Repository
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Raports> Raports { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<MainProject> MainProjects { get; set; }
        public DbSet<Week> Weeks { get; set; }
    }
}
