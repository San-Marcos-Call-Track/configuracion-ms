using Domain.Entities;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace configuracion_ms.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CampaignGuideController : ControllerBase
    {
        private readonly CampaignGuideRepository _campaignGuideRepository;
        public CampaignGuideController(CampaignGuideRepository campaignGuideRepository) =>
            _campaignGuideRepository = campaignGuideRepository;

        //List all documents (CampaignGuide)
        [HttpGet]
        public async Task<List<CampaignGuide>> Get()
        {
            var campaignGuide = await _campaignGuideRepository.GetAsync();
            return campaignGuide;
        }

        //Create new document (CampaignGuide)
        [HttpPost]
        public async Task<ActionResult> Post(CampaignGuide newCampaignGuide)
        {
            var existingCampaign = await _campaignGuideRepository.GetAsync(newCampaignGuide.Code);
            if (existingCampaign != null)
            {
                return BadRequest("Campaña existe");
            }
            var id = ObjectId.GenerateNewId();
            newCampaignGuide.Id = id;
            newCampaignGuide.LastEdition = DataTime.GetGTM5();
            await _campaignGuideRepository.CreateAsync(newCampaignGuide);
            return CreatedAtAction(nameof(Get), new { id = newCampaignGuide.Id }, newCampaignGuide);
        }

        //Consult document by Code
        [HttpGet("{code}")]
        public async Task<ActionResult<CampaignGuide>> Get(string code)
        {
            var campaignGuide = await _campaignGuideRepository.GetAsync(code);
            if (campaignGuide == null)
            {
                return NotFound();
            }
            return campaignGuide;
        }

        //Update document by Code
        [HttpPut("{code}")]
        public async Task<ActionResult> Put(string code, [FromBody] CampaignGuide updateCampaignGuide)
        {
            var existingCampaignGuide = await _campaignGuideRepository.GetAsync(code);
            if (existingCampaignGuide == null)
            {
                return NotFound();
            }
            existingCampaignGuide.LastEdition = DataTime.GetGTM5();
            existingCampaignGuide.CampaignName = updateCampaignGuide.CampaignName;
            existingCampaignGuide.StarDate = updateCampaignGuide.StarDate;
            existingCampaignGuide.EndingDate = updateCampaignGuide.EndingDate;
            existingCampaignGuide.CallScript = updateCampaignGuide.CallScript;
            await _campaignGuideRepository.UpdateAsync(code, existingCampaignGuide);
            return BadRequest("Actualizado");
        }
    }
}
