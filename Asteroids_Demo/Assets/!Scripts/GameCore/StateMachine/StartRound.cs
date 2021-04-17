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
            yield return new WaitForSeconds(GameManager.GameData.roundStartCountdown);

            GameManager.PlayerShip = ObjectPool.Instance.GetItem(GameManager.PlayerShip, Vector3.zero, Quaternion.identity);
            GameManager.PlayerShip.ControlEnabled = false;
            GameManager.PlayerShip.Respawn();

            GameManager.PlayerShip.ControlEnabled = true;

            GameManager.SetState(typeof(MainLoop));
        }
    }
}