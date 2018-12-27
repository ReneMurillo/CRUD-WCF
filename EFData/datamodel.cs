using EFData.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFData
{
    public class datamodel:DbContext
    {
        public DbSet<Persona> People { get; set; }
    }
}
