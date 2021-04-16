using UnityEngine;
using System.Collections.Generic;
using Map;
using Utils;
using Units.Asteroids;
using Random = UnityEngine.Random;

namespace Units
{
    public class UnitSpawner
    {
        private MapBounds _mapBounds;
        private Asteroid _asteroidPrefab;
        private AsteroidsConfiguration _asteroidsConfig;

        private float spawnOffset = 0f;

        private List<Unit> _instantiatedUnits = new List<Unit>();

        public UnitSpawner(MapBounds mapBounds, AsteroidsConfiguration config)
        {
            _mapBounds = mapBounds;
            _asteroidsConfig = config;
            _asteroidPrefab = _asteroidsConfig.AsteroidPrefab;
        }

        public void RandomSpawn()
        {
            // choose a random position around the bounds and get the closest point to spawn
            var randX = Random.Range(_mapBounds.Bounds.BottomLeft.x, _mapBounds.Bounds.BottomRight.x);
            var randY = Random.Range(_mapBounds.Bounds.BottomLeft.y, _mapBounds.Bounds.TopLeft.y);

            var x = randX - _mapBounds.Bounds.Origin.x <= 0 ? _mapBounds.Bounds.BottomLeft.x - spawnOffset : _mapBounds.Bounds.BottomRight.x + spawnOffset;
            var y = randY - _mapBounds.Bounds.Origin.y <= 0 ? _mapBounds.Bounds.BottomLeft.y - spawnOffset : _mapBounds.Bounds.TopLeft.y + spawnOffset;

            var spawnPosition = Mathf.Abs(randX) - Mathf.Abs(randY) < 0 ? new Vector2(x, randY) : new Vector2(randX, y);

            // instantiate and setup
            var size = (Asteroid.SizeType) Random.Range(0, 3);
            var rotation = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f));
            var speed = _asteroidsConfig.GetRandomSpeed();
            var sprite = _asteroidsConfig.GetSpriteBySize(size);
            var asteroid = ObjectPool.Instance.GetItem(_asteroidPrefab, spawnPosition, rotation); 
            asteroid.Setup(size, speed, sprite, OnAsteroidDeath);

            _instantiatedUnits.Add(asteroid);
        }

        public void ChildSpawn(Asteroid asteroid)
        {
            var newSize = asteroid.Size - 1;

            // Reuse current asteroid
            var rotation = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f));
            var speed = _asteroidsConfig.GetRandomSpeed();
            var sprite = _asteroidsConfig.GetSpriteBySize(newSize);
            asteroid.Setup(newSize, speed, sprite, OnAsteroidDeath);

            for (var i = 1; i < _asteroidsConfig.NumberOfChilds; i++)
            {
                rotation = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f));
                speed = _asteroidsConfig.GetRandomSpeed();
                sprite = _asteroidsConfig.GetSpriteBySize(newSize);
                var newAsteroid = ObjectPool.Instance.GetItem(_asteroidPrefab, asteroid.transform.position, rotation);
                newAsteroid.Setup(newSize, speed, sprite, OnAsteroidDeath);
                _instantiatedUnits.Add(newAsteroid);
            }
        }

        public void RemoveAllUnits()
        {
            foreach (var unit in _instantiatedUnits)
                ObjectPool.Instance.Recycle(unit.gameObject);

            _instantiatedUnits.Clear();
        }

        private void OnAsteroidDeath(Asteroid asteroid)
        {
            if (asteroid.Size == Asteroid.SizeType.Small)
            {
                ObjectPool.Instance.Recycle(asteroid.gameObject);
                _instantiatedUnits.Remove(asteroid);
                return;
            }

            ChildSpawn(asteroid);
        }
    }
}