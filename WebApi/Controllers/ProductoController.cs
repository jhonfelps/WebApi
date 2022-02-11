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
    public class ProductoController : ApiController
    {
        public HttpResponseMessage Get()
        {
            try
            {
                string query = @"
                select * from productos";
                DataTable table = new DataTable();
                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["UsuariosCv"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }
                if (table.Rows.Count > 0)
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
        public string Post(Producto producto)
        {
            try
            {
                if (producto.Codigo < 1 || producto.Nombre is null || producto.Descripcion is null
                    || producto.Precio < 1 || producto.Imagen is null)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Faltan campos obligatorios.").ToString();
                }
                string query = @"
                   insert into dbo.productos (Codigo, Nombre, Descripcion, Precio, Imagen) values
                    ('" + producto.Codigo + @"', '" + producto.Nombre + @"', '" + producto.Descripcion + @"',
                    '" + producto.Precio + @"','" + producto.Imagen + @"')
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
            catch (Exception)
            {
                return "Failed to Added!";
                throw;
            }
        }
        public string Put(Producto producto)
        {
            if (producto.Codigo < 1 || producto.Nombre is null || producto.Descripcion is null
                    || producto.Precio < 1 || producto.Imagen is null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Faltan campos obligatorios.").ToString();
            }
            try
            {
                string query = @"
                   update dbo.productos set Nombre ='" + producto.Nombre + @"',
                    Descripcion = '" + producto.Descripcion + @"',
                    Precio = '" + producto.Precio + @"',
                    Imagen = '" + producto.Imagen + @"'
                    Where Codigo = " + producto.Codigo + @"
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
                   delete from dbo.productos Where Codigo = " + id + @"
                ";
                DataTable table = new DataTable();
                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["ProductosCv"].ConnectionString))
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