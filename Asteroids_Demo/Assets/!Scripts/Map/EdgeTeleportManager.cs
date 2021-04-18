using System.Collections.Generic;
using UnityEngine;
using Utils;
using Units;

namespace Map
{
    public class EdgeTeleportManager : Singleton<EdgeTeleportManager>
    {
        [SerializeField] private MapBounds mapBounds;

        private HashSet<Unit> _units = new HashSet<Unit>();

        private new void Awake()
        {
            base.Awake();
        }

        private void Update()
        {
            foreach(var unit in _units)
            {
                var pos = unit.GetPosition();

                if (pos.x > mapBounds.Bounds.BottomRight.x) unit.TeleportHorizontalyTo(mapBounds.Bounds.BottomLeft.x);
                else if (pos.x < mapBounds.Bounds.BottomLeft.x) unit.TeleportHorizontalyTo(mapBounds.Bounds.BottomRight.x);

                if (pos.y > mapBounds.Bounds.TopLeft.y) unit.TeleportVerticalyTo(mapBounds.Bounds.BottomLeft.y);
                else if (pos.y < mapBounds.Bounds.BottomLeft.y) unit.TeleportVerticalyTo(mapBounds.Bounds.TopLeft.y);
            }
        }

        public void Register(Unit teleportable)
        {
            _units.Add(teleportable);
        }

        public void Unregister(Unit teleportable)
        {
            _units.Remove(teleportable);
        }
    }
}