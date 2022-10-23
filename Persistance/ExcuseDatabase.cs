using DTO;
using Microsoft.EntityFrameworkCore;
using Models;
using Persistance.Context;
using Persistance.Interfaces;

namespace Persistance;

public class ExcuseDatabase : IExcuseDatabase
{
    public readonly ExcuseContext _context;

    public ExcuseDatabase(ExcuseContext context)
    {
        _context = context;
    }

    public async Task<bool> Add(Excuse ex)
    {
        try
        {
            await _context.Excuses.AddAsync(ex);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }

    public async Task<Excuse?> GetExcuseById(int id)
    {
        return await _context.Excuses
            .FirstOrDefaultAsync(ex => ex.Id == id);
    }

    public async Task<IEnumerable<Excuse>> GetExcusesByCategory(ExcuseCategory category)
    {
        return _context.Excuses
            .Where(ex => ex.Category == category);
    }

    public async Task<IEnumerable<Excuse>> GetAllExcuses()
    {
        return _context.Excuses;
    }

    public async Task<bool> DeleteExcuse(int id)
    {
        try
        {
            var excuse = await _context.Excuses
                .FirstOrDefaultAsync(ex => ex.Id == id);
            _context.Excuses.Remove(excuse);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }

    public async Task<bool> UpdateExcuse(Excuse excuse)
    {
        try
        {
            var excuseToChange = await _context.Excuses
                .FirstOrDefaultAsync(ex => ex.Id == excuse.Id);
            if (excuseToChange == null)
                return false;
            excuseToChange.Id = excuse.Id;
            excuseToChange.Text = excuse.Text;
            excuseToChange.Category = excuse.Category;
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }
}