using FirstWebApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirstWebApi
{
    public class FirstWebApiEntities : DbContext
    {
        public FirstWebApiEntities(DbContextOptions<FirstWebApiEntities> options) : base(options)
        {

        }

        public DbSet<User> users { get; set; }

    }
}
