using dayz13;

namespace NunitTests
{
    public class Dayz13
    {
        private Day13 _sut;
        [SetUp]
        public void Setup()
        {
            _sut = new Day13();
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
