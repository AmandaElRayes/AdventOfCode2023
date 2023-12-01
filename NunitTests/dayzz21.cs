using dayzz21;

namespace NunitTests
{
    public class Dayzz21
    {
        private Day21 _sut;
        [SetUp]
        public void Setup()
        {
            _sut = new Day21();
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
