
using TabletopMatchMaster.Logic.Models;

namespace TabletopMatchMaster.Logic.Services
{
	public sealed class BijectionCalculator : IBijectionCalculator
	{
		public const string RootNodeName = "root";

		public IEnumerable<IEnumerable<(string, string)>> GetBijections(IEnumerable<string> setA, IEnumerable<string> setB)
		{
			return null!;
			
			try
			{
				if (setA.Count() != setB.Count())
				{
					return null!;
				}

			}
			catch (Exception ex)
			{

			}
		}

		public List<Node> BuildTree(IEnumerable<string> setA, IEnumerable<string> setB)
		{
			var treeDepth = setA.Count();

			var tree = new List<Node>();

			var rootNode = new Node(RootNodeName, null, null, setB);

			tree.Add(rootNode);

			void GetNextNodes(
				int currentLevel,
				Node parentNode)
			{
				var childAmount = treeDepth - currentLevel;

				for (int i = 0; i < childAmount; i++)
				{
					var childNode = new Node(
						setA.ElementAt(currentLevel),
						parentNode.ChildNames.ElementAt(i),
						parentNode,
						parentNode.ChildNames.Where(e => e != parentNode.ChildNames.ElementAt(i)));

					tree.Add(childNode);

					var nextLevel = currentLevel + 1;

					GetNextNodes(nextLevel, childNode);
				}
			}

			GetNextNodes(0, rootNode);

			return tree;
		}
	}
}
