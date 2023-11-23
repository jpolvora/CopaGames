using CopaGames.API.Domain;
using FluentAssertions;

namespace CopaGames.IntegrationTests
{
    public class UnitTest1
    {
        /// <summary>
        /// Returns shuffled teams data for creating a cup
        /// </summary>
        /// <returns></returns>
        private static async Task<Game[]> LoadData(int teamCount)
        {
            //const string path = @"C:\Users\Jone\source\repos\CopaGames";
            string? workingDirectory = Environment.CurrentDirectory;
            string? projectDirectory = Directory.GetParent(workingDirectory)?.Parent?.Parent?.FullName;
            //var fileName = Path.Combine(projectDirectory!, "CopaGames.IntegrationTests", "api-demo.json");
            var fileName = Path.Combine(projectDirectory!, "api-demo.json");

            var gameData = new GameData();
            await gameData.LoadJson(fileName);
            Random rng = new();
            gameData.Shuffle(rng);

            return gameData.Take(teamCount).ToArray();
        }

        [Theory]
        [InlineData(4)]
        [InlineData(8)]
        [InlineData(16)]
        public async void ShouldCreateANewCup(int teamCount)
        {
            var rounds = teamCount switch
            {
                16 => ERound.RoundOf16,
                8 => ERound.QuarterFinal,
                4 => ERound.SemiFinal,
                2 => ERound.Final,
                1 => ERound.Champion,
                _ => ERound.None
            };

            var cupBuilder = new CupBuilder(rounds);

            var data = await LoadData(teamCount);

            for (int i = 0; i < teamCount; i++)
            {
                cupBuilder.AddTeam(data[i]);
            }

            var cup = cupBuilder.BuildCup();
            cup.GenerateMatches();
            cup.Run();

            Console.WriteLine(cup.Matches);

            cup.Should().NotBeNull();
            cup.TeamCount.Should().Be(teamCount);
            cup.Matches.Should().HaveCount(teamCount / 2);
        }

        [Theory]
        [InlineData(3)]
        [InlineData(7)]
        [InlineData(15)]
        public async void ShouldNotCreateANewCup(int teamCount)
        {
            var rounds = teamCount switch
            {
                16 => ERound.RoundOf16,
                8 => ERound.QuarterFinal,
                4 => ERound.SemiFinal,
                2 => ERound.Final,
                1 => ERound.Champion,
                _ => ERound.None
            };

            var cupBuilder = new CupBuilder(rounds);

            var data = await LoadData(teamCount);

            for (int i = 0; i < teamCount; i++)
            {
                cupBuilder.AddTeam(data[i]);
            }

            var action = () => cupBuilder.BuildCup();
            action.Should().Throw<InvalidOperationException>();
        }
    }

}