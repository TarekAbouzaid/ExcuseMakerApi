using DTO;
using Services.Interfaces;

namespace Services.Services
{
    public class ExcuseService : IExcuseService
    {
        public Task<bool> Add(Excuse ex)
        {
            throw new NotImplementedException();
        }

        public Task<Excuse> GetExcuseById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Excuse>> GetAllExcuses()
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteExcuse(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateExcuse(int id)
        {
            throw new NotImplementedException();
        }
    }
}