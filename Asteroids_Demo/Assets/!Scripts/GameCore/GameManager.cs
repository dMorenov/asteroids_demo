using System.Collections;
using UnityEngine;
using Units;
using Map;
using Units.Ships;
using System.Collections.Generic;
using System;
using UnityEngine.SceneManagement;

namespace GameCore
{
    public class GameManager : StateMachine
    {
        private const string MenuScene = "MainMenu";
        public MapBounds MapBounds => mapBounds;
        public MapSettings MapSettings => mapSettings;
        public GameData GameData => _gameData;
        public Ship PlayerShip;
        public UnitSpawner Spawner;

        [SerializeField] private MapBounds mapBounds;
        [SerializeField] private MapSettings mapSettings;
        [SerializeField] private UiPlayerStats uiPlayerStats;
        [SerializeField] private UiGameOver uiGameOver;

        private GameData _gameData;

        private void Start()
        {
            var _typeToState = new Dictionary<Type, State>()
            {
                {typeof(StartRound), new StartRound(this) },
                {typeof(MainLoop), new MainLoop(this) },
                {typeof(GameOver), new GameOver(this) },
            };
            InitializeStateMachine(_typeToState);

            RestartGame();
        }
        public void RestartGame()
        {
            uiGameOver.HideGameOver();

            MapBounds.ResetOrigin();
            Spawner = new UnitSpawner(MapBounds, MapSettings.AsteroidsConfig);
            _gameData = new GameData(MapSettings);
            uiPlayerStats.SetScore(_gameData.PlayerScore);
            SetLives(_gameData.PlayerLives);

            SetState(typeof(StartRound));
        }

        #region UiPlayerStats
        public void SetLives(int lives)
        {
            uiPlayerStats.SetLives(lives);
        }

        public void RemoveLife()
        {
            GameData.PlayerLives--;
            uiPlayerStats.RemoveLife();
        }

        public void AddScore(int score)
        {
            _gameData.PlayerScore += score;

            uiPlayerStats.SetScore(_gameData.PlayerScore);

            if (_gameData.PlayerScore > _gameData.HiScore)
            {
                _gameData.HiScore = _gameData.PlayerScore;
                uiPlayerStats.SetHiScore(_gameData.HiScore);
            }
        }

        public void SaveGameTimers(float secondsElapsed, float spawnCounter)
        {
            _gameData.spawnCounter = spawnCounter;
            _gameData.secondsElapsed = secondsElapsed;
        }
        #endregion

        #region UiGameOver
        public void ExitToMenu()
        {
            SceneManager.LoadSceneAsync(MenuScene, LoadSceneMode.Single);
        }

        public void ShowEndScreen()
        {
            uiGameOver.ShowGameOver(_gameData.PlayerScore, _gameData.HiScore);
        }
        #endregion

        public void RunCoroutine(IEnumerator coroutine)
        {
            StartCoroutine(coroutine);
        }
    }
}