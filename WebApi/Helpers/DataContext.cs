using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using WebApi.Entities;
using AutoMapper;

namespace WebApi.Helpers
{
   // [Table("NgUsers")] 
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options){}
        
        
        public DbSet<User> Users { get;set; }
        public DbSet<Comment> Comments { get;set; }
        //[DisplayName("Users")]   

        // public DbSet<User> NgUsers { get;set; }

        //    protected override void OnModelCreating(DbModelBuilder modelBuilder )
        //    {

        //    }
        // protected override void OnModelCreating(ModelBuilder modelBuilder) 
        //     => modelBuilder.Entity<User>().
            

        
    }
}