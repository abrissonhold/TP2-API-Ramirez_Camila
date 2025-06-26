using Application.Exceptions;
using Application.Interfaces;
using Application.Request;
using Application.Response;
using Microsoft.AspNetCore.Http.HttpResults;
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
            {
                return BadRequest(new ApiError { message = "Parámetro de consulta inválido" });
            }

            List<ProjectShortResponse> result = await _service.Search(title, status, applicant, approvalUser);

            return result == null || result.Count == 0
                ? (ActionResult<List<ProjectShortResponse>>)NotFound(new ApiError { message = "No se encontraron propuestas que coincidan con los filtros." })
                : (ActionResult<List<ProjectShortResponse>>)Ok(result);
        }

        [HttpPost]
        [SwaggerRequestExample(typeof(ProjectCreateRequest), typeof(ProjectCreateRequestExample))]
        [ProducesResponseType(typeof(ProjectProposalResponseDetail), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiError), StatusCodes.Status400BadRequest)]
        [SwaggerResponseExample(StatusCodes.Status201Created, typeof(ProjectResponseExample))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ApiErrorExample))]
        public async Task<ActionResult<ProjectProposalResponseDetail>> Create([FromBody] ProjectCreateRequest request)
        {
            if (!ModelState.IsValid ||
                request.User <= 0 ||
                request.Duration <= 0 ||
                request.Type <= 0 ||
                request.Area <= 0 ||
                request.Amount <= 0 ||
                string.IsNullOrWhiteSpace(request.Description) ||
                string.IsNullOrWhiteSpace(request.Title))
            {
                return BadRequest(new ApiError { message = "Datos del proyecto inválidos" });
            }

            try
            {
                ProjectProposalResponseDetail response = await _service.CreateProjectProposal(request.Title, request.Description,
                    request.Area, request.Type, request.Amount, request.Duration, request.User);

                return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
            }
            catch (ValidationException ex)
            {
                return BadRequest(new ApiError { message = ex.Message });
            }
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
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Where(e => !string.IsNullOrWhiteSpace(e.ErrorMessage))
                    .Select(e => e.ErrorMessage)
                    .ToList();

                var errorMessage = errors.FirstOrDefault() ?? "Datos de decisión inválidos";
                return BadRequest(new ApiError { message = errorMessage });
            }

            if (request.Id <= 0 || request.User <= 0 || request.Status <= 0)
                return BadRequest(new ApiError { message = "Datos de decisión inválidos" });

            try
            {
                var result = await _service.ProcessDecision(id, request.Id, request.User, request.Status, request.Observation);

                if (result == null)
                    return Conflict(new ApiError { message = "El proyecto ya no se encuentra en un estado que permite modificaciones" });

                return Ok(result);
            }
            catch (ValidationException ex)
            {
                return BadRequest(new ApiError { message = ex.ToString() });
            }
            catch (NotFoundException ex)
            {
                return BadRequest(new ApiError { message = ex.Message });

            }
            catch (ConflictException ex)
            {
                return BadRequest(new ApiError { message = ex.Message });

            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Error inesperado: {ex.Message}" });
            }
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
            if (request.Duration <= 0 || string.IsNullOrWhiteSpace(request.Title))
                return BadRequest(new ApiError { message = "Datos de actualización inválidos" });
            if (!ModelState.IsValid)
                return BadRequest(new ApiError { message = "Datos de actualización inválidos" });
            try
            {
                ProjectProposalResponseDetail? result = await _service.UpdateProject(id, request.Title, request.Description, request.Duration);
                
                return Ok(result);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new ApiError { message = ex.Message });
            }
            catch (ConflictException ex)
            {
                return Conflict(new ApiError { message = ex.Message });
            }
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
            {
                return BadRequest(new ApiError { message = "Datos de actualización inválidos" });
            }

            try
            {
                var result = await _service.GetById(id);

                if (result == null)
                {
                    return NotFound(new ApiError { message = "Proyecto no encontrado" });
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiError
                {
                    message = "Ocurrió un error al recuperar el proyecto"
                });
            }
        }

    }
}
