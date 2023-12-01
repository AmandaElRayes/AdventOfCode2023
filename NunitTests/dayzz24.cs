using dayzz24;

namespace NunitTests
{
    public class Dayzz24
    {
        private Day24 _sut;
        [SetUp]
        public void Setup()
        {
            _sut = new Day24();
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
