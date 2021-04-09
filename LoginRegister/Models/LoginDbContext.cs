using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginRegister.Models  
{
    public class LoginDbContext:DbContext 
    {
        public LoginDbContext(DbContextOptions options):base(options)
        {

        }

        public DbSet<RegisterModel> RegisterModels { get; set; }

        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {
            base.OnModelCreating(modelbuilder);
            modelbuilder.ApplyConfiguration(new LoginEntityTypeBuilder());
        }

    }
}
