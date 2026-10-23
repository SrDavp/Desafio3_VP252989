using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio3.clases
{
    internal class Historial
    {
        public string Carnet { get; set; }
        public decimal Salario { get; set; }
        public decimal ISSS { get; set; }
        public decimal AFP { get; set; }
        public decimal Renta { get; set; }
        public decimal Total { get; set; }
        public DateTime fecha { get; set; }
        public int rol { get; set; }
        public bool activo { get; set; }
        public string detalles { get; set; }
    }
}
