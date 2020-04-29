using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace UncleWilfred.Phonethemes
{
    public class TextF : TextAnim
    {
        Vector3 startPos;
        Rigidbody2D rigidBody;
        [SerializeField]
        private float speed = 1f;
        void Start()
        {
            startPos = transform.position;
            rigidBody = GetComponent<Rigidbody2D>();
        }

        void OnTriggerEnter2D(Collider2D col)
        {
            ResetPosition();
            // if(col.tag == "base")
            // {
            //     rb.velocity = new Vector2(Random.Range(-5,5), Random.Range(3f,5f));
            // }
        }

        void Update()
        {   
            rigidBody.velocity = new Vector2(0,speed);
        }

        // void OnBecameInvisible() 
        // {
        //     // Destroy(gameObject);
        //     ResetPosition();
        // }

        void ResetPosition()
        {
            transform.position = startPos;
        }
    }
}