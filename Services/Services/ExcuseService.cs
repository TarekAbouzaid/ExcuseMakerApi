using DTO;
using Persistance.Interfaces;
using Services.Interfaces;
using Microsoft.Extensions.Logging;
using System.Net;
using Models;

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

        public async Task<bool> Add(Excuse? ex)
        {
            var added = await _database.Add(ex);
            if (!added)
            {
                _logger.LogError("Something went wrong adding excuse!");
                throw new Exception(HttpStatusCode.BadRequest + ": Something went wrong adding excuse!");
            }

            return added;
        }

        public async Task<Excuse> GetExcuseById(int id)
        {
            var excuseById = await _database.GetExcuseById(id);
            if (excuseById == null)
            {
                _logger.LogError($@"Something went wrong getting excuse with id({id})!");
                throw new Exception(
                    $@"{HttpStatusCode.BadRequest}: Something went wrong getting excuse with id({id})!");
            }

            return excuseById;
        }

        public async Task<IEnumerable<Excuse>> GetExcusesByCategory(ExcuseCategory category)
        {
            var excusesByCategory = await _database.GetExcusesByCategory(category);
            if (excusesByCategory == null)
            {
                _logger.LogError($@"Something went wrong getting excuse în categroy ({category})!");
                throw new Exception(
                    $@"{HttpStatusCode.BadRequest}: Something went wrong getting excuse în categroy ({category})!");
            }

            return excusesByCategory;
        }

        public async Task<IEnumerable<Excuse>> GetAllExcuses()
        {
            var excuses = await _database.GetAllExcuses();
            if (excuses == null)
            {
                _logger.LogError("Something went wrong getting all excuses!");
                throw new Exception($@"{HttpStatusCode.BadRequest}: Something went wrong getting all excuses!");
            }

            return excuses;
        }

        public async Task<bool> DeleteExcuse(int id)
        {
            var deleted = await _database.DeleteExcuse(id);
            if (!deleted)
            {
                _logger.LogError($@"Something went wrong deleting excuse with id{id}!");
                throw new Exception($@"{HttpStatusCode.BadRequest}: Something went wrong deleting excuse{id}!");
            }

            return true;
        }

        public async Task<bool> UpdateExcuse(Excuse excuse)
        {
            var updated = await _database.UpdateExcuse(excuse);
            if (!updated)
            {
                _logger.LogError($@"Something went wrong updating excuse with id{excuse.Id}!");
                throw new Exception($@"{HttpStatusCode.BadRequest}: Something went wrong updating excuse{excuse.Id}!");
            }

            return true;
        }

        public async Task<Excuse> GetRandomExcuse(ExcuseCategory category)
        {
            var randomExcuse = await _database.GetRandomExcuse(category);
            if (randomExcuse == null)
            {
                _logger.LogError($@"Something went wrong getting a random excuse in categroy ({category})!");
                throw new Exception(
                    $@"{HttpStatusCode.BadRequest}: Something went wrong getting a random excuse in categroy ({category})!");
            }

            return randomExcuse;
        }
    }
}