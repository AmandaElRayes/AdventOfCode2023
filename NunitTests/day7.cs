using day7;

namespace NunitTests
{
    public class Day7
    {
        private day7.Day7 _sut;
        [SetUp]
        public void Setup()
        {
            _sut = new day7.Day7();
        }

        [Test]
        public void RankTests()
        {
            // Arrange
            var rank = new day7.RankComparer();
            var listOfHands = new List<string>()
            {
                "KJ123",
                "QA333",
                "KK332",
                //"KK677",
                //"KTJJT"


            };
            // Act
            var x = _sut.Rank(rank, listOfHands);

            // Assert
            x.Should().NotBeNull();

        }
    }
}
