using Map;
using UnityEngine;

namespace Units
{
    public abstract class Unit : MonoBehaviour
    {
        public virtual void OnEnable()
        {
            EdgeTeleportManager.Instance.Register(this);
        }

        public virtual void OnDisable()
        {
            EdgeTeleportManager.Instance.Unregister(this);
        }

        public virtual Vector2 GetPosition() => transform.position;

        public virtual void TeleportHorizontalyTo(float x)
        {
            transform.position = new Vector2(x, transform.position.y);
        }

        public virtual void TeleportVerticalyTo(float y)
        {
            transform.position = new Vector2(transform.position.x, y);
        }
    }
}