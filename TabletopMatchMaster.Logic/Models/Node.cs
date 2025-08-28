namespace TabletopMatchMaster.Logic.Models
{
	public class Node
	{
		public string Name { get; set; }

		public string? BranchName { get; set; }

		public Node? ParentNode { get; set; }

		public List<string> ChildNames { get; set; }

		public Node(
			string name,
			string? branchName,
			Node? parent,
			List<string> childNames)
		{
			Name = name;
			BranchName = branchName;
			ParentNode = parent;
			ChildNames = childNames;
		}
	}
}
