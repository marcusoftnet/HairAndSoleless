using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Database;

namespace HairAndSoleless.Models.Storage
{
    public class HairAndSolelessContext : DbContext
    {
        public IDbSet<HairAndSoleless.Models.Activity> Activities { get; set; }

        public IDbSet<HairAndSoleless.Models.Customer> Customers { get; set; }

        public IDbSet<HairAndSoleless.Models.Coach> Coaches { get; set; }

        public HairAndSolelessContext()
        {
            // Instructions:
            //  * You can add custom code to this file. Changes will *not* be lost when you re-run the scaffolder.
            //  * If you want to regenerate the file totally, delete it and then re-run the scaffolder.
            //  * You can delete these comments if you wish
            //  * If you want Entity Framework to drop and regenerate your database automatically whenever you 
            //    change your model schema, uncomment the following line:
            DbDatabase.SetInitializer(new HairAndSolelessInitializer());
        }
    }

    public class HairAndSolelessInitializer : DropCreateDatabaseIfModelChanges<HairAndSolelessContext>
    {
        protected override void Seed(HairAndSolelessContext context)
        {
            var coaches = new List<Coach>
                              {
                                  new Coach { Name = "Marcus", Team = "Enzo", Email = "marcus@avega.se"},
                                  new Coach { Name = "Hugo", Team = "Modero", Email = "hugo@avega.se"},
                                  new Coach { Name = "Jocke", Team = "Modero", Email = "jocke@avega.se"},
                                  new Coach { Name = "Damra", Team = "Elevate", Email = "damra@avega.se"}
                              };
            coaches.ForEach(c => context.Coaches.Add((c)));


            var customers = new List<Customer>
                                {
                                    new Customer { Name = "Länsförsäkringar", Contact = "micke@lf.se"},
                                    new Customer { Name = "SHB", Contact = "anna@shb.se"},
                                    new Customer { Name = "EKN", Contact = "ulf@ekn.se"}
                                };
            customers.ForEach(c => context.Customers.Add((c)));


            var activities = new List<Activity>
                                 {
                                     new Activity{  Coach = coaches[0],  Customer = customers[0],  NumberOfHours = 8, Heading = "Workshop BDD", Date = DateTime.Now.AddDays(-100)},
                                     new Activity{  Coach = coaches[0],  Customer = customers[1],  NumberOfHours = 4, Heading = "Workshop Agile", Date = DateTime.Now.AddDays(-10)},
                                     new Activity{  Coach = coaches[2],  Customer = customers[0],  NumberOfHours = 16, Heading = "Workshop Web", Date = DateTime.Now.AddDays(-12)},
                                 };
            activities.ForEach(a => context.Activities.Add(a));

        }
    }
}