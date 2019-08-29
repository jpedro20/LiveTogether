
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using LiveTogether.Models;

namespace LiveTogether.Data.Repositories
{
    public interface IWarrantiesRepository
    {
        Task<List<Warranty>> GetAll();
    }


    public class WarrantiesRepository : IWarrantiesRepository
    {
        private readonly LiveTogetherContext _context;

        public WarrantiesRepository(LiveTogetherContext context)
        {
            _context = context;
        }


        public async Task<List<Warranty>> GetAll()
        {
            return await _context.Warranties.ToListAsync();
        }
    }
}