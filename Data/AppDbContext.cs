using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Permite trabajar con base de datos
using Microsoft.EntityFrameworkCore;
//Importa el modelo Turno que va a guardar
using ProyectoPeluqueriaApp.Models;

namespace ProyectoPeluqueriaApp.Data
{
    //Clase que hereda de DbContext (clase base de EF Core) y maneja la conexion con base de datos
    //Esta clase funsiona como entrada para acceder a tus datos desde codigo
    public class AppDbContext : DbContext
    {
        //Esto representa la tabla dentro de la base de datos real
        public DbSet<Turno> Turnos { get; set; }

        //Este metodo indica a EF tipo de db y ubicacion
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //Usa SQLite y usa archivo turnos.db como base de datos
            optionsBuilder.UseSqlite("Data Source=Turnos.db");
        }
    }
}
