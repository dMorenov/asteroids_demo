using System;
using Units.Ships;
using UnityEngine;
using Utils;

namespace Units
{
    public class Bullet : Unit
    {
        private float _speed;
        private float _lifeTime;

        public void Setup(ShipWeaponSettings settings)
        {
            _speed = settings.BulletSpeed;
            _lifeTime = settings.LifeTime;
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
            if (collision.TryGetComponent<IDamageable>(out var damageable))
            {
                damageable.TakeDamage();
                Recycle();
            }
        }

        public void Recycle()
        {
            ObjectPool.Instance.Recycle(this.gameObject);
        }
    }
}