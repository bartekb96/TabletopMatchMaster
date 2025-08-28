namespace TabletopMatchMaster.Logic.Services
{
	public interface IBijectionCalculator
	{
		List<List<(string, string)>> GetBijections(
			List<string> setA,
			List<string> setB);
	}
}
