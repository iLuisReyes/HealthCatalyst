using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using HealthCatalyst.Assessment.Domain.Models;
using Faker;

namespace HealthCatalyst.Assessment.Domain.DataAccess
{
    /// <summary>
    /// Initializes the database and seeds it with data.
    /// </summary>
    internal class DatabaseInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<RosterContext>
    {
        /// <summary>
        /// Seeds a table with data.
        /// </summary>
        /// <param name="context"></param>
        protected override void Seed(RosterContext context)
        {
            var people = new List<Teammate>
            {
                new Teammate { FirstName="Jeff", LastName="Hornacek", BirthDate = DateTime.Parse("05/03/1963"), Height = "6-3", PrimaryPosition = Position.SG, IsStarter = true, Interests = "Sticking near the permiter, taking long shots, scoring in threes.", Address=Faker.Address.StreetAddress(), City=Faker.Address.City(), State=Faker.Address.UsState(), Zipcode=Faker.Address.ZipCode()  },
                new Teammate { FirstName="Karl", LastName="Malone", BirthDate = DateTime.Parse("07/24/1963"), Height = "6-9", PrimaryPosition = Position.PF, IsStarter = true, Interests = "Hangin by the post, driving hard, delivering the mail.", Address=Faker.Address.StreetAddress(), City=Faker.Address.City(), State=Faker.Address.UsState(), Zipcode=Faker.Address.ZipCode() },
                new Teammate { FirstName="Greg", LastName="Ostertag", BirthDate = DateTime.Parse("03/06/1973"), Height = "7-2", PrimaryPosition = Position.C, IsStarter = true, Interests = "Standing tall, defending the rim, waiting to rebound.", Address=Faker.Address.StreetAddress(), City=Faker.Address.City(), State=Faker.Address.UsState(), Zipcode=Faker.Address.ZipCode() },
                new Teammate { FirstName="Bryon", LastName="Russell", BirthDate = DateTime.Parse("12/31/1970"), Height = "6-7", PrimaryPosition = Position.SF, IsStarter = true, Interests = "Driving the lanes, posting up, laying down picks.", Address=Faker.Address.StreetAddress(), City=Faker.Address.City(), State=Faker.Address.UsState(), Zipcode=Faker.Address.ZipCode() },
                new Teammate { FirstName="John", LastName="Stockton", BirthDate = DateTime.Parse("03/26/1962"), Height = "6-1", PrimaryPosition = Position.PG, IsStarter = true, Interests = "Psyching out defenders, stealing balls, serving shots to my friends.", Address=Faker.Address.StreetAddress(), City=Faker.Address.City(), State=Faker.Address.UsState(), Zipcode=Faker.Address.ZipCode() },
                new Teammate { FirstName="Shandon", LastName="Anderson", BirthDate = DateTime.Parse("12/31/1973"), Height = "6-6", PrimaryPosition = Position.SG, IsStarter = false, Interests = "Pick-n-rolls, hitting free throws, giving defenders the wobbly leg.", Address=Faker.Address.StreetAddress(), City=Faker.Address.City(), State=Faker.Address.UsState(), Zipcode=Faker.Address.ZipCode() },
                new Teammate { FirstName="Antoine", LastName="Carr", BirthDate = DateTime.Parse("07/23/1961"), Height = "6-9", PrimaryPosition = Position.PF, IsStarter = false, Interests = "Drawing fouls, hook shots, defending the lane.", Address=Faker.Address.StreetAddress(), City=Faker.Address.City(), State=Faker.Address.UsState(), Zipcode=Faker.Address.ZipCode() },
                new Teammate { FirstName="William", LastName="Cunningham", BirthDate = DateTime.Parse("03/25/1974"), Height = "6-11", PrimaryPosition = Position.C, IsStarter = false, Interests = "Boxing out, tending the goal, dunkin.", Address=Faker.Address.StreetAddress(), City=Faker.Address.City(), State=Faker.Address.UsState(), Zipcode=Faker.Address.ZipCode() },
                new Teammate { FirstName="Howard", LastName="Eisley", BirthDate = DateTime.Parse("12/04/1972"), Height = "6-2", PrimaryPosition = Position.PG, IsStarter = false, Interests = "Crossovers, jumpshots, and easy layups.", Address=Faker.Address.StreetAddress(), City=Faker.Address.City(), State=Faker.Address.UsState(), Zipcode=Faker.Address.ZipCode() },
                new Teammate { FirstName="Greg", LastName="Foster", BirthDate = DateTime.Parse("10/03/1968"), Height = "6-11", PrimaryPosition = Position.C, IsStarter = false, Interests = "Intimidating the opposition, grabbing boards, swatting flies.", Address=Faker.Address.StreetAddress(), City=Faker.Address.City(), State=Faker.Address.UsState(), Zipcode=Faker.Address.ZipCode() },
                new Teammate { FirstName="Troy", LastName="Hudson", BirthDate = DateTime.Parse("03/13/1976"), Height = "6-1", PrimaryPosition = Position.PG, IsStarter = false, Interests = "Splitting defenders, calling picks, assisting the mailman with his routes.", Address=Faker.Address.StreetAddress(), City=Faker.Address.City(), State=Faker.Address.UsState(), Zipcode=Faker.Address.ZipCode() },
                new Teammate { FirstName="Adam", LastName="Keefe", BirthDate = DateTime.Parse("02/22/1970"), Height = "6-9", PrimaryPosition = Position.C, IsStarter = false, Interests = "Jump balls, receiving the assist, and slammin em down.", Address=Faker.Address.StreetAddress(), City=Faker.Address.City(), State=Faker.Address.UsState(), Zipcode=Faker.Address.ZipCode() },
                new Teammate { FirstName="Chris", LastName="Morris", BirthDate = DateTime.Parse("01/20/1966"), Height = "6-8", PrimaryPosition = Position.SF, IsStarter = false, Interests = "Banks shots, alley-oops, finger lickin good.", Address=Faker.Address.StreetAddress(), City=Faker.Address.City(), State=Faker.Address.UsState(), Zipcode=Faker.Address.ZipCode() },
                new Teammate { FirstName="Jacque", LastName="Vaughn", BirthDate = DateTime.Parse("02/11/1975"), Height = "6-1", PrimaryPosition = Position.PG, IsStarter = false, Interests = "Fade aways, off the glass, nothing but net.", Address=Faker.Address.StreetAddress(), City=Faker.Address.City(), State=Faker.Address.UsState(), Zipcode=Faker.Address.ZipCode() },
            };

            people.ForEach(s => context.Team.Add(s));
            context.SaveChanges();
        }
    }
}
