using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTestingWebApiDemo;

namespace TestingProject
{
    public class MySqlTest : TestContact
    {
        public static string connectionString = "Server=localhost;port=3306;Database=student;user id=root;password=";

        public MySqlTest()
            : base(
                new DbContextOptionsBuilder<AppDbContext>()
                    .UseMySQL(connectionString)
                    .Options)
        {
        }
    }
}