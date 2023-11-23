
namespace CopaGames.API.Domain;

[Flags]
public enum ERound
{
    RoundOf16 = 16,
    QuarterFinal = 8,
    SemiFinal = 4,
    Final = 2,
    Champion = 1,
    None = default
}

