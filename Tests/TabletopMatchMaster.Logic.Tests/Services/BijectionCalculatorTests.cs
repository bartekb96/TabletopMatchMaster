using FluentAssertions;
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
			var setD = new List<string> { "D1", "D2", "D3" };
			var setZ = new List<string> { "Z1", "Z2", "Z3" };
			var bijectionCalculator = new BijectionCalculator();

			// Act
			var tree = bijectionCalculator.BuildTree(setD, setZ);

			// Assert
			tree.Should().NotBeNull();
			tree.Should().HaveCount(16);
			tree.Where(n => n.Name == "D1").Should().HaveCount(3);
			tree.Where(n => n.Name == "D2").Should().HaveCount(6);
			tree.Where(n => n.Name == "D3").Should().HaveCount(6);
		}
	}
}
