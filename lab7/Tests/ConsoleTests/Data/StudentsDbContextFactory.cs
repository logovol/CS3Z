using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ConsoleTests.Data
{
    public class StudentsDBContextFactory : IDesignTimeDbContextFactory<StudentsDB>
    {
        public StudentsDB CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<StudentsDB>();
            const string connection_str = @"Data Source=NEMAN\SQLEXPRESS;Initial Catalog=CS3;Integrated Security=True";
            optionsBuilder.UseSqlServer(connection_str);

            return new StudentsDB(optionsBuilder.Options);
        }
      
    }
}
