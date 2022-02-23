using ENSEKWebApp.Controllers;
using ENSEKWebApp.Data;
using Xunit;

namespace ENSEKWebApp_tests
{
    public class ReadControllerTest
    {
        private readonly ReadController _controller;
        private readonly IMeterReadingRepository _repository;

        public ReadControllerTest()
        {
            _repository = new MeterReadingRepositoryFake();
            _controller = new ReadController(_repository);
        }

        [Fact]
        public void ValidateReadingValue_InvalidReading_Test()
        {
            //Arrange
            string[] inputData = new string[] { "2344", "22/04/2019 09:24", "999999", "X" };
            string[] expectedOutput = new string[] { "2344", "22/04/2019 09:24", "999999", "isInvalid" };
            // Act
            var result = _controller.ValidateReadingValue(inputData);
            // Assert
            Assert.Equal(expectedOutput, result);
        }
        [Fact]
        public void ValidateReadingValue_ValidReading_Test()
        {
            //Arrange
            string[] inputData = new string[] { "2344", "22/04/2019 09:24", "12345", "X" };
            string[] expectedOutput = new string[] { "2344", "22/04/2019 09:24", "12345", "isValid" };
            // Act
            var result = _controller.ValidateReadingValue(inputData);
            // Assert
            Assert.Equal(expectedOutput, result);
        }
        [Fact]
        public void UploadReadingsToDB_Test()
        {
            //Arrange
            string[][] inputData = new string[][] {
                new string[] { "1234", "22/04/2019 09:24", "12345", "isValid" },
                new string[] { "2344", "22/04/2019 09:24", "23456", "isValid" } };

            string[][] expectedOutput = new string[][] {
                new string[] { "1234", "22/04/2019 09:24", "12345", "isValid" },
                new string[] { "2344", "22/04/2019 09:24", "23456", "isInvalidAccountId" } };
            // Act
            var result = _controller.UploadReadingsToDB(inputData);
            // Assert
            Assert.Equal(expectedOutput, result);
        }
        [Fact]
        public void CheckAllReading_Test()
        {
            //Arrange
            string[][] inputData = new string[][] {
                new string[] {"AccountId","MeterReadingDateTime","MeterReadValue"},
                new string[] { "1234", "22/04/2019 09:24", "12345" },
                new string[] { "1434", "22/04/2019 09:24", "999999" },
                new string[] { "1334", "22/04/2019 09:24", "8x16" },
                new string[] { "1234", "22/03/2019 09:24", "12344" },
                new string[] { "1234", "22/04/2019 09:24", "12345" },
                new string[] { "2344", "22/04/2019 09:24", "23456" } };
            string[][] expectedOutput = new string[][] {
                new string[] {"AccountId","MeterReadingDateTime","MeterReadValue", "Status"},
                new string[] { "1234", "22/04/2019 09:24", "12345", "isValid" },
                new string[] { "1434", "22/04/2019 09:24", "999999", "isInvalid" },
                new string[] { "1334", "22/04/2019 09:24", "8x16", "isInvalid" },
                new string[] { "1234", "22/03/2019 09:24", "12344", "isOlderValue" },
                new string[] { "1234", "22/04/2019 09:24", "12345", "isDuplicate" },
                new string[] { "2344", "22/04/2019 09:24", "23456", "isValid" } };
            // Act
            var result = _controller.CheckAllReading(inputData);
            // Assert
            Assert.Equal(expectedOutput, result);
        }


    }
}
