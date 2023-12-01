using dayzz22;

namespace NunitTests
{
    public class Dayzz22
    {
        private Day22 _sut;
        [SetUp]
        public void Setup()
        {
            _sut = new Day22();
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
