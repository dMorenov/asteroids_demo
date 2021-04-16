using Map;

namespace GameCore
{
    public class GameData
    {
        public int PlayerLives;
        public int PlayerScore;
        public int HiScore;

        public float roundStartCountdown;
        public float elapsedTime;

        public float spawnCounter = 0f;
        public float secondsElapsed;

        public GameData(MapSettings mapSettings)
        {
            PlayerScore = 0;
            HiScore = 0;
            PlayerLives = mapSettings.PlayerLives;
            spawnCounter = mapSettings.MaxSpawnDelay;
            roundStartCountdown = mapSettings.RoundStartCountdownSeconds;
            secondsElapsed = 0f;
        }
    }
}