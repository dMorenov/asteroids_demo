using System.Collections;
using UnityEngine;

namespace GameCore
{
    public class GameOver : State
    {
        public GameOver(GameManager gameManager) : base(gameManager)
        {
        }

        public override IEnumerator Start()
        {
            GameManager.ShowEndScreen();
            yield break;
        }
    }
}