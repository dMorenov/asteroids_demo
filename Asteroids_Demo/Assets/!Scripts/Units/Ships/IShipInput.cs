using System;

namespace Units.Ships
{
    public interface IShipInput
    {
        public void GetInput();

        public void SetFireCallback(Action fireCallback);
        public float Rotation { get; }
        public float ForwardForce { get; }
    }
}