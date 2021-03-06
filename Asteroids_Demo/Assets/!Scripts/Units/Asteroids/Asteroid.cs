using Audio;
using System.Collections.Generic;
using UnityEngine;

namespace Units.Asteroids
{
    public class Asteroid : Unit, IDamageable
    {
        public enum SizeType { Small = 0, Medium = 1, Big = 2}
        public SizeType Size => _size;

        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private PolygonCollider2D polygonCollider;

        private AsteroidsSettings _settings;
        private SizeType _size;
        private float _speed;
        private Sprite _sprite;

        public delegate void AsteroidKilled(Asteroid asteroid);
        public event AsteroidKilled OnAsteroidKilled;

        public void Setup(SizeType size, AsteroidsSettings settings)
        {
            _size = size;
            _speed = settings.GetRandomSpeed();
            _sprite = settings.GetSpriteBySize(_size);
            _settings = settings;
            spriteRenderer.sprite = _sprite;
            SetColliderSettings();
        }

        private void SetColliderSettings()
        {
            polygonCollider.pathCount = 0;
            polygonCollider.pathCount = _sprite.GetPhysicsShapeCount();

            List<Vector2> path = new List<Vector2>();
            for (int i = 0; i < polygonCollider.pathCount; i++)
            {
                path.Clear();
                _sprite.GetPhysicsShape(i, path);
                polygonCollider.SetPath(i, path.ToArray());
            }
        }

        public void TakeDamage()
        {
            AudioManager.Instance.PlayClip(_settings.ExplosionSound);
            Instantiate(_settings.RocksParticles, transform.position, Quaternion.identity);

            OnAsteroidKilled?.Invoke(this);
        }

        public void Update()
        {
            transform.position += transform.up * _speed * Time.deltaTime;
        }

        public override void OnDisable()
        {
            base.OnDisable();
        }
    }
}