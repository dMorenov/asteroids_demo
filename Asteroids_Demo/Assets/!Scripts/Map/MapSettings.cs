using UnityEngine;
using Units.Asteroids;

namespace Map
{
    [CreateAssetMenu(fileName = "Map_New", menuName = "Configs/Map/MapSettings")]
    public class MapSettings : ScriptableObject
    {
        [Header("Difficulty Settings")]
        [SerializeField] private int playerLives;
        [SerializeField] private float maxSpawnDelay;
        [SerializeField] private float minSpawnDelay;
        [SerializeField] private float timeUntilMaxDifficulty; // Time in seconds
        [SerializeField] private AnimationCurve spawnRateCurve;

        [Header("Asteroids")]
        [SerializeField] private AsteroidsConfiguration asteroidsConfig;

        [Header("Other Settings")]
        [SerializeField] private float roundStartCountdownSeconds;



        public int PlayerLives => playerLives;
        public float MaxSpawnDelay => maxSpawnDelay;
        public float MinSpawnDelay => minSpawnDelay; 
        public float TimeUntilMaxDifficulty => timeUntilMaxDifficulty;
        public float RoundStartCountdownSeconds => roundStartCountdownSeconds;

        public AsteroidsConfiguration AsteroidsConfig => asteroidsConfig;


    public float GetSpawnDelay(float currentTime)
        {
            var time = Mathf.Min(currentTime / timeUntilMaxDifficulty, 1f);
            var curveValue = Mathf.Clamp(spawnRateCurve.Evaluate(time), 0f, 1f);

            return curveValue * minSpawnDelay + (1 - curveValue) * maxSpawnDelay;
        }
    }
}