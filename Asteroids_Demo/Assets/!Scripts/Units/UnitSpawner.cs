using UnityEngine;
using Map;
using Random = UnityEngine.Random;

namespace Units
{
    public class UnitSpawner : MonoBehaviour
    {
        public MapBounds MapBounds;
        public Asteroid AsteroidPrefab;

        private float spawnOffset = 0.15f;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F1)) Spawn();
        }

        public void Spawn()
        {
            // choose a random position around the bounds and get the closest point to spawn
            var randX = Random.Range(MapBounds.Bounds.BottomLeft.x, MapBounds.Bounds.BottomRight.x);
            var randY = Random.Range(MapBounds.Bounds.BottomLeft.y, MapBounds.Bounds.TopLeft.y);

            var x = randX - MapBounds.Bounds.Origin.x <= 0 ? MapBounds.Bounds.BottomLeft.x - spawnOffset : MapBounds.Bounds.BottomRight.x + spawnOffset;
            var y = randY - MapBounds.Bounds.Origin.y <= 0 ? MapBounds.Bounds.BottomLeft.y - spawnOffset : MapBounds.Bounds.TopLeft.y + spawnOffset;

            var spawnPosition = Mathf.Abs(randX) - Mathf.Abs(randY) < 0 ? new Vector2(x, randY) : new Vector2(randX, y);

            //choose a random direction through the map
            var randomPoint = new Vector2(
                Random.Range(MapBounds.Bounds.BottomLeft.x, MapBounds.Bounds.BottomRight.x), 
                Random.Range(MapBounds.Bounds.BottomLeft.y, MapBounds.Bounds.TopLeft.y));

            var direction = (randomPoint - spawnPosition).normalized;

            // instantiate and setup
            var asteroid = Instantiate(AsteroidPrefab, spawnPosition, Quaternion.Euler(0f, 0f, Random.Range(0f, 360f)));
            asteroid.Setup(Random.Range(3f, 10f));
        }
    }
}