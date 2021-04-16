using System.Collections;

namespace GameCore
{
    public abstract class State
    {
        protected GameManager GameManager;

        public State(GameManager gameManager)
        {
            GameManager = gameManager;
        }
        public virtual IEnumerator Start()
        {
            yield break;
        }

        public virtual IEnumerator Pause()
        {
            yield break;
        }
    }
}