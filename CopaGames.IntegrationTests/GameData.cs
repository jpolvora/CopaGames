using CopaGames.API.Domain;
using System.Text.Json;

namespace CopaGames.IntegrationTests
{
    public class GameData : List<Game>
    {
        public GameData()
        {

        }

        public async Task<GameData> LoadJson(string fileName)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            try
            {
                using FileStream openStream = File.OpenRead(fileName);
                var games = await JsonSerializer.DeserializeAsync<Game[]>(openStream, options);
                if (games is not null) this.AddRange(games);
            }
            catch (Exception)
            {

                throw;
            }

            return this;
        }
    }
}