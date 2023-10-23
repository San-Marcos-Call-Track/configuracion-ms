using Domain.Entities;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Infrastructure.Repositories
{
    public class AgentRepository
    {
        private readonly IMongoCollection<Agent> _agentCollection;
        public AgentRepository(
            IOptions<BaseRepository> baseRepository)
        {
            var mongoClient = new MongoClient(
                baseRepository.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                baseRepository.Value.DatabaseName);

            _agentCollection = mongoDatabase.GetCollection<Agent>("Agent");
        }

        public async Task<List<Agent>> GetAsync()
        {
            var agents = await _agentCollection.Find(Builders<Agent>
                .Filter.Empty).ToListAsync();
            return agents;
        }

        public async Task<Agent?> GetAsync(string dni)
        {
            var agent = await _agentCollection.Find(Builders<Agent>
                .Filter.Eq(x => x.Dni, dni)).FirstOrDefaultAsync();
            return agent;
        }

        public async Task CreateAsync(Agent newAgent)
        {
            await _agentCollection.InsertOneAsync(newAgent);
        }

        public async Task UpdateAsync(string dni, Agent updateAgent)
        {
            await _agentCollection.ReplaceOneAsync(Builders<Agent>
                .Filter.Eq(x => x.Dni, dni), updateAgent);
        }

        public async Task DeleteByDniAsync(string dni)
        {
            await _agentCollection.DeleteManyAsync(Builders<Agent>
                .Filter.Eq(x => x.Dni, dni));
        }

        public async Task<List<Agent>> GetByWorkGroupAsync(string workgroup)
        {
            var agents = await _agentCollection.Find(Builders<Agent>
                .Filter.Eq(x => x.WorkGroup, workgroup)).ToListAsync();
            return agents;
        }
    }
}
