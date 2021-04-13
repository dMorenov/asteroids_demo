using System;
using UnityEngine;

namespace Ship
{
    public class ShipMovement : MonoBehaviour
    {
        private const string Horizontal = "Horizontal";
        private const string Vertical = "Vertical";

        [SerializeField] private Transform shipTransform;
        [SerializeField] private Rigidbody2D rb;

        [SerializeField] private float rotationSpeed;
        [SerializeField] private float maxSpeed;
        [SerializeField] private float acceleration;

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
    }
}
