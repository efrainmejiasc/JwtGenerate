using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ShineApi.Engine;
using ShineApi.Models;

namespace ShineApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginUserController : ControllerBase
    {
        private IConfiguration _config;

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login([FromBody] User login)
        {
            IActionResult response = Unauthorized();
            bool resultado = AuthenticateUser(login);
            if (!resultado)
                return response;

            var tokenString = GenerateJSONWebToken(login);
            response = Ok(new { token = tokenString });
            return response;
        }

        private bool AuthenticateUser(User login)
        {
            bool resultado = false;
            EngineDb Metodo = new EngineDb();
            EngineProyect Funcion = new EngineProyect();
            User user = new User();
            user = Metodo.GetUser(login);
            string entry = Funcion.ConvertirBase64(login.Username + login.Password);
            bool comparacion = Funcion.CompareString(user.Password, entry);
            if (!comparacion)
                return resultado;

            user.Token = GenerateJSONWebToken(user);
            resultado = true;
            return resultado;
        }

        private string GenerateJSONWebToken(User userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(EngineData.JwtKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[] {
                               new Claim(JwtRegisteredClaimNames.Sub, userInfo.Username),
                               new Claim(JwtRegisteredClaimNames.GivenName, userInfo.Name),
                               new Claim(JwtRegisteredClaimNames.Email, userInfo.Email),
                               new Claim("ExpiracionToken", DateTime.UtcNow.AddMinutes(15).ToString()),
                               new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(EngineData.JwtKey,
              EngineData.JwtIssuer,
              null,
              expires: DateTime.UtcNow.AddMinutes(15),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}