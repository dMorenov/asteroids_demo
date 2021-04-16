using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace Units.Ships
{
    public class ShipWeapon
    {
        public ShipWeaponSettings weaponSettings;
        private Transform _spawnTransform;

        private float _attackCounter = 0f;
        private bool _isOnCooldown = false;

        private List<Bullet> _instantiatedBullets = new List<Bullet>();

        public ShipWeapon(ShipWeaponSettings settings, Transform spawnTransform)
        {
            weaponSettings = settings;
            _spawnTransform = spawnTransform;
        }

        public void Tick()
        {
            if (_attackCounter > 0f)
            {
                _attackCounter -= Time.deltaTime;
                return;
            }
                
            _isOnCooldown = false;
        }

        public void Fire()
        {
            if (_isOnCooldown) return;

            _attackCounter = weaponSettings.AttackDelay;
            _isOnCooldown = true;

            var instance = ObjectPool.Instance.GetItem(weaponSettings.Bullet, _spawnTransform.position, _spawnTransform.rotation); // GameObject.Instantiate(weaponSettings.Bullet, _spawnTransform.position, _spawnTransform.rotation) as Bullet;
            instance.Setup(weaponSettings);

            _instantiatedBullets.Add(instance);
        }

        public void RecycleAllBullets()
        {
            foreach (var bullet in _instantiatedBullets)
                bullet.Recycle();

            _instantiatedBullets.Clear();
        }
    }
}