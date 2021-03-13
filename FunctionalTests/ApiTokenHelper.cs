
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FunctionalTests
{
    public class ApiTokenHelper
    {
        /*public static string GetAdminUserToken()
        {
            string userName = "admin@microsoft.com";
            string[] roles = { "Administrators" };

            return CreateToken(userName, roles);
        }*/

        public static string GetNormalUserToken()
        {
            string userName = "string";
            string userId = "1";
            //string[] roles = { };

            return CreateToken(userName, userId);
        }

        private static string CreateToken(string userName, string userId)
        {
            var claims = new List<Claim> { new Claim(ClaimTypes.Name, userName) };


            var key = Encoding.ASCII.GetBytes(
                new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("AppSettings")["Secret"]
                );
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, userId)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
