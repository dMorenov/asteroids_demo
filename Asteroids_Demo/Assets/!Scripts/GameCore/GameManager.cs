using System.Collections;
using UnityEngine;
using Units;
using Map;
using Units.Ships;

namespace GameCore
{
    public class GameManager : StateMachine
    {
        public MapBounds MapBounds;
        public MapSettings MapSettings;
        public Ship PlayerShip;
        public UnitSpawner Spawner;
        public GameData GameData;


        private void Start()
        {
            MapBounds.ResetOrigin();
            Spawner = new UnitSpawner(MapBounds, MapSettings.AsteroidsConfig);
            GameData = new GameData(MapSettings);

            SetState(new StartRound(this));
        }

        public void RunCoroutine(IEnumerator coroutine)
        {
            StartCoroutine(coroutine);
        }
    }
}