using System;
using UnityEngine;

namespace Units.Ships
{
    public class KeyboardInput : IShipInput
    {
        private const string Horizontal = "Horizontal";
        private const string Vertical = "Vertical";

        public float Rotation => rotation;
        public float ForwardForce => forwardForce;

        [SerializeField] private float rotation;
        [SerializeField] private float forwardForce;

        private Action _onFireCallback;

        public void GetInput()
        {
            rotation = Input.GetAxis(Horizontal);
            forwardForce = Mathf.Clamp(Input.GetAxis(Vertical), 0f, 1f);

            if (Input.GetKey(KeyCode.Space))
                _onFireCallback?.Invoke();
        }

        public void SetFireCallback(Action fireCallback)
        {
            _onFireCallback = fireCallback;
        }

        public void Reset()
        {
            rotation = 0f;
            forwardForce = 0f;
        }
    }
}