using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
using NUnit.Framework;

namespace UnitTests
{
    /// <summary>
    /// Test class for testing the Startup configuration and setup
    /// </summary>
    public class StartupTests
    {
        #region TestSetup

        /// <summary>
        /// Initializes test setup before each test run
        /// </summary>
        [SetUp]
        public void TestInitialize()
        {
            // Initialization logic (if required) would go here
        }

        /// <summary>
        /// Custom Startup class for testing, inherits from the application's Startup class
        /// </summary>
        public class Startup : ContosoCrafts.WebSite.Startup
        {
            /// <summary>
            /// Constructor passes configuration to the base Startup class
            /// </summary>
            /// <param name="config">Application configuration</param>
            public Startup(IConfiguration config) : base(config) { }
        }

        #endregion TestSetup

        #region ConfigureServices

        /// <summary>
        /// Tests the default configuration of services in the Startup class
        /// </summary>
        [Test]
        public void Startup_ConfigureServices_Valid_Default_Should_Pass()
        {
            // Create a default web host with the custom Startup configuration
            var webHost = Microsoft.AspNetCore.WebHost.CreateDefaultBuilder()
                            .UseStartup<Startup>()
                            .Build();

            // Asserts that the web host instance is successfully created
            Assert.That(webHost, Is.Not.Null);
        }

        #endregion ConfigureServices

        #region Configure

        /// <summary>
        /// Tests the default configuration setup of the Startup class
        /// </summary>
        [Test]
        public void Startup_Configure_Valid_Default_Should_Pass()
        {
            // Create and build the web host to test the Configure method
            var webHost = Microsoft.AspNetCore.WebHost.CreateDefaultBuilder()
                            .UseStartup<Startup>()
                            .Build();

            // Asserts that the web host instance is successfully created
            Assert.That(webHost, Is.Not.Null);
        }

        #endregion Configure
    }
}
