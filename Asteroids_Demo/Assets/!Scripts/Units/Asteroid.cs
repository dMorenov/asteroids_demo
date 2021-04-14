using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Units
{
    public class Asteroid : Unit
    {
        public float Speed;


        public void Setup(float speed)
        {
            Speed = speed;
        }


        public void Update()
        {
            transform.position += transform.up * Speed * Time.deltaTime;
        }
    }
}