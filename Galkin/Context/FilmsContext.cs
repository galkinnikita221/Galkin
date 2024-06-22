using Galkin.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Galkin.Context
{
    public class FilmsContext : DbContext
    {
        public DbSet<Films> Films { get; set; }
        public FilmsContext()
        {
            Database.EnsureCreated();
            Films.Load();
        }
        ///<summary>
        ///Переопределение метода конфигурации
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseMySql(Config.Config.Connect, Config.Config.mySqlServerVersion);
    }
}
