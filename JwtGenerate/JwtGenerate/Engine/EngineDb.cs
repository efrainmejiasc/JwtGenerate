using JwtGenerate.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace JwtGenerate.Engine
{
    public class EngineDb
    {
        public static string DefaultConnection { get; set; }
        private SqlConnection Conexion = new SqlConnection(EngineDb.DefaultConnection);

        public bool InsertUser(string SpName, User model)
        {
            bool resultado = false;
            using (Conexion)
            {
                Conexion.Open();
                SqlCommand command = new SqlCommand(SpName, Conexion);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@Username", model.Username);
                command.Parameters.AddWithValue("@Password", model.Password);
                command.Parameters.AddWithValue("@FechaRegistro", model.FechaRegistro); 
                Conexion.Close();
                resultado = true;
            }
            return resultado;
        }

        public string  LoginUser (string SpName, User model)
        {
            string resultado = string.Empty;
            using (Conexion)
            {
                Conexion.Open();
                SqlCommand command = new SqlCommand(SpName, Conexion);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@Username", model.Username);       
                SqlDataReader lector = command.ExecuteReader();
                int n = 0;
                if (lector.Read())
                {
                  resultado= lector.GetString(0);
                }
                lector.Close();
                Conexion.Close();
            }
            return resultado;
        }
    }
}
