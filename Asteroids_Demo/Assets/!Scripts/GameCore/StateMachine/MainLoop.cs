using System;
using System.Collections;
using Units.Ships;
using UnityEngine;
using Utils;

namespace GameCore
{
    public class MainLoop : State
    {
        private const float TransitionDelay = 1f;

        private float _spawnCounter;
        private float _secondsCounter;
        private bool _playerIsAlive;

        public MainLoop(GameManager gameManager) : base(gameManager)
        {
            Messenger<int>.AddListener(Messages.AsteroidKilled, OnAsteroidKilled);
        }

        ~MainLoop()
        {
            Messenger<int>.RemoveListener(Messages.AsteroidKilled, OnAsteroidKilled);
        }

        public override IEnumerator Start()
        {
            GameManager.PlayerShip.Setup(OnShipDead);
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
                return;
            }
        }

        private void Spawn()
        {
            _spawnCounter = GameManager.MapSettings.GetSpawnDelay(_secondsCounter);

            GameManager.Spawner.RandomSpawn();
        }
        private void OnAsteroidKilled(int size)
        {
            var score = GameManager.MapSettings.AsteroidsConfig.GetScoreBySizeInt(size);
            GameManager.GameData.PlayerScore += score;

            Messenger<int>.Broadcast(Messages.SetScore, GameManager.GameData.PlayerScore);

            if (GameManager.GameData.PlayerScore > GameManager.GameData.HiScore)
                Messenger<int>.Broadcast(Messages.SetHiScore, GameManager.GameData.PlayerScore);
        }

        private void OnShipDead(Ship ship)
        {
            _playerIsAlive = false;
            GameManager.GameData.PlayerLives--;

            GameManager.GameData.spawnCounter = _spawnCounter;
            GameManager.GameData.secondsElapsed = _secondsCounter;

            ObjectPool.Instance.Recycle(GameManager.PlayerShip.gameObject);
            Messenger.Broadcast(Messages.RemoveLife);


            GameManager.RunCoroutine(DelayedTransition());
        }

        private IEnumerator DelayedTransition()
        {
            yield return new WaitForSeconds(TransitionDelay);

            GameManager.Spawner.RemoveAllUnits();

            if (GameManager.GameData.PlayerLives > 0)
                GameManager.SetState(new StartRound(GameManager));
            else
                GameManager.SetState(new GameOver(GameManager));
        }
    }
}