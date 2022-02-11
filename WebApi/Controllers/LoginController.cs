using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class LoginController : ApiController
    {
        public HttpResponseMessage Get()
        {
            return Request.CreateResponse(HttpStatusCode.OK, "Ok");
        }
        public string Post(Usuario usuario)
        {
            string conn = ConfigurationManager.ConnectionStrings["UsuariosCv"].ConnectionString;
            SqlConnection sqlCon = new SqlConnection(conn);
            List<Usuario> user = new List<Usuario>();
            try
            {
                if (sqlCon.State == System.Data.ConnectionState.Closed)
                    sqlCon.Open();
                string query = @"SELECT * FROM usuarios WHERE Correo = '" + usuario.Correo + @"' AND Clave ='" + usuario.Clave + @"'";
                SqlCommand sqlComd = new SqlCommand(query, sqlCon);
                sqlComd.CommandType = System.Data.CommandType.Text;
                SqlDataReader reader = sqlComd.ExecuteReader();
                while (reader.Read())
                {
                    user.Add(new Usuario
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        Nombre = reader["Nombre"].ToString(),
                        Apellido = reader["Apellido"].ToString(),
                        Tipo_Documento = reader["Tipo_documento"].ToString(),
                        Documento = reader["Documento"].ToString(),
                        Clave = reader["Clave"].ToString(),
                        Correo = reader["Correo"].ToString(),
                        Rol = reader["Rol"].ToString()
                    });
                }
                reader.Close();
            }
            catch(Exception e)
            {
                return "Failed to Authenticated, try egain!" + e;
                throw;
            }
            if (user.First().Rol == "administrador")
            {
                return "Administrador";
            }
            else if (user.First().Rol == "comprador")
            {
                return "Comprador";
            } else
            {
                return "Usuario o Clave inválido.";
            }
        }
    }
}
