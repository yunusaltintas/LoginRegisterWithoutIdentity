using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace LoginRegister.Models
{
    public class LoginEntityTypeBuilder : IEntityTypeConfiguration<RegisterModel>
    {
        public void Configure(EntityTypeBuilder<RegisterModel> builder)
        {
            builder.HasKey(e => e.Id);
            
            builder.Property(e => e.UserName)
                .IsRequired()              
                .HasMaxLength(100);    
                
                 

            builder.Property(e => e.LastName)
               .IsRequired()
               .HasMaxLength(100);

            builder.Property(e => e.Email)
               .IsRequired()
               .HasMaxLength(100);

            builder.Property(e => e.Password1)
              .IsRequired()
              .HasMaxLength(400);

          
                
                





        }
    }
}
