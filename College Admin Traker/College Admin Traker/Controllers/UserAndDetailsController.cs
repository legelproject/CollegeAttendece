using ClassLibrary.Dtos;
using ClassLibrary.Models;
using College_Admin_Traker.Dbcontext;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Numerics;
using System.Reflection;
using System.Security.Claims;
using System.Text;

namespace College_Admin_Traker.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserAndDetailsController : ControllerBase
    {
        private readonly CollegeDbContext collegeDbContext;

        public UserAndDetailsController(CollegeDbContext collegeDbContext)
        {
            this.collegeDbContext = collegeDbContext;
        }
        [HttpPost]
        public IActionResult PostUserAndDetails([FromBody] UserDto userDto)
        {
            var post1 = new Users()
            {
                UserName = userDto.UserName,
                Password = userDto.Password,
                Email = userDto.Email,
                Created = userDto.Created,
                RoleId = userDto.RoleId,
            };
            collegeDbContext.Users.Add(post1);
            collegeDbContext.SaveChanges();
            var post2 = new Userdetails()  
            {
                FullName = userDto.FullName,
                Dob = userDto.Dob,
                Gender = userDto.Gender,
                Phone = userDto.Phone,
                Address = userDto.Address,
                Department = userDto.Department,
                UsersId = post1.UsersId,
            };
            collegeDbContext.Add(post2);
            collegeDbContext.SaveChanges();
            return Ok();
        }
        [HttpGet]
        public IActionResult GetAdmin()
        {
            var result = collegeDbContext.Users.Include(p => p.Userdetails).ToList();
            List<UserDto> list = new List<UserDto>();
            foreach (var item in result)
            {
                list.Add(new UserDto()
                {

                    UserName = item.UserName,
                    Password = item.Password,
                    Email = item.Email,
                    Created = item.Created,
                    RoleId = item.RoleId,
                    FullName = item.Userdetails.FullName,
                    Dob = item.Userdetails.Dob,
                    Gender = item.Userdetails.Gender,
                    Phone = item.Userdetails.Phone,
                    Address = item.Userdetails.Address,
                    Department = item.Userdetails.Department,
                    UsersId = item.UsersId,
                });
            }
            return Ok(list);
        }
        [Route("Staff/{id:int}")]
        [HttpGet]
        public IActionResult GetStaff([FromRoute]int id)
        {
            var users = collegeDbContext.Users.Include(u => u.Userdetails).Where(u => u.RoleId == 3 || (u.RoleId == 2 && u.UsersId == id)).ToList();
            List<UserDto> list = new List<UserDto>();
            foreach (var item in users)
            {
                list.Add(new UserDto()
                {

                    UserName = item.UserName,
                    Password = item.Password,
                    Email = item.Email,
                    Created = item.Created,
                    RoleId = item.RoleId,
                    FullName = item.Userdetails.FullName,
                    Dob = item.Userdetails.Dob,
                    Gender = item.Userdetails.Gender,
                    Phone = item.Userdetails.Phone,
                    Address = item.Userdetails.Address,
                    Department = item.Userdetails.Department,
                    UsersId = item.UsersId,
                });
            }
            return Ok(list);
        }
        [Route("Student/{id:int}")]
        [HttpGet]
        public IActionResult GetStudent([FromRoute] int id)
        {
            var result = collegeDbContext.Users.Include(p => p.Userdetails).FirstOrDefault(p => p.UsersId == id);

            var user = new UserDto()
            {
                UserName = result.UserName,
                Password = result.Password,
                Email = result.Email,

                Created = result.Created,
                RoleId = result.RoleId,
                FullName = result.Userdetails.FullName,
                Dob = result.Userdetails.Dob,
                Gender = result.Userdetails.Gender,
                Phone = result.Userdetails.Phone,
                Address = result.Userdetails.Address,
                Department = result.Userdetails.Department,
                UsersId = result.UsersId,
            };
            return Ok(user);
        }
        [HttpPut]
        [Route("{id:int}")]
        public IActionResult PutAttendence([FromRoute] int id, [FromBody] UserDto userDto)
        {
            var post = collegeDbContext.Users.Include(p=>p.Userdetails).FirstOrDefault(x => x.UsersId == id);

            post.UserName = userDto.UserName;
            post.Password = userDto.Password;
            post.Email    = userDto.Email;
            post.Created  = userDto.Created;

            post.Userdetails.FullName = userDto.FullName;
            post.Userdetails.Dob = userDto.Dob;
            post.Userdetails.Gender = userDto.Gender;
            post.Userdetails.Phone = userDto.Phone;
            post.Userdetails.Address = userDto.Address;
            post.Userdetails.Department = userDto.Department;
            collegeDbContext.SaveChanges();

            return Ok(post);
        }
        [HttpGet]
        [Route("{id:int}")]
        public IActionResult get([FromRoute] int id)
        {
            var result = collegeDbContext.Users.Include(p => p.Userdetails).FirstOrDefault(p => p.UsersId == id);

            var user = new UserDto()
            {
                UserName   = result.UserName,
                Password   = result.Password,
                Email      = result.Email,
                Created    = result.Created,
                RoleId     = result.RoleId,
                FullName   = result.Userdetails.FullName,
                Dob        = result.Userdetails.Dob,
                Gender     = result.Userdetails.Gender,
                Phone      = result.Userdetails.Phone,
                Address    = result.Userdetails.Address,
                Department = result.Userdetails.Department,
                UsersId    = result.UsersId,
            };
            return Ok(user);
        }
        [HttpDelete]
        [Route("{id:int}")]
        public IActionResult delete([FromRoute] int id)
        {
            var delete = collegeDbContext.Users.Include(p => p.Userdetails).FirstOrDefault(p => p.UsersId == id);
            if (delete == null)
            {
                return NotFound();
            }
            collegeDbContext.Users.Remove(delete);
            collegeDbContext.SaveChanges();
            return Ok(delete);
        }
    }
}
