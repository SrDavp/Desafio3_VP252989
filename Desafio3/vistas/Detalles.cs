using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Desafio3.vistas
{
    public partial class Detalles : Form
    {
        string carnet;
        public Detalles(string carnetSeleccionado)
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            InitializeComponent();
            carnet = carnetSeleccionado;
            detalles();
            UsuarioUnicoDgv.Hide();
        }

        public void MostrarDgv(string carnetSeleccionado)
        {
            Empleado.Text = "Historial de pagos de " + carnetSeleccionado;
            UsuarioUnicoDgv.AllowUserToAddRows = false;
            UsuarioUnicoDgv.Show();
            UsuarioUnicoDgv.Columns.Clear();
            UsuarioUnicoDgv.AllowUserToAddRows = false;
            List<clases.Historial> Historial = clases.Panel.Historial(carnetSeleccionado);
            UsuarioUnicoDgv.Columns.Add("Carnet", "Carnet");
            UsuarioUnicoDgv.Columns.Add("Salario", "Salario");
            UsuarioUnicoDgv.Columns.Add("ISSS", "ISSS");
            UsuarioUnicoDgv.Columns.Add("AFP", "AFP");
            UsuarioUnicoDgv.Columns.Add("Renta", "Renta");
            UsuarioUnicoDgv.Columns.Add("Total", "Total");
            UsuarioUnicoDgv.Columns.Add("Fecha", "Fecha");
            UsuarioUnicoDgv.Columns.Add("Rol", "Rol");
            UsuarioUnicoDgv.Columns.Add("Activo", "Activo");
            UsuarioUnicoDgv.Columns.Add("Detalles", "Detalles");
            foreach (var u in Historial)
            {
                string rol;
                if (u.rol != 1)
                {
                    rol = "Empleado";
                }
                else
                {
                    rol = "Jefatura";
                }
                UsuarioUnicoDgv.Rows.Add(u.Carnet, "$" + u.Salario, "$"+u.ISSS, "$"+u.AFP, "$" + u.Renta, "$" + u.Total, u.fecha, rol, u.activo, u.detalles);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            detalles();
        }

        public void detalles()
        {
            Empleado.Text = "Detalles";
            UsuarioUnicoDgv.Hide();
            label2.Show();
            label3.Show();
            label4.Show();
            label5.Show();
            label6.Show();
            label7.Show();
            nombreEmpleado.Show();
            Salario.Show();
            correo.Show();
            apellido.Show();
            rol.Show();
            departamento.Show();
            correo.Show();


            List<clases.Usuario> usuarios = clases.Panel.UsuarioUnico(carnet);
            nombreEmpleado.Text = usuarios[0].Nombre;
            apellido.Text = usuarios[0].Apellidos;
            Salario.Text = "$" + usuarios[0].Salario;
            correo.Text = usuarios[0].Correo;
            rol.Text = usuarios[0].Rol == 1 ? "Jefatura" : "Empleado";
            departamento.Text = usuarios[0].Departamento;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            label2.Hide();
            label3.Hide();
            label4.Hide();
            label5.Hide();
            label6.Hide();
            label7.Hide();
            nombreEmpleado.Hide();
            Salario.Hide();
            correo.Hide();
            apellido.Hide();
            rol.Hide();
            departamento.Hide();
            correo.Hide();
            MostrarDgv(carnet);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int resultado = clases.Panel.Pagar(carnet);

                if (resultado > 0)
                {
                    MessageBox.Show("Pago registrado correctamente.");

                    // Actualizar el historial si está visible
                    if (UsuarioUnicoDgv.Visible)
                    {
                        MostrarDgv(carnet);
                    }
                }
                else if (resultado == 0)
                {
                    MessageBox.Show("Este empleado ya recibió su pago este mes.");
                }
                else if (resultado == -1)
                {
                    MessageBox.Show("El empleado no tiene un salario activo.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al registrar pago: " + ex.Message);
            }
        }
    }
}
