using System.Collections;
using WebAPI.Models;

namespace WebAPI.DAL
{
    public interface ICompanyRepository
    {
        Task<IEnumerable<Company>> GetAllCompanies();
        Task<Company?> GetSingleCompany();
        Task<Company?> GetCompanyById(Guid id);
        Task InsertCompany(Company company);
        Task UpdateCompany(Company company);
        Task DeleteCompany(Company company);
    }
}