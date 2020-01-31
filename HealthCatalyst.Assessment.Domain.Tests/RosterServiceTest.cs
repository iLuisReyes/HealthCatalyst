using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using Moq;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using HealthCatalyst.Assessment.Domain.Models;
using HealthCatalyst.Assessment.Domain.Services;
using HealthCatalyst.Assessment.Domain.DataAccess;

namespace HealthCatalyst.Assessment.Domain.Tests
{
    [TestClass]
    public class RosterServiceTest
    {
        RosterService service;
        Mock<RosterContext> dbContext;
        Mock<DbSet<Teammate>> dbSet;
        Teammate testSubject;


        [TestInitialize]
        public void Initialize()
        {
            dbSet = new Mock<DbSet<Teammate>>();
            dbContext = new Mock<RosterContext>();
            service = new RosterService(dbContext.Object, false);

            testSubject = new Teammate
            {
                FirstName = Faker.Name.First(),
                LastName = Faker.Name.Last(),
                Address = Faker.Address.StreetAddress(),
                City = Faker.Address.City(),
                BirthDate = DateTime.Parse($"{Faker.RandomNumber.Next(1, 12)}/{Faker.RandomNumber.Next(1, 28)}/{Faker.RandomNumber.Next(1960, 2000)}"),
                ID = Faker.RandomNumber.Next(1, 1000),
                Interests = Faker.Lorem.Sentence(3),
                IsStarter = Convert.ToBoolean(Faker.RandomNumber.Next(0, 1)),
                State = Faker.Address.UsStateAbbr(),
                Zipcode = Faker.Address.ZipCode(),
            };
        }

        [TestMethod]
        public void CreateTeammate()
        {
            dbContext.Setup(x => x.Set<Teammate>())
                .Returns(dbSet.Object);;

            var result = service.CreateTeammate(testSubject);

            Assert.AreEqual(testSubject, result);
            dbSet.Verify(x => x.Add(It.IsAny<Teammate>()), Times.Once);
            dbContext.Verify(x => x.SaveChanges(), Times.Once);
        }

        [TestMethod]
        public void GetTeam()
        {
            var data = new List<Teammate>
            {
                testSubject
            }.AsQueryable();

            dbSet.As<IQueryable<Teammate>>().Setup(m => m.Provider).Returns(data.Provider);
            dbSet.As<IQueryable<Teammate>>().Setup(m => m.Expression).Returns(data.Expression);
            dbSet.As<IQueryable<Teammate>>().Setup(m => m.ElementType).Returns(data.ElementType);
            dbSet.As<IQueryable<Teammate>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            dbContext.Setup(c => c.Team).Returns(dbSet.Object);

            var result = service.GetTeam();

            Assert.AreEqual(1, result?.Count());
            Assert.IsTrue(testSubject.Equals(result.FirstOrDefault()));
        }

        [TestMethod]
        public void SearchTeam()
        {
            var data = new List<Teammate>
            {
                testSubject
            }.AsQueryable();

            dbSet.As<IQueryable<Teammate>>().Setup(m => m.Provider).Returns(data.Provider);
            dbSet.As<IQueryable<Teammate>>().Setup(m => m.Expression).Returns(data.Expression);
            dbSet.As<IQueryable<Teammate>>().Setup(m => m.ElementType).Returns(data.ElementType);
            dbSet.As<IQueryable<Teammate>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            dbContext.Setup(c => c.Team).Returns(dbSet.Object);

            var result = service.SearchTeam(testSubject.LastName);

            Assert.AreEqual(1, result?.Count());
            Assert.IsTrue(testSubject.Equals(result.FirstOrDefault()));
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void SearchTeam_NullSearchTerm()
        {
            var data = new List<Teammate>
            {
                testSubject
            }.AsQueryable();

            dbSet.As<IQueryable<Teammate>>().Setup(m => m.Provider).Returns(data.Provider);
            dbSet.As<IQueryable<Teammate>>().Setup(m => m.Expression).Returns(data.Expression);
            dbSet.As<IQueryable<Teammate>>().Setup(m => m.ElementType).Returns(data.ElementType);
            dbSet.As<IQueryable<Teammate>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            dbContext.Setup(c => c.Team).Returns(dbSet.Object);

            var result = service.SearchTeam(null);
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void SearchTeam_EmptySearchTerm()
        {
            var data = new List<Teammate>
            {
                testSubject
            }.AsQueryable();

            dbSet.As<IQueryable<Teammate>>().Setup(m => m.Provider).Returns(data.Provider);
            dbSet.As<IQueryable<Teammate>>().Setup(m => m.Expression).Returns(data.Expression);
            dbSet.As<IQueryable<Teammate>>().Setup(m => m.ElementType).Returns(data.ElementType);
            dbSet.As<IQueryable<Teammate>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            dbContext.Setup(c => c.Team).Returns(dbSet.Object);

            var result = service.SearchTeam(string.Empty);
        }


        [TestMethod]
        public void GetTeammate()
        {
            var data = new List<Teammate>
            {
                testSubject
            }.AsQueryable();

            dbSet.As<IQueryable<Teammate>>().Setup(m => m.Provider).Returns(data.Provider);
            dbSet.As<IQueryable<Teammate>>().Setup(m => m.Expression).Returns(data.Expression);
            dbSet.As<IQueryable<Teammate>>().Setup(m => m.ElementType).Returns(data.ElementType);
            dbSet.As<IQueryable<Teammate>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            dbContext.Setup(c => c.Team).Returns(dbSet.Object);

            var result = service.GetTeammate(testSubject.ID);

            Assert.AreEqual(result, testSubject);            
        }

        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        [TestMethod]
        public void GetTeammate_IdIsZero()
        {
            var data = new List<Teammate>
            {
                testSubject
            }.AsQueryable();

            dbSet.As<IQueryable<Teammate>>().Setup(m => m.Provider).Returns(data.Provider);
            dbSet.As<IQueryable<Teammate>>().Setup(m => m.Expression).Returns(data.Expression);
            dbSet.As<IQueryable<Teammate>>().Setup(m => m.ElementType).Returns(data.ElementType);
            dbSet.As<IQueryable<Teammate>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            dbContext.Setup(c => c.Team).Returns(dbSet.Object);

            var result = service.GetTeammate(0);
        }

        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        [TestMethod]
        public void GetTeammate_IdIsNegative()
        {
            var data = new List<Teammate>
            {
                testSubject
            }.AsQueryable();

            dbSet.As<IQueryable<Teammate>>().Setup(m => m.Provider).Returns(data.Provider);
            dbSet.As<IQueryable<Teammate>>().Setup(m => m.Expression).Returns(data.Expression);
            dbSet.As<IQueryable<Teammate>>().Setup(m => m.ElementType).Returns(data.ElementType);
            dbSet.As<IQueryable<Teammate>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            dbContext.Setup(c => c.Team).Returns(dbSet.Object);

            var result = service.GetTeammate(-10);
        }
    }
}
