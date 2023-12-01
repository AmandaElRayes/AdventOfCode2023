using dayzz20;

namespace NunitTests
{
    public class Dayzz20
    {
        private Day20 _sut;
        [SetUp]
        public void Setup()
        {
            _sut = new Day20();
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
