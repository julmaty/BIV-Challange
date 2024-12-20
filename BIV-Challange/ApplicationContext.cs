﻿using BIV_Challange.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Xml;

namespace BIV_Challange
{
    public class ApplicationContext : IdentityDbContext<User>
    {
        public DbSet<Cutoff> Cutoffs { get; set; }
        public DbSet<CutoffValue> CutoffValues { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<CutoffForProduct> CutoffsForProduct { get; set; }
        public DbSet<TableForParam> TablesForParams { get; set; }
        public DbSet<Category> Categories { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
