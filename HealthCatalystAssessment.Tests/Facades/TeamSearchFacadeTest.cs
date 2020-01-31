using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Faker;
using HealthCatalyst.Assessment.API.Models.Team;
using HealthCatalyst.Assessment.API.Facades;
using HealthCatalyst.Assessment.API.Models;
using HealthCatalyst.Assessment.Domain.Services;
using HealthCatalyst.Assessment.Domain.Models;

namespace HealthCatalyst.Assessment.API.Tests.Facades
{
    [TestClass]
    public class TeamSearchFacadeTest
    {
        Mock<RosterService> service;
        AutoMapper.IMapper mapper;
        TeammateSearchFacade facade;

        [TestInitialize]
        public void Initialize()
        {
            service = new Mock<RosterService>();
            mapper = MappingConfig.Mapper.CreateMapper();
            facade = new TeammateSearchFacade(service.Object, mapper);
        }

        [TestMethod]
        public void GetTeammate()
        {
            var subject = new Teammate
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

            var mapped = mapper.Map<Teammate, GetTeammateResponse>(subject);

            service.Setup(x => x.GetTeammate(subject.ID))
                .Returns(subject)
                .Verifiable();

            var result = facade.GetTeammate(subject.ID);

            Assert.IsTrue(result.FirstName == mapped.FirstName &&
                result.LastName == mapped.LastName &&
                result.ID == mapped.ID &&
                result.Interests == mapped.Interests &&
                result.IsStarter == mapped.IsStarter &&
                result.BirthDate.Equals(mapped.BirthDate) &&
                result.Address == mapped.Address &&
                result.Age == mapped.Age);
            service.Verify(x => x.GetTeammate(It.IsAny<long>()), Times.Once);
        }

        [TestMethod]
        public void GetTeammate_NotFound()
        {
            var subject = new Teammate
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

            var mapped = mapper.Map<Teammate, GetTeammateResponse>(subject);

            service.Setup(x => x.GetTeammate(subject.ID))
                .Returns(subject)
                .Verifiable();

            var result = facade.GetTeammate(1008);

            Assert.IsNull(result);
            service.Verify(x => x.GetTeammate(It.IsAny<long>()), Times.Once);
        }

        [TestMethod]
        public void CreateTeammate()
        {

        }

        [TestMethod]
        public void GetTeam()
        {
            var subject = new Teammate
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

            var mapped = mapper.Map<Teammate, GetTeammateResponse>(subject);

            service.Setup(x => x.GetTeam())
                .Returns(new List<Teammate>() { subject })
                .Verifiable();

            var result = facade.GetTeam().FirstOrDefault();

            Assert.IsNotNull(result);
            Assert.IsTrue(result.FirstName == mapped.FirstName &&
                result.LastName == mapped.LastName &&
                result.ID == mapped.ID &&
                result.Interests == mapped.Interests &&
                result.IsStarter == mapped.IsStarter &&
                result.BirthDate.Equals(mapped.BirthDate) &&
                result.Address == mapped.Address &&
                result.Age == mapped.Age);
            service.Verify(x => x.GetTeam(), Times.Once);
        }

        [TestMethod]
        public void SearchTeam()
        {
            var subject = new Teammate
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

            var mapped = mapper.Map<Teammate, GetTeammateResponse>(subject);

            service.Setup(x => x.SearchTeam(It.IsAny<string>()))
                .Returns(new List<Teammate>() { subject })
                .Verifiable();

            var result = facade.SearchTeam(subject.FirstName).FirstOrDefault();

            Assert.IsNotNull(result);
            Assert.IsTrue(result.FirstName == mapped.FirstName &&
                result.LastName == mapped.LastName &&
                result.ID == mapped.ID &&
                result.Interests == mapped.Interests &&
                result.IsStarter == mapped.IsStarter &&
                result.BirthDate.Equals(mapped.BirthDate) &&
                result.Address == mapped.Address &&
                result.Age == mapped.Age);
            service.Verify(x => x.SearchTeam(It.IsAny<string>()), Times.Once);
        }

        [TestMethod]
        public void SearchTeam_NoTeammateFound()
        {
            var subject = new Teammate
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

            var mapped = mapper.Map<Teammate, GetTeammateResponse>(subject);

            service.Setup(x => x.SearchTeam(It.IsAny<string>()))
                .Returns(new List<Teammate>() )
                .Verifiable();

            var result = facade.SearchTeam(subject.FirstName);

            Assert.IsNull(result);
            service.Verify(x => x.SearchTeam(It.IsAny<string>()), Times.Once);
        }
    }
}
