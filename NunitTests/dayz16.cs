using dayz16;

namespace NunitTests
{
    public class Dayz16
    {
        private Day16 _sut;
        [SetUp]
        public void Setup()
        {
            _sut = new Day16();
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
