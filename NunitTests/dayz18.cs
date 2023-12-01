using dayz18;

namespace NunitTests
{
    public class Dayz18
    {
        private Day18 _sut;
        [SetUp]
        public void Setup()
        {
            _sut = new Day18();
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
