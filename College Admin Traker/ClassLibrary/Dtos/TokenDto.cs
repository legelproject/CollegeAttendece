using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Dtos
{
    public class TokenDto
    {
        public string Tokens { get; set; }
        public int RoleId { get; set; }
        public int UsersId { get; set; }
        public string UserName { get; set; }
    }
}
