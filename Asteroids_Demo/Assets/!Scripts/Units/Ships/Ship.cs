using Audio;
using UnityEngine;
using Utils;

namespace Units.Ships
{
    public class Ship : Unit, IDamageable
    {
        private const string Enemy = "Enemy";

        public bool ControlEnabled = true;
        public bool GodModeEnabled = false;

        [SerializeField] private ShipSettings shipSettings;
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private Transform bulletSpawnTransform;
        [SerializeField] private ParticleSystem thrustParticles;


        private IShipInput shipInput;
        private ShipMotor shipMotor;
        private ShipWeapon shipWeapon;

        public delegate void ShipKilled(Ship ship);
        public event ShipKilled OnShipKilled;

        public void Setup(IShipInput input)
        {
            shipInput = input;
            shipInput.OnFirePressed += FireBullet;
            shipMotor = new ShipMotor(shipInput, shipSettings, rb, transform, thrustParticles);
            shipWeapon = new ShipWeapon(shipSettings.WeaponSettings, bulletSpawnTransform);
        }

        public void Respawn()
        {
            AudioManager.Instance.PlayClip(shipSettings.RespawnSound); 
        }

        private void Update()
        {
            if (!ControlEnabled) return;

            shipInput.GetInput();
            shipMotor.Tick();
            shipWeapon.Tick();
        }

        private void FixedUpdate()
        {
            if (!ControlEnabled) return;

            shipMotor.TickPhysics();
        }

        private void FireBullet()
        {
            shipWeapon.Fire();
        }

        public void TakeDamage()
        {
            if (GodModeEnabled) return;

            Instantiate(shipSettings.ExplosionParticles, transform.position, Quaternion.identity);
            AudioManager.Instance.PlayClip(shipSettings.ExplosionSound);
            shipMotor.StopMotor();

            OnShipKilled?.Invoke(this);
        }

        public override void OnDisable()
        {
            base.OnDisable();
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0f;
            shipInput.Reset();
            shipWeapon.RecycleAllBullets();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == Enemy)
            {
                TakeDamage();
            }
        }
    }
}