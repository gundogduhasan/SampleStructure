using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations
{

    public class AuditableEntityConfiguration<TEntity> : AuditableEntityConfiguration<Guid, TEntity>
        where TEntity : AuditableEntity<Guid>
    { }

    public class AuditableEntityConfiguration<TKey, TEntity> : BaseEntityConfiguration<TKey, TEntity>
        where TEntity : AuditableEntity<TKey>
    {
        public override void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.Property(p => p.CreatedDate).HasDefaultValueSql("getdate()");

            base.Configure(builder);
        }
    }
}
