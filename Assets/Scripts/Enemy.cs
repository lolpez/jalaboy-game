using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jalaboy
{
    public class Enemy : MonoBehaviour
    {
        public float speed = 3f;
        public float lifeTime = 20f;
        private float expired;
        // Start is called before the first frame update
        void Start()
        {
            expired = lifeTime + Time.time;
        }

        // Update is called once per frame
        void Update()
        {
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
            if (Time.time > expired)
            {
                Destroy(gameObject);
                GameLogic.EnemyEscaped();
            }
        }
    }
}

