using System.Collections;
using UnityEngine;
using Units;
using Map;

public class GameManager : MonoBehaviour
{
    public MapBounds MapBounds;
    public Asteroid AsteroidPrefab;

    public float spawnDelay = 3f;
    public bool isPlaying = false;

    private float _spawnCounter = 0f;

    private UnitSpawner spawner;

    private IEnumerator Start()
    {
        spawner = new UnitSpawner(MapBounds, AsteroidPrefab);

        yield return new WaitForSeconds(1.5f);
        isPlaying = true;
    }

    private void Update()
    {
        if (!isPlaying) return;

        if (spawnDelay > 1f)
            spawnDelay -= 0.01f * Time.deltaTime;

        _spawnCounter -= Time.deltaTime;

        if (_spawnCounter <= 0f)
        {
            Spawn();
            return;
        }
    }

    private void Spawn()
    {
        _spawnCounter = spawnDelay;

        spawner.Spawn();
    }
}
