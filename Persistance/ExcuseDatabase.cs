using DTO;
using Microsoft.EntityFrameworkCore;
using Models;
using Persistance.Context;
using Persistance.Interfaces;

namespace Persistance;

public class ExcuseDatabase : IExcuseDatabase
{
    public readonly ExcuseContext _context;
    private readonly Random _randomizer;

    public ExcuseDatabase(ExcuseContext context)
    {
        _context = context;
        _randomizer = new Random();
    }

    public async Task<bool> Add(Excuse? ex)
    {
        try
        {
            await _context.Excuses.AddAsync(ex);
            await _context.SaveChangesAsync();
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<Excuse?> GetExcuseById(int id)
    {
        return await _context.Excuses
            .FirstOrDefaultAsync(ex => ex.Id == id);
    }

    public async Task<IEnumerable<Excuse?>> GetExcusesByCategory(ExcuseCategory category)
    {
        return await _context.Excuses
            .Where(ex => ex.Category == category)
            .ToListAsync();
    }

    public async Task<IEnumerable<Excuse?>> GetAllExcuses()
    {
        return await _context.Excuses.ToListAsync();
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
        catch
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
        catch
        {
            return false;
        }
    }

    public async Task<Excuse?> GetRandomExcuse(ExcuseCategory category)
    {
        try
        {
            var excusesBySelectedCategory = _context.Excuses.Where(ex => ex.Category == category).OrderBy(ex => ex.Id);
            var rNum = _randomizer.Next(excusesBySelectedCategory.FirstOrDefault()!.Id,
                excusesBySelectedCategory.LastOrDefault()!.Id);
            return await _context.Excuses.FirstOrDefaultAsync(ex => ex.Id == rNum);
        }
        catch
        {
            return null;
        }
    }
}