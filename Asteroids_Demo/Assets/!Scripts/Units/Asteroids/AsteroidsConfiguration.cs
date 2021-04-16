using UnityEngine;
using System.Collections.Generic;

namespace Units.Asteroids
{
    [CreateAssetMenu(fileName = "Asteroids_New", menuName = "Configs/Asteroids/AsteroidsConfiguration")]
    public class AsteroidsConfiguration : ScriptableObject
    {
        public Asteroid AsteroidPrefab;

        public int NumberOfChilds => numberOfChilds; 

        [SerializeField] private float minSpeed;
        [SerializeField] private float maxSpeed;
        [SerializeField] private int numberOfChilds;

        [SerializeField] private Sprite[] smallAsteroids;
        [SerializeField] private Sprite[] mediumAsteroids;
        [SerializeField] private Sprite[] bigAsteroids;

        private Dictionary<Asteroid.SizeType, Sprite[]> _sizeToSprites = new Dictionary<Asteroid.SizeType, Sprite[]>();

        private void OnValidate()
        {
            _sizeToSprites[Asteroid.SizeType.Small] = smallAsteroids;
            _sizeToSprites[Asteroid.SizeType.Medium] = mediumAsteroids;
            _sizeToSprites[Asteroid.SizeType.Big] = bigAsteroids;
        }

        private void OnDisable()
        {
            _sizeToSprites.Clear();
        }

        public Sprite GetSpriteBySize(Asteroid.SizeType size)
        {
            return _sizeToSprites[size][Random.Range(0, _sizeToSprites[size].Length)];
        }

        public float GetRandomSpeed()
        {
            return Random.Range(minSpeed, maxSpeed);
        }
    }
}