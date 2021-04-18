using UnityEngine;

namespace Units.Ships
{
    [CreateAssetMenu(fileName = "Ship_New", menuName = "Configs/Ship/ShipSettings")]
    public class ShipSettings : ScriptableObject
    {
        [Header("Behaviour")]
        [SerializeField] private float rotationSpeed;
        [SerializeField] private float maxVelocity;
        [SerializeField] private float acceleration;

        [Header("ShipWeaponSettings")]
        [SerializeField] private ShipWeaponSettings weaponSettings;

        [Header("Audio")]
        [SerializeField] private AudioClip respawnSound;
        [SerializeField] private AudioClip explosionSound;
        [SerializeField] private AudioClip thrustSound;

        [Header("FX")]
        [SerializeField] private ParticleSystem explosionParticles;


        public float RotationSpeed => rotationSpeed;
        public float MaxVelocity => maxVelocity;
        public float Acceleration => acceleration;
        public ShipWeaponSettings WeaponSettings => weaponSettings;
        public AudioClip RespawnSound => respawnSound;
        public AudioClip ExplosionSound => explosionSound;
        public AudioClip ThrustSound => thrustSound;
        public ParticleSystem ExplosionParticles => explosionParticles;
    }
}