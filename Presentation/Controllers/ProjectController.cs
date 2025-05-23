using Application.Exceptions;
using Application.Interfaces;
using Application.Request;
using Application.Response;
using Microsoft.AspNetCore.Mvc;
using Presentation.Examples;
using Swashbuckle.AspNetCore.Filters;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectProposalService _service;

        public ProjectController(IProjectProposalService service)
        {
            _service = service;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<ProjectShortResponse>), StatusCodes.Status200OK, "application/json")]
        [ProducesResponseType(typeof(ApiError), StatusCodes.Status400BadRequest)]
        [SwaggerResponseExample(200, typeof(ProjectShortResponseExample))]
        [SwaggerResponseExample(400, typeof(ApiErrorExample))]
        public async Task<ActionResult<List<ProjectShortResponse>>> GetFilteredProjects(
            [FromQuery] string? title,
            [FromQuery] int? status,
            [FromQuery] int? applicant,
            [FromQuery] int? approvalUser)
        {
            if (status < 0 || applicant < 0 || approvalUser < 0)
                return BadRequest(new ApiError { Message = "Parámetro de consulta inválido" });

            var result = await _service.Search(title, status, applicant, approvalUser);

            if (result == null || result.Count == 0)
                return NotFound(new ApiError { Message = "No se encontraron propuestas que coincidan con los filtros." });

            return Ok(result);
        }

        [HttpPost]
        [SwaggerRequestExample(typeof(ProjectCreateRequest), typeof(ProjectCreateRequestExample))]
        [ProducesResponseType(typeof(ProjectProposalResponseDetail), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiError), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ProjectProposalResponseDetail>> Create([FromBody] ProjectCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiError { Message = "Datos del proyecto inválidos" });
            }
            if (_service.ExistingProject(request.Title))
            {
                return BadRequest(new Conflict("Ya existe un proyecto creado con ese nombre"));
            }
            
            var response = await _service.CreateProjectProposal(request.Title, request.Description,
                request.Area, request.Type, request.Amount, request.Duration, request.User);

            return Ok(response);
        }

        /*[HttpPut("{id}/decision")]
        public async Task<IActionResult> ApproveStep(Guid id, [FromQuery] long stepId, [FromQuery] int userId)
        {
            var success = await _service.ApproveStep(id, stepId, userId);
            return success ? Ok() : BadRequest("No se pudo aprobar el paso.");
        }        
        
        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectProposalResponseDetail>> GetById(Guid id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _service.GetById(id);
            return result == null ? NotFound("No se encontraron propuestas que coincidan con el id.") : Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ProjectProposalResponse>> Update(Guid id, [FromBody] ProjectProposalUpdateRequest request)
        {
            var result = await _service.Update(id, request);
            return result == null ? NotFound() : Ok(result);
        }*/
    }
}
