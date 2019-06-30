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
    public class CreateUserController : ControllerBase
    {
        [AllowAnonymous]
        [HttpPost]
        public HttpResponseMessage Post([FromBody] User create)
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

            response.Headers.Location = new Uri(EngineData.UrlBase + EngineData.EndPoitLogin);
            return response;
        }
    }
}