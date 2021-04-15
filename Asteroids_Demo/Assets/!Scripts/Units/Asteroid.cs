using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Units
{
    public class Asteroid : Unit, IDamageable
    {
        public float Speed;

        private Action<Asteroid> _onDeathCallback;

        public void Setup(float speed, Action<Asteroid> onDeathCallback )
        {
            Speed = speed;
            _onDeathCallback = onDeathCallback;
            gameObject.SetActive(true);
        }

        public void TakeDamage()
        {
            _onDeathCallback?.Invoke(this);
            _onDeathCallback = null;
        }

        public void Update()
        {
            transform.position += transform.up * Speed * Time.deltaTime;
        }
    }
}