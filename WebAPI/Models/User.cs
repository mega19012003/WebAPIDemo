﻿namespace WebAPI.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; } 
        public string Fullname { get; set; }
    }
}
