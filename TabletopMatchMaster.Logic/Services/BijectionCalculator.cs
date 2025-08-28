using TabletopMatchMaster.Logic.Models;

namespace TabletopMatchMaster.Logic.Services
{
	public sealed class BijectionCalculator : IBijectionCalculator
	{
		public const string RootNodeName = "root";

		public List<List<(string, string)>> GetBijections(List<string> setA, List<string> setB)
		{
			try
			{
				if (setA.Count() != setB.Count())
				{
					return null!;
				}

				var tree = BuildTree(setA, setB);

				var endNodes = tree.FindAll(n => !n.ChildNames.Any());

				List<List<(string, string)>> bijections = new();

				foreach (var node in endNodes)
				{
					Node currentNode = node;

					var bijection = new List<(string, string)>();

					while (currentNode!.Name != RootNodeName)
					{
						bijection.Add((currentNode.Name!, currentNode.BranchName!));
						currentNode = currentNode.ParentNode!;
					}

					bijections.Add(bijection);
				}

				return bijections;
			}
			catch (Exception ex)
			{
				return null!;
			}
		}

		private List<Node> BuildTree(IEnumerable<string> setA, IEnumerable<string> setB)
		{
			var treeDepth = setA.Count();

			var tree = new List<Node>();

			var rootNode = new Node(RootNodeName, null, null, setB.ToList());

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
						parentNode.ChildNames.FindAll(e => e != parentNode.ChildNames.ElementAt(i)));

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
