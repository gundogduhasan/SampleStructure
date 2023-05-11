using System.Net;

namespace Business.EntityServices
{
    public class CompanyService : BaseService<Company>, ICompanyService
    {
        private readonly ICompanyService _companyService;
        
        public CompanyService(ICompanyService companyService)
        {
            _companyService=companyService;
        }

        public IQueryable<Company> GetByName(string name)
        {
            return repository.GetWhere(x => x.Name == name);
        }
     
        public Guid AddCompany(Company companyModel)
        {
            companyModel.Id =  repository.Add(companyModel);
           
            return new Guid();

        }

        public async Task<Guid> AddCompanyAsync(Company companyModel)
        {

            companyModel.Id = await repository.AddAsync(companyModel);
          
            return new Guid();

        }
        public string GetUserIP() {

            string result ="";

            //Getting public ipAddress
            string url = "http://checkip.dyndns.org";
            System.Net.WebRequest req = System.Net.WebRequest.Create(url);
            System.Net.WebResponse resp = req.GetResponse();
            System.IO.StreamReader sr = new System.IO.StreamReader(resp.GetResponseStream());
            string response = sr.ReadToEnd().Trim();
            string[] ipAddressWithText = response.Split(':');
            string ipAddressWithHTMLEnd = ipAddressWithText[1].Substring(1);
            string[] ipAddress = ipAddressWithHTMLEnd.Split('<');
            string publicIP = ipAddress[0];

            // Getting Local IpAddress
            string host = Dns.GetHostName();
            IPHostEntry ip = Dns.GetHostEntry(host);
            string localIP = ip.AddressList[1].ToString();

            result = string.Format("public IP:{0},  local IP:{1}", publicIP, localIP);

            return result;
        }
    }
}
