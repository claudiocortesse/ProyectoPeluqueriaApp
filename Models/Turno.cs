using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoPeluqueriaApp.Models
{
    //Clase Turno
    public class Turno
    {
        public int Id { get; set; }
        public required  string NombreCliente { get; set; }
        public required string Dia { get; set; }
        public required string Hora { get; set; }

    }
}
