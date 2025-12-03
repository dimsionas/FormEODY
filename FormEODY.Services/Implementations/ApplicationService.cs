using FormEODY.DataAccess;
using FormEODY.DataAccess.Entities;
using FormEODY.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FormEODY.Services.Implementations;


public class ApplicationService : IApplicationService
{
    private readonly AppDbContext _context;

    public ApplicationService(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Application application)
    {
        _context.Applications.Add(application);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Application>> GetAllAsync()
    {
        return await _context.Applications.Include(o => o.Occupation).ToListAsync();
    }

    public IQueryable<Application> Query()
    {
        return _context.Applications.AsNoTracking().Include(o => o.Occupation);
    }

    public async Task<Application?> GetByIdAsync(int id)
    {
        return await _context.Applications.Include(o => o.Occupation).FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task UpdateAsync(Application application)
    {
        _context.Applications.Update(application);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var item = await _context.Applications.FindAsync(id);
        if (item != null)
        {
            _context.Applications.Remove(item);
            await _context.SaveChangesAsync();
        }
    }
}
