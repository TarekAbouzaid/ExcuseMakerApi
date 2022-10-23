using DTO;
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
        var excuse = _context.Excuses
            .FirstOrDefault(ex => ex.Id == id);
        return excuse;
    }

    public async Task<IEnumerable<Excuse>> GetAllExcuses()
    {
        var contextExcuses = _context.Excuses;
        return contextExcuses;
    }

    public async Task<bool> DeleteExcuse(int id)
    {
        try
        {
            var excuse = _context.Excuses
                .FirstOrDefault(ex => ex.Id == id);
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
            var excuseToChange = _context.Excuses
                .FirstOrDefault(ex => ex.Id == excuse.Id);
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