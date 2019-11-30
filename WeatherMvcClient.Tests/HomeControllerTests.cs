using System.Threading.Tasks;
using WeatherMvcClient.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WeatherMvcClient.Tests
{
    [TestClass]
    public class HomeControllerTests
    {
        [TestMethod]
        public async Task Index_Null_TestAsync()
        {
            // Arrange
            HomeController controller = new HomeController(null, null);

            // Act
            var result = await controller.IndexAsync();

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
