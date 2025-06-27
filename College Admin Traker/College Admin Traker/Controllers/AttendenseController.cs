using ClassLibrary.Dtos;
using ClassLibrary.Models;
using College_Admin_Traker.Dbcontext;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace College_Admin_Traker.Controllers
{  
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AttendenseController : ControllerBase
    {
        private readonly CollegeDbContext collegeDbContext;

        public AttendenseController(CollegeDbContext collegeDbContext)
        {
            this.collegeDbContext = collegeDbContext;
        }
        [HttpPost]
        public IActionResult PostAttendence([FromBody] AttendenceDto attendenceDto)
        {
            var post1 = new Attendence()
            {
                UsersId=attendenceDto.UsersId,
                Date = attendenceDto.Date,
                Status = attendenceDto.Status,
                Course = attendenceDto.Course,
                RecorderdBy= attendenceDto.RecorderdBy
            };
            collegeDbContext.Add(post1);
            collegeDbContext.SaveChanges();
            return Ok(post1);
        }
        [HttpGet]
        public IActionResult GetAllAttendence()
        {
            var result = collegeDbContext.Attendence.ToList();
            List<Attendence> list = new List<Attendence>();
            foreach (var item in result)
            {
                list.Add(new Attendence()
                {
                    AttendenceId = item.AttendenceId,
                    UsersId = item.UsersId,
                    Date = item.Date,
                    Status = item.Status,
                    Course = item.Course,
                    RecorderdBy = item.RecorderdBy
                });
            }
            return Ok(list);
        }
        [Route("Staff/{id:int}")]
        [HttpGet]
        public IActionResult GetAllStaffAttendence([FromRoute] int id)
        {
            var result = collegeDbContext.Attendence.Include(a => a.Users).Where(a => a.Users.RoleId == 3 || (a.Users.RoleId == 2 && a.UsersId == id)).ToList();

            return Ok(result);
        }
        [HttpGet]
        [Route("Student/{id:int}")]
        public IActionResult GetStudentAttendence([FromRoute] int id)
        {
            var result = collegeDbContext.Attendence.Where(p => p.UsersId == id).ToList();

            var attendanceList = result.Select(p => new AttendenceDto
            {
                AttendenceId = p.AttendenceId,
                Date = p.Date,
                Status = p.Status,
                Course = p.Course,
                RecorderdBy = p.RecorderdBy,
                UsersId = p.UsersId
            }).ToList();
            return Ok(attendanceList);
        }
        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetAttendence([FromRoute] int id)
        {
            var result = collegeDbContext.Attendence.FirstOrDefault(p => p.AttendenceId == id);

            var user = new AttendenceDto()
            {
                AttendenceId = result.AttendenceId,
                Date = result.Date,
                Status = result.Status,
                Course = result.Course,
                RecorderdBy = result.RecorderdBy,
                UsersId = result.UsersId
            };
            return Ok(user);
        }
        [HttpPut]
        [Route("{id:int}")]
        public IActionResult PutAttendence([FromRoute] int id, [FromBody] AttendenceDto attendenceDto)
        {
            var post = collegeDbContext.Attendence.FirstOrDefault(x => x.AttendenceId == id);

            post.Date = attendenceDto.Date;
            post.Status = attendenceDto.Status;
            post.Course = attendenceDto.Course;
            post.RecorderdBy =attendenceDto.RecorderdBy;
            collegeDbContext.SaveChanges();

            return Ok(post);
        }
        [HttpDelete]
        [Route("{id:int}")]
        public IActionResult DeleteAttendence([FromRoute] int id)
        {
            var delete = collegeDbContext.Attendence.FirstOrDefault(p => p.AttendenceId == id);
            if (delete == null)
            {
                return NotFound();
            }
            collegeDbContext.Attendence.Remove(delete);
            collegeDbContext.SaveChanges();
            return Ok(delete);
        }
    }
}
