using UnityEngine;
using Units.Asteroids;

namespace Map
{
    [CreateAssetMenu(fileName = "Map_New", menuName = "Configs/Map/MapSettings")]
    public class MapSettings : ScriptableObject
    {
        [Header("Difficulty Settings")]
        [SerializeField] private float maxSpawnDelay;
        [SerializeField] private float minSpawnDelay;
        [SerializeField] private float timeUntilMaxDifficulty; // Time in seconds
        [SerializeField] private AnimationCurve spawnRateCurve;

        [Header("Asteroids")]
        [SerializeField] private AsteroidsConfiguration asteroidsConfig;

        public float MaxSpawnDelay { get => maxSpawnDelay; }
        public float MinSpawnDelay { get => minSpawnDelay; }
        public float TimeUntilMaxDifficulty { get => timeUntilMaxDifficulty; }

        public AsteroidsConfiguration AsteroidsConfig { get => asteroidsConfig; }


    public float GetSpawnDelay(float currentTime)
        {
            var time = Mathf.Min(currentTime / timeUntilMaxDifficulty, 1f);
            var curveValue = Mathf.Clamp(spawnRateCurve.Evaluate(time), 0f, 1f);

            return curveValue * minSpawnDelay + (1 - curveValue) * maxSpawnDelay;
        }
    }
}