using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShineApi.Engine
{
    public class EngineData
    {
        private static EngineData valor;
        public static EngineData Instance()
        {
            if ((valor == null))
            {
                valor = new EngineData();
            }
            return valor;
        }


        public static string DefaultConnection { get; set; }

        public static string UrlBase { get; set; }

        public static string JwtKey { get; set; }

        public static string JwtIssuer { get; set; }

        public static string JwtAudience{ get; set; }

        public static string EndPointLogin = "LoginUser";

        public static string SignatureApp = "MiFirmaElectronica";

        public static string InsertUser = "Sp_InsertUser";

        public static string GettUser = "Sp_GetUser";
    }
}
