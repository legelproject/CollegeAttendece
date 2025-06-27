using ClassLibrary.Dtos;
using College_Admin_Traker.Dbcontext;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace College_Admin_Traker.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly CollegeDbContext collegeDbContext;

        public UsersController(CollegeDbContext collegeDbContext)
        {
            this.collegeDbContext = collegeDbContext;
        }
        [HttpGet]
        public IActionResult GetAllUser()
        {
            var result = collegeDbContext.Users.ToList();
            List<UserDto> list = new List<UserDto>();
            foreach (var item in result)
            {
                list.Add(new UserDto()
                {
                    UsersId = item.UsersId,
                    UserName = item.UserName,
                    Password = item.Password,
                    Email = item.Email,
                    Created = item.Created
                });
            }
            return Ok(list);
        }
        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetUser([FromRoute] int id)
        {
            var result = collegeDbContext.Users.FirstOrDefault(p => p.UsersId == id);

            var user = new UserDto()
            {
                UsersId = result.UsersId,
                UserName = result.UserName,
                Email = result.Email,
                Password = result.Password,
                Created = result.Created,
                RoleId = result.RoleId
            };
            return Ok(user);
        }
        [HttpPut]
        [Route("{id:int}")]
        public IActionResult put([FromRoute] int id, [FromBody] UserDto userDto)
        {
            var post = collegeDbContext.Users.FirstOrDefault(x => x.UsersId == id);

            post.UserName = userDto.UserName;
            post.Password = userDto.Password;
            post.Email = userDto.Email;
            post.Created = userDto.Created;
            collegeDbContext.SaveChanges();

            return Ok(post);
        }
    }
}
