using Microsoft.EntityFrameworkCore;
using WeatherByZipAPI.Models;

namespace WeatherByZip.API
{
    public interface ICityInfoObjectService
    {
        Task WriteRequestLog(CityInfo cityInfo);
        Task<IEnumerable<CityInfo>> GetRequestHistory();
    }

    public class CityInfoObjectService : ICityInfoObjectService
    {
        private readonly DatabaseContext _context;

        public CityInfoObjectService(DatabaseContext context)
        {
            _context = context;
        }

        public async Task WriteRequestLog(CityInfo cityInfo)
        {
            try
            {
                _context.CityInfos.Add(cityInfo);
                await _context.SaveChangesAsync();
            }
            catch { }
        }

        public async Task<IEnumerable<CityInfo>> GetRequestHistory()
        {
            return await _context.CityInfos.ToArrayAsync();
        }
    }
}
