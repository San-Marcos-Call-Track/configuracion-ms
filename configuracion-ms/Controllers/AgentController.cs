using Domain.Entities;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace configuracion_ms.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AgentController : ControllerBase
    {
        private readonly AgentRepository _agentRepository;
        public AgentController(AgentRepository agentsRepository) =>
            _agentRepository = agentsRepository;

        //List all documents (Agent)
        [HttpGet]
        public async Task<List<Agent>> Get()
        {
            var agent = await _agentRepository.GetAsync();
            return agent;
        }

        //Create new document (Agent)
        [HttpPost]
        public async Task<ActionResult> Post(Agent newAgent)
        {
            var existingAgent = await _agentRepository.GetAsync(newAgent.Dni);
            if (existingAgent != null)
            {
                return BadRequest("DNI usado");
            }
            var id = ObjectId.GenerateNewId();
            newAgent.Id = id;
            newAgent.LastEdition = DataTime.GetGtm5();
            await _agentRepository.CreateAsync(newAgent);
            return CreatedAtAction(nameof(Get), new { id = newAgent.Id }, newAgent);
        }

        //Consult document by Dni
        [HttpGet("{dni}")]
        public async Task<ActionResult<Agent>> Get(string dni)
        {
            var agent = await _agentRepository.GetAsync(dni);
            if (agent == null)
            {
                return NotFound();
            }
            return agent;
        }

        //Delete document by Dni
        [HttpDelete("{dni:length(8)}")]
        public async Task<ActionResult> Delete(string dni)
        {
            var agent = await _agentRepository.GetAsync(dni);
            if (agent == null) { 
                return NotFound();
            }
            await _agentRepository.DeleteByDniAsync(dni);
            return BadRequest("Eliminado ${dni}");
        }

        //Update document by Dni
        [HttpPut("{dni:length(8)}")]
        public async Task<ActionResult> Put(string dni, [FromBody] Agent updateAgent)
        {
            var existingAgent = await _agentRepository.GetAsync(dni);
            if (existingAgent == null)
            {
                return NotFound();
            }
            existingAgent.FirstName = updateAgent.FirstName;
            existingAgent.LastName = updateAgent.LastName;
            existingAgent.WorkGroup = updateAgent.WorkGroup;
            existingAgent.LastEdition = DataTime.GetGtm5();
            existingAgent.PersonalMail = updateAgent.PersonalMail;
            await _agentRepository.UpdateAsync(dni, existingAgent);
            return BadRequest("Agent actualizado"); 
        }

        // Consult documents by WorkGroup
        [HttpGet("workgroup/{workgroup}")]
        public async Task<List<Agent>> GetByWorkGroup(string workgroup)
        {
            var agents = await _agentRepository.GetByWorkGroupAsync(workgroup);
            return agents;
        }
    }
}
