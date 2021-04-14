using Map;
using System;
using UnityEngine;

namespace Ship
{
    public class ShipMovement : MonoBehaviour, IEdgeTeleportable
    {
        private const string Horizontal = "Horizontal";
        private const string Vertical = "Vertical";

        [SerializeField] private Transform shipTransform;
        [SerializeField] private Rigidbody2D rb;

        [SerializeField] private float rotationSpeed;
        [SerializeField] private float maxSpeed;
        [SerializeField] private float acceleration;


        public Vector2 GetPosition() => transform.position;


        private void Start()
        {
            // TODO: register/unregister on spawner/death
            EdgeTeleportManager.Instance.Register(this);
        }

        private void Update()
        {
            shipTransform.Rotate(Vector3.back, rotationSpeed * Input.GetAxis(Horizontal) * Time.deltaTime);
        }

        private void FixedUpdate()
        {
            var forwardForce = Mathf.Clamp(Input.GetAxis(Vertical), 0f, 1f);

            if (rb.velocity.magnitude < maxSpeed)
                rb.AddForce(transform.up * acceleration * forwardForce);
        }

        public void TeleportHorizontalyTo(float x)
        {
            transform.position = new Vector2(x, transform.position.y);
        }

        public void TeleportVerticalyTo(float y)
        {
            transform.position = new Vector2(transform.position.x, y);
        }
    }
}
