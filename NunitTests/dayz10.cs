using dayz10;

namespace NunitTests
{
    public class Dayz10
    {
        private Day10 _sut;
        [SetUp]
        public void Setup()
        {
            _sut = new Day10();
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
