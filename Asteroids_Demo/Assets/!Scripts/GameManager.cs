using System.Collections;
using UnityEngine;
using Units;
using Map;

public class GameManager : MonoBehaviour
{
    public MapBounds MapBounds;
    public MapSettings MapSettings;

    public bool isPlaying = false;

    private float _spawnCounter = 0f;
    private float _secondsCounter;

    private UnitSpawner spawner;

    private IEnumerator Start()
    {
        spawner = new UnitSpawner(MapBounds, MapSettings.AsteroidsConfig);

        yield return new WaitForSeconds(1.5f);
        isPlaying = true;
    }

    private void Update()
    {
        if (!isPlaying) return;

        _spawnCounter -= Time.deltaTime;
        _secondsCounter += Time.deltaTime;
            
        if (_spawnCounter <= 0f)
        {
            Spawn();
            return;
        }
    }

    private void Spawn()
    {
        _spawnCounter = MapSettings.GetSpawnDelay(_secondsCounter);

        spawner.RandomSpawn();
    }
}
