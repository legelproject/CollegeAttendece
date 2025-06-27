﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Models
{
    public class Userdetails
    {
        public int UserdetailsId { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public DateOnly Dob { get; set; }
        public string Gender{ get; set; }
        public string Address { get; set; }   
        public string Department { get; set; }
        public int UsersId  { get; set; }
        public Users Users { get; set; }
    }
}
