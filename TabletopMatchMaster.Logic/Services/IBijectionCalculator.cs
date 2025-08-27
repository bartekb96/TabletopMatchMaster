namespace TabletopMatchMaster.Logic.Services
{
	public interface IBijectionCalculator
	{
		IEnumerable<IEnumerable<(string, string)>> GetBijections(
			IEnumerable<string> setA,
			IEnumerable<string> setB);
	}
}
