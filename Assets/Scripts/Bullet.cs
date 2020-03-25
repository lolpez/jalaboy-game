using System;
using UnityEngine;

namespace Jalaboy
{
    public class Bullet : MonoBehaviour
    {
        public float speed = 10f;
        public float lifeTime = 3f;
        private float expired;
        private Vector3 startDistance;
        // Start is called before the first frame update
        void Start()
        {            
            gameObject.GetComponent<Rigidbody>().velocity = transform.TransformDirection(new Vector3(0, 0, speed));
            startDistance = gameObject.transform.position;
            expired = lifeTime + Time.time;
        }

        // Update is called once per frame
        void Update()
        {
            if (Time.time > expired)
            {
                Destroy(gameObject);
            }
        }

        void OnCollisionEnter(Collision targetObj)
        {
            if (targetObj.gameObject.tag == "Enemy")
            {
                float distance = Vector3.Distance(targetObj.gameObject.transform.position, startDistance);
                Destroy(targetObj.gameObject);
                Destroy(gameObject);
                GameLogic.KillConfirmed(Math.Round(distance, 2));
            }
        }
    }
}