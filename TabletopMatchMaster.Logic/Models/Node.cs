namespace TabletopMatchMaster.Logic.Models
{
	internal class Node
	{
		public string Name { get; set; }

		public string? BranchName { get; set; }

		public Node? ParentNode { get; set; }

		public Node(
			string name,
			string? branchName,
			Node? parent)
		{
			Name = name;
			BranchName = branchName;
			ParentNode = parent;
		}
	}
}
