using dayz19;

namespace NunitTests
{
    public class Dayz19
    {
        private Day19 _sut;
        [SetUp]
        public void Setup()
        {
            _sut = new Day19();
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
