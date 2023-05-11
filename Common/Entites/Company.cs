namespace Common.Entites
{
    public class Company : AuditableEntity
    {
        public int? CoRegNo { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int PostCode { get; set; }
        public string TownCity { get; set; }
        public Guid SuperUserId { get; set; }
        public Guid ResponsibleId { get; set; }
        public int SubscriptionId { get; set; }
        public string EMail { get; set; }
        public string Telephone { get; set; }
        public string Fax { get; set; }
        public string Mobile { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public Guid? AccountNoId { get; set; }
        public DateTime? YourLastLogin { get; set; }
        public string IpAddress { get; set; }
        public int CompanyDefaultSetupId { get; set; }
    }
}
