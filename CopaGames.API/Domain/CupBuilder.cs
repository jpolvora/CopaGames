
namespace CopaGames.API.Domain;

public class CupBuilder(ERound round)
{
    private readonly List<Team> _selectedTeams = [];

    public ERound Round { get; } = round;

    public CupBuilder AddTeam(Team team)
    {
        _selectedTeams.Add(team);

        return this;
    }

    public void ClearTeams()
    {
        _selectedTeams.Clear();
    }

    public Cup BuildCup()
    {
        //must contain an even number of teams
        //if (_selectedTeams.Count % 2 != 0) throw new Exception("Number of available teams must be even");

        if (Enum.TryParse<ERound>(_selectedTeams.Count.ToString(), out var roundOption))
        {
            if (roundOption == Round)
            {
                var cup = new Cup(_selectedTeams);

                return cup;
            }
        }

        throw new InvalidOperationException("Invalid number of teams for the cup. Should match the provided ERound enumeration");
    }

    private bool IsValid()
    {
        return _selectedTeams.Count != 0;
    }
}