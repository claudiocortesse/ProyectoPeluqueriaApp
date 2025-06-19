using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProyectoPeluqueriaApp.Services
{
    public class MensajesService
    {
        //Cambio el color del mensaje segun sea necesario
        public void MostrarMensaje(string mensaje, ConsoleColor color = ConsoleColor.White, int tiempo = 3000)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(mensaje);
            Thread.Sleep(tiempo);
            Console.ResetColor();
        }
    }
}
