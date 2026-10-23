using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Desafio3.clases;
using Desafio3.sql;

namespace Desafio3.vistas
{
    public partial class Editar : Form
    {
        string carnetActual = "";
        public Editar(string carnetSeleccionado)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            categorias();
            label9.Hide();
            label10.Hide();
            label11.Hide();
            label12.Hide();
            List<clases.Usuario> usuarios = obtenerUsuario(carnetSeleccionado);
            carnetActual = carnetSeleccionado;
            nombretxt.Text = usuarios[0].Nombre;
            apellidotxt.Text = usuarios[0].Apellidos;
            salariotxt.Text = usuarios[0].Salario.ToString("0.00");
            correotxt.Text = usuarios[0].Correo;
            rolCombo.SelectedValue = usuarios[0].Rol;
        }

        static List<Usuario> obtenerUsuario(string carnet)
        {
            conexion db = new conexion();
            List<Usuario> usuarioSeleccionado = new List<Usuario>();
            using (SqlConnection cnn = db.Conexion())
            {
                cnn.Open();
                string query = "SELECT *  FROM Usuarios " +
                    "INNER JOIN Departamentos ON Departamentos.idDepartamento = Usuarios.idDepartamento " +
                    "INNER JOIN Salarios ON Salarios.Carnet = Usuarios.Carnet WHERE Salarios.Activo = 1 AND Usuarios.Carnet = @carnet";
                using (SqlCommand cmd = new SqlCommand(query, cnn))
                {
                    cmd.Parameters.AddWithValue("@carnet", carnet);
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
                                Salario = Convert.ToDecimal(reader["Salario"])
                            };
                            usuarioSeleccionado.Add(usuario);
                        }

                    }
                }

            }
            return usuarioSeleccionado;
        }

        public void categorias()
        {
            conexion db = new conexion();
            using (SqlConnection conn = db.Conexion())
            {
                conn.Open();
                string sql = "SELECT * FROM Departamentos";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            depaCombo.Items.Add(reader["nombre"].ToString());
                        }
                    }
                }
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!decimal.TryParse(salariotxt.Text.Trim(), out decimal salario))
            {
                MessageBox.Show(
                    "Ingrese un salario numérico válido.",
                    "Salario inválido",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                salariotxt.Focus();
                return;
            }

            if (salario <= 0)
            {
                MessageBox.Show(
                    "El salario debe ser mayor que cero.",
                    "Salario inválido",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                salariotxt.Focus();
                return;
            }

            if (salario < 408.80m)
            {
                MessageBox.Show(
                    "El salario no puede ser menor al salario mínimo.",
                    "Salario inválido",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                salariotxt.Focus();
                return;
            }
            decimal isss = Math.Round(
                Math.Min(salario, 1000m) * 0.03m, 2);

            decimal afp = Math.Round(
                salario * 0.0725m, 2);

            decimal baseRenta = salario - isss - afp;
            decimal renta = 0;

            if (baseRenta <= 550m)
            {
                renta = 0;
            }
            else if (baseRenta <= 895.24m)
            {
                renta = (baseRenta - 550m) * 0.10m + 17.67m;
            }
            else if (baseRenta <= 2038.10m)
            {
                renta = (baseRenta - 895.24m) * 0.20m + 60m;
            }
            else
            {
                renta = (baseRenta - 2038.10m) * 0.30m + 288.57m;
            }

            renta = Math.Round(renta, 2);

            decimal descuentos = isss + afp + renta;
            decimal salarioNeto = salario - descuentos;

            label10.Text = $"ISSS (3%): -${isss:N2}";
            label11.Text = $"AFP (7.25%): -${afp:N2}";
            label9.Text = $"Salario a recibir: ${salarioNeto:N2}";
            label12.Text = $"Renta (ISR): -${renta:N2}";

            label10.ForeColor = Color.Firebrick;
            label11.ForeColor = Color.Firebrick;
            label9.ForeColor = Color.ForestGreen;
            label12.ForeColor = Color.Firebrick;

            label10.Font = new Font("Segoe UI", 10, FontStyle.Regular);
            label11.Font = new Font("Segoe UI", 10, FontStyle.Regular);
            label9.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            label12.Font = new Font("Segoe UI", 10, FontStyle.Regular);

            label10.AutoSize = true;
            label11.AutoSize = true;
            label9.AutoSize = true;
            label12.AutoSize = true;

            label10.Show();
            label11.Show();
            label9.Show();
            label12.Show();

        }

        private void guardar_Click(object sender, EventArgs e)
        {
            string nombre = nombretxt.Text.Trim();
            string apellidos = apellidotxt.Text.Trim();
            string correo = correotxt.Text.Trim();
            string contraseña = contratxt.Text;

            if (string.IsNullOrWhiteSpace(nombre))
            {
                MessageBox.Show("Debe ingresar el nombre.");
                nombretxt.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(apellidos))
            {
                MessageBox.Show("Debe ingresar los apellidos.");
                apellidotxt.Focus();
                return;
            }

            if (!System.Text.RegularExpressions.Regex.IsMatch(
                nombre, @"^[\p{L}\s]+$") ||
                !System.Text.RegularExpressions.Regex.IsMatch(
                apellidos, @"^[\p{L}\s]+$"))
            {
                MessageBox.Show("El nombre y los apellidos solo deben contener letras.");
                return;
            }

            if (string.IsNullOrWhiteSpace(correo) ||
                !System.Text.RegularExpressions.Regex.IsMatch(
                    correo, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                MessageBox.Show("Ingrese un correo electrónico válido.");
                correotxt.Focus();
                return;
            }

            decimal salario;

            if (!decimal.TryParse(salariotxt.Text.Trim(), out salario))
            {
                MessageBox.Show("Ingrese un salario numérico válido.");
                salariotxt.Focus();
                return;
            }

            if (salario <= 0)
            {
                MessageBox.Show("El salario debe ser mayor que cero.");
                salariotxt.Focus();
                return;
            }

            if (salario < 408.80m)
            {
                MessageBox.Show("El salario no puede ser menor al salario minimo.");
                salariotxt.Focus();
                return;
            }


            if (rolCombo.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar un rol.");
                rolCombo.Focus();
                return;
            }

            if (depaCombo.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar un departamento.");
                depaCombo.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(contraseña))
            {
                MessageBox.Show("Debe ingresar una contraseña.");
                contratxt.Focus();
                return;
            }

            if (contraseña.Length < 8)
            {
                MessageBox.Show("La contraseña debe tener al menos 8 caracteres.");
                contratxt.Focus();
                return;
            }

            string rol = rolCombo.SelectedItem.ToString();
            string departamento = depaCombo.SelectedItem.ToString();
            try
            {
                editar(nombre, apellidos, correo, salario,
                      rol, departamento, contraseña, carnetActual);
                MessageBox.Show("El usuario fue actualizado:",
                "Actualizado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar el usuario: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        
        }

        public void editar(string nombre, string apellidos, string correo, decimal salario, string rol, string departamento, string contraseña, string carnet)
        {
            int tipo = 0;
            conexion db2 = new conexion();
            using (SqlConnection cnn2 = db2.Conexion())
            {
                cnn2.Open();
                if (rol == "Empleado")
                {
                    tipo = 2;
                }
                else
                {
                    tipo = 1;
                }
                int depa;
                if (departamento == "San Salvador")
                {
                    depa = 1;
                }
                else if (departamento == "Sonsonate")
                {
                    depa = 2;
                }
                else if (departamento == "San Miguel")
                {
                    depa = 3;
                }
                else if (departamento == "Usulutan")
                {
                    depa = 4;
                }
                else
                {
                    depa = 5;
                }
                string sql2 = @"UPDATE Usuarios
                SET idDepartamento = @departamento,
                    Correo = @correo,
                    Nombre = @nombre,
                    Apellidos = @apellidos,
                    Contraseña = @contraseña,
                    Rol = @rol
                WHERE Carnet = @carnet";

                using (SqlCommand cmd2 = new SqlCommand(sql2, cnn2))
                {
                    cmd2.Parameters.AddWithValue("@carnet", carnet);
                    cmd2.Parameters.AddWithValue("@departamento", depa);
                    cmd2.Parameters.AddWithValue("@correo", correo);
                    cmd2.Parameters.AddWithValue("@nombre", nombre);
                    cmd2.Parameters.AddWithValue("@apellidos", apellidos);
                    cmd2.Parameters.AddWithValue("@contraseña", contraseña);
                    cmd2.Parameters.AddWithValue("@rol", tipo);
                    int resultado = cmd2.ExecuteNonQuery();
                }   
            }

            conexion db3 = new conexion();
            using (SqlConnection cnn3 = db3.Conexion())
            {
                cnn3.Open();
                string sql = "UPDATE Salarios SET Activo = 0 WHERE Carnet = @carnet";
                using (SqlCommand cmd3 = new SqlCommand(sql, cnn3))
                {
                    cmd3.Parameters.AddWithValue("@carnet", carnet);
                    int resultado = cmd3.ExecuteNonQuery();
                }
            }
            conexion db4 = new conexion();
            using (SqlConnection cnn4 = db4.Conexion())
            {
                // Calcular ISSS (3%, máximo $30)
                decimal isss = Math.Round(
                    Math.Min(salario, 1000m) * 0.03m, 2);

                // Calcular AFP (7.25%)
                decimal afp = Math.Round(salario * 0.0725m, 2);

                // Calcular base gravada para renta
                decimal baseRenta = salario - isss - afp;
                decimal renta = 0;

                // Calcular renta según tabla mensual
                if (baseRenta <= 550m)
                {
                    renta = 0;
                }
                else if (baseRenta <= 895.24m)
                {
                    renta = (baseRenta - 550m) * 0.10m + 17.67m;
                }
                else if (baseRenta <= 2038.10m)
                {
                    renta = (baseRenta - 895.24m) * 0.20m + 60m;
                }
                else
                {
                    renta = (baseRenta - 2038.10m) * 0.30m + 288.57m;
                }

                renta = Math.Round(renta, 2);

                // Calcular salario final
                decimal salarioTotal = salario - isss - afp - renta;

                cnn4.Open();

                string sql = @"INSERT INTO Salarios
        (Carnet, Salario, ISSS, AFP, Renta, SalarioTotal, Activo)
        VALUES
        (@carnet, @salario, @isss, @afp, @renta, @salarioTotal, 1)";

                using (SqlCommand cmd3 = new SqlCommand(sql, cnn4))
                {
                    cmd3.Parameters.AddWithValue("@carnet", carnet);
                    cmd3.Parameters.AddWithValue("@salario", salario);
                    cmd3.Parameters.AddWithValue("@isss", isss);
                    cmd3.Parameters.AddWithValue("@afp", afp);
                    cmd3.Parameters.AddWithValue("@renta", renta);
                    cmd3.Parameters.AddWithValue("@salarioTotal", salarioTotal);

                    int resultado = cmd3.ExecuteNonQuery();
                }
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
