using OLTruck.Domain.Models;

namespace OLTruck.Services.Interfaces
{
    public interface ITempService
    {
        Task<IEnumerable<Truck>> GetAllTrucksAsync();
    }
}
