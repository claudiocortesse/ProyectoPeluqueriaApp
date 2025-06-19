using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ProyectoPeluqueriaApp.Data;
using ProyectoPeluqueriaApp.Models;

namespace ProyectoPeluqueriaApp.Services
{
    //Implementacion de interface
    public class TurnoService : InterfazService
    {

        MensajesService mensajesService = new MensajesService();

        //Propiedades privadas de la clase
        private readonly AppDbContext _db;

        //Constructor de la clase
        public TurnoService()
        {
            _db = new AppDbContext();
        }
        //Metodo que recibe datos y los guarda en la lista
        public void AgregarTurnos(string nombre, string dia, string hora)
        {
            //Controla que los datos ingresados sean validos
            if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(dia) || string.IsNullOrWhiteSpace(hora))
            {
                mensajesService.MostrarMensaje("Los datos ingresados no son validos.", ConsoleColor.Red);
                return;
            }
            using (var db = new AppDbContext())
            {
                if (db.Turnos.Any(t => t.Dia == dia && t.Hora == hora))
                {
                    mensajesService.MostrarMensaje("Ya existe un turno agendado para este dia y hora.", ConsoleColor.Red);
                    return;
                }
                //Instancia para appdbcontext
                //Crea objeto de la clase turno
                var turno = new Turno()
                {
                    NombreCliente = nombre,
                    Dia = dia,
                    Hora = hora
                };
                //Agrega el objeto creado a la lista
                db.Turnos.Add(turno);
                //Suma 1 al proximo idTurno
                db.SaveChanges();
            }
            mensajesService.MostrarMensaje("Turno Agendado.", ConsoleColor.Green);
            Console.Clear();

        }
        //Metodo que muestra la lista
        public List<Turno> ObtenerTurnos()
        {
            using (var db = new AppDbContext())
            {
                return db.Turnos.ToList();
            }
        }
        //Metodo que elimina el id ingresado por el usuario
        public bool EliminarTurnoPorId(int id)
        {
            using (var db = new AppDbContext())
            {
                //Si encuentra el valor ingresado lo guarda en la variable
                var turnosAEliminar = db.Turnos.FirstOrDefault(t => t.Id == id);
                //Devuelve
                if (turnosAEliminar != null)
                {
                    db.Turnos.Remove(turnosAEliminar);
                    db.SaveChanges();
                    mensajesService.MostrarMensaje("El turno se elimino correctamente.", ConsoleColor.Green);
                    return true;
                }
                else
                {
                    mensajesService.MostrarMensaje("El Id ingresado no existe.\n", ConsoleColor.Red);
                    return false;
                }
            }
        }

    }
}
