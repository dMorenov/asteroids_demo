using UnityEngine;

namespace Units.Ships
{
    [CreateAssetMenu(fileName = "Ship_New", menuName = "Configs/Ship/ShipSettings")]
    public class ShipSettings : ScriptableObject
    {
        [SerializeField] private float rotationSpeed;
        [SerializeField] private float maxVelocity;
        [SerializeField] private float acceleration;

        [SerializeField] private ShipWeaponSettings weaponSettings;

        public float RotationSpeed => rotationSpeed;
        public float MaxVelocity => maxVelocity;
        public float Acceleration => acceleration;
        public ShipWeaponSettings WeaponSettings => weaponSettings;
    }
}