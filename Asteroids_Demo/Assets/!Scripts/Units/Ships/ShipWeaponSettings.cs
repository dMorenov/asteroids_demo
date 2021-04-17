using UnityEngine;

namespace Units.Ships
{
    [CreateAssetMenu(fileName = "ShipWeapon_", menuName = "Configs/Ship/ShipWeaponSettings")]
    public class ShipWeaponSettings : ScriptableObject
    {
        [SerializeField] private float attackDelay;
        [SerializeField] private float bulletSpeed;
        [SerializeField] private float lifeTime;
        [SerializeField] private AudioClip fireSound;

        public Bullet Bullet;

        public float AttackDelay => attackDelay;
        public float BulletSpeed => bulletSpeed; 
        public float LifeTime => lifeTime;
        public AudioClip FireSound => fireSound;

    }
}