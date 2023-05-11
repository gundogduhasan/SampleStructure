namespace Common.Entites
{
    /// <summary>
    /// Bir çok entity'de kullanılan No ve Name alanlarını içerin base class.
    /// </summary>
    public class AccountancyBase : CompanyBase
    {
        public int No { get; set; }
        public string Name { get; set; }
    }
}
