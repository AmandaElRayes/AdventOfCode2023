using dayz14;

namespace NunitTests
{
    public class Dayz14
    {
        private Day14 _sut;
        [SetUp]
        public void Setup()
        {
            _sut = new Day14();
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
