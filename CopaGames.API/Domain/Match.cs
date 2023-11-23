
namespace CopaGames.API.Domain;

public class Match
{
    public Guid MatchId { get; set; }
    public DateTime? DateTime { get; set; }
    public Team HomeTeam { get; set; }
    public int HomeTeamScore { get; set; }
    public Team AwayTeam { get; set; }
    public int AwayTeamScore { get; set; }
    public ERound MatchRound { get; set; }

    private Team _winner;

    public Team GetWinner()
    {
        if (_winner == null) throw new Exception("match is still running, wait, try again later");
        return _winner;
    }

    public void SetWinner(Team game)
    {
        if (game.Id == HomeTeam.Id || game.Id == AwayTeam.Id)
            this._winner = game;
        else throw new Exception("team not allowed to win this match");
    }

    public void Run() { }
}

