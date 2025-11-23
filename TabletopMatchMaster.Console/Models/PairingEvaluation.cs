namespace TabletopMatchMaster.Console.Models
{
    public sealed class PairingEvaluation
    {
        public List<(string, string)> Pairs { get; set; }

        public int Rating { get; set; }
    }
}
