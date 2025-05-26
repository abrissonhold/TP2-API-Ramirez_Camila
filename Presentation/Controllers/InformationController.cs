using Application.Interfaces;
using Application.Response;
using Application.UserCase;
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
            var areas = await areaService.GetAll();
            return Ok(areas);
        }
        /// <summary>
        /// Listado de tipos de proyecto disponibles
        /// </summary>
        [HttpGet("ProjectType")]
        [ProducesResponseType(typeof(List<GenericResponse>), StatusCodes.Status200OK)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(ProjectTypeExample))]
        public async Task<ActionResult<List<GenericResponse>>> GetProjectTypes()
        {
            var types = await projectTypeService.GetAll();
            return Ok(types);
        }

        /// <summary>
        /// Listado de roles disponibles
        /// </summary>
        [HttpGet("Role")]
        [ProducesResponseType(typeof(List<GenericResponse>), StatusCodes.Status200OK)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(RoleExample))]
        public async Task<ActionResult<List<GenericResponse>>> GetRoles()
        {
            var roles = await roleService.GetAll();
            return Ok(roles);
        }

        /// <summary>
        /// Listado de estados de aprobación
        /// </summary>
        [HttpGet("ApprovalStatus")]
        [ProducesResponseType(typeof(List<GenericResponse>), StatusCodes.Status200OK)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(ApprovalStatusExample))]
        public async Task<ActionResult<List<GenericResponse>>> GetApprovalStatus()
        {
            var statuses = await approvalStatusService.GetAll();
            return Ok(statuses);
        }

        /// <summary>
        /// Listado de usuarios del sistema
        /// </summary>
        [HttpGet("User")]
        [ProducesResponseType(typeof(List<UserResponse>), StatusCodes.Status200OK)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(UserResponseExample))]
        public async Task<ActionResult<List<UserResponse>>> GetUsers()
        {
            var users = await userService.GetAll();
            return Ok(users);
        }
    }
}