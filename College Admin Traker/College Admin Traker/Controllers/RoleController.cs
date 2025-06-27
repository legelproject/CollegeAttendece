using ClassLibrary.Dtos;
using ClassLibrary.Models;
using College_Admin_Traker.Dbcontext;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace College_Admin_Traker.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly CollegeDbContext collegeDbContext;

        public RoleController(CollegeDbContext collegeDbContext)
        {
            this.collegeDbContext = collegeDbContext;
        }
        [HttpPost]
        public IActionResult PostRole([FromBody] RoleDto roleDto)
        {
            var post1 = new Role()
            {
                RollName = roleDto.RoleName,
                Description = roleDto.Description,
            };
            collegeDbContext.Add(post1);
            collegeDbContext.SaveChanges();
            return Ok(post1);
        }
        [HttpGet]
        public IActionResult GetAllRole()
        {
            var result = collegeDbContext.Role.ToList();
            List<Role> list = new List<Role>();
            foreach (var item in result)
            {
                list.Add(new Role()
                {
                    RoleId = item.RoleId,
                    RollName = item.RollName,
                    Description = item.Description
                });
            }
            return Ok(list);
        }
    }
}
