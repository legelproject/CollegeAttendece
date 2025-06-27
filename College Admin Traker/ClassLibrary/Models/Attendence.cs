using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Models
{
    public class Attendence
    {
        public int AttendenceId { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; }
        public string Course { get; set; }
        [ForeignKey(nameof(Users))]
        public int RecorderdBy { get; set; }
        public int UsersId { get; set; }
        public Users Users { get; set; }
    }
}