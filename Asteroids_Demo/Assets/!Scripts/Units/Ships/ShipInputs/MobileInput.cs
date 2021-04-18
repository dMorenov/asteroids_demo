using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Units.Ships
{
    public class MobileInput : MonoBehaviour, IShipInput
    {
        public float Rotation => _rotation;
        public float ForwardForce => _forwardForce;

        public event IShipInput.OnFire OnFirePressed;

        private float _rotation;
        private float _forwardForce;

        private bool _isPushingLeft = false;
        private bool _isPushingRight = false;
        private bool _isPushingThrust = false;
        private bool _isPushingShoot = false;

        public void GetInput()
        {
            if (_isPushingLeft)
                _rotation = -1f;
            else if (_isPushingRight)
                _rotation = 1f;
            else _rotation = 0f;

            if (_isPushingThrust)
                _forwardForce = 1f;
            else
                _forwardForce = 0f;

            if (_isPushingShoot)
                OnFirePressed?.Invoke();
        }

        public void Reset()
        {
            _rotation = 0f;
            _forwardForce = 0f;
        }

        public void LeftButton(bool isPushing) => _isPushingLeft = isPushing;

        public void RightButton(bool isPushing) => _isPushingRight = isPushing;

        public void ThrustButton(bool isPushing) =>_isPushingThrust = isPushing;

        public void ShootButton(bool isPushing) => _isPushingShoot = isPushing;
    }
}