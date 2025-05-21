using Application.Interfaces;
using Application.UserCase;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
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

        [HttpGet("Area")]
        public async Task<IActionResult> GetAreas()
        {
            var areas = await areaService.GetAll();
            return Ok(areas);
        }
        [HttpGet("ProjectType")]
        public async Task<IActionResult> GetProjectTypes()
        {
            var projectTypes = await projectTypeService.GetAll();
            return Ok(projectTypes);
        }
        [HttpGet("Role")]
        public async Task<IActionResult> GetRoles()
        {
            var roles = await roleService.GetAll();
            return Ok(roles);
        }
        [HttpGet("ApprovalStatus")]
        public async Task<IActionResult> GetApprovalStatus()
        {
            var approvalStatus = await approvalStatusService.GetAll();
            return Ok(approvalStatus);
        }
        [HttpGet("User")]
        public async Task<IActionResult> GetUsers()
        {
            var users = await userService.GetAll();
            return Ok(users);
        }
    }
}
