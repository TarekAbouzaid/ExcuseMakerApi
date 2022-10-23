﻿using DTO;
using Microsoft.EntityFrameworkCore;
using Persistance.Interfaces;

namespace Persistance.Context
{
    public class ExcuseContext : DbContext
    {
        public ExcuseContext(DbContextOptions<ExcuseContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Excuse> Excuses { get; set; }
    }
}