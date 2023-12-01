using dayzz25;

namespace NunitTests
{
    public class Dayzz25
    {
        private Day25 _sut;
        [SetUp]
        public void Setup()
        {
            _sut = new Day25();
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
