﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Any;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Ticket_App.Dto;
using Ticket_App.Repositories.interfaces;
using Ticket_App.Service.Interface;

namespace Ticket_App.Service
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration configuration;
        private readonly IUserRepository userRepository;
        private PasswordHasher<string> criptography;

        public TokenService(IConfiguration _configuration, IUserRepository _userRepository)
        {
            configuration = _configuration;
            userRepository = _userRepository;
            criptography = new PasswordHasher<string>();
            
        }
        public async Task<string> GenerateToken(LoginDto loginDto)
        {
            var userDatabase = await userRepository.GetUserByEmail(loginDto.Email);
            if(userDatabase is null)
            {
                throw new Exception("user not exists");
            }
            var comparePassword = criptography.VerifyHashedPassword(null, userDatabase.Password, loginDto.Password);

          
            if (comparePassword.Equals(PasswordVerificationResult.Failed))
            {
                return String.Empty;
            }



            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:key"] ?? String.Empty));
            var issuer = configuration["Jwt:issuer"];
            var audience = configuration["Jwt:Audience"];

            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var tokenOptions = new JwtSecurityToken(audience:audience, issuer:issuer, claims: new[]
            {
                new Claim(type:ClaimTypes.Name, userDatabase!.Email),
                new Claim(type:ClaimTypes.NameIdentifier, userDatabase.Id.ToString())

            },
            expires:DateTime.Now.AddHours(2),
            signingCredentials:signinCredentials
            );

            var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);


            return token;

        }
    }
}
