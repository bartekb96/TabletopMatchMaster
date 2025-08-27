namespace TabletopMatchMaster.Logic.Models
{
	public class Node
	{
		public string Name { get; set; }

		public string? BranchName { get; set; }

		public Node? ParentNode { get; set; }

		public IEnumerable<string> ChildNames { get; set; }

		public Node(
			string name,
			string? branchName,
			Node? parent,
			IEnumerable<string> childNames)
		{
			Name = name;
			BranchName = branchName;
			ParentNode = parent;
			ChildNames = childNames;
		}
	}
}
