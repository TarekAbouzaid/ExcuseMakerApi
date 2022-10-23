using DTO;
using Persistance.Interfaces;
using Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace Services.Services
{
    public class ExcuseService : IExcuseService
    {
        private readonly IExcuseDatabase _database;
        private readonly ILogger<ExcuseService> _logger;

        public ExcuseService(IExcuseDatabase database, ILogger<ExcuseService> logger)
        {
            _database = database;
            _logger = logger;
        }

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