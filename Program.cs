using System;
using System.Collections.Generic;
using System.Threading;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ProyectoPeluqueriaApp.Models;
using ProyectoPeluqueriaApp.Services;
using Microsoft.EntityFrameworkCore;
using ProyectoPeluqueriaApp.Data;

namespace ProyectoPeluqueria
{
    internal class Program
    {

        //Variables global
        static bool salir = true;
        //Instanciar clase TurnoService para acceder a sus metodos en el main
        static InterfazService turnoService = new TurnoService();
        static MensajesService mensajesService = new MensajesService();
        static void Main(string[] args)
        {
            //Crea una instancia para appdbcontext
            using (var db = new AppDbContext())
            { 
                db.Database.Migrate();
            }
            //Llamada a funsiones en main
            MenuPrincipal();

            //Funsion Menu Principal
            void MenuPrincipal()
            {
                while (salir)
                {
                    int opcion;
                    //Menu opciones
                    Console.Clear();
                    Console.WriteLine("===MENU PRINCIPAL===\n");
                    Console.WriteLine("1.Ver turnos");
                    Console.WriteLine("2.Agregar turnos");
                    Console.WriteLine("3.Eliminar turno");
                    Console.WriteLine("4.Salir");
                    //Controlar que ingrese valor numerico
                    while (!int.TryParse(Console.ReadLine(), out opcion))
                    {
                        Console.WriteLine("La opcion ingresada no existe.");
                    }
                    //Para controlar opciones usas switch
                    switch (opcion)
                    {
                        case 1:
                            VerTurnos();
                            break;

                        case 2:
                            AgregarTurno();
                            break;

                        case 3:
                            EliminarTurno();
                            break;

                        case 4:
                            SalirApp();
                            break;
                        //Si no elije una opcion numerica que exista
                        default:
                            mensajesService.MostrarMensaje("Seleccione una de las opciones.", ConsoleColor.Red);
                            break;

                    }
                }
            }
            //Funsion lista de turnos
            void VerTurnos()
            {
                Console.Clear();
                Console.WriteLine("===TURNOS===");
                //Crea una variable para el metodo que contiene la lista
                var lista = turnoService.ObtenerTurnos();
                //Ingresa si la lista contiene valores
                if (lista.Count > 0)
                {
                    foreach (var turno in lista)
                    {
                        Console.WriteLine($"Turno: ID {turno.Id} - {turno.NombreCliente} - {turno.Dia} - {turno.Hora}");
                    }
                    Console.WriteLine();
                    //Mostrara la lista hasta que presione 1
                    VolverMenu();
                }
                //Sale de la lista si no hay valores
                else
                {
                    mensajesService.MostrarMensaje("No se encontrarion turnos agendados", ConsoleColor.Red);
                }
            }
            //Funsion agregar turnos
            void AgregarTurno()
            {
                //Variable local de la funsion que maneja el bucle
                bool continuar = true;
                while (continuar)
                {
                    Console.Clear();
                    //Datos del cliente
                    Console.WriteLine("Por favor ingrese los datos del cliente.\n");
                    //Comprobar que los datos no sean nulos
                    Console.WriteLine("Nombre");
                    string? nombre = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(nombre))
                    {
                        mensajesService.MostrarMensaje("El Nombre no puede estar vacio.", ConsoleColor.Red);
                    }
                    Console.WriteLine("Dia del turno");
                    string? dia = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(dia))
                    {
                        mensajesService.MostrarMensaje("El dia no puede estar vacio.", ConsoleColor.Red);
                    }
                    Console.WriteLine("Hora del turno");
                    string? hora = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(hora))
                    {
                        mensajesService.MostrarMensaje("La hora no puede estar vacio.", ConsoleColor.Red);
                    }
                    //Llama al metodo que almacena los datos en la lista
                    turnoService.AgregarTurnos(nombre!, dia!, hora!);

                    //Consulta
                    Console.WriteLine("Agendar un nuevo turno?\n" +
                       "1.SI\n" +
                       "2.NO");
                    //Funsion repetir
                    if (!Repetir())
                    {
                        break;
                    }
                }
            }
            //Funsion eliminar turnos
            void EliminarTurno()
            {
                //Variable para el metodo que contiene la lista
                var lista = turnoService.ObtenerTurnos();
                //Variable local para el bucle
                bool continuar = true;
                //Si hay turnos
                if (lista.Count > 0)
                {
                    while (continuar)
                    {
                        Console.Clear();
                        Console.WriteLine("===ELIMINAR TURNOS===\n");
                        //Muestra todos los turnos
                        foreach (var turno in lista)
                        {
                            Console.WriteLine($"Cliente {turno.Id}: {turno.NombreCliente} - {turno.Dia} - {turno.Hora}");
                        }
                        Console.WriteLine();
                        Console.WriteLine("Por favor ingrese el numero de Cliente.");
                        //Pide un id de la lista
                        int.TryParse(Console.ReadLine(), out int idCliente);

                        //Verificar que exista el id ingresado
                        turnoService.EliminarTurnoPorId(idCliente);

                        //Si no hay mas turnos que salga directamente
                        if (lista.Count == 0)
                        {
                            Console.Clear();
                            mensajesService.MostrarMensaje("No quedan turnos por eliminar!", ConsoleColor.Blue);
                            continuar = false;
                            break;
                        }
                        else
                        {
                            //Si hay turnos que pregunte
                            Console.Clear();
                            Console.WriteLine("Quiere eliminar otro turno?\n" +
                                "1.SI\n" +
                                "2.NO");
                            //Funsion repetir si es "false" sale
                            if (!Repetir())
                            {
                                continuar = false;
                                break;
                            }
                        }
                    }
                }
                //Si no hay turnos
                else
                {
                    mensajesService.MostrarMensaje("No hay turnos agendados.", ConsoleColor.Red);
                }

            }
            //Funsion Salir del programa
            void SalirApp()
            {
                Console.Clear();
                Console.WriteLine("Estas seguro que quieres salir?\n" +
                    "1.SI\n" +
                    "2.NO");
                while (salir)
                {
                    int.TryParse(Console.ReadLine(), out int opcion);
                    if (opcion == 1)
                    {
                        Console.Clear();
                        mensajesService.MostrarMensaje("Adios..", ConsoleColor.Blue);
                        salir = false;

                    }
                    else if (opcion == 2)
                    {
                        opcion = 0;
                        Console.Clear();
                        mensajesService.MostrarMensaje("Volviendo al menu principal..", ConsoleColor.Green);
                        break;

                    }
                    else
                    {
                        Console.WriteLine("Opcion invalida.");
                    }
                }
            }

            //Mini Funsiones
            void VolverMenu()
            {
                bool continuar = true;
                while (continuar)
                {
                    Console.WriteLine("Para volver al menu presione (1).");
                    int.TryParse(Console.ReadLine(), out int opcion);
                    if (opcion == 1)
                    {
                        continuar = false;
                    }
                }
            }

            bool Repetir()
            {
                //Repetir funsion
                while (true)
                {
                    if (int.TryParse(Console.ReadLine(), out int opcion))
                    {
                        if (opcion == 1)
                        {
                            return true;
                        }
                        else if (opcion == 2)
                        {
                            mensajesService.MostrarMensaje("Volviendo al menu principal..", ConsoleColor.Green);
                            return false;
                        }
                        else
                        {
                            Console.WriteLine("Opcion incorrecta.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Opcion incorrecta.");
                    }
                }

            }
        }

    }

}