using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProyectoPeluqueriaApp.Models;

namespace ProyectoPeluqueriaApp.Services
{
    //Solo estructura de una interface
    public interface InterfazService
    {
        //Metodos que debe tener una clase para implementar la Interfaz
        void AgregarTurnos(string nombre, string dia, string hora);
        List<Turno> ObtenerTurnos();
        bool EliminarTurnoPorId(int id);

    }
}
