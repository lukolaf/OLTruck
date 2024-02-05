using Microsoft.EntityFrameworkCore;
using OLTruck.Domain.Models;
using OLTruck.Infrastructure;
using OLTruck.Services.Interfaces;

namespace OLTruck.Services
{
    public class TempService : ITempService
    {
        private readonly OlTruckDbContext _context;
        public TempService(OlTruckDbContext context)
        {
          _context = context;
        }
        public async Task<IEnumerable<Truck>> GetAllTrucksAsync()
        {
            return await _context.Trucks.ToListAsync();
        }
    }
}
