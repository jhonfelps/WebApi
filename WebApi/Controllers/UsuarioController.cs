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
    public class UsuarioController : ApiController
    {
        public HttpResponseMessage Get()
        {
            try
            {
                string query = @"
                select * from usuarios";
                DataTable table = new DataTable();
                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["UsuariosCv"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }
                if (table.Rows.Count > 1)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, table);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NoContent, table);
                }
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, e);
                throw;
            }


        }
        public string Post(Usuario usuario)
        {
            try
            {
                if(usuario.Nombre is null || usuario.Apellido is null || usuario.Correo is null
                    || usuario.Tipo_Documento is null || usuario.Documento is null
                    || usuario.Correo is null || usuario.Clave is null)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Faltan campos obligatorios.").ToString();
                }
                string query = @"
                   insert into dbo.usuarios (Nombre, Apellido, Tipo_documento, Documento, Clave, Correo) values
                    ('" + usuario.Nombre+ @"', '" + usuario.Apellido + @"', '" + usuario.Tipo_Documento + @"',
                    '" + usuario.Documento + @"','" + usuario.Clave + @"','" + usuario.Correo + @"')
                ";
                DataTable table = new DataTable();
                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["UsuariosCv"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }
                return "Added Succsessfully!!";
            }
            catch(Exception)
            {
                return "Failed to Added!";
                throw;
            }
        }
        public string Put(Usuario usuario)
        {
            if (usuario.Id < 1 || usuario.Nombre is null || usuario.Apellido is null || usuario.Correo is null
                    || usuario.Tipo_Documento is null || usuario.Documento is null
                    || usuario.Correo is null || usuario.Clave is null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Faltan campos obligatorios.").ToString();
            }
            try
            {
                string query = @"
                   update dbo.usuarios set Nombre ='" + usuario.Nombre + @"',
                    Apellido = '" + usuario.Apellido + @"',
                    Tipo_documento = '" + usuario.Tipo_Documento + @"',
                    Documento = '" + usuario.Documento + @"',
                    Clave = '" + usuario.Clave + @"',
                    Correo = '" + usuario.Correo + @"'
                    Where id = "+usuario.Id+@"
                ";
                DataTable table = new DataTable();
                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["UsuariosCv"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }
                return "Updated Succsessfully!!";
            }
            catch (Exception)
            {
                return "Failed to Updated!!";
                throw;
            }
        }
        public string Delete(int id)
        {
            try
            {
                string query = @"
                   delete from dbo.usuarios Where id = " + id + @"
                ";
                DataTable table = new DataTable();
                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["UsuariosCv"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }
                return "Deleted Succsessfully!";
            }
            catch (Exception)
            {
                return "Failed to Delete";
                throw;
            }
        }
    }
}
