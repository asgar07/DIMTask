using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApp.Models;

namespace WebApp.Configurations
{
    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            //builder.Property(m => m.MainImage).IsRequired();
            builder
         .Property(p => p.Id)
         .UseIdentityColumn();
            builder
              .Property(p => p.Name)
              .HasMaxLength(30);

            builder
                 .Property(p => p.Surname)
                 .HasMaxLength(30).IsRequired();

            builder
                 .Property(p => p.Father)
                 .HasMaxLength(30).IsRequired();

            builder
                .Property(p => p.Mother)
                .HasMaxLength(30).IsRequired();

            builder
                .Property(p => p.FIN)
                .HasMaxLength(15).IsRequired();

            builder
                 .Property(p => p.Address)
                 .HasMaxLength(250).IsRequired();

            builder
              .Property(p => p.Gender).IsRequired();

            builder
                 .Property(p => p.MobileTel)
                 .HasMaxLength(15).IsRequired();

            builder
               .Property(p => p.Foto)
               .IsRequired();
        }
    }
}
