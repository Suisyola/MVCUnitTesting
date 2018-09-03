using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MVCUnitTesting.Controllers;
using NUnit.Framework;
using Telerik.JustMock;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using MVCUnitTesting.Models;

namespace MVCUnitTesting.Tests.Controllers
{
    [TestFixture]
    public class HomeControllerTest
    {
        [Test]
        public void FindByGenreReturnsAllInGenre()
        {
            // Arrange
            var productRepository = Mock.Create<Repository>();
            Mock.Arrange(() => productRepository.GetAll()).Returns(
                new List<Product>()
                {
                    new Product { Genre="Fiction", ID=1, Name="Moby Dick", Price=12.50m},
                    new Product{ Genre="Fiction", ID = 2, Name="War and Peace", Price=17m},
                    new Product{ Genre="Non-Fiction", ID=3, Name="Chemistry", Price=35m},
                }).MustBeCalled();

            HomeController controller = new HomeController(productRepository);
            ViewResult viewResult = controller.FindByGenre("Fiction");
            var model = viewResult.Model as IEnumerable<Product>;

            //Assert
            Assert.AreEqual(2, model.Count());
            Assert.AreEqual("Moby Dick", model.ToList()[0].Name);
            Assert.AreEqual("War and Peace", model.ToList()[1].Name);

        }

        [Test]
        public void Index_Returns_All_Products_In_DB()
        {
            // Arrange
            var productRepository = Mock.Create<Repository>();
            // Mock(Imitate) Repository.GetAll() to returns 2 Products. MustBeCalled denotes that test will fail, if the GetAll() is not called.
            Mock.Arrange( () => productRepository.GetAll()).
                Returns( new List<Product>()
                {
                    new Product { Genre="Fiction", ID=1, Name="Moby Dick", Price=12.50m},
                    new Product { Genre="Fiction", ID=2, Name="War and Peace", Price=17m},
                }).MustBeCalled();

            // Act
            HomeController controller = new HomeController(productRepository);
            ViewResult viewResult = controller.Index();
            var model = viewResult.Model as IEnumerable<Product>;

            // Assert
            Assert.AreEqual(2, model.Count());
        }

        [Test]
        public void Index()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void About()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.About() as ViewResult;

            // Assert
            Assert.AreEqual("Your application description page.", result.ViewBag.Message);
        }

        [Test]
        public void Contact()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Contact() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
