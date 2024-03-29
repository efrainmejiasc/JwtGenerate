﻿using System;
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

        public static string EndPointLogin = "LoginClient";

        public static string SignatureApp = "U0hJTkVhcGlTZWNyZXRLZXkyMDE5RWZyYWluQmFja0VuZEhlY3RvckFwcE1vYmls";

        public static string InsertClient= "Sp_InsertClient";

        public static string GetClient = "Sp_GetClient";

        public static string GetClientExist = "Sp_GetClientExist";

        public static string PutActivateAccount  = "Sp_PutActivateAccount";
    }
}
