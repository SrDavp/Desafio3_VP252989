using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Desafio3.sql
{
    internal class usuarios
    {
        public static clases.Usuario inicioSesion(string usuario, string contraseña)
        {
            conexion db = new conexion();
            using (SqlConnection conn = db.Conexion())
            {
                conn.Open();
                string cadena = "SELECT Usuarios.Carnet, Usuarios.Nombre, Apellidos, Contraseña, Rol, usuarioActivo,Departamentos.Nombre as Departamento, correo FROM usuarios " +
                    "INNER JOIN Departamentos ON Departamentos.idDepartamento = Usuarios.idDepartamento INNER JOIN Salarios ON Salarios.Carnet = Usuarios.Carnet " +
                    "WHERE Correo = @usuario AND contraseña = @contraseña";
                using (SqlCommand cmd = new SqlCommand(cadena, conn))
                {
                    cmd.Parameters.AddWithValue("@usuario", usuario);
                    cmd.Parameters.AddWithValue("@contraseña", contraseña);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            clases.Usuario sesion = new clases.Usuario
                            {
                                Carnet = reader.GetString(0),
                                Nombre = reader.GetString(1),
                                Apellidos = reader.GetString(2),
                                Contraseña = reader.GetString(3),
                                Rol = reader.GetInt32(4),
                                usuarioActivo = reader.GetBoolean(5),
                                Departamento = reader.GetString(6),
                                Correo = reader.GetString(7),
                            };
                            return sesion;
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
        }
    }
}
