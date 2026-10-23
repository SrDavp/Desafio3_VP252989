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
using Desafio3.sql;

namespace Desafio3.vistas
{
    public partial class Nuevo : Form
    {
        public Nuevo()
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            InitializeComponent();
            label9.Hide();
            label10.Hide();
            label11.Hide();
            label12.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void guardar_Click(object sender, EventArgs e)
        {
            string nombre = nombretxt.Text.Trim();
            string apellidos = apellidotxt.Text.Trim();
            string correo = correotxt.Text.Trim();
            string contraseña = contratxt.Text;

            // Validar nombre
            if (string.IsNullOrWhiteSpace(nombre))
            {
                MessageBox.Show("Debe ingresar el nombre.");
                nombretxt.Focus();
                return;
            }

            // Validar apellidos
            if (string.IsNullOrWhiteSpace(apellidos))
            {
                MessageBox.Show("Debe ingresar los apellidos.");
                apellidotxt.Focus();
                return;
            }

            // Validar que nombre y apellidos solo tengan letras
            if (!System.Text.RegularExpressions.Regex.IsMatch(
                nombre, @"^[\p{L}\s]+$") ||
                !System.Text.RegularExpressions.Regex.IsMatch(
                apellidos, @"^[\p{L}\s]+$"))
            {
                MessageBox.Show("El nombre y los apellidos solo deben contener letras.");
                return;
            }

            // Validar correo
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

                nuevo(nombre, apellidos, correo, salario,
                      rol, departamento, contraseña);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar el usuario: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        


        private void Nuevo_Load(object sender, EventArgs e)
        {
            categorias();
        }

        public void nuevo(string nombre, string apellidos, string correo,
    decimal salario, string rol, string departamento, string contraseña)
        {
            conexion db = new conexion();

            using (SqlConnection cnn = db.Conexion())
            {
                cnn.Open();

                using (SqlTransaction transaccion = cnn.BeginTransaction())
                {
                    try
                    {
                        int i = 0;

                        string sql = "SELECT COUNT(*) FROM Usuarios";

                        using (SqlCommand cmd = new SqlCommand(sql, cnn, transaccion))
                        {
                            i = Convert.ToInt32(cmd.ExecuteScalar());
                        }
                        i = i + 1;
                        string iniciales = $"{char.ToUpper(nombre.Trim()[0])}{char.ToUpper(apellidos.Trim()[0])}";
                        string año = DateTime.Now.Year.ToString();
                        string numero = i.ToString("D2");

                        string carnet = $"{iniciales}-{año}{numero}";

                        int tipo = rol == "Empleado" ? 2 : 1;

                        int depa;

                        if (departamento == "San Salvador")
                            depa = 1;
                        else if (departamento == "Sonsonate")
                            depa = 2;
                        else if (departamento == "San Miguel")
                            depa = 3;
                        else if (departamento == "Usulutan")
                            depa = 4;
                        else
                            depa = 5;

                        string sql2 = @"INSERT INTO Usuarios
                    (Carnet, idDepartamento, Correo, Nombre,
                     Apellidos, Contraseña, Rol)
                    VALUES
                    (@carnet, @departamento, @correo, @nombre,
                     @apellidos, @contraseña, @rol)";

                        using (SqlCommand cmd2 = new SqlCommand(sql2, cnn, transaccion))
                        {
                            cmd2.Parameters.AddWithValue("@carnet", carnet);
                            cmd2.Parameters.AddWithValue("@departamento", depa);
                            cmd2.Parameters.AddWithValue("@correo", correo);
                            cmd2.Parameters.AddWithValue("@nombre", nombre);
                            cmd2.Parameters.AddWithValue("@apellidos", apellidos);
                            cmd2.Parameters.AddWithValue("@contraseña", contraseña);
                            cmd2.Parameters.AddWithValue("@rol", tipo);

                            cmd2.ExecuteNonQuery();
                        }

                        decimal[] descuentos = CalcularDescuentos(salario);

                        decimal isss = descuentos[0];
                        decimal afp = descuentos[1];
                        decimal renta = descuentos[2];

                        decimal totalDescuentos = isss + afp + renta;
                        decimal salarioNeto = salario - totalDescuentos;
                        string sql3 = @"INSERT INTO Salarios
                    (Carnet, Salario, ISSS, AFP, Renta,SalarioTotal, Activo)
                    VALUES
                    (@carnet, @salario, @isss, @afp, @renta, @salarioNeto, 1)";

                        using (SqlCommand cmd3 = new SqlCommand(sql3, cnn, transaccion))
                        {
                            cmd3.Parameters.AddWithValue("@carnet", carnet);
                            cmd3.Parameters.AddWithValue("@salario", salario);
                            cmd3.Parameters.AddWithValue("@isss", isss);
                            cmd3.Parameters.AddWithValue("@afp", afp);
                            cmd3.Parameters.AddWithValue("@renta", renta);
                            cmd3.Parameters.AddWithValue("@salarioNeto", salarioNeto);
                            cmd3.ExecuteNonQuery();
                        }
                        transaccion.Commit();
                        MessageBox.Show(
                            "El usuario fue guardado",
                            "Registro exitoso",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information
                        );

                        this.Close();
                    }
                    catch
                    {
                        transaccion.Rollback();
                        throw;
                    }
                }
            }
        }

        public decimal[] CalcularDescuentos(decimal salario)
        {
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

            return new decimal[] { isss, afp, renta };
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

        private void depaCombo_SelectedIndexChanged(object sender, EventArgs e)
        {

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
    }
}
