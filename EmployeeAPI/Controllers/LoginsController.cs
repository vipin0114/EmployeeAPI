using Microsoft.AspNetCore.Mvc;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Http;
using System.Security.Claims;

namespace WebApP.Controllers
{

    [Route("api/[controller]")]
    public class LoginsController : ControllerBase
    {
        [Route("Test")]
        [HttpGet]
        public string Get()
        {
            return "Hello";
        }

        [Route("Token")]
        [HttpPost]
        public string GetToken([FromBody] LoginRequest login)
        {

            LoginRequest loginrequest = new LoginRequest { };
            loginrequest.UserName = login.UserName.ToLower();
            loginrequest.Password = login.Password;

            // IHttpActionResult response;
            bool isUsernamePasswordValid = false;

            if (login != null)
                isUsernamePasswordValid = loginrequest.Password == "admin" ? true : false;
            if (isUsernamePasswordValid)
            {
                string token = createToken(loginrequest.UserName);
                return token;
            }
            else
            {
                return "UnAuthorized User";
            }
        }

        public string createToken(string username)
        {
            DateTime issuedAt = DateTime.UtcNow;
            DateTime expires = DateTime.UtcNow.AddDays(7);
            var tokenHandler = new JwtSecurityTokenHandler();

            //create a identity and add claims to the user which we want to log in
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, username)
                ,new Claim(ClaimTypes.Role, "ADMIN-SuperUser")
            });
            var sec = "This is my Security Key";
            var now = DateTime.UtcNow;
            var securityKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(System.Text.Encoding.Default.GetBytes(sec));
            var signingCredentials = new Microsoft.IdentityModel.Tokens.SigningCredentials(securityKey, Microsoft.IdentityModel.Tokens.SecurityAlgorithms.HmacSha256Signature);
            //create the jwt
            var token =
                (JwtSecurityToken)
                    tokenHandler.CreateJwtSecurityToken(
                    issuer: "http://localhost:50191", 
                    audience: "http://localhost:50191",
                    subject: claimsIdentity, 
                    notBefore: issuedAt,
                    expires: expires, 
                    signingCredentials: signingCredentials);
            var tokenString = tokenHandler.WriteToken(token);

            return tokenString;
        }
    }
}


