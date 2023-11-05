using System.Collections;
using WebAPI.Models;

namespace WebAPI.DAL
{
    public interface ILaundryServiceRepository
    {
        Task<IEnumerable<LaundryService>> GetAllLaundryServices();
        Task<LaundryService?> GetLaundryServiceById(Guid id);
        Task InsertLaundryService(LaundryService laundryService);
        Task UpdateLaundryService(LaundryService laundryService);
        Task DeleteLaundryService(LaundryService laundryService);
    }
}