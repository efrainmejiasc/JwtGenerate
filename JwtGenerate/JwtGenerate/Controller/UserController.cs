using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using JwtGenerate.Engine;
using JwtGenerate.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace JwtGenerate.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
       private IConfiguration _config;

        [AllowAnonymous]
        [HttpPost]
        public IActionResult CreateUser([FromBody] User create)
        {
            IActionResult response = Unauthorized();
            EngineJwt Funcion = new EngineJwt();
            bool resultado = false;
            resultado = Funcion.CompareString(create.SignatureApp, EngineData.SignatureApp);
            if (resultado)
            {
                EngineDb Metodo = new EngineDb();
                resultado = Metodo.InsertUser(create);
                if (resultado)
                {
                    User model = new User();
                    var tokenString = GenerateJSONWebToken(model);
                    response = Ok(new { token = tokenString });
                }
            }
            return response;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login([FromBody] User login)
        {
            IActionResult response = Unauthorized();
            User model = AuthenticateUser(login);

            if (model != null)
            {
                var tokenString = GenerateJSONWebToken(model);
                response = Ok(new { token = tokenString });
            }
            return response;
        }

        private User AuthenticateUser(User model)
        {
            EngineDb Metodo = new EngineDb();
            User user = Metodo.GetUser(model);
            string entry = model.Username + model.Password;
            EngineJwt Funcion = new EngineJwt();
            bool comparacion = Funcion.CompareString(model.Password, entry);
            if (comparacion)
            {
                return user;
            }
            else
            {
                model = null;
            }
            return model;
        }

        private string GenerateJSONWebToken(User userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[] {
                               new Claim(JwtRegisteredClaimNames.Sub, userInfo.Username),
                               new Claim(JwtRegisteredClaimNames.Email, userInfo.EmailAddress),
                               new Claim("DateOfJoing", userInfo.DateOfJoing),
                               new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
              null,
              expires: DateTime.Now.AddMinutes(20),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
     
    }
}