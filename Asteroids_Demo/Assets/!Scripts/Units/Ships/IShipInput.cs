using System;

namespace Units.Ships
{
    public interface IShipInput
    {
        public void GetInput();

        public delegate void OnFire();
        public event OnFire OnFirePressed;
        public float Rotation { get; }
        public float ForwardForce { get; }

        public void Reset();
    }
}