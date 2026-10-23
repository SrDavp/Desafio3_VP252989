using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Desafio3
{
    public partial class Inicio : Form
    {
        public Inicio()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void contraseñaTxt_TextChanged(object sender, EventArgs e)
        {

        }

        private void iniciar_Click(object sender, EventArgs e)
        {
            string usuario = usuarioTxt.Text.Trim();
            string contraseña = contraseñaTxt.Text;

            if (string.IsNullOrWhiteSpace(usuario))
            {
                MessageBox.Show("Debe ingresar su correo electrónico.",
                    "Campo obligatorio",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                usuarioTxt.Focus();
                return;
            }

            if (!System.Text.RegularExpressions.Regex.IsMatch(
                usuario, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                MessageBox.Show("Ingrese un correo electrónico válido.",
                    "Correo inválido",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                usuarioTxt.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(contraseña))
            {
                MessageBox.Show("Debe ingresar su contraseña.",
                    "Campo obligatorio",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                contraseñaTxt.Focus();
                return;
            }

            try
            {
                clases.Usuario sesion =
                    sql.usuarios.inicioSesion(usuario, contraseña);

                if (sesion != null)
                {
                    if (sesion.usuarioActivo == true)
                    {
                        if (sesion.Rol == 1)
                        {
                            vistas.Panel panel = new vistas.Panel(sesion);
                            panel.Show();
                            this.Hide();
                        }
                        else
                        {
                            vistas.Empleado empleado = new vistas.Empleado(sesion);
                            empleado.Show();
                            this.Hide();
                        }
                    }
                    else
                    {
                        MessageBox.Show(
                            "El usuario está desactivado. Si considera que es un error, contacte con su jefe inmediato.",
                            "Error de autenticación",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);

                        usuarioTxt.Clear();
                        contraseñaTxt.Clear();
                        usuarioTxt.Focus();
                    }
                }
                else
                {
                    MessageBox.Show("Correo o contraseña incorrectos.",
                        "Error de autenticación",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);

                    contraseñaTxt.Clear();
                    contraseñaTxt.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error al iniciar sesión: "
                    + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}
