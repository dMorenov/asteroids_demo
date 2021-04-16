using System.Collections;
using UnityEngine;
using Utils;

namespace GameCore
{
    public class StartRound : State
    {
        public StartRound(GameManager gameManager) : base(gameManager)
        {
        }

        public override IEnumerator Start()
        {
            GameManager.PlayerShip = ObjectPool.Instance.GetItem(GameManager.PlayerShip, Vector3.zero, Quaternion.identity);
            GameManager.PlayerShip.ControlEnabled = false;

            yield return new WaitForSeconds(GameManager.GameData.roundStartCountdown);

            GameManager.PlayerShip.ControlEnabled = true;

            GameManager.SetState(new MainLoop(GameManager));
        }
    }
}