using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio3.clases
{
    public class Usuario
    {
        public string Carnet { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string Correo { get; set; }
        public string Contraseña { get; set; }
        public int Rol { get; set; }
        public string Departamento { get; set; }
        public decimal Salario { get; set; }
        public bool usuarioActivo { get; set; }
    }
}
