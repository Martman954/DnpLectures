﻿namespace Entities;

public class User
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }

    public User(string username, string password)
    {
        this.Username = username;
        this.Password = password;
    }
    
}