using dayz12;

namespace NunitTests
{
    public class Dayz12
    {
        private Day12 _sut;
        [SetUp]
        public void Setup()
        {
            _sut = new Day12();
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
