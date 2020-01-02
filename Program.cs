using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Rabbits_Task_Connect_To_SQL_EntityFramework
{
    class Program
    {
        public static List<Rabbit> rabbits = new List<Rabbit>();
        static void Main(string[] args)
        {
            addRabbit();

            listRabbits();

            updateRabbit();

            listRabbits();
        }

        #region listRabbits
        static void listRabbits()
        {
            using (var db = new RabbitDbContext())
            {
                rabbits = db.Rabbits.ToList();
            }
            rabbits.ForEach(r => Console.WriteLine($"{r.RabbitId,-10}{r.RabbitName,-20}{r.Age}"));
        }
        #endregion

        #region addRabbit
        static void addRabbit()
        {
            using (var db = new RabbitDbContext())
            {
                var rabbit = new Rabbit(3, "Rabbit3", 0);
                //add to database
                db.Rabbits.Add(rabbit);
                db.SaveChanges();
            }
        }
        #endregion

        #region updateRabbit
        static void updateRabbit()
        {
            using (var db = new RabbitDbContext())
            {
                var rabbitToUpdate = db.Rabbits.Find(3);
                rabbitToUpdate.RabbitName = "Rabbit3 has a new name";
                db.SaveChanges();
            }
        }
        #endregion

        //remove or delete just uses db.Remove(rabbit)
    }

    #region Rabbit_class
    class Rabbit
    {
        public int RabbitId { get; set; }
        public string RabbitName { get; set; }
        public int Age { get; set; }

        public Rabbit(int RabbitId, string RabbitName, int Age)
        {
            this.RabbitId = RabbitId;
            this.RabbitName = RabbitName;
            this.Age = Age;
        }
    }
    #endregion

    #region RabbitDbContext : connect to database
    class RabbitDbContext : DbContext
    {
        //set table in Database called 'Rabbits'
        public DbSet<Rabbit> Rabbits { get; set; }

        //method to connect to database
        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=RabbitDb;";
            builder.UseSqlServer(connectionString);
        }
    }
    #endregion
}
