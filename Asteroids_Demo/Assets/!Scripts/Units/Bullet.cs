using System;
using Units.Ships;
using UnityEngine;

namespace Units
{
    public class Bullet : Unit
    {
        private float _speed;
        private float _lifeTime;

        private Action<Bullet> _onDeathCallback;

        public void Setup(ShipWeaponSettings settings, Action<Bullet> onDeathCallback)
        {
            _speed = settings.BulletSpeed;
            _lifeTime = settings.LifeTime;
            _onDeathCallback = onDeathCallback;
            gameObject.SetActive(true);
        }

        private void Update()
        {
            transform.position += transform.up * _speed * Time.deltaTime;

            _lifeTime -= Time.deltaTime;

            if (_lifeTime <= 0f)
            {
                OnDeath();
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent<IDamageable>(out var damageable))
            {
                damageable.TakeDamage();
                OnDeath();
            }
        }

        private void OnDeath()
        {
            _onDeathCallback?.Invoke(this);
            _onDeathCallback = null;
        }
    }
}