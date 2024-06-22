using Galkin.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Galkin.Context
{
    public class UserContext : DbContext
    {
        public DbSet<User> User { get; set; }
        public UserContext()
        {
            Database.EnsureCreated();
            User.Load();
        }
        ///<summary>
        ///Переопределение метода конфигурации
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseMySql(Config.Config.Connect, Config.Config.mySqlServerVersion);
    }
}
