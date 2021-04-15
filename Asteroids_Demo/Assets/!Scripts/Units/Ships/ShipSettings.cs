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

        public float RotationSpeed { get => rotationSpeed; }
        public float MaxVelocity { get => maxVelocity; }
        public float Acceleration { get => acceleration; }
        public ShipWeaponSettings WeaponSettings => weaponSettings;
    }
}