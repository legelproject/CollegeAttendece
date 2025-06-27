using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Dtos
{
    public class AttendenceDto
    {
        public int AttendenceId { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; }
        public string Course { get; set; }
        public int RecorderdBy { get; set; }
        public int UsersId { get; set; }
    }
}
