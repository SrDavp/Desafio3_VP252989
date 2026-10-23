using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Collections.Specialized.BitVector32;

namespace Desafio3.vistas
{
    public partial class Empleado : Form
    {
        public Empleado(clases.Usuario sesion)
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            InitializeComponent();
            mostrarEmpleado(sesion.Carnet);
            nombre.Text = "Bienvenido " + sesion.Nombre + " " + sesion.Apellidos;
            label3.Text = "Departamente: " + sesion.Departamento;
            label4.Text = "Carnet: " + sesion.Carnet;
            label5.Text = "Correo: " + sesion.Correo;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        public void mostrarEmpleado(string carnet)
        {
            dgvEmpleado.Columns.Clear();
            dgvEmpleado.AllowUserToAddRows = false;
            List<clases.Historial> Historial = clases.Panel.HistorialEmpleado(carnet);
            if (Historial.Count == 0)
            {
                dgvEmpleado.Hide();
                nopagos1.Show();
                nopagos2.Show();
                nopagos3.Show();
                return;
            }
            nopagos1.Hide();
            nopagos2.Hide();
            nopagos3.Hide();
            dgvEmpleado.Columns.Add("Carnet", "Carnet");
            dgvEmpleado.Columns.Add("Salario", "Salario");
            dgvEmpleado.Columns.Add("ISSS", "ISSS");
            dgvEmpleado.Columns.Add("AFP", "AFP");
            dgvEmpleado.Columns.Add("Renta", "Renta");
            dgvEmpleado.Columns.Add("Total", "Total");
            dgvEmpleado.Columns.Add("Fecha", "Fecha");
            dgvEmpleado.Columns.Add("Rol", "Rol");
            dgvEmpleado.Columns.Add("Activo", "Activo");
            dgvEmpleado.Columns.Add("Detalles", "Detalles");
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
                dgvEmpleado.Rows.Add(u.Carnet, "$" + u.Salario, "$" + u.ISSS, "$" + u.AFP, "$" + u.Renta, "$" + u.Total, u.fecha, rol, u.activo, u.detalles);
            }
        }

        private void cerrar_Click(object sender, EventArgs e)
        {
            this.Hide();
            Inicio login = new Inicio();
            login.Show();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
