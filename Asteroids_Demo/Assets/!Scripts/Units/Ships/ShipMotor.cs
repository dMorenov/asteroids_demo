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
        private ParticleSystem _particles;
        private bool _isThrustingFlag = false;

        public ShipMotor(IShipInput input, ShipSettings settings, Rigidbody2D rigidbody, Transform shipTransform, ParticleSystem thrustParticles)
        {
            _shipInput = input;
            _shipSettings = settings;
            _rigidbody = rigidbody;
            _shipTransform = shipTransform;
            _particles = thrustParticles;
        }

        public void Tick()
        {
            _shipTransform.Rotate(Vector3.back, _shipSettings.RotationSpeed * _shipInput.Rotation * Time.deltaTime);

            if (_shipInput.ForwardForce > float.Epsilon && !_isThrustingFlag)
            {
                _isThrustingFlag = true;
                _particles.Play();
                AudioManager.Instance.PlayClip(_shipSettings.ThrustSound, true);
            }
            else if (_shipInput.ForwardForce <= float.Epsilon && _isThrustingFlag)
            {
                _isThrustingFlag = false;
                _particles.Stop();
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
            _particles.Stop();
            AudioManager.Instance.StopLoop(_shipSettings.ThrustSound);
        }
    }
}