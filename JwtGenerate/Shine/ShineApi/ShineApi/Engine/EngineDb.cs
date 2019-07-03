using ShineApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ShineApi.Engine
{
    public class EngineDb
    {
        private SqlConnection Conexion = new SqlConnection(EngineData.DefaultConnection);
        private EngineData DataName = EngineData.Instance();
        private EngineProyect Funcion = new EngineProyect();
        private string failure = string.Empty;
        private readonly ShineContext context;

        public EngineDb()
        {

        }

        public EngineDb (ShineContext _context)
        {
            context = _context;
        }

        public bool InsertUser(User model)
        {
            bool resultado = false;
            using (Conexion)
            {
                Conexion.Open();
                SqlCommand command = new SqlCommand(EngineData.InsertClient, Conexion);
                try
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@Name", model.Name);
                    command.Parameters.AddWithValue("@LastName", model.LastName);
                    command.Parameters.AddWithValue("@Username", model.Username);
                    command.Parameters.AddWithValue("@Password",Funcion.ConvertirBase64(model.Username + model.Password));
                    command.Parameters.AddWithValue("@Email", model.Email);
                    command.Parameters.AddWithValue("@PhoneNumber", model.PhoneNumber);
                    command.Parameters.AddWithValue("@FavoriteGame", model.FavoriteGame);
                    command.Parameters.AddWithValue("@BirthDate", model.BirthDate);
                    command.Parameters.AddWithValue("@Gender", model.Gender);
                    command.Parameters.AddWithValue("@RegisteredDate", model.RegisteredDate);
                    command.Parameters.AddWithValue("@RegisteredStatus", model.RegisteredStatus);
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Conexion.Close();
                    failure = ex.ToString();
                    return resultado;
                }
                Conexion.Close();
                resultado = true;
            }
            return resultado;
        }

        public User GetUser(User model)
        {
            User resultado = new User();
            SqlDataReader lector = null;
            using (Conexion)
            {
                Conexion.Open();
                SqlCommand command = new SqlCommand(EngineData.GetClient, Conexion);
                try
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@Password", Funcion.ConvertirBase64(model.Username + model.Password));
                    command.Parameters.AddWithValue("@Email", model.Email);
                    command.Parameters.AddWithValue("@RegisteredStatus", model.RegisteredStatus);
                    lector = command.ExecuteReader();
                    if (lector.Read())
                    {
                        resultado.Id = lector.GetInt32(0);
                        resultado.Name = lector.GetString(1);
                        resultado.LastName = lector.GetString(2);
                        resultado.Username = lector.GetString(3);
                        resultado.Password = lector.GetString(4);
                        resultado.Email = lector.GetString(5);
                        resultado.PhoneNumber = lector.GetString(6);
                        resultado.FavoriteGame = lector.GetString(7);
                        resultado.BirthDate = lector.GetDateTime(8);
                        resultado.Gender = lector.GetString(9);
                        resultado.RegisteredDate = lector.GetDateTime(10);
                        resultado.RegisteredStatus = lector.GetBoolean(11);
                    }
                }
                catch (Exception ex)
                {
                    lector.Close();
                    Conexion.Close();
                    failure = ex.ToString();
                }
                lector.Close();
                Conexion.Close();
            }
            return resultado;
        }

        public string Failure()
        {
            return failure;
        }

        public bool GetUser (string username , string password , string email)
        {
            Conexion.Open();
            Int64 resultado = -1;
            SqlCommand command = new SqlCommand(EngineData.GetClientExist, Conexion);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Clear();
            command.Parameters.AddWithValue("@Password", Funcion.ConvertirBase64(username + password));
            command.Parameters.AddWithValue("@Email", email);
            object obj = command.ExecuteScalar();
            Conexion.Close();
            if (obj != DBNull.Value)
                resultado = Convert.ToInt64(obj);
           
            if (resultado > 0)
                return true;
            else
                return false;
        }



        public bool InsertCodeToVerification (CodeToVerification model)
        {
            bool resultado = false;
            try
            {
                context.CodeToVerification.Add(model);
                context.SaveChanges();
                resultado = true;
            }
            catch
            {
                return resultado;
            }
            return resultado;
        }

        public bool PutActivateAccount(CodeToVerification model)
        {
            bool resultado = false;
            using (Conexion)
            {
                Conexion.Open();
                SqlCommand command = new SqlCommand(EngineData.PutActivateAccount, Conexion);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@Password", Funcion.ConvertirBase64(model.Username + model.Password));
                command.Parameters.AddWithValue("@Email", model.Email);
                command.Parameters.AddWithValue("@Code", model.Code);
                command.Parameters.AddWithValue("@VerificationDate", DateTime.UtcNow);
                command.Parameters.AddWithValue("@Status", model.Status);
                resultado = (bool) command.ExecuteScalar();
                Conexion.Close();
            }
            return resultado;
        }

    }
}
