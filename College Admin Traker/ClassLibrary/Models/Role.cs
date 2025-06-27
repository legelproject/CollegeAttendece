﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Models
{
    public class Role
    {
        public int RoleId { get; set; }
        public string RollName { get; set; }
        public string Description { get; set; }
        public ICollection<Users> Users { get; set; }
    }
}
