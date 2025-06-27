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
    public class UserdetailsController : ControllerBase
    {
        private readonly CollegeDbContext collegeDbContext;

        public UserdetailsController(CollegeDbContext collegeDbContext)
        {
            this.collegeDbContext = collegeDbContext;
        }
        [HttpGet]
        public IActionResult GetAllUserdetails()
        {
            var result = collegeDbContext.Userdetail.ToList();
            List<Userdetails> list = new List<Userdetails>();
            foreach (var item in result)
            {
                list.Add(new Userdetails()
                {
                    UserdetailsId = item.UsersId,
                    UsersId = item.UsersId,
                    FullName = item.FullName,
                    Dob = item.Dob,
                    Gender = item.Gender,
                    Phone = item.Phone,
                    Address = item.Address,
                    Department = item.Department,
                });
            }
            return Ok(list);
        }
        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetUserDetail([FromRoute] int id)
        {
            var result = collegeDbContext.Userdetail.FirstOrDefault(p => p.UserdetailsId == id);

            var user = new UserdetailsDto()
            {
                UserdetailId=result.UserdetailsId,
                FullName = result.FullName,
                Dob = result.Dob,
                Gender = result.Gender,
                Phone = result.Phone,
                Address = result.Address,
                Department = result.Department,
                UsersId=result.UsersId
            };
            return Ok(user);
        }
        [HttpPut]
        [Route("{id:int}")]
        public IActionResult PutUserDetail([FromRoute] int id, [FromBody] UserdetailsDto userdetailsDto)
        {
            var post = collegeDbContext.Userdetail.FirstOrDefault(x => x.UserdetailsId == id);

            post.FullName = userdetailsDto.FullName;
            post.Dob = userdetailsDto.Dob;
            post.Gender = userdetailsDto.Gender;
            post.Phone = userdetailsDto.Phone;
            post.Address = userdetailsDto.Address;
            post.Department = userdetailsDto.Department;
            collegeDbContext.SaveChanges();

            return Ok(post);
        }
    }
}
