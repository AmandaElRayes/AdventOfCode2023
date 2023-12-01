using day2;

namespace NunitTests
{
    public class Tests
    {
        private Day2 _sut;
        [SetUp]
        public void Setup()
        {
            _sut = new Day2();
        }

        [Test]
        public void Test1()
        {
            // Arrange

            // Act
            _sut.Run();
            // Assert
        }
    }
}