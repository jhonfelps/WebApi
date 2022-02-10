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
            int count = 0;
            string conn = ConfigurationManager.ConnectionStrings["UsuariosCv"].ConnectionString;
            SqlConnection sqlCon = new SqlConnection(conn);
            try
            {
                if (sqlCon.State == System.Data.ConnectionState.Closed)
                    sqlCon.Open();
                string query = @"
                   SELECT COUNT(1) FROM usuarios WHERE Correo = '" + usuario.Correo + @"' AND Clave ='" + usuario.Clave + @"'             ";
                SqlCommand sqlComd = new SqlCommand(query, sqlCon);
                sqlComd.CommandType = System.Data.CommandType.Text;
                count = Convert.ToInt32(sqlComd.ExecuteScalar());
                if (count == 1)
                {
                    return "Authenticated!!";
                }
                else
                {
                    return "Usuario o Clave inválido.";
                }
            }
            catch(Exception e)
            {
                return "Failed to Authenticated, try egain!" + e;
                throw;
            }
        }
    }
}
