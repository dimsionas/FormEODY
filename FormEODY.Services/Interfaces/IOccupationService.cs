using FormEODY.DataAccess.Entities;

namespace FormEODY.Services.Interfaces;

public interface IOccupationService
{
    Task<List<Occupation>> GetAllAsync();
    Task AddAsync(Occupation occupation);
    Task DeleteAsync(int id);
}
