using dayz11;

namespace NunitTests
{
    public class Dayz11
    {
        private Day11 _sut;
        [SetUp]
        public void Setup()
        {
            _sut = new Day11();
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
