using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HealthCatalyst.Assessment.API.Models.Team;
using HealthCatalyst.Assessment.Domain.Models;

namespace HealthCatalyst.Assessment.API.Tests
{
    [TestClass]
    public class MappingConfigTest
    {
        AutoMapper.IMapper mapper;

        [TestInitialize]
        public void Initialize()
        {
            mapper = MappingConfig.Mapper.CreateMapper();
        }

        [TestMethod]
        public void CreateTeammateRequest_to_Teammate()
        {
            var subject = ObjectFactory.Create<CreateTeammateRequest>() as CreateTeammateRequest;
            var comparer = new ObjectEqualityComparer();

            var result = mapper.Map<Teammate>(subject);

            Assert.IsTrue(comparer.Equals(subject, result));
        }

        [TestMethod]
        public void Teammate_to_CreateTeammateResponse()
        {
            var subject = ObjectFactory.Create<Teammate>() as Teammate;
            var comparer = new ObjectEqualityComparer();

            var result = mapper.Map<CreateTeammateResponse>(subject);

            Assert.IsTrue(comparer.Equals(subject, result));
        }

        [TestMethod]
        public void Teammate_to_GetTeammateResponse()
        {
            var subject = ObjectFactory.Create<Teammate>() as Teammate;
            var comparer = new ObjectEqualityComparer();

            var result = mapper.Map<GetTeammateResponse>(subject);

            Assert.IsTrue(comparer.Equals(subject, result));
        }



        private static class ObjectFactory
        {
            public static object Create<T>() 
            {
                if (typeof(T) == typeof(Teammate))
                    return teammate;

                if (typeof(T) == typeof(CreateTeammateRequest))
                    return createTeammateRequest;


                throw new NotImplementedException();
            }

            private static readonly Teammate teammate = new Teammate
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


            private static readonly CreateTeammateRequest createTeammateRequest = new CreateTeammateRequest
            {
                FirstName = Faker.Name.First(),
                LastName = Faker.Name.Last(),
                Address = Faker.Address.StreetAddress(),
                City = Faker.Address.City(),
                BirthDate = DateTime.Parse($"{Faker.RandomNumber.Next(1, 12)}/{Faker.RandomNumber.Next(1, 28)}/{Faker.RandomNumber.Next(1960, 2000)}"),
                Interests = Faker.Lorem.Sentence(3),
                IsStarter = Convert.ToBoolean(Faker.RandomNumber.Next(0, 1)),
                State = Faker.Address.UsStateAbbr(),
                Zipcode = Faker.Address.ZipCode(),
            };
        }

        private class ObjectEqualityComparer 
        {
            public bool Equals(CreateTeammateRequest x, Teammate y)
            {
                if (x == null || y == null)
                    return false;

                string position = y.PrimaryPosition.HasValue ?
                   Enum.GetName(y.PrimaryPosition.Value.GetType(), y.PrimaryPosition.Value) : null;

                return y != null &&
                       x.FirstName == y.FirstName &&
                       x.LastName == y.LastName &&
                       x.BirthDate == y.BirthDate &&
                       x.Height == y.Height &&
                       x.PrimaryPosition == position &&
                       x.IsStarter == y.IsStarter &&
                       x.Interests == y.Interests &&
                       x.Address == y.Address &&
                       x.City == y.City &&
                       x.State == y.State &&
                       x.Zipcode == y.Zipcode;
            }

            public bool Equals(Teammate x, GetTeammateResponse y)
            {
                if (x == null || y == null)
                    return false;

                string position = x.PrimaryPosition.HasValue ?
                   Enum.GetName(x.PrimaryPosition.Value.GetType(), x.PrimaryPosition.Value) : null;

                return y != null &&
                       x.ID == y.ID &&
                       x.FirstName == y.FirstName &&
                       x.LastName == y.LastName &&
                       x.BirthDate == y.BirthDate &&
                       x.Height == y.Height &&
                       position == y.PrimaryPosition &&
                       x.IsStarter == y.IsStarter &&
                       x.Interests == y.Interests &&
                       x.Address == y.Address &&
                       x.City == y.City &&
                       x.State == y.State &&
                       x.Zipcode == y.Zipcode;
            }

            public bool Equals(Teammate x, CreateTeammateResponse y)
            {
                if (x == null || y == null)
                    return false;

                return y != null &&
                       x.ID == y.ID &&
                       x.FirstName == y.FirstName &&
                       x.LastName == y.LastName;
            }
        }
    }
}
