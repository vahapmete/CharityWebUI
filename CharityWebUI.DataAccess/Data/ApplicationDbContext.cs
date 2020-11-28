using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using CharityWebUI.Models.DbModels;

namespace CharityWebUI.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Charity> Charities { get; set; }
        public DbSet<ApplicationUser> ApplicationUser { get; set; }

    }
}
