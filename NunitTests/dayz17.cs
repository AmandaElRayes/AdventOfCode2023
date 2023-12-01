using dayz17;

namespace NunitTests
{
    public class Dayz17
    {
        private Day17 _sut;
        [SetUp]
        public void Setup()
        {
            _sut = new Day17();
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
