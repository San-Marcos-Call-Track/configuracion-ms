using Domain.Entities;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Infrastructure.Repositories
{
    public class CampaignGuideRepository
    {
        private readonly IMongoCollection<CampaignGuide> _campaignGuideCollection;
        public CampaignGuideRepository(
            IOptions<BaseRepository> baseRepository)
        {
            var mongoClient = new MongoClient(
                baseRepository.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                baseRepository.Value.DatabaseName);

            _campaignGuideCollection = mongoDatabase.GetCollection<CampaignGuide>(
               "campaign_guide");
        }

        public async Task<List<CampaignGuide>> GetAsync()
        {
            var campaignGuide = await _campaignGuideCollection.Find(Builders<CampaignGuide>
                .Filter.Empty).ToListAsync();
            return campaignGuide;
        }

        public async Task<CampaignGuide?> GetAsync(string code)
        {
            var campaignGuide = await _campaignGuideCollection.Find(Builders<CampaignGuide>
                .Filter.Eq(x => x.Code, code)).FirstOrDefaultAsync();
            return campaignGuide;
        }

        public async Task CreateAsync(CampaignGuide newCampaignGuide)
        {
            await _campaignGuideCollection.InsertOneAsync(newCampaignGuide);
        }

        public async Task UpdateAsync(string code, CampaignGuide updateCampaignGuide)
        {
            await _campaignGuideCollection.ReplaceOneAsync(Builders<CampaignGuide>
                .Filter.Eq(x => x.Code, code), updateCampaignGuide);
        }
    }
}
