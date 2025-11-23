namespace TabletopMatchMaster.Console.Configurations
{
    public sealed class TeamArmies
    {
        public static readonly string SectionName = nameof(TeamArmies);

        public TeamArmies()
        {
            GoodArmies = new();
            EvilArmies = new();
        }

        //public AllyArmies(
        //    List<string> goodArmies,
        //    List<string> evilArmies)
        //{
        //    GoodArmies = goodArmies;
        //    EvilArmies = evilArmies;
        //}

        public List<string> GoodArmies { get; set; }

        public List<string> EvilArmies { get; set; }
    }
}
