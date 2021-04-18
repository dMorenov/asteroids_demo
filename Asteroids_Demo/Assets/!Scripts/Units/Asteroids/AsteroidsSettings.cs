using UnityEngine;
using System.Collections.Generic;
using System;
using Random = UnityEngine.Random;

namespace Units.Asteroids
{
    [CreateAssetMenu(fileName = "Asteroids_New", menuName = "Configs/Asteroids/AsteroidsConfiguration")]
    public class AsteroidsSettings : ScriptableObject
    {
        public Asteroid AsteroidPrefab;

        public int NumberOfChilds => numberOfChilds;
        public AudioClip ExplosionSound => explosionSound;
        public ParticleSystem RocksParticles => rocksParticles;


        [Header("Behaviour")]
        [SerializeField] private float minSpeed;
        [SerializeField] private float maxSpeed;
        [SerializeField] private int numberOfChilds;

        [Header("Score")]
        [SerializeField] private int smallScoreValue;
        [SerializeField] private int mediumScoreValue;
        [SerializeField] private int bigScoreValue;

        [Header("Sprites")]
        [SerializeField] private Sprite[] smallAsteroids;
        [SerializeField] private Sprite[] mediumAsteroids;
        [SerializeField] private Sprite[] bigAsteroids;

        [Header("Audio")]
        [SerializeField] private AudioClip explosionSound;

        [Header("FX")]
        [SerializeField] private ParticleSystem rocksParticles;



        private Dictionary<Asteroid.SizeType, Sprite[]> _sizeToSprites = new Dictionary<Asteroid.SizeType, Sprite[]>();
        private Dictionary<Asteroid.SizeType, int> _sizeToScore = new Dictionary<Asteroid.SizeType, int>();

        private void OnValidate()
        {
            _sizeToSprites[Asteroid.SizeType.Small] = smallAsteroids;
            _sizeToSprites[Asteroid.SizeType.Medium] = mediumAsteroids;
            _sizeToSprites[Asteroid.SizeType.Big] = bigAsteroids;

            _sizeToScore[Asteroid.SizeType.Small] = smallScoreValue;
            _sizeToScore[Asteroid.SizeType.Medium] = mediumScoreValue;
            _sizeToScore[Asteroid.SizeType.Big] = bigScoreValue;
        }

        private void OnDisable()
        {
            _sizeToSprites.Clear();
        }

        public int GetScoreBySizeInt(int size)
        {
            if (!Enum.IsDefined(typeof(Asteroid.SizeType), size))
                throw new ArgumentOutOfRangeException($"Size {size} is not defined on Asteroid.SizeType!");

            return _sizeToScore[(Asteroid.SizeType) size];
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