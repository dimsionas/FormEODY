using FormEODY.DataAccess;
using FormEODY.DataAccess.Entities;
using FormEODY.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FormEODY.Services.Implementations;

public class OccupationService : IOccupationService
{
    private readonly AppDbContext _context;

    public OccupationService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Occupation>> GetAllAsync()
    {
        return await _context.Occupations.AsNoTracking().OrderBy(o => o.Name).ToListAsync();
    }

    public async Task AddAsync(Occupation occupation)
    {
        _context.Occupations.Add(occupation);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var occ = await _context.Occupations.FindAsync(id);
        if (occ != null)
        {
            _context.Occupations.Remove(occ);
            await _context.SaveChangesAsync();
        }
    }
}
