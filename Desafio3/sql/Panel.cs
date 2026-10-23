using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using Desafio3.sql;

namespace Desafio3.clases
{
    internal class Panel
    {
        public static List<Usuario> MostrarUsuarios(string Carnet)
        {
            conexion db = new conexion();
            List<Usuario> listaUsuarios = new List<Usuario>();

            using (SqlConnection conn = db.Conexion())
            {
                string cadena = @"SELECT Usuarios.Carnet, Usuarios.Nombre, Apellidos, Rol, Departamentos.Nombre AS Departamento, Salario, usuarioActivo FROM Usuarios 
INNER JOIN Departamentos ON Departamentos.idDepartamento = Usuarios.idDepartamento
INNER JOIN Salarios ON Salarios.Carnet = Usuarios.Carnet WHERE Salarios.Activo != 0 AND Usuarios.Carnet != @Carnet AND usuarioActivo != 0";

                using (SqlCommand cmd = new SqlCommand(cadena, conn))
                {
                    cmd.Parameters.AddWithValue("@Carnet", Carnet);

                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Usuario usuario = new Usuario
                            {
                                Carnet = reader["Carnet"].ToString(),
                                Nombre = reader["Nombre"].ToString(),
                                Apellidos = reader["Apellidos"].ToString(),
                                Rol = Convert.ToInt32(reader["Rol"]),
                                Departamento = reader["Departamento"].ToString(),
                                Salario = Convert.ToDecimal(reader["Salario"])
                            };

                            listaUsuarios.Add(usuario);
                        }
                    }
                }
            }

