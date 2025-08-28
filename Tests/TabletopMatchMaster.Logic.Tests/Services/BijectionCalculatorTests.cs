using FluentAssertions;
using NUnit.Framework;
using TabletopMatchMaster.Logic.Services;

namespace TabletopMatchMaster.Logic.Tests.Services
{
	[TestFixture]
	public sealed class BijectionCalculatorTests
	{
		[Test]
		public void GetBijections_WithValidSets_ReturnsBijections()
		{
			// Arrange
			var setD = new List<string> { "D1", "D2", "D3" };
			var setZ = new List<string> { "Z1", "Z2", "Z3" };
			var bijectionCalculator = new BijectionCalculator();

			// Act
			var bijections = bijectionCalculator.GetBijections(setD, setZ);

			// Assert
			bijections.Should().NotBeNull();
			bijections.Should().HaveCount(6);
			
			for (int i = 0; i < bijections.Count(); i++)
			{
				for (int j = i + 1; j < bijections.Count(); j++)
				{
					AreListsEqual(bijections[i], bijections[j]).Should().BeFalse();
				}
			}
		}

		/// <summary>
		/// returns true when lists contains all the same elements
		/// i.e.
		/// A, B, C
		/// A, B, D
		/// Are not equal
		/// </summary>
		private bool AreListsEqual(List<(string, string)> listA, List<(string, string)> listB)
		{
			if (listA.Count != listB.Count)
			{
				return false;
			}

			return listA.All(a => listB.Any(b => b.Item1 == a.Item1 && b.Item2 == a.Item2));
		}
	}
}
