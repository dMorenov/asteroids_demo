using Audio;
using UnityEngine;

namespace Units.Ships
{
    public class ShipMotor
    {
        private IShipInput _shipInput;
        private Rigidbody2D _rigidbody;
        private Transform _shipTransform;
        private ShipSettings _shipSettings;
        private bool _isThrustingFlag = false;

        public ShipMotor(IShipInput input, ShipSettings settings, Rigidbody2D rigidbody, Transform shipTransform)
        {
            _shipInput = input;
            _shipSettings = settings;
            _rigidbody = rigidbody;
            _shipTransform = shipTransform;
        }

        public void Tick()
        {
            _shipTransform.Rotate(Vector3.back, _shipSettings.RotationSpeed * _shipInput.Rotation * Time.deltaTime);

            if (_shipInput.ForwardForce > float.Epsilon && !_isThrustingFlag)
            {
                AudioManager.Instance.PlayClip(_shipSettings.ThrustSound, true);
                _isThrustingFlag = true;
            }
            else if (_shipInput.ForwardForce <= float.Epsilon && _isThrustingFlag)
            {
                _isThrustingFlag = false;
                AudioManager.Instance.StopLoop(_shipSettings.ThrustSound);
            }

        }

        public void TickPhysics()
        {
            if (_rigidbody.velocity.magnitude < _shipSettings.MaxVelocity)
                _rigidbody.AddForce(_shipTransform.up * _shipSettings.Acceleration * _shipInput.ForwardForce);
        }

        public void StopMotor()
        {
            _isThrustingFlag = false;
            AudioManager.Instance.StopLoop(_shipSettings.ThrustSound);
        }
    }
}