﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Models
{
    public class User
    {
        public long User_Id { get; set; }
        public string Name { get; set; }
        public string Password {  get; set; }
        public string Email { get; set; }
    }
}