            return listaUsuarios;
        }

        public static List<Usuario> MostraTodos(string Carnet)
        {
            conexion db = new conexion();
            List<Usuario> listaUsuarios = new List<Usuario>();

            using (SqlConnection conn = db.Conexion())
            {
                string cadena = @"SELECT Usuarios.Carnet, Usuarios.Nombre, Apellidos, Rol, Departamentos.Nombre AS Departamento, Salario, usuarioActivo FROM Usuarios 
INNER JOIN Departamentos ON Departamentos.idDepartamento = Usuarios.idDepartamento
INNER JOIN Salarios ON Salarios.Carnet = Usuarios.Carnet WHERE Salarios.Activo != 0 AND Usuarios.Carnet != @Carnet";

                using (SqlCommand cmd = new SqlCommand(cadena, conn))
                {
                    cmd.Parameters.AddWithValue("@Carnet", Carnet);

                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Usuario usuario = new Usuario
                            {
                                Carnet = reader["Carnet"].ToString(),
                                Nombre = reader["Nombre"].ToString(),
                                Apellidos = reader["Apellidos"].ToString(),
                                Rol = Convert.ToInt32(reader["Rol"]),
                                Departamento = reader["Departamento"].ToString(),
                                Salario = Convert.ToDecimal(reader["Salario"]),
                                usuarioActivo = Convert.ToBoolean(reader["usuarioActivo"])
                            };

                            listaUsuarios.Add(usuario);
                        }
                    }
                }
            }

            return listaUsuarios;
        }

        public static List<Historial> Historial(string Carnet)
        {
            conexion db = new conexion();
            List<Historial> listaHistorial = new List<Historial>();

            using (SqlConnection conn = db.Conexion())
            {
                string cadena = @"SELECT Usuarios.Carnet, Salario, ISSS,AFP,Renta,SalarioTotal,Fecha,Rol, Activo,Detalles FROM Usuarios 
INNER JOIN Salarios ON Usuarios.Carnet = Salarios.Carnet
INNER JOIN Salarios_Historial ON Salarios.idSalario = Salarios_Historial.idSalario
INNER JOIN Historial ON Salarios_Historial.idHistorial = Historial.idHistorial 
WHERE Usuarios.Carnet = @Carnet";

                using (SqlCommand cmd = new SqlCommand(cadena, conn))
                {
                    cmd.Parameters.AddWithValue("@Carnet", Carnet);

                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Historial historial = new Historial
                            {
                                Carnet = reader["Carnet"].ToString(),
                                Salario = Convert.ToDecimal(reader["Salario"]),
                                ISSS = Convert.ToDecimal(reader["ISSS"]),
                                AFP = Convert.ToDecimal(reader["AFP"]),
                                Renta = Convert.ToDecimal(reader["Renta"]),
                                Total = Convert.ToDecimal(reader["SalarioTotal"]),
                                fecha = Convert.ToDateTime(reader["Fecha"]),
                                rol = Convert.ToInt32(reader["Rol"]),
                                activo = Convert.ToBoolean(reader["Activo"]),
                                detalles = reader["Detalles"].ToString()
                            };

                            listaHistorial.Add(historial);
                        }
                    }
                }
            }

            return listaHistorial;
        }

        public static List<Historial> HistorialEmpleado(string Carnet)
        {
            conexion db = new conexion();
            List<Historial> listaHistorial = new List<Historial>();

            using (SqlConnection conn = db.Conexion())
            {
                string cadena = @"SELECT Usuarios.Carnet, Salario, ISSS,AFP,Renta,SalarioTotal,Fecha,Rol, Activo,Detalles FROM Usuarios 
INNER JOIN Salarios ON Usuarios.Carnet = Salarios.Carnet
INNER JOIN Salarios_Historial ON Salarios.idSalario = Salarios_Historial.idSalario
INNER JOIN Historial ON Salarios_Historial.idHistorial = Historial.idHistorial 
WHERE Usuarios.Carnet = @Carnet AND YEAR(Fecha) = YEAR(GETDATE())";

                using (SqlCommand cmd = new SqlCommand(cadena, conn))
                {
                    cmd.Parameters.AddWithValue("@Carnet", Carnet);

                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Historial historial = new Historial
                            {
                                Carnet = reader["Carnet"].ToString(),
                                Salario = Convert.ToDecimal(reader["Salario"]),
                                ISSS = Convert.ToDecimal(reader["ISSS"]),
                                AFP = Convert.ToDecimal(reader["AFP"]),
                                Renta = Convert.ToDecimal(reader["Renta"]),
                                Total = Convert.ToDecimal(reader["SalarioTotal"]),
                                fecha = Convert.ToDateTime(reader["Fecha"]),
                                rol = Convert.ToInt32(reader["Rol"]),
                                activo = Convert.ToBoolean(reader["Activo"]),
                                detalles = reader["Detalles"].ToString()
                            };

                            listaHistorial.Add(historial);
                        }
                    }
                }
            }

            return listaHistorial;
        }

        public static List<Usuario> UsuarioUnico(string Carnet)
        {
            conexion db = new conexion();
            List<Usuario> listaUsuarios = new List<Usuario>();

            using (SqlConnection conn = db.Conexion())
            {
                string cadena = @"SELECT Usuarios.Carnet, Usuarios.Nombre, Apellidos, Correo, Rol, Departamentos.Nombre AS Departamento, Salario FROM Usuarios 
INNER JOIN Departamentos ON Departamentos.idDepartamento = Usuarios.idDepartamento
INNER JOIN Salarios ON Salarios.Carnet = Usuarios.Carnet WHERE Salarios.Activo != 0 AND Usuarios.Carnet = @Carnet";

                using (SqlCommand cmd = new SqlCommand(cadena, conn))
                {
                    cmd.Parameters.AddWithValue("@Carnet", Carnet);

                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Usuario usuario = new Usuario
                            {
                                Carnet = reader["Carnet"].ToString(),
                                Nombre = reader["Nombre"].ToString(),
                                Apellidos = reader["Apellidos"].ToString(),
                                Correo = reader["Correo"].ToString(),
                                Rol = Convert.ToInt32(reader["Rol"]),
                                Departamento = reader["Departamento"].ToString(),
                                Salario = Convert.ToDecimal(reader["Salario"])
                            };

                            listaUsuarios.Add(usuario);
                        }
                    }
                }
            }

            return listaUsuarios;
        }

        public static int Pagar(string carnet)
        {
            conexion db = new conexion();

            using (SqlConnection conn = db.Conexion())
            {
                conn.Open();

                using (SqlTransaction transaccion =
                    conn.BeginTransaction(System.Data.IsolationLevel.Serializable))
                {
                    try
                    {
                        string verificar = @"
                    SELECT COUNT(*)
                    FROM Historial
                    WHERE Carnet = @Carnet
                    AND Fecha >= DATEFROMPARTS(
                        YEAR(GETDATE()), MONTH(GETDATE()), 1)
                    AND Fecha < DATEADD(MONTH, 1,
                        DATEFROMPARTS(
                            YEAR(GETDATE()), MONTH(GETDATE()), 1))";

                        using (SqlCommand cmd =
                            new SqlCommand(verificar, conn, transaccion))
                        {
                            cmd.Parameters.AddWithValue("@Carnet", carnet);

                            int cantidad = Convert.ToInt32(cmd.ExecuteScalar());

                            if (cantidad > 0)
                            {
                                transaccion.Rollback();
                                return 0;
                            }
                        }

                        int idSalario;

                        string consultarSalario = @"
                    SELECT idSalario
                    FROM Salarios
                    WHERE Carnet = @Carnet
                    AND Activo = 1";

                        using (SqlCommand cmd =
                            new SqlCommand(consultarSalario, conn, transaccion))
                        {
                            cmd.Parameters.AddWithValue("@Carnet", carnet);

                            object resultado = cmd.ExecuteScalar();

                            if (resultado == null)
                            {
                                transaccion.Rollback();
                                return -1;
                            }

                            idSalario = Convert.ToInt32(resultado);
                        }

                        int idHistorial;

                        string insertarHistorial = @"
                    INSERT INTO Historial(Carnet, Fecha, Detalles)
                    VALUES (@Carnet, GETDATE(), 'Pago realizado');

                    SELECT CAST(SCOPE_IDENTITY() AS INT);";

                        using (SqlCommand cmd =
                            new SqlCommand(insertarHistorial, conn, transaccion))
                        {
                            cmd.Parameters.AddWithValue("@Carnet", carnet);

                            idHistorial = Convert.ToInt32(cmd.ExecuteScalar());
                        }

                        // Relacionar salario con historial
                        string insertarRelacion = @"
                    INSERT INTO Salarios_Historial
                    (idSalario, idHistorial)
                    VALUES (@idSalario, @idHistorial)";

                        using (SqlCommand cmd =
                            new SqlCommand(insertarRelacion, conn, transaccion))
                        {
                            cmd.Parameters.AddWithValue("@idSalario", idSalario);
                            cmd.Parameters.AddWithValue("@idHistorial", idHistorial);

                            cmd.ExecuteNonQuery();
                        }

                        transaccion.Commit();

                        return idHistorial;
                    }
                    catch
                    {
                        transaccion.Rollback();
                        throw;
                    }
                }
            }
        }

    }
}