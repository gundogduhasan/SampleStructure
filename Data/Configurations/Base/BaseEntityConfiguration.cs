global using System;
global using Common.Entites;
global using Microsoft.EntityFrameworkCore;

using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations
{
    public abstract class BaseEntityConfiguration<TEntity> : BaseEntityConfiguration<Guid, TEntity>
        where TEntity : BaseEntity<Guid>
    { }

    public abstract class BaseEntityConfiguration<TKey, TEntity> : IEntityTypeConfiguration<TEntity>
        where TEntity : BaseEntity<TKey>
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.Property(p => p.IsActive).IsRequired().HasDefaultValue(true);
            builder.Property(p => p.IsDeleted).IsRequired().HasDefaultValue(false);

            if (typeof(TKey).Equals(typeof(Guid)))
                //builder.Property(p => p.Id).HasDefaultValueSql("NEWID()");
                builder.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

            builder.HasKey(p => p.Id);
        }
    }
}
