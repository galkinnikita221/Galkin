using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Galkin.Context.Config
{
    public class Config
    {
        public static readonly string Connect = "server=localhost;uid=root;port=3307;database=cinemaGalkin;pwd=";
        public static MySqlServerVersion mySqlServerVersion = new MySqlServerVersion(new Version(8, 0, 11));
    }
}
