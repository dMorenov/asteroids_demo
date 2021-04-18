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

        public event IShipInput.OnFire OnFirePressed;
        public delegate void OnFire();

        public void GetInput()
        {
            rotation = Input.GetAxis(Horizontal);
            forwardForce = Mathf.Clamp(Input.GetAxis(Vertical), 0f, 1f);

            if (Input.GetKey(KeyCode.Space))
                OnFirePressed?.Invoke();
        }

        public void Reset()
        {
            rotation = 0f;
            forwardForce = 0f;
        }
    }
}