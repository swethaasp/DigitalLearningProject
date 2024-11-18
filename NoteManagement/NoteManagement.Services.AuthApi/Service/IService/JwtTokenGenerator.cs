﻿using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NoteManagement.Srevices.AuthApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NoteManagement.Srevices.AuthApi.Service.IService
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        private readonly JwtOptions _jwtOptions;

        public JwtTokenGenerator(IOptions<JwtOptions> jwtOptions)

        {

            _jwtOptions = jwtOptions.Value;

        }

        public async Task<string> GenerateToken(ApplicationUser applicationUser, UserManager<ApplicationUser> userManager)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtOptions.Secret);

            var claimList = new List<Claim>
    {
        new Claim(JwtRegisteredClaimNames.Email, applicationUser.Email),
        new Claim(JwtRegisteredClaimNames.Sub, applicationUser.Id),
        new Claim(JwtRegisteredClaimNames.Name, applicationUser.UserName),
        new Claim("userid",applicationUser.Id),
    };

            // Retrieve user role and add to claims
            var roles = await userManager.GetRolesAsync(applicationUser);
            foreach (var role in roles)
            {
                claimList.Add(new Claim("RoleS", role));
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Audience = _jwtOptions.Audience,
                Issuer = _jwtOptions.Issuer,
                Subject = new ClaimsIdentity(claimList),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

    }
}
