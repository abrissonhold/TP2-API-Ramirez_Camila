using Application.Interfaces;
using Application.Response;
using Microsoft.AspNetCore.Mvc;
using Presentation.Examples;
using Swashbuckle.AspNetCore.Filters;

namespace Presentation.Controllers
{
    [Route("api/")]
    [ApiController]
    public class InformationController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly IAreaService areaService;
        private readonly IProjectTypeService projectTypeService;
        private readonly IRoleService roleService;
        private readonly IApprovalStatusService approvalStatusService;

        public InformationController(IUserService userService, IAreaService areaService, IProjectTypeService projectTypeService, IRoleService roleService, IApprovalStatusService approvalStatusService)
        {
            this.userService = userService;
            this.areaService = areaService;
            this.projectTypeService = projectTypeService;
            this.roleService = roleService;
            this.approvalStatusService = approvalStatusService;
        }

        /// <summary>
        /// Listado de Áreas 
        /// </summary>
        [HttpGet("Area")]
        [ProducesResponseType(typeof(List<GenericResponse>), StatusCodes.Status200OK)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(AreaExample))]
        public async Task<ActionResult<List<GenericResponse>>> GetAreas()
        {
            List<GenericResponse> areas = await areaService.GetAll();
            return Ok(areas);
        }
        /// <summary>
        /// Listado de tipos de proyectos
        /// </summary>
        [HttpGet("ProjectType")]
        [ProducesResponseType(typeof(List<GenericResponse>), StatusCodes.Status200OK)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(ProjectTypeExample))]
        public async Task<ActionResult<List<GenericResponse>>> GetProjectTypes()
        {
            List<GenericResponse> types = await projectTypeService.GetAll();
            return Ok(types);
        }

        /// <summary>
        /// Listado de roles de usuario
        /// </summary>
        [HttpGet("Role")]
        [ProducesResponseType(typeof(List<GenericResponse>), StatusCodes.Status200OK)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(RoleExample))]
        public async Task<ActionResult<List<GenericResponse>>> GetRoles()
        {
            List<GenericResponse> roles = await roleService.GetAll();
            return Ok(roles);
        }

        /// <summary>
        /// Listado de estados para una solicitud de proyecto y pasos de aprobación
        /// </summary>
        [HttpGet("ApprovalStatus")]
        [ProducesResponseType(typeof(List<GenericResponse>), StatusCodes.Status200OK)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(ApprovalStatusExample))]
        public async Task<ActionResult<List<GenericResponse>>> GetApprovalStatus()
        {
            List<GenericResponse> statuses = await approvalStatusService.GetAll();
            return Ok(statuses);
        }

        /// <summary>
        /// Listado de usuarios
        /// </summary>
        [HttpGet("User")]
        [ProducesResponseType(typeof(List<UserResponse>), StatusCodes.Status200OK)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(UserResponseExample))]
        public async Task<ActionResult<List<UserResponse>>> GetUsers()
        {
            List<UserResponse> users = await userService.GetAll();
            return Ok(users);
        }
    }
}