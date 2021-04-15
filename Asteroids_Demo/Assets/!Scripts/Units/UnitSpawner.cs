using UnityEngine;
using Map;
using Random = UnityEngine.Random;
using Utils;

namespace Units
{
    public class UnitSpawner
    {
        private MapBounds _mapBounds;
        private Asteroid _asteroidPrefab;

        private float spawnOffset = 0f;//0.15f;

        public UnitSpawner(MapBounds mapBounds, Asteroid prefab)
        {
            _mapBounds = mapBounds;
            _asteroidPrefab = prefab;
        }

        public void Spawn()
        {
            // choose a random position around the bounds and get the closest point to spawn
            var randX = Random.Range(_mapBounds.Bounds.BottomLeft.x, _mapBounds.Bounds.BottomRight.x);
            var randY = Random.Range(_mapBounds.Bounds.BottomLeft.y, _mapBounds.Bounds.TopLeft.y);

            var x = randX - _mapBounds.Bounds.Origin.x <= 0 ? _mapBounds.Bounds.BottomLeft.x - spawnOffset : _mapBounds.Bounds.BottomRight.x + spawnOffset;
            var y = randY - _mapBounds.Bounds.Origin.y <= 0 ? _mapBounds.Bounds.BottomLeft.y - spawnOffset : _mapBounds.Bounds.TopLeft.y + spawnOffset;

            var spawnPosition = Mathf.Abs(randX) - Mathf.Abs(randY) < 0 ? new Vector2(x, randY) : new Vector2(randX, y);

            // instantiate and setup
            var rotation = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f));
            var speed = Random.Range(3f, 10f);
            var asteroid = ObjectPool.Instance.GetItem(_asteroidPrefab, spawnPosition, rotation); //GameObject.Instantiate(_asteroidPrefab, spawnPosition, rotation);
            asteroid.Setup(speed, OnAsteroidDeath);
        }

        private void OnAsteroidDeath(Asteroid asteroid)
        {
            ObjectPool.Instance.Recycle(asteroid.gameObject);
        }
    }
}