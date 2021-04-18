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

        public GameData(int playerLives, float maxSpawnDelay, float roundCountdown)
        {
            PlayerScore = 0;
            HiScore = 0;
            PlayerLives = playerLives;
            spawnCounter = maxSpawnDelay;
            roundStartCountdown = roundCountdown;
            secondsElapsed = 0f;
        }
    }
}