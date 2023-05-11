using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations
{
    public class AccountancyBaseConfiguration<TEntity> : CompanyBaseConfiguration<TEntity> where TEntity : AccountancyBase
    {
        public override void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.Property(i => i.Name).IsRequired().HasMaxLength(150);
            builder.Property(p => p.No).Metadata.SetAfterSaveBehavior(Microsoft.EntityFrameworkCore.Metadata.PropertySaveBehavior.Ignore);

            builder.HasIndex(k => new { k.No, k.CompanyId, k.IsDeleted }).IsUnique();

            base.Configure(builder);
        }
    }
}
