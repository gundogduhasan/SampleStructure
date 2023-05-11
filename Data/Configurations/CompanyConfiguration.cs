using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations
{
    public class CompanyConfiguration : AuditableEntityConfiguration<Company>
    {
        public override void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.Property(t => t.Name).IsRequired().HasMaxLength(150);
            builder.Property(t => t.CoRegNo).IsRequired(false).HasMaxLength(9);
            builder.Property(t => t.PostCode).IsRequired(true).HasMaxLength(5);
            builder.Property(t => t.Address).IsRequired().HasMaxLength(400);
            builder.Property(t => t.TownCity).IsRequired().HasMaxLength(100);
            builder.Property(t => t.Telephone).IsRequired(false).HasMaxLength(14);
            builder.Property(t => t.Fax).IsRequired(false).HasMaxLength(14);
            builder.Property(t => t.Mobile).IsRequired(false).HasMaxLength(14);
            builder.Property(t => t.EMail).IsRequired(false).HasMaxLength(200);


            base.Configure(builder);
        }
    }
}
