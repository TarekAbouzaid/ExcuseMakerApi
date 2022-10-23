using DTO;
using Models;

namespace Persistance.Interfaces;

public interface IExcuseDatabase
{
    public Task<bool> Add(Excuse ex);
    public Task<Excuse> GetExcuseById(int id);
    public Task<IEnumerable<Excuse>> GetExcusesByCategory(ExcuseCategory category);
    public Task<IEnumerable<Excuse>> GetAllExcuses();
    public Task<bool> DeleteExcuse(int id);
    public Task<bool> UpdateExcuse(Excuse excuse);
}