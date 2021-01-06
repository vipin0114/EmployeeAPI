using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Threading;

namespace WebApiJWT.Model
{
   
        public class AuthorizationToken : AuthorizeAttribute,IAuthorizationFilter
        {
           public void OnAuthorization(AuthorizationFilterContext context)
            {

               string token ;
                // IEnumerable<string> authzHeaders;
                HttpStatusCode statusCode;
                 StringValues authzHeaders;
                if (!context.HttpContext.Request.Headers.TryGetValue("Authorization", out authzHeaders) || authzHeaders.Count() > 1)
                {
                    throw new Exception("Authorization header Not found in request");
                }
                var bearerToken = authzHeaders.ElementAt(0);
                token = bearerToken.StartsWith("Bearer ") ? bearerToken.Substring(7) : bearerToken;
                try
                {
                    const string sec = "This is my Security Key";
                    var now = DateTime.UtcNow;
                    var securityKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(System.Text.Encoding.Default.GetBytes(sec));


                    SecurityToken securityToken;
                    JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
                    TokenValidationParameters validationParameters = new TokenValidationParameters()
                    {
                        ValidAudience = "http://localhost:50191",
                        ValidIssuer = "http://localhost:50191",
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        LifetimeValidator = this.LifetimeValidator,
                        IssuerSigningKey = securityKey
                    };
                    //extract and assign the user of the jwt
                    Thread.CurrentPrincipal = handler.ValidateToken(token, validationParameters, out securityToken);
                    context.HttpContext.User = handler.ValidateToken(token, validationParameters, out securityToken);

                }
                catch (SecurityTokenValidationException e)
                {
                    statusCode = HttpStatusCode.Unauthorized;
                }
                catch (Exception ex)
                {
                    statusCode = HttpStatusCode.InternalServerError;
                }
           
               
            }

            public bool LifetimeValidator(DateTime? notBefore, DateTime? expires, SecurityToken securityToken, TokenValidationParameters validationParameters)
            {
                if (expires != null)
                {
                    if (DateTime.UtcNow < expires) return true;
                }
                return false;
            }
        }
    }


