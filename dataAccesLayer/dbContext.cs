using System;
using System.Collections.Generic;
using System.Text;

//importación paquetes necesarios
using Microsoft.EntityFrameworkCore;
using entityesLayer;


namespace dataAccesLayer
{
    public class dbContext : DbContext
    {
        public DbSet<productCategory> productcategory { get; set; }
        public DbSet<products> products { get; set; }
        public DbSet<registeredUsers> registeredusers { get; set; }
        public DbSet<rol> rol { get; set; }
        public DbSet<ventas> ventas { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("workstation id=ApiDatabasePruebaTecnica.mssql.somee.com;packet size=4096;user id=RJAV4;pwd=12345678;data source=ApiDatabasePruebaTecnica.mssql.somee.com;persist security info=False;initial catalog=ApiDatabasePruebaTecnica");
        }
    }
}
