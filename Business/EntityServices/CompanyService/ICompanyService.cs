namespace Business.EntityServices
{
    public interface ICompanyService : IServiceManager<Company>
    {
        IQueryable<Company> GetByName(string name);
       
        Task<Guid> AddCompanyAsync(Company companyModel);
        Guid AddCompany(Company companyModel);
        string  GetUserIP();
    }
}
