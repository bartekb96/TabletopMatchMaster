using NUnit.Framework;
using TabletopMatchMaster.Logic.Services;

namespace TabletopMatchMaster.Logic.Tests.Services
{
	[TestFixture]
	public sealed class BijectionCalculatorTests
	{
		[Test]
		public void BuildTree_WithValidSets_ShouldReturnCorrectTree()
		{
			// Arrange
			var setA = new List<string> { "A1", "A2", "A3" };
			var setB = new List<string> { "B1", "B2", "B3" };
			var bijectionCalculator = new BijectionCalculator();

			// Act
			var tree = bijectionCalculator.BuildTree(setA, setB);

			// Assert
			tree.Should()
		}
	}
}
