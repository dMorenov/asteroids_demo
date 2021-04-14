using System;
using UnityEngine;

namespace Units.Ships
{
    public class KeyboardInput : IShipInput
    {
        private const string Horizontal = "Horizontal";
        private const string Vertical = "Vertical";

        public float Rotation { get; private set; }

        public float ForwardForce { get; private set; }

        private Action _onFireCallback;

        public void GetInput()
        {
            Rotation = Input.GetAxis(Horizontal);
            ForwardForce = Mathf.Clamp(Input.GetAxis(Vertical), 0f, 1f);

            if (Input.GetKey(KeyCode.Space))
                _onFireCallback?.Invoke();
        }

        public void SetFireCallback(Action fireCallback)
        {
            _onFireCallback = fireCallback;
        }
    }
}