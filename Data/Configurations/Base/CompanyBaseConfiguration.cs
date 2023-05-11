using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations
{
    public class CompanyBaseConfiguration<TEntity> : CompanyBaseConfiguration<Guid, TEntity>
        where TEntity : CompanyBase
    { }

    public class CompanyBaseConfiguration<TKey, TEntity> : AuditableEntityConfiguration<TKey, TEntity>
        where TEntity : CompanyBase<TKey>
    {
        public override void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.Property(p => p.CompanyId).IsRequired(true);
            builder.Property(p => p.CompanyId).Metadata.SetAfterSaveBehavior(Microsoft.EntityFrameworkCore.Metadata.PropertySaveBehavior.Ignore);

            builder.HasOne(x => x.Company).WithMany().OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.Restrict);

            base.Configure(builder);
        }
    }
}
