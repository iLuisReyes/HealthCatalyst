using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HealthCatalyst.Assessment.API.Controllers;
using System.Web.Http.Results;
using HealthCatalyst.Assessment.API.Models.Team;
using HealthCatalyst.Assessment.API.Facades;
using HealthCatalyst.Assessment.API.Models;
using Moq;
using Faker;
using Faker.Extensions;

namespace HealthCatalyst.Assessment.API.Tests.Controllers
{
    [TestClass]
    public class TeamControllerTest
    {
        Mock<TeammateSearchFacade> facade;

        [TestInitialize]
        public void Initialize()
        {
            facade = new Mock<TeammateSearchFacade>();
        }

        [TestMethod]
        public void GetTeam()
        {
            facade.Setup(x => x.GetTeam())
                .Returns(new List<GetTeammateResponse> { new GetTeammateResponse { FirstName = Faker.Name.First(), LastName = Faker.Name.Last() } })
                .Verifiable();

            // Arrange
            TeamController controller = new TeamController(facade.Object);

            // Act
            var result = controller.GetTeam();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(OkNegotiatedContentResult<IEnumerable<GetTeammateResponse>>));

            var response = result as OkNegotiatedContentResult<IEnumerable<GetTeammateResponse>>;
            Assert.AreEqual(1, response.Content.Count());
            facade.Verify(x => x.GetTeam(), Times.Once);
            facade.VerifyNoOtherCalls();
        }

        [TestMethod]
        public void GetTeammate()
        {
            var subject = new GetTeammateResponse { FirstName = Faker.Name.First(), LastName = Faker.Name.Last(), ID = 5000, PrimaryPosition = "SF", IsStarter = false };
            facade.Setup(x => x.GetTeammate(It.IsAny<long>()))
                .Returns(subject)
                .Verifiable();

            // Arrange
            TeamController controller = new TeamController(facade.Object);

            // Act
            var result = controller.GetTeammate(5000);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(OkNegotiatedContentResult<GetTeammateResponse>));

            var response = result as OkNegotiatedContentResult<GetTeammateResponse>;
            Assert.AreEqual(subject, response.Content);
            facade.Verify(x => x.GetTeammate(It.IsAny<long>()), Times.Once);
            facade.VerifyNoOtherCalls();
        }

        [TestMethod]
        public void SearchTeam()
        {            
            facade.Setup(x => x.SearchTeam(It.IsAny<string>()))
                .Returns(new List<GetTeammateResponse> { new GetTeammateResponse { FirstName = Faker.Name.First(), LastName = Faker.Name.Last() } })
                .Verifiable();

            // Arrange
            TeamController controller = new TeamController(facade.Object);

            // Act
            var result = controller.SearchTeam("John");

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(OkNegotiatedContentResult<IEnumerable<GetTeammateResponse>>));

            var response = result as OkNegotiatedContentResult<IEnumerable<GetTeammateResponse>>;
            Assert.AreEqual(1, response.Content.Count());
            facade.Verify(x => x.SearchTeam(It.IsAny<string>()), Times.Once);
            facade.VerifyNoOtherCalls();
        }

        [TestMethod]
        public void SaveTeammate()
        {
            var body = new CreateTeammateRequest { FirstName = Faker.Name.First(), LastName = Faker.Name.Last(), PrimaryPosition = "SF", IsStarter = false };
            var subject = new CreateTeammateResponse { FirstName = Faker.Name.First(), LastName = Faker.Name.Last(), ID = 5000 };

            facade.Setup(x => x.CreateTeammate(It.IsNotNull<CreateTeammateRequest>()))
                .Returns(subject)
                .Verifiable();

            // Arrange
            TeamController controller = new TeamController(facade.Object);

            // Act
            var result = controller.CreateTeammate(body);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(CreatedContentActionResult<CreateTeammateResponse>));

            var response = result as CreatedContentActionResult<CreateTeammateResponse>;
            Assert.AreEqual(subject, response._response);
            facade.Verify(x => x.CreateTeammate(It.IsNotNull<CreateTeammateRequest>()), Times.Once);
            facade.VerifyNoOtherCalls();
        }
    }
}
