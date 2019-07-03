using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShineApi.Engine;
using ShineApi.Models;

namespace ShineApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CreateClientController : ControllerBase
    {

        private readonly ShineContext context;

        public CreateClientController(ShineContext _context)
        {
            context = _context;
        }

        [AllowAnonymous]
        [HttpPost]
        public HttpResponseMessage PostCreateUser([FromBody] User create)
        {
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            if (create == null)
                return response = new HttpResponseMessage(HttpStatusCode.NotImplemented);//Modelo incompleto 501

            EngineProyect Funcion = new EngineProyect();
            bool resultado = false;
            resultado = Funcion.CompareString(create.SignatureApp, EngineData.SignatureApp);
            if (!resultado)
                return response = new HttpResponseMessage(HttpStatusCode.ExpectationFailed);//No es la firma 417

            EngineDb Metodo = new EngineDb();
            resultado = Metodo.InsertUser(create);
            if (!resultado)
            {
                response = new HttpResponseMessage(HttpStatusCode.NotAcceptable);//No pudo crearse usuario 406
                response.Content = new StringContent(Metodo.Failure());//Informacion especifica del error
                return response;
            }

            response.Headers.Location = new Uri(EngineData.UrlBase + EngineData.EndPointLogin);
            return response;
        }

        [AllowAnonymous]
        [HttpGet]
        public string GetConfirmationCode(string username , string password , string email)//retorna JSON
        {
            string response = string.Empty;
            EngineProyect Funcion = new EngineProyect();
            if (username == null || username == string.Empty || password == null || password == string.Empty || email == null || email == string.Empty)
                return response = Funcion.BuildingVerificationCode(string.Empty, HttpStatusCode.NotImplemented.ToString());//Modelo incompleto 501

            EngineDb Metodo = new EngineDb();
            bool resultado = Metodo.GetUser(username, password, email);
            if(!resultado)
                return response = Funcion.BuildingVerificationCode(string.Empty, HttpStatusCode.ExpectationFailed.ToString());//No existe el usuario 417

            string code = Funcion.NumberFactory();
            CodeToVerification model = new CodeToVerification();
            model = Funcion.BuildingVerificationCode(username, password, email, code,false);

            EngineDb Entidad = new EngineDb(this.context);
            resultado = Entidad.InsertCodeToVerification(model);
            if (!resultado)
                return response = Funcion.BuildingVerificationCode(string.Empty, HttpStatusCode.Conflict.ToString());//No inserto el codigo 409

            return response = Funcion.BuildingVerificationCode(code, HttpStatusCode.OK.ToString());
        }

        [AllowAnonymous]
        [HttpPut]
        public string PutActivateAcount([FromBody] CodeToVerification act)//retorna JSON
        {
            string response = string.Empty;
            EngineProyect Funcion = new EngineProyect();
            if (act.Username == null || act.Username == string.Empty || act.Password == null || act.Password == string.Empty || act.Email == null || act.Email == string.Empty || act.Code == null || act.Code == string.Empty)
                return response = Funcion.BuildingVerificationCode(string.Empty, HttpStatusCode.NotImplemented.ToString());

            EngineDb Metodo = new EngineDb();
            bool resultado = false;
            resultado = Metodo.PutActivateAccount(act);
            if (!resultado)
                return response = Funcion.BuildingVerificationCode(act.Code, HttpStatusCode.Conflict.ToString());//No Actualizo el codigo 409

            return response = Funcion.BuildingVerificationCode(act.Code, HttpStatusCode.OK.ToString());
        }

    }
}