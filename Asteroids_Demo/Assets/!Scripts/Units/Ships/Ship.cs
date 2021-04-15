using UnityEngine;

namespace Units.Ships
{
    public class Ship : Unit, IDamageable
    {
        [SerializeField] private ShipSettings shipSettings;
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private Transform bulletSpawnTransform;

        private IShipInput shipInput;
        private ShipMotor shipMotor;
        private ShipWeapon shipWeapon;

        private void Awake()
        {
            shipInput = new KeyboardInput();
            shipInput.SetFireCallback(FireBullet);

            shipMotor = new ShipMotor(shipInput, shipSettings, rb, transform);
            shipWeapon = new ShipWeapon(shipSettings.WeaponSettings, bulletSpawnTransform);
        }

        private void Update()
        {
            shipInput.GetInput();
            shipMotor.Tick();
            shipWeapon.Tick();
        }

        private void FixedUpdate()
        {
            shipMotor.TickPhysics();
        }

        private void FireBullet()
        {
            shipWeapon.Fire();
        }

        public void TakeDamage()
        {
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Enemy")
            {
                TakeDamage();
            }
        }
    }
}