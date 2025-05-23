using Application.Exceptions;
using Application.Interfaces;
using Application.Mappers;
using Application.Request;
using Application.Response;
using Azure;
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
        [SwaggerResponseExample(StatusCodes.Status201Created, typeof(ProjectResponseExample))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ApiErrorExample))]                
        public async Task<ActionResult<ProjectProposalResponseDetail>> Create([FromBody] ProjectCreateRequest request)
        {
            if (!ModelState.IsValid || request.User <= 0 || request.Duration <= 0 || request.Type <= 0 || request.Area <= 0)
                return BadRequest(new ApiError { Message = "Datos del proyecto inválidos" });
            if (_service.ExistingProject(request.Title))
                return BadRequest(new Conflict("Ya existe un proyecto creado con ese nombre"));
            
            var response = await _service.CreateProjectProposal(request.Title, request.Description,
                request.Area, request.Type, request.Amount, request.Duration, request.User);

            return Ok(response);
        }
        [HttpPatch("{id}/decision")]
        [ProducesResponseType(typeof(ProjectProposalResponseDetail), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiError), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiError), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiError), StatusCodes.Status409Conflict)]
        [SwaggerRequestExample(typeof(DecisionStepRequest), typeof(DecisionStepExample))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(ProjectResponseExample))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ApiErrorExample))]
        [SwaggerResponseExample(StatusCodes.Status404NotFound, typeof(ApiErrorExample))]
        [SwaggerResponseExample(StatusCodes.Status409Conflict, typeof(ApiErrorExample))]
        public async Task<ActionResult<ProjectProposalResponseDetail>> Decide(Guid id, [FromBody] DecisionStepRequest request)
        {
            if (!ModelState.IsValid || request.Id <= 0 || request.User <= 0 || request.Status < 2 || request.Status > 4)
                return BadRequest(new ApiError { Message = "Datos de decisión inválidos" });

            var result = await _service.ProcessDecision(id, request.Id, request.User, request.Status, request.Observation);

            if (result == null)
                return NotFound(new ApiError { Message = "Proyecto no encontrado" });
            if (result == ProjectProposalResponseDetail.Conflict)
                return Conflict(new ApiError { Message = "El proyecto ya no se encuentra en un estado que permite modificaciones" });

            return Ok(result);
        }
        [HttpPatch("{id}")]
        [ProducesResponseType(typeof(ProjectProposalResponseDetail), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiError), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiError), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiError), StatusCodes.Status409Conflict)]
        [SwaggerRequestExample(typeof(ProjectUpdateRequest), typeof(ProjectUpdateExample))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(ProjectResponseExample))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ApiErrorExample))]
        [SwaggerResponseExample(StatusCodes.Status404NotFound, typeof(ApiErrorExample))]
        [SwaggerResponseExample(StatusCodes.Status409Conflict, typeof(ApiErrorExample))]
        public async Task<ActionResult<ProjectProposalResponseDetail>> UpdateProject(Guid id, [FromBody] ProjectUpdateRequest request)
        {
            if (!ModelState.IsValid || request.Duration <= 0 || string.IsNullOrWhiteSpace(request.Title))
                return BadRequest(new ApiError { Message = "Datos de actualización inválidos" });

            var result = await _service.UpdateProject(id, request.Title, request.Description, request.Duration);

            if (result == null)
                return NotFound(new ApiError { Message = "Proyecto no encontrado" });

            if (result == ProjectProposalResponseDetail.Conflict)
                return Conflict(new ApiError { Message = "El proyecto ya no se encuentra en un estado que permite modificaciones" });

            return Ok(result);
        }
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ProjectProposalResponseDetail), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiError), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiError), StatusCodes.Status404NotFound)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(ProjectResponseExample))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ApiErrorExample))]
        [SwaggerResponseExample(StatusCodes.Status404NotFound, typeof(ApiErrorExample))]
        public async Task<ActionResult<ProjectProposalResponseDetail>> GetById(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest(new ApiError { Message = "Datos de actualización inválidos" });

            var result = await _service.GetById(id);
            if (result == null)
                return NotFound(new ApiError { Message = "Datos de actualización inválidos" });

            return Ok(result);
        }

    }
}
