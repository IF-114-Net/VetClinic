﻿namespace VetClinic.API.DTO.ClientDto
{
    public class ReadClientDto : ReadUserDto
    {
        public int Id { get; set; }
        public string UserId { get; set; }
    }
}