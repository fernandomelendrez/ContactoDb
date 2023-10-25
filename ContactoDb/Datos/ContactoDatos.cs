using ContactoDb.Models;
using System.Data.SqlClient;
using System.Data;

namespace ContactoDb.Datos
{
    public class ContactoDatos
    {
        public List<ContactoModel> ListarContacto()
        {
            List<ContactoModel> lista = new List<ContactoModel>();
            var conexion = new Conexion();
            using (var conexion1 = new SqlConnection(conexion.CadenaSql()))
            {
                conexion1.Open();
                SqlCommand cmd = new SqlCommand("sp_ListarContacto", conexion1);
                cmd.CommandType = CommandType.StoredProcedure;
                using (var dr = cmd.ExecuteReader())
                {
                    while(dr.Read())
                    {
                        lista.Add(new ContactoModel()
                        {
                            IdContacto = Convert.ToInt32(dr["IdContacto"]),
                            Nombre = dr["Nombre"].ToString(),
                            Telefono = dr["Telefono"].ToString(),
                            Correo = dr["Correo"].ToString(),
                            Clave = dr["Clave"].ToString()
                        });
                    }
                }
            }
            return lista;
        }

        public ContactoModel ObtenerContacto(int IdContacto)
        {
            //Creo un objeto vacio
            var oContacto = new ContactoModel();
            var cn = new Conexion();
            //Utilizar using para establecer la cadena de conexion
            using (var conexion = new SqlConnection(cn.CadenaSql()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("sp_Obtener", conexion);
                //Enviando un parametro al procedimiento almacenado
                cmd.Parameters.AddWithValue("IdContacto", IdContacto);
                cmd.CommandType = CommandType.StoredProcedure;
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        //asigno los valores al objeto oContacto
                        oContacto.IdContacto = Convert.ToInt32(dr["IdContacto"]);
                        oContacto.Nombre = dr["Nombre"].ToString();
                        oContacto.Telefono = dr["Telefono"].ToString();
                        oContacto.Correo = dr["Correo"].ToString();
                        oContacto.Clave = dr["Clave"].ToString();
                    }
                }
            }
            return oContacto;
        }

        public bool GuardarContacto(ContactoModel model)
        {
            //creo una variable boolean
            bool respuesta;
            try
            {
                var cn = new Conexion();
                //utilizar using para establecer la cadena de conexion
                using (var conexion = new SqlConnection(cn.CadenaSql()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_GuardarContacto", conexion);
                    //Enviando un parametro al procedimiento almacenado
                    cmd.Parameters.AddWithValue("Nombre", model.Nombre);
                    cmd.Parameters.AddWithValue("Telefono", model.Telefono);
                    cmd.Parameters.AddWithValue("Correo", model.Correo);
                    cmd.Parameters.AddWithValue("Clave", model.Clave);
                    cmd.CommandType = CommandType.StoredProcedure;
                    //Ejecutar el procedimiento almacenado
                    cmd.ExecuteNonQuery();   
                }
                //Si no ocurre un error la variable respuesta sera true
                respuesta = true;
            }
            catch (Exception e)
            {
                string error = e.Message;
                respuesta = false;
            }

            return respuesta;
        }

        public bool EditarContacto(ContactoModel model)
        {
            //creo una variable boolean
            bool respuesta;
            try
            {
                var cn = new Conexion();
                //utilizar using para establecer la cadena de conexion
               using (var conexion = new SqlConnection(cn.CadenaSql()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_EditarContacto", conexion);
                    //Enviando un parametro al procedimiento alamcenado
                    cmd.Parameters.AddWithValue("IdContacto", model.IdContacto);
                    cmd.Parameters.AddWithValue("Nombre", model.Nombre);
                    cmd.Parameters.AddWithValue("Telefono", model.Telefono);
                    cmd.Parameters.AddWithValue("Correo", model.Correo);
                    cmd.Parameters.AddWithValue("Clave", model.Clave);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }
                //si no ocurre un error la variable respuesta sera true
                respuesta = true;
            }
            catch (Exception e)
            {
                string error = e.Message;
                respuesta = false;
            }

            return respuesta;
        }

        public bool EliminarContacto(ContactoModel model)
        {
            bool respuesta;
            try
            {
                var cn = new Conexion();
                using (var conexion = new SqlConnection(cn.CadenaSql()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_Eliminar", conexion);
                    cmd.Parameters.AddWithValue("IdContacto", model.IdContacto);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }
                respuesta = true;
            }
            catch (Exception e) 
            {
                string error = e.Message;
                respuesta = false;
            }
            return respuesta;
        }
    } 
}
