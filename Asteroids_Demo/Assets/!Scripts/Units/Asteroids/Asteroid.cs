using System;
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

        private SizeType _size;
        private float _speed;
        private Sprite _sprite;

        private Action<Asteroid> _onDeathCallback;

        public void Setup(SizeType size, float speed, Sprite sprite, Action<Asteroid> onDeathCallback )
        {
            _size = size;
            _speed = speed;
            _sprite = sprite;
            spriteRenderer.sprite = _sprite;
            _onDeathCallback = onDeathCallback;
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
            _onDeathCallback?.Invoke(this);
        }

        public void Update()
        {
            transform.position += transform.up * _speed * Time.deltaTime;
        }

        public override void OnDisable()
        {
            base.OnDisable();
            _onDeathCallback = null;
        }
    }
}