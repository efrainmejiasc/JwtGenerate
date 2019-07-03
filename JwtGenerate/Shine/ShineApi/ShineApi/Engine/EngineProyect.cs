using Newtonsoft.Json;
using ShineApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace ShineApi.Engine
{
    public class EngineProyect
    {
        public string ConvertirBase64(string cadena)
        {
            var comprobanteXmlPlainTextBytes = Encoding.UTF8.GetBytes(cadena);
            var cadenaBase64 = Convert.ToBase64String(comprobanteXmlPlainTextBytes);
            return cadenaBase64;
        }

        public string DecodeBase64(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }

        public bool EmailEsValido(string email)
        {
            string expresion = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
            bool resultado = false;
            if (Regex.IsMatch(email, expresion))
            {
                if (Regex.Replace(email, expresion, string.Empty).Length == 0)
                {
                    resultado = true;
                }
            }
            return resultado;
        }

        public bool CompareString(string a, string b)
        {
            bool resultado = false;
            if (a == b)
            {
                resultado = true;
            }
            return resultado;
        }

        public string NumberFactory()
        {
            string resultado = string.Empty;
            int s = DateTime.Now.Millisecond;
            for (int i = 0; i<= 3; i++)
            {
                if (i == 0)
                    resultado = Aleatorio(s).ToString();
                else 
                   resultado = s.ToString() + Aleatorio(s).ToString();

                Thread.Sleep(600);
                s = DateTime.Now.Millisecond;
            }
            return resultado;
        }

        private int Aleatorio(int s)
        {
            Random rnd = new Random(s);
            int n = rnd.Next(0, 9999);
            return n;
        }

        public string BuildingVerificationCode(string code , string status)
        {
            string json = string.Empty;
            VerificationCode R = new VerificationCode();
            R.Code = code;
            R.Status = status;
            json = JsonConvert.SerializeObject(R);
            return json;
        }

        public CodeToVerification BuildingVerificationCode(string username, string password, string email,string code,bool status )
        {
            CodeToVerification R = new CodeToVerification()
            {
                Username = username,
                Password = ConvertirBase64(username + password),
                Email = email,
                SendDate = DateTime.UtcNow,
                VerificationDate = DateTime.UtcNow,
                Status = status ,
                Code = code,
            };
            return R;
        }
    }
}
