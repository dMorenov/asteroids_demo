using System.Collections;
using Units.Asteroids;
using Units.Ships;
using UnityEngine;
using Utils;

namespace GameCore
{
    public class MainLoop : State
    {
        private const float TransitionDelay = 0.5f;

        private float _spawnCounter;
        private float _secondsCounter;
        private bool _playerIsAlive;

        public MainLoop(GameManager gameManager) : base(gameManager)
        {
        }

        public override IEnumerator Start()
        {
            GameManager.Spawner.OnAsteroidKilled += OnAsteroidKilled;
            GameManager.PlayerShip.OnShipKilled += OnShipKilled;

            _spawnCounter = GameManager.GameData.spawnCounter;
            _secondsCounter = GameManager.GameData.secondsElapsed;
            _playerIsAlive = true;

            while (_playerIsAlive)
            {
                Tick();
                yield return 0;
            }
        }

        private void Tick()
        {
            _spawnCounter -= Time.deltaTime;
            _secondsCounter += Time.deltaTime;

            if (_spawnCounter <= 0f)
            {
                Spawn();
            }
        }

        private void Spawn()
        {
            _spawnCounter = GameManager.MapSettings.GetSpawnDelay(_secondsCounter);

            GameManager.Spawner.RandomSpawn();
        }
        private void OnAsteroidKilled(Asteroid asteroid)
        {
            var score = GameManager.MapSettings.AsteroidsConfig.GetScoreBySizeInt((int)asteroid.Size);
            GameManager.AddScore(score);
        }

        private void OnShipKilled(Ship ship)
        {
            _playerIsAlive = false;
            GameManager.RemoveLife();
            GameManager.SaveGameTimers(_secondsCounter, _spawnCounter);

            ObjectPool.Instance.Recycle(GameManager.PlayerShip.gameObject);

            GameManager.RunCoroutine(DelayedTransition());
        }

        private IEnumerator DelayedTransition()
        {
            yield return new WaitForSeconds(TransitionDelay);

            GameManager.Spawner.RemoveAllUnits();

            yield return new WaitForSeconds(TransitionDelay);

            if (GameManager.GameData.PlayerLives > 0)
                GameManager.SetState(typeof(StartRound));
            else
                GameManager.SetState(typeof(GameOver));
        }
    }
}