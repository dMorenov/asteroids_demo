using UnityEngine;

namespace Units.Ships
{
    public class ShipWeapon
    {
        public ShipWeaponSettings weaponSettings;
        private Transform _spawnTransform;

        private float _attackCounter = 0f;
        private bool _isOnCooldown = false;


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

            var instance = GameObject.Instantiate(weaponSettings.Bullet, _spawnTransform.position, _spawnTransform.rotation) as Bullet;
            instance.Setup(weaponSettings);
        }
    }
}