﻿using JwtGenerate.Models;
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
    
        private SqlConnection Conexion = new SqlConnection(EngineData.DefaultConnection);
        private EngineData DataName = EngineData.Instance();

        public bool InsertUser (User model)
        {
            bool resultado = false;
            using (Conexion)
            {
                Conexion.Open();
                SqlCommand command = new SqlCommand(EngineData.InsertUser, Conexion);
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

        public User GetUser ( User model)
        {
            User resultado = new User();
            using (Conexion)
            {
                Conexion.Open();
                SqlCommand command = new SqlCommand(EngineData.GettUser, Conexion);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@Username", model.Username);       
                SqlDataReader lector = command.ExecuteReader();
                int n = 0;
                if (lector.Read())
                { 
                  resultado.Username = lector.GetString(0);
                  resultado.Password= lector.GetString(1);
                  resultado.EmailAddress = lector.GetString(2);
                }
                lector.Close();
                Conexion.Close();
            }
            return resultado;
        }
    }
}
