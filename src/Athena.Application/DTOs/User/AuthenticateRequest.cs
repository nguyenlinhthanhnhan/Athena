﻿using System.ComponentModel.DataAnnotations;

namespace Athena.Application.DTOs.User;

public class AuthenticateRequest
{
    [Required]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }
}