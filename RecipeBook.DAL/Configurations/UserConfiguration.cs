using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RecipeBook.Domain.Entity;

namespace RecipeBook.DAL
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Login).HasMaxLength(128).IsRequired();
            builder.Property(x => x.Password).IsRequired();

            //многие к 1
            builder.HasMany<Recipe>(x => x.Recipes)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId)
                .HasPrincipalKey(x => x.Id);

            //многие ко многим
            builder.HasMany(x => x.Roles)
                .WithMany(x => x.Users)
                .UsingEntity<UserRole>(
                    l => l.HasOne<Role>().WithMany().HasForeignKey(x => x.RoleId),
                    l => l.HasOne<User>().WithMany().HasForeignKey(x => x.UserId)
                );
        }
    }
}
