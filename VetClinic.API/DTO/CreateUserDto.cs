﻿using System.Collections.Generic;

namespace VetClinic.API.DTO
{
    public class CreateUserDto
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }

        public IEnumerable<string> MyRoles { get; set; }
    }
}