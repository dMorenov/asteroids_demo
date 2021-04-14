using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace Map
{
    public class EdgeTeleportManager : Singleton<EdgeTeleportManager>
    {
        [SerializeField] private MapBounds mapBounds;

        private HashSet<IEdgeTeleportable> _teleportables = new HashSet<IEdgeTeleportable>();

        private void Start()
        {
            // TODO: check mapbounds null
            _teleportables.Clear();
        }

        private void Update()
        {
            foreach(var unit in _teleportables)
            {
                var pos = unit.GetPosition();

                if (pos.x > mapBounds.Bounds.BottomRight.x) unit.TeleportHorizontalyTo(mapBounds.Bounds.BottomLeft.x);
                else if (pos.x < mapBounds.Bounds.BottomLeft.x) unit.TeleportHorizontalyTo(mapBounds.Bounds.BottomRight.x);

                if (pos.y > mapBounds.Bounds.TopLeft.y) unit.TeleportVerticalyTo(mapBounds.Bounds.BottomLeft.y);
                else if (pos.y < mapBounds.Bounds.BottomLeft.y) unit.TeleportVerticalyTo(mapBounds.Bounds.TopLeft.y);
            }
        }

        public void Register(IEdgeTeleportable teleportable)
        {
            _teleportables.Add(teleportable);
        }

        public void Unregister(IEdgeTeleportable teleportable)
        {
            _teleportables.Remove(teleportable);
        }
    }
}