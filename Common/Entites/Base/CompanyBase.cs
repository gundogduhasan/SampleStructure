namespace Common.Entites
{
    public class CompanyBase : CompanyBase<Guid>
    { }
    public class CompanyBase<Tkey> : AuditableEntity<Tkey>
    {
        public Guid? CompanyId { get; set; }
        public Company Company { get; set; }
    }
}
