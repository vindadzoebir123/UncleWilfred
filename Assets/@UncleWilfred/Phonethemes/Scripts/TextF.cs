using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace UncleWilfred.Phonethemes
{
    public class TextF : TextAnim
    {
        void OnTriggerEnter2D(Collider2D col)
        {
            if(col.tag == "base")
            {
                rb.velocity = new Vector2(Random.Range(-5,5), Random.Range(3f,5f));
            }
        }

        void OnBecameInvisible() 
        {
            Destroy(gameObject);
        }
    }
}