﻿using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Athena.API.Helpers;
using Athena.Application.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Athena.API.Middlewares;

public class JwtMiddleware
{
    private readonly RequestDelegate _next;
    private readonly AuthSettings _authSettings;

    public JwtMiddleware(RequestDelegate next, IOptions<AuthSettings> appSettings)
    {
        _next = next;
        _authSettings = appSettings.Value;
    }

    public async Task Invoke(HttpContext context, IUserService userService)
    {
        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

        if (token != null)
            AttachUserToContext(context, userService, token);

        await _next(context);
    }

    private void AttachUserToContext(HttpContext context, IUserService userService, string token)
    {
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            byte[] key = Encoding.ASCII.GetBytes(_authSettings.AccessTokenSecret);
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            }, out var validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;
            var userId = int.Parse(jwtToken.Claims.First(c => c.Type == "sub").Value);

            context.Items["User"] = userService.GetById(userId);
        }
        catch (Exception)
        {
            
        }
    }
}