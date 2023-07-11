using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace dotnet_rpg.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }

       
        // we are able to query and save our data - we do this whenever we want a table of our model in the db
        public DbSet<User> Users => Set<User>();  

        public DbSet<TinyUrl> TinyUrls => Set<TinyUrl>();

         public DbSet<Usage> Usages => Set<Usage>();

         public DbSet<QandA> QandAs => Set<QandA>();

         public DbSet<Answer> Answers => Set<Answer>();

         public DbSet<Bill> Bills => Set<Bill>();

        
    }

}