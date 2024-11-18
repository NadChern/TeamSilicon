using System.IO;
using NUnit.Framework;

namespace UnitTests
{
    /// <summary>
    /// Setup class for unit tests, runs once before and after any tests
    /// in test suite
    /// </summary>
    [SetUpFixture]
    public class TestFixture
    {
        /// <summary>
        /// Path to Web Root directory used by the application
        /// </summary>
        public static string DataWebRootPath = "./wwwroot";

        /// <summary>
        ///  Path to data folder for the content used in application
        /// </summary>
        public static string DataContentRootPath = "./data/";

        /// <summary>
        /// Runs once before any tests in the test suite are executed.
        /// Sets up the testing environment, including copying necessary data files
        /// </summary>
        [OneTimeSetUp]
        public void RunBeforeAnyTests()
        {
            // Define the source path where the original data files are located
            var DataWebPath = "../../../../src/bin/Debug/net7.0/wwwroot/data";
            
            // Define the destination root directory for the unit tests
            var DataUTDirectory = "wwwroot";
            
            // Define the destination path for the copied data files
            var DataUTPath = DataUTDirectory + "/data";

            // Ensure the destination folder does not exist to avoid stale files
            // If it exists, delete the folder and its contents
            if (Directory.Exists(DataUTDirectory))
            {
                Directory.Delete(DataUTDirectory, true);
            }
            
            // Create the destination directory structure
            Directory.CreateDirectory(DataUTPath);

            // Copy over all data files
            var filePaths = Directory.GetFiles(DataWebPath);
            
            foreach (var filename in filePaths)
            {
                // Get original file's full path
                string OriginalFilePathName = filename.ToString();
                
                // Create new file path in the destination directory
                var newFilePathName = OriginalFilePathName.Replace(DataWebPath, DataUTPath);

                // Copy file to new destination
                File.Copy(OriginalFilePathName, newFilePathName);
            }
        }

        /// <summary>
        /// Runs once after all tests in the test suite are executed.
        /// Can be used for cleanup, though currently no actions are performed.
        /// </summary>
        [OneTimeTearDown]
        public void RunAfterAnyTests()
        {
        }
    }
}