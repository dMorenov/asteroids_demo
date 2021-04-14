using Units.Ships;
using UnityEngine;

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
        }

        private void Update()
        {
            transform.position += transform.up * _speed * Time.deltaTime;

            _lifeTime -= Time.deltaTime;

            if (_lifeTime <= 0f)
            {
                Destroy(this.gameObject);
            }
        }
    }
}