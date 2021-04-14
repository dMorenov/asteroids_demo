using UnityEngine;

namespace Units.Ships
{
    [CreateAssetMenu(fileName = "ShipWeapon_", menuName = "Configs/Ship/ShipWeaponSettings")]
    public class ShipWeaponSettings : ScriptableObject
    {
        [SerializeField] private float attackDelay;
        [SerializeField] private float bulletSpeed;
        [SerializeField] private float lifeTime;

        public Bullet Bullet;

        public float AttackDelay { get => attackDelay; }
        public float BulletSpeed { get => bulletSpeed; }
        public float LifeTime { get => lifeTime; }

    }
}