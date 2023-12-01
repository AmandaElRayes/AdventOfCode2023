using dayz15;

namespace NunitTests
{
    public class Dayz15
    {
        private 
            Day15 _sut;
        [SetUp]
        public void Setup()
        {
            _sut = new Day15();
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
