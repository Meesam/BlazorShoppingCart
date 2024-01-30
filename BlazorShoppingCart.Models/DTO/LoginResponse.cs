﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorShoppingCart.Models.DTO
{
    public class LoginResponse
    {
        public string? UserId { get; set; }

        public string? UserName { get; set; }

        public string? Email { get; set; }

        public string? Role { get; set; }
    }
}
