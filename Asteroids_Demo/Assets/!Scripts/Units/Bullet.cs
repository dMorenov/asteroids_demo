using System;
using Units.Ships;
using UnityEngine;
using Utils;

namespace Units
{
    public class Bullet : Unit
    {
        private const string Player = "Player";

        private float _speed;
        private float _lifeTime;
        private float _maxLifeTime;
        private float _safeThresholdTime = 0.15f;

        public void Setup(ShipWeaponSettings settings)
        {
            _speed = settings.BulletSpeed;
            _maxLifeTime = settings.LifeTime;
            _lifeTime = _maxLifeTime;
            gameObject.SetActive(true);
        }

        private void Update()
        {
            transform.position += transform.up * _speed * Time.deltaTime;

            _lifeTime -= Time.deltaTime;

            if (_lifeTime <= 0f)
            {
                Recycle();
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (_maxLifeTime - _lifeTime < _safeThresholdTime && collision.tag == Player) return;

            if (collision.TryGetComponent<IDamageable>(out var damageable))
            {
                damageable.TakeDamage();
                Recycle();
            }
        }

        public void Recycle()
        {
            if (gameObject != null)
                ObjectPool.Instance.Recycle(this.gameObject);
        }
    }
}