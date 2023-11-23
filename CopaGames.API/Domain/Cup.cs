
namespace CopaGames.API.Domain;

public class Cup
{
    public Guid CupId { get; }

    public int TeamCount => this._teams.Count;

    private readonly List<Team> _teams = [];
    public List<Match> Matches { get; } = [];

    public ERound KnockoutRounds { get; }

    public Cup(List<Team> teams)
    {
        CupId = Guid.NewGuid();

        KnockoutRounds = teams.Count switch
        {
            16 => ERound.RoundOf16,
            8 => ERound.QuarterFinal,
            4 => ERound.SemiFinal,
            2 => ERound.Final,
            1 => ERound.Champion,
            _ => ERound.None,
        };

        _teams.AddRange(teams);
    }

    //constructor para o repository
    public Cup(Guid cupId, IEnumerable<Team> teams, IEnumerable<Match> matches, ERound knockoutRounds)
    {
        CupId = cupId;
        _teams.AddRange(teams);
        Matches.AddRange(matches);
        KnockoutRounds = knockoutRounds;
    }

    public void GenerateMatches()
    {
        if (KnockoutRounds < ERound.Final) throw new Exception("Not ready for generating matches");

        int left = 0;
        int right = this._teams.Count - 1;

        var orderedTeams = this._teams.OrderBy(x => x.Title).ToArray();

        var totalMatches = this.TeamCount / 2;

        this.Matches.Clear();

        while (this.Matches.Count < totalMatches)
        {
            var match = new Match
            {
                HomeTeam = orderedTeams[left],
                AwayTeam = orderedTeams[right]
            };

            this.Matches.Add(match);

            left++;
            right--;
        }

    }

    public void Run()
    {
        foreach (var match in Matches)
        {
            match.Run();
        }
    }
}

