using FormEODY.DataAccess.Entities;

namespace FormEODY.Services.Interfaces;

public interface IApplicationService
{
    Task AddAsync(Application application);
    Task<List<Application>> GetAllAsync();
    Task<Application?> GetByIdAsync(int id);
    Task UpdateAsync(Application application);
    Task DeleteAsync(int id);
    IQueryable<Application> Query();
}
