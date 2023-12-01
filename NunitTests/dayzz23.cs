using dayzz23;

namespace NunitTests
{
    public class Dayzz23
    {
        private Day23 _sut;
        [SetUp]
        public void Setup()
        {
            _sut = new Day23();
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
