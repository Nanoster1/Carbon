using Carbon.Core.Domain.EntityFrameworkCore.Extensions;
using Carbon.Domain.Users;
using Carbon.Domain.Users.ValueObjects;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Carbon.Infrastructure.Persistance.Configurations;

/// <summary>
/// Конфигурация сущности пользователя
/// </summary>
public sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(x => x.Id)
            .IsValueObjectWrapper<Guid, UserId>()
            .IsRequired()
            .ValueGeneratedOnAdd();

        builder.Property(x => x.Username)
            .IsValueObjectWrapper<string, Username>()
            .IsRequired();

        builder.Property(x => x.Email)
            .IsValueObjectWrapper<string, UserEmail>()
            .IsRequired();

        builder.Property(x => x.AccessToken)
            .IsValueObjectWrapper<string, UserAccessToken>()
            .IsRequired();

        builder.Property(x => x.Role).IsRequired();

        var passwordBuilder = builder.OwnsOne(x => x.Password);
        {
            passwordBuilder.Property(x => x.Hash).IsRequired();
            passwordBuilder.Property(x => x.Salt).IsRequired();
            passwordBuilder.Ignore(x => x.IsValid);
        }

        builder.HasIndex(x => x.Email).IsUnique();
    }
}