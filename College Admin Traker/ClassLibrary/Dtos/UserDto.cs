﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Dtos
{
    public class UserDto:UserdetailsDto
    {
        public int UsersId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateOnly Created { get; set; }
        public int RoleId { get; set; }
    }
}
