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
    public partial class Panel : Form
    {
        private string carnetSeleccionado;
        private string sesion;
        public Panel(clases.Usuario usuario)
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            InitializeComponent();
            nombre.Text = "Bienvenid@ " + usuario.Nombre +" " + usuario.Apellidos;
            sesion = usuario.Carnet;
            mostrar(usuario.Carnet);
        }

        public void mostrar(string Carnet)
        {
            usuariosDgv.AllowUserToAddRows = false;
            List<clases.Usuario> usuarios = clases.Panel.MostrarUsuarios(Carnet);
            usuariosDgv.Columns.Clear();

            usuariosDgv.Columns.Add("Carnet", "Carnet");
            usuariosDgv.Columns.Add("Nombre", "Nombre");
            usuariosDgv.Columns.Add("Apellidos", "Apellidos");
            usuariosDgv.Columns.Add("Rol", "Rol");
            usuariosDgv.Columns.Add("Departamento", "Departamento");
            usuariosDgv.Columns.Add("Salario", "Salario");
            if (usuarios != null)
            {

                foreach (clases.Usuario u in usuarios)
                {
                    string rol;
                    if (u.Rol != 1)
                    {rol = "Empleado";
                    } else { rol = "Jefatura"; };
                    usuariosDgv.Rows.Add(u.Carnet, u.Nombre, u.Apellidos, rol, u.Departamento, "$"+u.Salario);
                }
            }
        }

        private void Panel_Load(object sender, EventArgs e)
        {

        }

        private void usuariosDgv_DoubleClick(object sender, EventArgs e)
        {
            DataGridViewRow filaSeleccionada = usuariosDgv.CurrentRow;
            string nombre = filaSeleccionada.Cells["Nombre"].Value.ToString();
            string apellidos = filaSeleccionada.Cells["Apellidos"].Value.ToString();
            carnetSeleccionado = filaSeleccionada.Cells["Carnet"].Value.ToString();
            bool activo;
            conexion db = new conexion();
            using (SqlConnection cnn = db.Conexion())
            {
                cnn.Open();
                string sql = "SELECT usuarioActivo FROM Usuarios WHERE carnet = @carnet";
                using (SqlCommand cmd = new SqlCommand(sql, cnn))
                {
                    cmd.Parameters.AddWithValue("@carnet", carnetSeleccionado);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            activo = Convert.ToBoolean(reader["usuarioActivo"]);
                            if (activo)
                            {
                                button2.Show();
                                button5.Show();
                            }
                            else
                            {
                                button2.Hide();
                                button5.Hide();

                            }
                        }
                    }
                }
            }
            Empleado.Text = "Empleado Seleccionado: " + nombre + " " + apellidos;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(carnetSeleccionado))
            {
                MessageBox.Show("Por favor, seleccione un empleado de la lista.",
                    "Campo obligatorio",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }
            vistas.Detalles Detalles = new Detalles(carnetSeleccionado);
            Detalles.ShowDialog();
        }

        private void usuariosDgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(carnetSeleccionado))
            {
                MessageBox.Show("Por favor, seleccione un empleado de la lista.",
                    "Campo obligatorio",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }
            vistas.Editar Editar = new Editar(carnetSeleccionado);
            Editar.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            vistas.Nuevo form = new Nuevo();
            form.ShowDialog();
        }

        private void actualizar_Click(object sender, EventArgs e)
        {
            mostrar(sesion);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Inicio sesion = new Inicio();
            sesion.Show();
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(carnetSeleccionado))
            {
                MessageBox.Show("Por favor, seleccione un empleado de la lista.",
                    "Campo obligatorio",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }
            else
            {
                DialogResult resultado = MessageBox.Show(
                    "¿Desea desactivar al usuario seleccionado?",
                    "Confirmar desactivación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                );

                if (resultado == DialogResult.Yes)
                {
                    try
                    {
                        desactivar(carnetSeleccionado);
                        MessageBox.Show("El usuario fue actualizado:",
                        "Actualizado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al guardar el usuario: " + ex.Message,
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        public bool desactivar(string carnet)
        {
            conexion cnn = new conexion();
            using (SqlConnection con = cnn.Conexion())
            {
                con.Open();
                string SQL = "UPDATE Usuarios SET usuarioActivo = 0 WHERE carnet = @carnet";
                using (SqlCommand cmd = new SqlCommand(SQL, con))
                {
                    cmd.Parameters.AddWithValue("@carnet", carnet);
                    int resultado = cmd.ExecuteNonQuery();
                    if (resultado > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            List<clases.Usuario> usuarios = clases.Panel.MostraTodos(sesion);
            usuariosDgv.Columns.Clear();

            usuariosDgv.Columns.Add("Carnet", "Carnet");
            usuariosDgv.Columns.Add("Nombre", "Nombre");
            usuariosDgv.Columns.Add("Apellidos", "Apellidos");
            usuariosDgv.Columns.Add("Rol", "Rol");
            usuariosDgv.Columns.Add("Departamento", "Departamento");
            usuariosDgv.Columns.Add("Salario", "Salario");
            usuariosDgv.Columns.Add("Activo", "Activo");
            if (usuarios != null)
            {

                foreach (clases.Usuario u in usuarios)
                {
                    string rol;
                    if (u.Rol != 1)
                    {
                        rol = "Empleado";
                    }
                    else { rol = "Jefatura"; }
                    ;
                    usuariosDgv.Rows.Add(u.Carnet, u.Nombre, u.Apellidos, rol, u.Departamento, "$" + u.Salario, u.usuarioActivo);
                }
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            mostrar(sesion);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(carnetSeleccionado))
            {
                MessageBox.Show("Por favor, seleccione un empleado de la lista.",
                    "Campo obligatorio",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }
            else
            {
                DialogResult resultado = MessageBox.Show(
                    "¿Desea activar al usuario seleccionado?",
                    "Confirmar activacion",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                );

                if (resultado == DialogResult.Yes)
                {
                    try
                    {
                        activar(carnetSeleccionado);
                        MessageBox.Show("El usuario fue actualizado:",
                        "Actualizado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al guardar el usuario: " + ex.Message,
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        public bool activar(string carnet)
        {
            conexion cnn = new conexion();
            using (SqlConnection con = cnn.Conexion())
            {
                con.Open();
                string SQL = "UPDATE Usuarios SET usuarioActivo = 1 WHERE carnet = @carnet";
                using (SqlCommand cmd = new SqlCommand(SQL, con))
                {
                    cmd.Parameters.AddWithValue("@carnet", carnet);
                    int resultado = cmd.ExecuteNonQuery();
                    if (resultado > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }
    }
}
